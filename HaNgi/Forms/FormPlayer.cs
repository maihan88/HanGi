using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using Sunny.UI;

namespace HaNgi
{
    public partial class FormPlayer : Sunny.UI.UIPage
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private Media _currentMedia;

        private List<Song> currentPlaylist = new List<Song>();
        private List<Song> originalPlaylist = new List<Song>();
        private int currentIndex = -1;
        private bool isShuffled = false;
        private enum LoopMode { None, LoopOne, LoopAll }
        private LoopMode currentLoopMode = LoopMode.None;
        private Random random = new Random();
        private bool isUserSeeking = false;
        private List<int> _shuffledIndices;
        private int _shuffledPlaybackIndex = -1;
        private bool _isChangingTrack = false;

        private List<LyricLine> _currentLyricLines;
        private int _currentLyricIndex = -1;
        private bool _isTimedLyric = false;
        private Font _lyricFont = new Font("Lora", 12F, FontStyle.Regular);
        private Color _focusedColor = Color.FromArgb(255, 0, 127);
        private Color _unfocusedColor = Color.FromArgb(240, 240, 240);

        private FormPlayerSettings settingsFormInstance;
        private Theme _currentTheme;

        public FormPlayer()
        {
            InitializeComponent();
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView.MediaPlayer = _mediaPlayer;

            _mediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
            _mediaPlayer.LengthChanged += MediaPlayer_LengthChanged;
            _mediaPlayer.EndReached += MediaPlayer_EndReached;
            _mediaPlayer.EncounteredError += MediaPlayer_EncounteredError;
            _mediaPlayer.Playing += MediaPlayer_Playing;
            _mediaPlayer.Paused += MediaPlayer_Paused;
            _mediaPlayer.Stopped += MediaPlayer_Stopped;

            rtbLyrics.SelectionChanged += RtbLyrics_SelectionChanged;

            timer.Interval = 50;
            timer.Enabled = false;

            trackVolume.Value = 100;
            _mediaPlayer.Volume = 100;
            SetInitialUIState();
        }

        private void FormPlayer_Load(object sender, EventArgs e)
        {
            trackProgress.MouseDown += trackProgress_MouseDown;
            trackProgress.MouseUp += trackProgress_MouseUp;

            ApplyTheme("HanGi");
        }

        public void NavigateToNowPlayingAndPlay(List<Song> songsToPlay, int startIndex = 0)
        {
            if (songsToPlay == null || !songsToPlay.Any()) return;
            foreach (Control ctrl in pnlBottomControls.Controls)
            {
                ctrl.Enabled = true;
            }
            originalPlaylist = new List<Song>(songsToPlay);
            currentPlaylist = new List<Song>(songsToPlay);
            HandlePlaylistUI();

            if (isShuffled && btnShuffle.Enabled)
            {
                ShuffleCurrentPlaylist(songsToPlay[startIndex]);
            }
            else
            {
                currentIndex = startIndex;
            }
            UpdateNowPlayingQueue();
            PlayCurrentTrack();
        }

        private async void PlayCurrentTrack()
        {
            if (_isChangingTrack) return;

            _isChangingTrack = true;

            try
            {
                if (currentIndex < 0 || currentIndex >= currentPlaylist.Count) return;

                Song songToPlay = currentPlaylist[currentIndex];

                UpdateAllUIForCurrentSong(songToPlay);
                ProcessLyrics(songToPlay.FullLyric);

                await Task.Run(() =>
                {
                    if (_mediaPlayer.IsPlaying)
                    {
                        _mediaPlayer.Stop();
                    }
                    _currentMedia?.Dispose();
                    _currentMedia = new Media(_libVLC, new Uri(songToPlay.FilePath));
                    _mediaPlayer.Play(_currentMedia);
                });
            }
            finally
            {
                _isChangingTrack = false;
            }
        }

        private void PlayNext()
        {
            if (!currentPlaylist.Any()) return;

            if (isShuffled && _shuffledIndices != null && _shuffledIndices.Any())
            {
                _shuffledPlaybackIndex = (_shuffledPlaybackIndex + 1) % _shuffledIndices.Count;
                currentIndex = _shuffledIndices[_shuffledPlaybackIndex];
            }
            else
            {
                currentIndex = (currentIndex + 1) % currentPlaylist.Count;
            }
            PlayCurrentTrack();
        }

        private void PlayPrevious()
        {
            if (!currentPlaylist.Any()) return;

            if (isShuffled && _shuffledIndices != null && _shuffledIndices.Any())
            {
                _shuffledPlaybackIndex--;
                if (_shuffledPlaybackIndex < 0)
                {
                    _shuffledPlaybackIndex = _shuffledIndices.Count - 1;
                }
                currentIndex = _shuffledIndices[_shuffledPlaybackIndex];
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = currentPlaylist.Count - 1;
                }
            }
            PlayCurrentTrack();
        }

        private void ProcessLyrics(string fullLyricText)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ProcessLyrics(fullLyricText)));
                return;
            }
            rtbLyrics.Text = "";
            _currentLyricLines = LrcParser.Parse(fullLyricText);
            _currentLyricIndex = -1;
            _isTimedLyric = _currentLyricLines.Any() && _currentLyricLines.Any(l => l.Time > TimeSpan.Zero);
            if (_currentLyricLines != null && _currentLyricLines.Any())
            {
                var fullLyricsBuilder = new StringBuilder();
                foreach (var line in _currentLyricLines)
                {
                    line.StartIndex = fullLyricsBuilder.Length;
                    fullLyricsBuilder.Append(line.Text + "\n");
                    line.Length = line.Text.Length;
                }
                rtbLyrics.Text = fullLyricsBuilder.ToString();
                rtbLyrics.SelectAll();
                rtbLyrics.SelectionFont = _lyricFont;
                rtbLyrics.SelectionColor = _unfocusedColor;
                rtbLyrics.DeselectAll();
            }
            else
            {
                rtbLyrics.Text = "Không có lời cho bài hát này.";
            }
        }

        private void RtbLyrics_SelectionChanged(object sender, EventArgs e)
        {
            if (!_isTimedLyric)
            {
                rtbLyrics.DeselectAll();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (currentLoopMode == LoopMode.LoopOne)
            {
                PlayCurrentTrack();
            }
            else
            {
                PlayNext();
            }
        }

        private void MediaPlayer_EndReached(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => timer.Start()));
        }

        private void MediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            if (isUserSeeking) return;
            long currentSeconds = e.Time / 1000;
            this.Invoke(new Action(() =>
            {
                if (currentSeconds >= 0 && currentSeconds <= trackProgress.Maximum)
                {
                    trackProgress.Value = (int)currentSeconds;
                }
                lblCurrentTime.Text = TimeSpan.FromMilliseconds(e.Time).ToString(@"mm\:ss");
            }));
            if (!_isTimedLyric || _currentLyricLines == null || !_currentLyricLines.Any()) return;
            var currentTime = TimeSpan.FromMilliseconds(e.Time);
            int newLyricIndex = -1;
            for (int i = 0; i < _currentLyricLines.Count; i++)
            {
                if (currentTime >= _currentLyricLines[i].Time) newLyricIndex = i;
                else break;
            }
            if (newLyricIndex != -1 && newLyricIndex != _currentLyricIndex)
            {
                _currentLyricIndex = newLyricIndex;
                this.Invoke(new Action(() =>
                {
                    rtbLyrics.SelectAll();
                    rtbLyrics.SelectionFont = _lyricFont;
                    rtbLyrics.SelectionColor = _unfocusedColor;
                    var currentLine = _currentLyricLines[_currentLyricIndex];
                    rtbLyrics.Select(currentLine.StartIndex, currentLine.Length);
                    rtbLyrics.SelectionColor = _focusedColor;
                    rtbLyrics.ScrollToCaret();
                    rtbLyrics.DeselectAll();
                }));
            }
        }

        private void MediaPlayer_LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            long totalSeconds = e.Length / 1000;
            this.Invoke(new Action(() =>
            {
                trackProgress.Maximum = (int)totalSeconds;
                lblTotalTime.Text = TimeSpan.FromMilliseconds(e.Length).ToString(@"mm\:ss");
            }));
        }

        private void MediaPlayer_Playing(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => btnPlayPause.Symbol = 61516));
        }

        private void MediaPlayer_Paused(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => btnPlayPause.Symbol = 61515));
        }

        private void MediaPlayer_Stopped(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => btnPlayPause.Symbol = 61515));
        }

        void MediaPlayer_EncounteredError(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                MessageBox.Show("VLC gặp lỗi khi phát file này.", "Lỗi Media", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }));
        }

        private void trackProgress_MouseUp(object sender, MouseEventArgs e)
        {
            if (_mediaPlayer.IsSeekable)
            {
                _mediaPlayer.Time = trackProgress.Value * 1000;
            }
            isUserSeeking = false;
        }

        private void trackProgress_MouseDown(object sender, MouseEventArgs e)
        {
            isUserSeeking = true;
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            _mediaPlayer.Pause();
        }

        private void btnLoop_Click(object sender, EventArgs e)
        {
            currentLoopMode = (currentLoopMode == LoopMode.None) ? LoopMode.LoopOne : LoopMode.LoopAll;
            if (currentLoopMode == LoopMode.LoopOne)
            {
                btnLoop.Symbol = 61469; btnLoop.FillColor = Color.DeepSkyBlue;
                this.ShowInfoTip("Lặp lại một bài");
            }
            else if (currentLoopMode == LoopMode.LoopAll)
            {
                btnLoop.Symbol = 61470; btnLoop.FillColor = Color.DeepSkyBlue;
                this.ShowInfoTip("Lặp lại Toàn bộ danh sách");
            }
            else
            {
                btnLoop.Symbol = 61470; btnLoop.FillColor = Color.Transparent;
                this.ShowInfoTip("Tắt lặp lại");
            }
        }

        private void btnNext_Click(object sender, EventArgs e) => PlayNext();
        private void btnPrevious_Click(object sender, EventArgs e) => PlayPrevious();

        #region Các hàm phụ trợ
        private void UpdateAllUIForCurrentSong(Song song)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateAllUIForCurrentSong(song)));
                return;
            }
            if (song == null) return;
            string fileExtension = Path.GetExtension(song.FilePath ?? "").ToLower();
            if (fileExtension == ".mp4")
            {
                videoView.Visible = true;
                avatarCenterDisplay.Visible = false;
            }
            else
            {
                videoView.Visible = false;
                avatarCenterDisplay.Visible = true;
                SetImage(avatarCenterDisplay, song.CoverPath, 61442);
            }
            lblInfoSongName.Text = song.SongName;
            lblInfoArtist.Text = song.Artist;
            SetImage(avatarInfo, song.CoverPath, 61442);
            if (currentIndex >= 0 && currentIndex < lstNowPlayingQueue.Items.Count)
            {
                lstNowPlayingQueue.SelectedIndex = currentIndex;
            }
        }

        private void SetInitialUIState()
        {
            foreach (Control ctrl in pnlBottomControls.Controls)
            {
                ctrl.Enabled = false;
            }

            lblCurrentTime.Enabled = true;
            lblTotalTime.Enabled = true;

            pnlBottomControls.FillColor = Color.FromArgb(44, 44, 84);

            lblInfoSongName.Text = "Chưa có bài hát nào";
            lblInfoArtist.Text = "Vui lòng chọn một bài hát từ Trang chủ";
            lblCurrentTime.Text = "00:00";
            lblTotalTime.Text = "00:00";
            trackProgress.Value = 0;
            videoView.Visible = false;
            avatarCenterDisplay.Visible = true;
            avatarInfo.Image = null;
            avatarInfo.Symbol = 61442;
            rtbLyrics.Text = "";
            lstNowPlayingQueue.Items.Clear();
        }

        private void ShuffleCurrentPlaylist(Song songToKeepFirst)
        {
            _shuffledIndices = Enumerable.Range(0, currentPlaylist.Count).OrderBy(x => random.Next()).ToList();
            int currentSongIndexInShuffledList = _shuffledIndices.IndexOf(currentIndex);
            if (currentSongIndexInShuffledList != -1)
            {
                _shuffledIndices.RemoveAt(currentSongIndexInShuffledList);
            }
            _shuffledIndices.Insert(0, currentIndex);
            _shuffledPlaybackIndex = 0;
        }

        private void HandlePlaylistUI()
        {
            bool isPlaylist = currentPlaylist.Count > 1;
            pnlQueue.Visible = isPlaylist;
            btnShuffle.Enabled = isPlaylist;
            if (!isPlaylist && isShuffled)
            {
                isShuffled = false;
                btnShuffle.FillColor = Color.Transparent;
            }
        }

        private void UpdateNowPlayingQueue()
        {
            lstNowPlayingQueue.Items.Clear();
            foreach (var song in currentPlaylist)
            {
                lstNowPlayingQueue.Items.Add($"{song.SongName} - {song.Artist}");
            }
        }

        private void SetImage(UIAvatar avatar, string imagePath, int defaultSymbol)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        avatar.Image = Image.FromStream(ms);
                    }
                }
                catch { avatar.Image = null; avatar.Symbol = defaultSymbol; }
            }
            else { avatar.Image = null; avatar.Symbol = defaultSymbol; }
        }

        private void trackVolume_ValueChanged(object sender, EventArgs e)
        {
            if (_mediaPlayer != null)
                _mediaPlayer.Volume = trackVolume.Value;
        }

        private void lstNowPlayingQueue_DoubleClick(object sender, EventArgs e)
        {
            if (lstNowPlayingQueue.SelectedIndex >= 0)
            {
                currentIndex = lstNowPlayingQueue.SelectedIndex;
                PlayCurrentTrack();
            }
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            if (!currentPlaylist.Any()) return;
            isShuffled = !isShuffled;
            if (isShuffled)
            {
                btnShuffle.FillColor = Color.DeepSkyBlue;
                this.ShowInfoTip("Bật phát ngẫu nhiên");
                ShuffleCurrentPlaylist(currentPlaylist[currentIndex]);
            }
            else
            {
                btnShuffle.FillColor = Color.Transparent;
                this.ShowInfoTip("Tắt phát ngẫu nhiên");
                _shuffledIndices = null;
                _shuffledPlaybackIndex = -1;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (settingsFormInstance == null || settingsFormInstance.IsDisposed)
            {
                settingsFormInstance = new FormPlayerSettings(pnlQueue.Visible, pnlRightInfo.Visible, _currentTheme.Name);

                settingsFormInstance.QueueVisibilityChanged += (isVisible) => { pnlQueue.Visible = isVisible; };
                settingsFormInstance.InfoVisibilityChanged += (isVisible) => { pnlRightInfo.Visible = isVisible; };
                settingsFormInstance.ThemeNameChanged += ApplyTheme;

                settingsFormInstance.Show(this.FindForm());
            }
            else
            {
                settingsFormInstance.BringToFront();
            }
        }

        private void ApplyTheme(string themeName)
        {
            _currentTheme = ThemeManager.GetTheme(themeName);

            pnlCenterDisplay.FillColor = _currentTheme.GradientStart;
            pnlCenterDisplay.FillColor2 = _currentTheme.GradientEnd;

            this.BackColor = _currentTheme.GradientEnd;
            pnlQueue.BackColor = _currentTheme.GradientEnd;
            pnlRightInfo.BackColor = _currentTheme.GradientEnd;
            pnlBottomControls.FillColor = _currentTheme.SubtleColor;

            foreach (TabPage page in uiTabControl1.TabPages)
            {
                page.BackColor = _currentTheme.GradientEnd;
            }
            lstNowPlayingQueue.FillColor = _currentTheme.GradientEnd;
            lstNowPlayingQueue.RectColor = _currentTheme.SubtleColor;

            trackProgress.ForeColor = _currentTheme.AccentColor;
            trackVolume.ForeColor = _currentTheme.AccentColor;

            trackProgress.FillColor = Color.FromArgb(80, _currentTheme.SubtleColor);
            trackVolume.FillColor = Color.FromArgb(80, _currentTheme.SubtleColor);

            lstNowPlayingQueue.ItemSelectBackColor = _currentTheme.AccentColor;
            uiTabControl1.TabSelectedColor = _currentTheme.AccentColor;
            _focusedColor = _currentTheme.AccentColor;

            Color contrastColor = (_currentTheme.AccentColor.GetBrightness() < 0.5) ? Color.White : Color.Black;
            lstNowPlayingQueue.ItemSelectForeColor = contrastColor;
            uiTabControl1.TabSelectedForeColor = contrastColor;

            uiTabControl1.ForeColor = _currentTheme.TextColor;
            uiLabel1.ForeColor = _currentTheme.TextColor;
            lblCurrentTime.ForeColor = _currentTheme.TextColor;
            lblTotalTime.ForeColor = _currentTheme.TextColor;
            lblInfoSongName.ForeColor = _currentTheme.TextColor;
            lblInfoArtist.ForeColor = _currentTheme.TextColor;
            lstNowPlayingQueue.ForeColor = _currentTheme.TextColor;

            rtbLyrics.FillColor = _currentTheme.GradientEnd;
            rtbLyrics.ForeColor = _currentTheme.TextColor;
            _unfocusedColor = _currentTheme.TextColor;

            foreach (Control ctrl in pnlBottomControls.Controls)
            {
                if (ctrl is UISymbolButton button)
                {
                    button.SymbolColor = _currentTheme.TextColor;
                }
            }

            pnlCenterDisplay.Invalidate();
        }
        #endregion

        private void lblInfoSongName_Click(object sender, EventArgs e)
        {

        }
    }
}