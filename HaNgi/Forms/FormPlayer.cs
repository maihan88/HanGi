using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using Sunny.UI;

namespace HaNgi
{
    public partial class FormPlayer : Sunny.UI.UIPage
    {
        private readonly PlayerManager _playerManager;
        private readonly PlaylistManager _playlistManager;
        private readonly LyricManager _lyricManager;

        private bool _isUserSeeking = false;
        private int _currentLyricIndex = -1;
        private Theme _currentTheme;
        private FormPlayerSettings _settingsFormInstance;

        private PlayerState _lastPlayerState = PlayerState.Stopped;  // thêm field

        private bool _suppressSelectionEvent = false;

        public int CurrentlyPlayingSongId { get; private set; } = -1;

        public FormPlayer()
        {
            InitializeComponent();
            _playerManager = new PlayerManager(this.videoView);
            _playlistManager = new PlaylistManager();
            _lyricManager = new LyricManager();
            SubscribeToEvents();
            _playerManager.SetVolume(trackVolume.Value);
            SetInitialUIState();

            _playerManager.StateChanged += state =>
            {
                _lastPlayerState = state;
                PlayerManager_StateChanged(state);
            };
        }

        private void SubscribeToEvents()
        {
            _playerManager.TimeChanged += PlayerManager_TimeChanged;
            _playerManager.LengthChanged += PlayerManager_LengthChanged;
            _playerManager.EndReached += PlayerManager_EndReached;
            _playerManager.StateChanged += PlayerManager_StateChanged;

            trackProgress.MouseDown += (s, e) => { _isUserSeeking = true; };
            trackProgress.MouseUp += TrackProgress_MouseUp;
            rtbLyrics.SelectionChanged += (s, e) => { if (!_lyricManager.IsTimedLyric) rtbLyrics.DeselectAll(); };
        }

        private void FormPlayer_Load(object sender, EventArgs e)
        {
            ApplyTheme("HanGi");

            // Mỗi khi user (hoặc UIListBox) thay đổi selection, ta kiểm tra:
            lstNowPlayingQueue.SelectedIndexChanged += (s, ev) =>
            {
                if (_suppressSelectionEvent) return;

                // Nếu là user chọn (không phải do ta set), 
                // trả selection về đúng current song index
                var idx = _playlistManager.GetCurrentSongIndex();
                if (lstNowPlayingQueue.SelectedIndex != idx)
                {
                    lstNowPlayingQueue.SelectedIndex = idx;
                }
            };
        }

        public void NavigateToNowPlayingAndPlay(List<Song> songsToPlay, int startIndex = 0)
        {
            if (songsToPlay == null || !songsToPlay.Any()) return;

            pnlBottomControls.Enabled = true;

            // <<< SỬA LỖI: Vô hiệu hóa nút khi chỉ có 1 bài hát
            bool isPlaylist = songsToPlay.Count > 1;
            btnNext.Enabled = isPlaylist;
            btnPrevious.Enabled = isPlaylist;
            btnShuffle.Enabled = isPlaylist;
            pnlQueue.Visible = isPlaylist;

            _playlistManager.LoadPlaylist(songsToPlay, startIndex);
            UpdateNowPlayingQueueUI();
            PlaySong(_playlistManager.GetCurrentSong());
        }

        private void PlaySong(Song song)
        {
            if (song == null)
            {
                CurrentlyPlayingSongId = -1;
                SetInitialUIState();
                return;
            };

            CurrentlyPlayingSongId = song.SongID;
            _playerManager.Play(song);
            _lyricManager.Parse(song.FullLyric);
            UpdateAllUIForNewSong(song);
        }

        /// <summary>
        /// THÊM HÀM MỚI: Dừng trình phát và reset giao diện.
        /// Hàm này sẽ được gọi từ FormHome trước khi xóa file.
        /// </summary>
        public void StopPlayerAndReset()
        {
            _playerManager.StopAndRelease();
            _playlistManager.LoadPlaylist(new List<Song>()); // Xóa sạch playlist
            CurrentlyPlayingSongId = -1;
            SetInitialUIState();
        }

        #region UI Update Methods

        private void UpdateAllUIForNewSong(Song song)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateAllUIForNewSong(song)));
                return;
            }

            bool isVideo = Path.GetExtension(song.FilePath ?? "").ToLower() == ".mp4";
            videoView.Visible = isVideo;
            avatarCenterDisplay.Visible = !isVideo;
            SetImage(avatarCenterDisplay, song.CoverPath, 61442);

            lblInfoSongName.Text = song.SongName;
            lblInfoArtist.Text = song.Artist;
            SetImage(avatarInfo, song.CoverPath, 61442);
            UpdateLyricsUI();

            int displayIndex = _playlistManager.GetCurrentSongIndex();
            if (displayIndex >= 0 && displayIndex < lstNowPlayingQueue.Items.Count)
            {
                lstNowPlayingQueue.SelectedIndex = displayIndex;
            }

            // Cuối cùng highlight dòng mới:
            var idx = _playlistManager.GetCurrentSongIndex();
            if (idx >= 0 && idx < lstNowPlayingQueue.Items.Count)
            {
                _suppressSelectionEvent = true;
                lstNowPlayingQueue.SelectedIndex = idx;
                _suppressSelectionEvent = false;
                lstNowPlayingQueue.TopIndex = idx;
            }
        }

        private void UpdateLyricsUI()
        {
            rtbLyrics.Text = _lyricManager.GetFullLyricText();
            _currentLyricIndex = -1;

            if (_lyricManager.ParsedLines.Any())
            {
                rtbLyrics.SelectAll();
                rtbLyrics.SelectionFont = new Font("Lora", 16F, FontStyle.Regular);
                rtbLyrics.SelectionColor = _currentTheme.TextColor;
                rtbLyrics.DeselectAll();
            }
        }

        private void HighlightCurrentLyric(TimeSpan currentTime)
        {
            int newIndex = _lyricManager.GetCurrentLineIndex(currentTime);

            if (newIndex != -1 && newIndex != _currentLyricIndex)
            {
                _currentLyricIndex = newIndex;
                this.Invoke(new Action(() =>
                {
                    rtbLyrics.SelectAll();
                    rtbLyrics.SelectionColor = _currentTheme.TextColor;

                    var currentLine = _lyricManager.ParsedLines[_currentLyricIndex];
                    rtbLyrics.Select(currentLine.StartIndex, currentLine.Length);
                    rtbLyrics.SelectionColor = _currentTheme.AccentColor;
                    rtbLyrics.ScrollToCaret();
                    rtbLyrics.DeselectAll();
                }));
            }
        }

        private void SetInitialUIState()
        {
            pnlBottomControls.Enabled = false;
            lblInfoSongName.Text = "Chưa có bài hát nào";
            lblInfoArtist.Text = "Vui lòng chọn một bài hát từ Trang chủ";
            lblCurrentTime.Text = "00:00";
            lblTotalTime.Text = "00:00";
            trackProgress.Value = 0;
            trackVolume.Value = 100;
            videoView.Visible = false;
            avatarCenterDisplay.Visible = true;
            avatarCenterDisplay.Image = null;
            avatarCenterDisplay.Symbol = 61442;
            avatarInfo.Image = null;
            avatarInfo.Symbol = 61442;
            rtbLyrics.Text = "";
            lstNowPlayingQueue.Items.Clear();
        }

        private void UpdateNowPlayingQueueUI()
        {
            lstNowPlayingQueue.Items.Clear();
            // <<< SỬA LỖI 2: Luôn hiển thị danh sách gốc, không thay đổi khi shuffle
            foreach (var song in _playlistManager.DisplayPlaylist)
            {
                lstNowPlayingQueue.Items.Add($"{song.SongName} - {song.Artist}");
            }

            // sau khi load xong, set highlight đúng bài current
            var idx = _playlistManager.GetCurrentSongIndex();
            _suppressSelectionEvent = true;
            lstNowPlayingQueue.SelectedIndex = idx;
            _suppressSelectionEvent = false;
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

        #endregion

        #region PlayerManager Event Handlers

        private void PlayerManager_StateChanged(PlayerState state)
        {
            this.Invoke(new Action(() => {
                btnPlayPause.Symbol = (state == PlayerState.Playing) ? 61516 : 61515;
            }));
        }

        private async void PlayerManager_EndReached(object sender, EventArgs e)
        {
            // Chờ một khoảng thời gian rất ngắn để trình phát ổn định trạng thái
            await Task.Delay(100);

            Song nextSongToPlay = null;

            if (_playlistManager.LoopMode == LoopMode.LoopOne)
            {
                nextSongToPlay = _playlistManager.GetCurrentSong();
            }
            else
            {
                if (_playlistManager.PlaylistCount > 1)
                {
                    nextSongToPlay = _playlistManager.GoToNext();
                }
            }

            // Đưa hành động cuối cùng về lại luồng UI
            this.Invoke(new Action(() => {
                if (nextSongToPlay != null)
                {
                    PlaySong(nextSongToPlay);
                }
                else
                {
                    // Chỉ stop playback, giữ currentMedia để seek hoặc resume
                    _playerManager.Stop();
                    // Cập nhật lại UI: đặt progress = 0, thời gian hiện tại = 00:00
                    this.Invoke(new Action(() =>
                    {
                        trackProgress.Value = 0;
                        lblCurrentTime.Text = "00:00";
                        btnPlayPause.Symbol = 61515; // Biểu tượng Play
                    }));
                }
            }));
        }

        private void PlayerManager_LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            this.Invoke(new Action(() => {
                trackProgress.Maximum = (int)(e.Length / 1000);
                lblTotalTime.Text = TimeSpan.FromMilliseconds(e.Length).ToString(@"mm\:ss");
            }));
        }

        private void PlayerManager_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            if (_isUserSeeking) return;

            this.Invoke(new Action(() => {
                long currentSeconds = e.Time / 1000;
                if (currentSeconds >= 0 && currentSeconds <= trackProgress.Maximum)
                {
                    trackProgress.Value = (int)currentSeconds;
                }
                lblCurrentTime.Text = TimeSpan.FromMilliseconds(e.Time).ToString(@"mm\:ss");
            }));

            HighlightCurrentLyric(TimeSpan.FromMilliseconds(e.Time));
        }

        #endregion

        #region UI Control Event Handlers

        // Sửa lỗi: Thêm lại các hàm xử lý sự kiện mà Designer cần
        private void timer_Tick(object sender, EventArgs e)
        {
            // Logic của timer cũ đã được chuyển vào PlayerManager_EndReached
            // Hàm này có thể để trống hoặc xóa đi nếu bạn xóa cả liên kết trong file Designer.
        }

        private void lblInfoSongName_Click(object sender, EventArgs e)
        {
            // Có thể thêm chức năng gì đó ở đây nếu muốn, ví dụ: copy tên bài hát
        }

        private void TrackProgress_MouseUp(object sender, MouseEventArgs e)
        {
            _playerManager.Seek(trackProgress.Value * 1000);
            _isUserSeeking = false;
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (_lastPlayerState == PlayerState.Stopped && CurrentlyPlayingSongId != -1)
            {
                // Nếu đã load 1 bài (CurrentlyPlayingSongId != -1) nhưng đang Stopped => phát lại từ đầu
                var song = _playlistManager.GetCurrentSong();
                PlaySong(song);
            }
            else
            {
                // Toggle pause/play
                _playerManager.Pause();
            }
        }
        private void btnNext_Click(object sender, EventArgs e) => PlaySong(_playlistManager.GoToNext());
        private void btnPrevious_Click(object sender, EventArgs e) => PlaySong(_playlistManager.GoToPrevious());
        private void trackVolume_ValueChanged(object sender, EventArgs e) => _playerManager.SetVolume(trackVolume.Value);
        private void lstNowPlayingQueue_DoubleClick(object sender, EventArgs e)
        {
            if (lstNowPlayingQueue.SelectedIndex >= 0)
            {
                // Cần thêm logic để chuyển đến bài hát được chọn trong PlaylistManager
            }
        }

        private void btnLoop_Click(object sender, EventArgs e)
        {
            // <<< SỬA LỖI: Đơn giản hóa logic lặp lại
            _playlistManager.ToggleLoopMode();
            var currentMode = _playlistManager.LoopMode;

            if (currentMode == LoopMode.LoopOne)
            {
                btnLoop.Symbol = 61469; // Icon lặp lại 1 bài
                btnLoop.FillColor = _currentTheme.AccentColor;
                this.ShowInfoTip("Lặp lại bài hát hiện tại");
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else // LoopMode.None
            {
                btnLoop.Symbol = 61470; // Icon không lặp
                btnLoop.FillColor = Color.Transparent;
                this.ShowInfoTip("Tắt lặp lại");
                if (_playlistManager.PlaylistCount > 1)
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }
            }
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            _playlistManager.ToggleShuffle();
            btnShuffle.FillColor = _playlistManager.IsShuffled ? _currentTheme.AccentColor : Color.Transparent;
            this.ShowInfoTip(_playlistManager.IsShuffled ? "Bật phát ngẫu nhiên" : "Tắt phát ngẫu nhiên");
            UpdateNowPlayingQueueUI();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (_settingsFormInstance == null || _settingsFormInstance.IsDisposed)
            {
                _settingsFormInstance = new FormPlayerSettings(pnlQueue.Visible, pnlRightInfo.Visible, _currentTheme.Name);
                _settingsFormInstance.QueueVisibilityChanged += (isVisible) => { pnlQueue.Visible = isVisible; };
                _settingsFormInstance.InfoVisibilityChanged += (isVisible) => { pnlRightInfo.Visible = isVisible; };
                _settingsFormInstance.ThemeNameChanged += ApplyTheme;
                _settingsFormInstance.Show(this.FindForm());
            }
            else
            {
                _settingsFormInstance.BringToFront();
            }
        }

        #endregion

        #region Theme Management
        private void ApplyTheme(string themeName)
        {
            _currentTheme = ThemeManager.GetTheme(themeName);

            pnlCenterDisplay.FillColor = _currentTheme.GradientStart;
            pnlCenterDisplay.FillColor2 = _currentTheme.GradientEnd;
            this.BackColor = _currentTheme.GradientEnd;
            pnlQueue.BackColor = _currentTheme.GradientEnd;
            pnlRightInfo.BackColor = _currentTheme.GradientEnd;
            pnlBottomControls.FillColor = _currentTheme.SubtleColor;
            foreach (TabPage page in uiTabControl1.TabPages) page.BackColor = _currentTheme.GradientEnd;
            lstNowPlayingQueue.FillColor = _currentTheme.GradientEnd;
            lstNowPlayingQueue.RectColor = _currentTheme.SubtleColor;
            trackProgress.ForeColor = _currentTheme.AccentColor;
            trackVolume.ForeColor = _currentTheme.AccentColor;
            lstNowPlayingQueue.ItemSelectBackColor = _currentTheme.AccentColor;
            uiTabControl1.TabSelectedColor = _currentTheme.AccentColor;
            if (_playlistManager.LoopMode != LoopMode.None) btnLoop.FillColor = _currentTheme.AccentColor;
            if (_playlistManager.IsShuffled) btnShuffle.FillColor = _currentTheme.AccentColor;
            Color contrastColor = (_currentTheme.AccentColor.GetBrightness() < 0.5) ? Color.White : Color.Black;
            lstNowPlayingQueue.ItemSelectForeColor = contrastColor;
            uiTabControl1.TabSelectedForeColor = contrastColor;
            uiLabel1.ForeColor = _currentTheme.TextColor;
            lblCurrentTime.ForeColor = _currentTheme.TextColor;
            lblTotalTime.ForeColor = _currentTheme.TextColor;
            lblInfoSongName.ForeColor = _currentTheme.TextColor;
            lblInfoArtist.ForeColor = _currentTheme.TextColor;
            lstNowPlayingQueue.ForeColor = _currentTheme.TextColor;
            rtbLyrics.FillColor = _currentTheme.GradientEnd;
            rtbLyrics.ForeColor = _currentTheme.TextColor;
            foreach (Control ctrl in pnlBottomControls.Controls)
            {
                if (ctrl is UISymbolButton button) button.SymbolColor = _currentTheme.TextColor;
            }
            UpdateLyricsUI();
            pnlCenterDisplay.Invalidate();
        }
        #endregion
    }
}
