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
        private UserControl selectedCard = null;
        private FormPlayer _playerForm;

        public FormHome()
        {
            InitializeComponent();
            this.Load += FormHome_Load;
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void SetPlayerFormReference(FormPlayer playerForm)
        {
            _playerForm = playerForm;
        }

        public void LoadData()
        {
            LoadSongs();
            LoadPlaylists();
            ClearSelection();
        }

        private void ClearSelection()
        {
            selectedCard = null;
            pnlSongDetails.Visible = false;
            pnlPlaylistDetails.Visible = false;
        }

        private void LoadSongs()
        {
            flpSongs.SuspendLayout();
            flpSongs.Controls.Clear();
            List<Song> allSongs = DataAccess.GetAllSongs();
            foreach (var song in allSongs)
            {
                var card = new SongCard();
                card.SetData(song.SongID, song.SongName, song.Artist);
                string absolutePath = PathHelper.GetAbsoluteCoverPath(song.CoverPath);
                card.SetCoverImage(absolutePath);
                card.Click += new EventHandler(SongCard_Click);
                flpSongs.Controls.Add(card);
            }
            flpSongs.ResumeLayout(true);
        }

        private void LoadPlaylists()
        {
            flpPlaylists.SuspendLayout();
            flpPlaylists.Controls.Clear();
            List<PlaylistCardData> allPlaylists = DataAccess.GetAllPlaylistsWithPreviews();
            foreach (var pData in allPlaylists)
            {
                var card = new PlaylistCard();
                card.SetData(pData.PlaylistID, pData.PlaylistName, pData.SongPreviews);
                string absolutePath = PathHelper.GetAbsoluteCoverPath(pData.PlaylistImage);
                card.SetCoverImage(absolutePath);
                card.Click += new EventHandler(PlaylistCard_Click);
                flpPlaylists.Controls.Add(card);
            }
            flpPlaylists.ResumeLayout(true);
        }

        private void SongCard_Click(object sender, EventArgs e)
        {
            selectedCard = sender as SongCard;
            if (selectedCard == null) return;
            UpdateSongDetailsPanel();
            pnlSongDetails.Visible = true;
            pnlPlaylistDetails.Visible = false;
        }

        private void PlaylistCard_Click(object sender, EventArgs e)
        {
            selectedCard = sender as PlaylistCard;
            if (selectedCard == null) return;
            UpdatePlaylistDetailsPanel();
            pnlPlaylistDetails.Visible = true;
            pnlSongDetails.Visible = false;
        }

        private void UpdateSongDetailsPanel()
        {
            if (!(selectedCard is SongCard songCard)) return;
            var song = DataAccess.GetSongById(songCard.SongID);
            if (song == null) return;

            detailLblSongName.Text = song.SongName;
            detailLblArtist.Text = song.Artist;
            TimeSpan time = TimeSpan.FromSeconds(song.Duration);
            detailLblDuration.Text = $"Thời lượng: {time:mm\\:ss}";
            if (File.Exists(song.CoverPath))
            {
                using (var img = Image.FromFile(song.CoverPath)) { detailAvatar.Image = new Bitmap(img); }
            }
            else
            {
                detailAvatar.Image = null;
                detailAvatar.Symbol = 61442;
            }
        }

        private void UpdatePlaylistDetailsPanel()
        {
            if (!(selectedCard is PlaylistCard playlistCard)) return;
            var playlist = DataAccess.GetPlaylistById(playlistCard.PlaylistID);
            if (playlist == null) return;

            playlistDetailLblName.Text = playlist.PlaylistName;

            var songsInPlaylist = DataAccess.GetSongsByPlaylistId(playlist.PlaylistID);
            lstPlaylistSongsDetail.Items.Clear();
            foreach (var song in songsInPlaylist)
            {
                lstPlaylistSongsDetail.Items.Add($"{song.SongName} - {song.Artist}");
            }

            if (!string.IsNullOrEmpty(playlist.PlaylistImage) && File.Exists(playlist.PlaylistImage))
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
                    uiAvatar1.Image = null; uiAvatar1.Symbol = 61449;
                }
            }
            else
            {
                uiAvatar1.Image = null; uiAvatar1.Symbol = 61449;
            }
        }

        private void btnPlaySong_Click(object sender, EventArgs e)
        {
            if (selectedCard is SongCard songCard)
            {
                var song = DataAccess.GetSongById(songCard.SongID);
                if (song != null)
                {
                    PlayerService.RequestPlay(new List<Song> { song });
                }
            }
        }

        private void btnPlayPlaylist_Click(object sender, EventArgs e)
        {
            if (selectedCard is PlaylistCard playlistCard)
            {
                var songs = DataAccess.GetSongsByPlaylistId(playlistCard.PlaylistID);
                if (songs != null && songs.Any())
                {
                    PlayerService.RequestPlay(songs);
                }
            }
        }

        private void btnAddSong_Click(object sender, EventArgs e)
        {
            using (var formEdit = new FormEditSong())
            {
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnAddPlaylist_Click(object sender, EventArgs e)
        {
            using (FormEditPlaylist formEdit = new FormEditPlaylist())
            {
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

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
                        LoadData();
                    }
                }
            }
            else if (selectedCard is PlaylistCard playlistCard)
            {
                using (var formEdit = new FormEditPlaylist(playlistCard.PlaylistID))
                {
                    if (formEdit.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCard == null)
            {
                UIMessageBox.ShowWarning("Vui lòng chọn một mục để xóa!");
                return;
            }

            if (selectedCard is SongCard songCard)
            {
                HandleDeleteSong(songCard);
            }
            else if (selectedCard is PlaylistCard playlistCard)
            {
                HandleDeletePlaylist(playlistCard);
            }
        }

        private void HandleDeleteSong(SongCard songCard)
        {
            string titleToDelete = songCard.Title;

            // Bước 1: Kiểm tra xem bài hát có đang phát không
            if (_playerForm != null && _playerForm.CurrentlyPlayingSongId == songCard.SongID)
            {
                // Bước 2: Hiển thị cảnh báo theo yêu cầu của bạn
                if (UIMessageBox.ShowAsk($"Bài hát '{titleToDelete}' đang phát.\nĐể xóa, bạn cần dừng phát bài hát này. Bạn có muốn tiếp tục không?"))
                {
                    // Bước 3: Dừng trình phát và reset giao diện Player
                    _playerForm.StopPlayerAndReset();
                    // Chờ một chút để hệ thống giải phóng file
                    System.Threading.Thread.Sleep(200);
                }
                else
                {
                    return; // Người dùng chọn "Không", không làm gì cả
                }
            }

            // Bước 4: Hỏi lại lần nữa để xác nhận xóa (kể cả khi không phát nhạc)
            if (UIMessageBox.ShowAsk($"Bạn có chắc chắn muốn xóa vĩnh viễn '{titleToDelete}' không?"))
            {
                var songToDelete = DataAccess.GetSongById(songCard.SongID);
                if (songToDelete != null)
                {
                    if (DataAccess.DeleteSong(songCard.SongID))
                    {
                        try
                        {
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

        private void HandleDeletePlaylist(PlaylistCard playlistCard)
        {
            if (UIMessageBox.ShowAsk($"Bạn có chắc chắn muốn xóa playlist '{playlistCard.PlaylistName}' không?"))
            {
                var playlistToDelete = DataAccess.GetPlaylistById(playlistCard.PlaylistID);
                if (playlistToDelete != null)
                {
                    if (DataAccess.DeletePlaylist(playlistCard.PlaylistID))
                    {
                        try
                        {
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
                        LoadData();
                    }
                }
            }
        }

        private void uiPanel1_Click(object sender, EventArgs e)
        {
            // Bỏ chọn khi click vào nền
            ClearSelection();
        }
    }
}