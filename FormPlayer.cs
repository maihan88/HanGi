using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxWMPLib;
using Sunny.UI;
using WMPLib;

namespace HaNgi
{
    public partial class FormPlayer : Sunny.UI.UIPage
    {
        private List<Song> currentPlaylist = new List<Song>();
        private List<Song> originalPlaylist = new List<Song>();
        private int currentIndex = -1;
        private bool isShuffled = false;
        private enum LoopMode { None, LoopOne, LoopAll }
        private LoopMode currentLoopMode = LoopMode.None;
        private Random random = new Random();

        public FormPlayer()
        {
            InitializeComponent();
            wmp.PlayStateChange += Wmp_PlayStateChange;
            trackVolume.Value = wmp.settings.volume;

            // SỬA LỖI: Gọi hàm thiết lập giao diện ban đầu
            SetInitialUIState();
        }

        private void SetInitialUIState()
        {
            // Vô hiệu hóa tất cả các nút điều khiển
            pnlBottomControls.Enabled = false;
            // Hoặc vô hiệu hóa từng nút riêng lẻ nếu pnlBottomControls không có
            // btnPlayPause.Enabled = false;
            // btnNext.Enabled = false;
            // ...

            // Đặt lại các nhãn và thanh trượt
            lblInfoSongName.Text = "Chưa có bài hát nào";
            lblInfoArtist.Text = "Vui lòng chọn một bài hát từ Trang chủ";
            lblCurrentTime.Text = "00:00";
            lblTotalTime.Text = "00:00";
            trackProgress.Value = 0;

            // Ẩn cả video player và ảnh bìa
            wmp.Visible = false;
            avatarCenterDisplay.Visible = false;

            // Xóa thông tin cũ
            avatarInfo.Image = null;
            avatarInfo.Symbol = 61442;
            txtLyrics.Clear();
            lstNowPlayingQueue.Items.Clear();
        }

        private void FormPlayer_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Phương thức chính được gọi từ FormHome để bắt đầu phát nhạc.
        /// </summary>
        public void NavigateToNowPlayingAndPlay(List<Song> songsToPlay, int startIndex = 0)
        {
            if (songsToPlay == null || !songsToPlay.Any()) return;

            pnlBottomControls.Enabled = true;

            // Cập nhật danh sách phát
            originalPlaylist = new List<Song>(songsToPlay);
            currentPlaylist = new List<Song>(songsToPlay);

            // Xử lý giao diện dựa trên số lượng bài hát
            HandlePlaylistUI();

            // Xử lý xáo trộn nếu cần
            if (isShuffled && btnShuffle.Enabled)
            {
                ShuffleCurrentPlaylist(songsToPlay[startIndex]);
            }
            else
            {
                currentIndex = startIndex;
            }

            // Cập nhật hàng đợi và bắt đầu phát
            UpdateNowPlayingQueue();
            PlayCurrentTrack();
        }

        /// <summary>
        /// Quyết định xem có nên hiển thị panel danh sách phát hay không.
        /// </summary>
        private void HandlePlaylistUI()
        {
            bool isPlaylist = currentPlaylist.Count > 1;
            pnlQueue.Visible = isPlaylist; // Chỉ hiện panel trái nếu có nhiều hơn 1 bài
            btnShuffle.Enabled = isPlaylist; // Chỉ cho phép shuffle nếu có nhiều hơn 1 bài

            if (!isPlaylist && isShuffled)
            {
                // Tắt shuffle nếu chỉ còn 1 bài
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

        private void PlayCurrentTrack()
        {
            if (currentIndex < 0 || currentIndex >= currentPlaylist.Count) return;

            Song songToPlay = currentPlaylist[currentIndex];
            wmp.URL = songToPlay.FilePath;
            wmp.Ctlcontrols.play();

            UpdateAllUIForCurrentSong(songToPlay); // Cập nhật toàn bộ giao diện
            timer.Start();
            btnPlayPause.Symbol = 61516; // Chuyển sang icon Pause
        }

        /// <summary>
        /// Cập nhật tất cả các thành phần giao diện với thông tin bài hát hiện tại.
        /// </summary>
        private void UpdateAllUIForCurrentSong(Song song)
        {
            // --- Cập nhật Panel Giữa (Khu vực hiển thị chính) ---
            string fileExtension = Path.GetExtension(song.FilePath).ToLower();
            if (fileExtension == ".mp4")
            {
                wmp.Visible = true;
                avatarCenterDisplay.Visible = false;
                wmp.BringToFront();
            }
            else
            {
                wmp.Visible = false;
                avatarCenterDisplay.Visible = true;
                avatarCenterDisplay.BringToFront();
                SetImage(avatarCenterDisplay, song.CoverPath, 61442);
            }

            // --- Cập nhật Panel Phải (Thông tin & Lời bài hát) ---
            lblInfoSongName.Text = song.SongName;
            lblInfoArtist.Text = song.Artist;
            txtLyrics.Text = song.FullLyric;
            SetImage(avatarInfo, song.CoverPath, 61442);

            // --- Cập nhật Panel Trái (Hàng đợi) ---
            if (currentIndex >= 0 && currentIndex < lstNowPlayingQueue.Items.Count)
            {
                lstNowPlayingQueue.SelectedIndex = currentIndex;
            }
        }

        private void SetImage(UIAvatar avatar, string imagePath, int defaultSymbol)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                try { using (var img = Image.FromFile(imagePath)) { avatar.Image = new Bitmap(img); } }
                catch { avatar.Image = null; avatar.Symbol = defaultSymbol; }
            }
            else { avatar.Image = null; avatar.Symbol = defaultSymbol; }
        }

        #region Logic Điều khiển và Sự kiện (Giữ nguyên)
        private void Wmp_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((WMPPlayState)e.newState == WMPPlayState.wmppsMediaEnded)
            { 
                PlayNext(); 
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (wmp.playState == WMPPlayState.wmppsPlaying && wmp.currentMedia != null)
            {
                trackProgress.Maximum = (int)wmp.currentMedia.duration;
                trackProgress.Value = (int)wmp.Ctlcontrols.currentPosition;
                lblCurrentTime.Text = wmp.Ctlcontrols.currentPositionString;
                lblTotalTime.Text = wmp.currentMedia.durationString;
            }
        }

        private void PlayNext()
        {
            if (currentLoopMode == LoopMode.LoopOne)
            {
                PlayCurrentTrack();
                return;
            }
            if (!currentPlaylist.Any()) return;

            currentIndex++;
            if (currentIndex >= currentPlaylist.Count)
            {
                if (currentLoopMode == LoopMode.LoopAll)
                {
                    currentIndex = 0; // Quay lại bài đầu tiên
                }
                else
                {
                    // Dừng phát nếu hết playlist và không lặp lại
                    wmp.Ctlcontrols.stop();
                    timer.Stop();
                    btnPlayPause.Symbol = 61515; // Chuyển về icon Play
                    return;
                }
            }
            PlayCurrentTrack();
        }

        private void PlayPrevious()
        {
            if (!currentPlaylist.Any()) return;
            // Nếu nhạc đang phát quá 3 giây, quay lại đầu bài hát hiện tại
            if (wmp.Ctlcontrols.currentPosition > 3)
            {
                PlayCurrentTrack();
                return;
            }

            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = currentPlaylist.Count - 1; // Vòng lại bài cuối
            }
            PlayCurrentTrack();
        }

        private void ShuffleCurrentPlaylist(Song songToKeepFirst)
        {
            // Xáo trộn danh sách gốc để tạo ra thứ tự mới
            currentPlaylist = originalPlaylist.OrderBy(x => random.Next()).ToList();
            // Đưa bài hát đang được chọn lên đầu danh sách đã xáo trộn
            currentPlaylist.Remove(songToKeepFirst);
            currentPlaylist.Insert(0, songToKeepFirst);
            currentIndex = 0; // Bắt đầu phát từ bài hát này
        }

        // --- Event Handlers ---

        #endregion

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (!currentPlaylist.Any()) return;

            if (wmp.playState == WMPPlayState.wmppsPlaying)
            {
                wmp.Ctlcontrols.pause();
                btnPlayPause.Symbol = 61515; // Play icon
            }
            else
            {
                wmp.Ctlcontrols.play();
                btnPlayPause.Symbol = 61516; // Pause icon
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            PlayNext();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            PlayPrevious();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            // SỬA LỖI: Thêm "guard clause" để ngăn lỗi khi chưa có nhạc
            if (!currentPlaylist.Any() || currentIndex < 0) return;

            isShuffled = !isShuffled;
            btnShuffle.FillColor = isShuffled ? Color.DeepSkyBlue : Color.Transparent;

            Song currentSong = currentPlaylist[currentIndex]; // Dòng này giờ đã an toàn
            if (isShuffled)
            {
                ShuffleCurrentPlaylist(currentSong);
            }
            else
            {
                currentPlaylist = new List<Song>(originalPlaylist);
                currentIndex = currentPlaylist.FindIndex(s => s.SongID == currentSong.SongID);
            }
            UpdateNowPlayingQueue();
            lstNowPlayingQueue.SelectedIndex = currentIndex;
        }

        private void trackProgress_Scroll(object sender, EventArgs e)
        {
            // SỬA LỖI: Chỉ cho phép tua khi media đã được tải
            if (wmp.currentMedia != null && (wmp.playState == WMPPlayState.wmppsPlaying || wmp.playState == WMPPlayState.wmppsPaused))
            {
                wmp.Ctlcontrols.currentPosition = trackProgress.Value;
            }
        }

        private void btnLoop_Click(object sender, EventArgs e)
        {
            currentLoopMode = (LoopMode)(((int)currentLoopMode + 1) % 3); // Quay vòng qua 3 chế độ
            switch (currentLoopMode)
            {
                case LoopMode.None:
                    btnLoop.Symbol = 61470; // Loop icon
                    btnLoop.FillColor = Color.Transparent;
                    this.ShowInfoTip("Tắt lặp lại");
                    break;
                case LoopMode.LoopAll:
                    btnLoop.Symbol = 61470; // Loop icon
                    btnLoop.FillColor = Color.DeepSkyBlue;
                    this.ShowInfoTip("Lặp lại Toàn bộ danh sách");
                    break;
                case LoopMode.LoopOne:
                    btnLoop.Symbol = 61469; // Loop one icon
                    btnLoop.FillColor = Color.DeepSkyBlue;
                    this.ShowInfoTip("Lặp lại Một bài");
                    break;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show("Chức năng cài đặt sẽ được phát triển trong tương lai!");
        }

        private void trackProgress_ValueChanged(object sender, EventArgs e)
        {
            if (wmp.playState == WMPPlayState.wmppsPlaying || wmp.playState == WMPPlayState.wmppsPaused)
            {
                wmp.Ctlcontrols.currentPosition = trackProgress.Value;
            }
        }

        private void trackVolume_ValueChanged(object sender, EventArgs e)
        {
            wmp.settings.volume = trackVolume.Value;
        }

        private void lstNowPlayingQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNowPlayingQueue.SelectedIndex >= 0)
            {
                currentIndex = lstNowPlayingQueue.SelectedIndex;
                PlayCurrentTrack();
            }
        }

        private void lstNowPlayingQueue_DoubleClick(object sender, EventArgs e)
        {
            // SỬA LỖI: Chỉ xử lý khi có item được chọn
            if (lstNowPlayingQueue.SelectedIndex >= 0)
            {
                currentIndex = lstNowPlayingQueue.SelectedIndex;
                PlayCurrentTrack();
            }
        }
    }
}
