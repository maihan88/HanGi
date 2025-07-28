using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Sunny.UI;

namespace HaNgi
{
    public partial class FormHome : Sunny.UI.UIPage
    {
        private UserControl selectedCard = null; // Lưu trữ thẻ (card) được chọn hiện tại
        private FormPlayer _playerForm; // Tham chiếu đến form trình phát nhạc

        public FormHome()
        {
            InitializeComponent();
            this.Load += FormHome_Load; // Đăng ký sự kiện Load cho form
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            LoadData(); // Tải dữ liệu khi form được load
        }

        /// <summary>
        /// Thiết lập tham chiếu đến form trình phát nhạc.
        /// </summary>
        public void SetPlayerFormReference(FormPlayer playerForm)
        {
            _playerForm = playerForm;
        }

        /// <summary>
        /// Tải dữ liệu bài hát và playlist, đồng thời xóa trạng thái chọn.
        /// </summary>
        public void LoadData()
        {
            LoadSongs(); // Tải danh sách bài hát
            LoadPlaylists(); // Tải danh sách playlist
            ClearSelection(); // Xóa trạng thái chọn
        }

        /// <summary>
        /// Xóa trạng thái chọn và ẩn các panel chi tiết.
        /// </summary>
        private void ClearSelection()
        {
            selectedCard = null;
            pnlSongDetails.Visible = false; // Ẩn panel chi tiết bài hát
            pnlPlaylistDetails.Visible = false; // Ẩn panel chi tiết playlist
        }

        /// <summary>
        /// Tải danh sách bài hát từ cơ sở dữ liệu và hiển thị trên giao diện.
        /// </summary>
        private void LoadSongs()
        {
            flpSongs.SuspendLayout(); // Tạm dừng cập nhật giao diện để tăng hiệu suất
            flpSongs.Controls.Clear(); // Xóa các control cũ
            List<Song> allSongs = DataAccess.GetAllSongs(); // Lấy danh sách bài hát từ cơ sở dữ liệu
            foreach (var song in allSongs)
            {
                var card = new SongCard();
                card.SetData(song.SongID, song.SongName, song.Artist); // Thiết lập dữ liệu cho thẻ bài hát
                string absolutePath = PathHelper.GetAbsoluteCoverPath(song.CoverPath); // Lấy đường dẫn tuyệt đối của ảnh bìa
                card.SetCoverImage(absolutePath); // Thiết lập ảnh bìa
                card.Click += new EventHandler(SongCard_Click); // Đăng ký sự kiện click cho thẻ
                flpSongs.Controls.Add(card); // Thêm thẻ vào giao diện
            }
            flpSongs.ResumeLayout(true); // Tiếp tục cập nhật giao diện
        }

        /// <summary>
        /// Tải danh sách playlist từ cơ sở dữ liệu và hiển thị trên giao diện.
        /// </summary>
        private void LoadPlaylists()
        {
            flpPlaylists.SuspendLayout(); // Tạm dừng cập nhật giao diện
            flpPlaylists.Controls.Clear(); // Xóa các control cũ
            List<PlaylistCardData> allPlaylists = DataAccess.GetAllPlaylistsWithPreviews(); // Lấy danh sách playlist từ cơ sở dữ liệu
            foreach (var pData in allPlaylists)
            {
                var card = new PlaylistCard();
                card.SetData(pData.PlaylistID, pData.PlaylistName, pData.SongPreviews); // Thiết lập dữ liệu cho thẻ playlist
                string absolutePath = PathHelper.GetAbsoluteCoverPath(pData.PlaylistImage); // Lấy đường dẫn tuyệt đối của ảnh bìa
                card.SetCoverImage(absolutePath); // Thiết lập ảnh bìa
                card.Click += new EventHandler(PlaylistCard_Click); // Đăng ký sự kiện click cho thẻ
                flpPlaylists.Controls.Add(card); // Thêm thẻ vào giao diện
            }
            flpPlaylists.ResumeLayout(true); // Tiếp tục cập nhật giao diện
        }

        /// <summary>
        /// Xử lý sự kiện khi thẻ bài hát được click.
        /// </summary>
        private void SongCard_Click(object sender, EventArgs e)
        {
            selectedCard = sender as SongCard; // Lưu thẻ được chọn
            if (selectedCard == null) return;
            UpdateSongDetailsPanel(); // Cập nhật thông tin chi tiết bài hát
            pnlSongDetails.Visible = true; // Hiển thị panel chi tiết bài hát
            pnlPlaylistDetails.Visible = false; // Ẩn panel chi tiết playlist
        }

        /// <summary>
        /// Xử lý sự kiện khi thẻ playlist được click.
        /// </summary>
        private void PlaylistCard_Click(object sender, EventArgs e)
        {
            selectedCard = sender as PlaylistCard; // Lưu thẻ được chọn
            if (selectedCard == null) return;
            UpdatePlaylistDetailsPanel(); // Cập nhật thông tin chi tiết playlist
            pnlPlaylistDetails.Visible = true; // Hiển thị panel chi tiết playlist
            pnlSongDetails.Visible = false; // Ẩn panel chi tiết bài hát
        }

        /// <summary>
        /// Cập nhật thông tin chi tiết bài hát trên giao diện.
        /// </summary>
        private void UpdateSongDetailsPanel()
        {
            if (!(selectedCard is SongCard songCard)) return;
            var song = DataAccess.GetSongById(songCard.SongID); // Lấy thông tin bài hát từ cơ sở dữ liệu
            if (song == null) return;

            detailLblSongName.Text = song.SongName; // Hiển thị tên bài hát
            detailLblArtist.Text = song.Artist; // Hiển thị tên nghệ sĩ
            TimeSpan time = TimeSpan.FromSeconds(song.Duration); // Chuyển đổi thời lượng từ giây sang định dạng mm:ss
            detailLblDuration.Text = $"Thời lượng: {time:mm\\:ss}";
            if (File.Exists(song.CoverPath)) // Kiểm tra xem ảnh bìa có tồn tại không
            {
                using (var img = Image.FromFile(song.CoverPath)) { detailAvatar.Image = new Bitmap(img); }
            }
            else
            {
                detailAvatar.Image = null;
                detailAvatar.Symbol = 61442; // Biểu tượng mặc định nếu không có ảnh bìa
            }
        }

        /// <summary>
        /// Cập nhật thông tin chi tiết playlist trên giao diện.
        /// </summary>
        private void UpdatePlaylistDetailsPanel()
        {
            if (!(selectedCard is PlaylistCard playlistCard)) return;
            var playlist = DataAccess.GetPlaylistById(playlistCard.PlaylistID); // Lấy thông tin playlist từ cơ sở dữ liệu
            if (playlist == null) return;

            playlistDetailLblName.Text = playlist.PlaylistName; // Hiển thị tên playlist

            var songsInPlaylist = DataAccess.GetSongsByPlaylistId(playlist.PlaylistID); // Lấy danh sách bài hát trong playlist
            lstPlaylistSongsDetail.Items.Clear();
            foreach (var song in songsInPlaylist)
            {
                lstPlaylistSongsDetail.Items.Add($"{song.SongName} - {song.Artist}"); // Hiển thị danh sách bài hát
            }

            if (!string.IsNullOrEmpty(playlist.PlaylistImage) && File.Exists(playlist.PlaylistImage)) // Kiểm tra ảnh bìa
            {
                try
                {
                    using (var originalImage = Image.FromFile(playlist.PlaylistImage))
                    {
                        uiAvatar1.Image = new Bitmap(originalImage);
                    }
                }
                catch
                {
                    uiAvatar1.Image = null; uiAvatar1.Symbol = 61449; // Biểu tượng mặc định nếu không có ảnh bìa
                }
            }
            else
            {
                uiAvatar1.Image = null; uiAvatar1.Symbol = 61449;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút phát bài hát.
        /// </summary>
        private void btnPlaySong_Click(object sender, EventArgs e)
        {
            if (selectedCard is SongCard songCard)
            {
                var song = DataAccess.GetSongById(songCard.SongID); // Lấy thông tin bài hát
                if (song != null)
                {
                    PlayerService.RequestPlay(new List<Song> { song }); // Yêu cầu phát bài hát
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút phát playlist.
        /// </summary>
        private void btnPlayPlaylist_Click(object sender, EventArgs e)
        {
            if (selectedCard is PlaylistCard playlistCard)
            {
                var songs = DataAccess.GetSongsByPlaylistId(playlistCard.PlaylistID); // Lấy danh sách bài hát trong playlist
                if (songs != null && songs.Any())
                {
                    PlayerService.RequestPlay(songs); // Yêu cầu phát playlist
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút thêm bài hát.
        /// </summary>
        private void btnAddSong_Click(object sender, EventArgs e)
        {
            using (var formEdit = new FormEditSong())
            {
                if (formEdit.ShowDialog() == DialogResult.OK) // Hiển thị form chỉnh sửa bài hát
                {
                    LoadData(); // Tải lại dữ liệu sau khi thêm
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút thêm playlist.
        /// </summary>
        private void btnAddPlaylist_Click(object sender, EventArgs e)
        {
            using (FormEditPlaylist formEdit = new FormEditPlaylist())
            {
                if (formEdit.ShowDialog() == DialogResult.OK) // Hiển thị form chỉnh sửa playlist
                {
                    LoadData(); // Tải lại dữ liệu sau khi thêm
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút sửa bài hát hoặc playlist.
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedCard == null)
            {
                UIMessageBox.ShowWarning("Vui lòng chọn một mục để sửa!");
                return;
            }

            if (selectedCard is SongCard songCard)
            {
                using (var formEdit = new FormEditSong(songCard.SongID))
                {
                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Tải lại dữ liệu sau khi sửa
                    }
                }
            }
            else if (selectedCard is PlaylistCard playlistCard)
            {
                using (var formEdit = new FormEditPlaylist(playlistCard.PlaylistID))
                {
                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Tải lại dữ liệu sau khi sửa
                    }
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút xóa bài hát hoặc playlist.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCard == null)
            {
                UIMessageBox.ShowWarning("Vui lòng chọn một mục để xóa!");
                return;
            }

            if (selectedCard is SongCard songCard)
            {
                HandleDeleteSong(songCard); // Xử lý xóa bài hát
            }
            else if (selectedCard is PlaylistCard playlistCard)
            {
                HandleDeletePlaylist(playlistCard); // Xử lý xóa playlist
            }
        }

        /// <summary>
        /// Xử lý xóa bài hát, bao gồm kiểm tra trạng thái phát và xóa file vật lý.
        /// </summary>
        private void HandleDeleteSong(SongCard songCard)
        {
            string titleToDelete = songCard.Title;

            // Kiểm tra xem bài hát có đang phát không
            if (_playerForm != null && _playerForm.CurrentlyPlayingSongId == songCard.SongID)
            {
                // Hiển thị cảnh báo nếu bài hát đang phát
                if (UIMessageBox.ShowAsk($"Bài hát '{titleToDelete}' đang phát.\nĐể xóa, bạn cần dừng phát bài hát này. Bạn có muốn tiếp tục không?"))
                {
                    _playerForm.StopPlayerAndReset(); // Dừng trình phát và reset giao diện
                    System.Threading.Thread.Sleep(200); // Chờ để hệ thống giải phóng file
                }
                else
                {
                    return; // Người dùng chọn "Không", không làm gì cả
                }
            }

            // Hỏi lại lần nữa để xác nhận xóa
            if (UIMessageBox.ShowAsk($"Bạn có chắc chắn muốn xóa vĩnh viễn '{titleToDelete}' không?"))
            {
                var songToDelete = DataAccess.GetSongById(songCard.SongID);
                if (songToDelete != null)
                {
                    if (DataAccess.DeleteSong(songCard.SongID)) // Xóa bài hát khỏi cơ sở dữ liệu
                    {
                        try
                        {
                            // Xóa file vật lý nếu tồn tại
                            if (!string.IsNullOrEmpty(songToDelete.FilePath) && File.Exists(songToDelete.FilePath))
                            {
                                File.Delete(songToDelete.FilePath);
                            }
                            if (!string.IsNullOrEmpty(songToDelete.CoverPath) && File.Exists(songToDelete.CoverPath))
                            {
                                File.Delete(songToDelete.CoverPath);
                            }
                            UIMessageBox.ShowSuccess("Xóa thành công!");
                        }
                        catch (Exception ex)
                        {
                            UIMessageBox.ShowWarning("Đã xóa khỏi thư viện nhưng không thể xóa file vật lý:\n" + ex.Message);
                        }
                        LoadData(); // Tải lại dữ liệu
                    }
                }
            }
        }

        /// <summary>
        /// Xử lý xóa playlist, bao gồm xóa file ảnh bìa nếu có.
        /// </summary>
        private void HandleDeletePlaylist(PlaylistCard playlistCard)
        {
            if (UIMessageBox.ShowAsk($"Bạn có chắc chắn muốn xóa playlist '{playlistCard.PlaylistName}' không?"))
            {
                var playlistToDelete = DataAccess.GetPlaylistById(playlistCard.PlaylistID);
                if (playlistToDelete != null)
                {
                    if (DataAccess.DeletePlaylist(playlistCard.PlaylistID)) // Xóa playlist khỏi cơ sở dữ liệu
                    {
                        try
                        {
                            // Xóa file ảnh bìa nếu tồn tại
                            if (!string.IsNullOrEmpty(playlistToDelete.PlaylistImage) && File.Exists(playlistToDelete.PlaylistImage))
                            {
                                File.Delete(playlistToDelete.PlaylistImage);
                            }
                            UIMessageBox.ShowSuccess("Xóa playlist thành công!");
                        }
                        catch (Exception ex)
                        {
                            UIMessageBox.ShowWarning("Đã xóa playlist nhưng không thể xóa file ảnh bìa:\n" + ex.Message);
                        }
                        LoadData(); // Tải lại dữ liệu
                    }
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi click vào nền để bỏ chọn.
        /// </summary>
        private void uiPanel1_Click(object sender, EventArgs e)
        {
            ClearSelection(); // Xóa trạng thái chọn
        }
    }
}