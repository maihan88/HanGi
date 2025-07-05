using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public FormHome()
        {
            InitializeComponent();
            this.Load += FormHome_Load;
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            LoadSongs();
            LoadPlaylists();
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
            var song = GetFullSongInfoFromDb(songCard.SongID);
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
            }
        }

        private void UpdatePlaylistDetailsPanel()
        {
            if (!(selectedCard is PlaylistCard playlistCard)) return;
            var playlist = GetFullPlaylistInfoFromDb(playlistCard.PlaylistID);
            if (playlist == null) return;
            playlistDetailLblName.Text = playlist.PlaylistName;
            lstPlaylistSongsDetail.Items.Clear();
            foreach (var song in playlist.Songs)
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

        private Song GetFullSongInfoFromDb(int songId)
        {
            Song song = null;
            string query = "SELECT * FROM dbo.Song WHERE SongID = @ID";
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", songId);
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            song = new Song
                            {
                                SongID = songId,
                                SongName = reader["SongName"].ToString(),
                                Artist = reader["Artist"].ToString(),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                FilePath = PathHelper.GetAbsoluteMusicPath(reader["FilePath"].ToString()),
                                CoverPath = PathHelper.GetAbsoluteCoverPath(reader["CoverPath"].ToString()),
                                FullLyric = reader["FullLyric"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex) { UIMessageBox.ShowError("Lỗi lấy thông tin bài hát: " + ex.Message); }
            }
            return song;
        }

        private Playlist GetFullPlaylistInfoFromDb(int playlistId)
        {
            var playlist = new Playlist { PlaylistID = playlistId };
            using (var conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT PlaylistName, PlaylistImage FROM dbo.Playlist WHERE PlaylistID = @ID";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", playlistId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                playlist.PlaylistName = reader["PlaylistName"].ToString();
                                playlist.PlaylistImage = PathHelper.GetAbsoluteCoverPath(reader["PlaylistImage"].ToString());
                            }
                            else return null;
                        }
                    }
                    playlist.Songs = GetSongsFromDbByPlaylistId(playlistId, conn);
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError("Lỗi tải thông tin playlist: " + ex.Message);
                    return null;
                }
            }
            return playlist;
        }

        private List<Song> GetSongsFromDbByPlaylistId(int playlistId, SqlConnection externalConn = null)
        {
            var list = new List<Song>();
            SqlConnection conn = externalConn ?? DatabaseHelper.GetConnection();
            bool shouldClose = externalConn == null;
            try
            {
                if (shouldClose) conn.Open();
                string query = @"SELECT s.* FROM dbo.Song s
                         JOIN dbo.PlaylistSong ps ON s.SongID = ps.SongID
                         WHERE ps.PlaylistID = @ID ORDER BY ps.OrderIndex";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", playlistId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Song
                            {
                                SongID = reader.GetInt32(reader.GetOrdinal("SongID")),
                                SongName = reader["SongName"].ToString(),
                                Artist = reader["Artist"].ToString(),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                FilePath = PathHelper.GetAbsoluteMusicPath(reader["FilePath"].ToString()),
                                CoverPath = PathHelper.GetAbsoluteCoverPath(reader["CoverPath"].ToString()),
                                FullLyric = reader["FullLyric"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Lỗi tải các bài hát trong playlist: " + ex.Message);
            }
            finally
            {
                if (shouldClose && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }

        private void btnPlaySong_Click(object sender, EventArgs e)
        {
            if (selectedCard is SongCard songCard)
            {
                var song = GetFullSongInfoFromDb(songCard.SongID);
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
                var songs = GetSongsFromDbByPlaylistId(playlistCard.PlaylistID, null);
                if (songs != null && songs.Any())
                {
                    PlayerService.RequestPlay(songs);
                }
            }
        }

        // Các hàm thêm, sửa, xóa giữ nguyên
        private void btnAddSong_Click(object sender, EventArgs e)
        {
            using (var formEdit = new FormEditSong())
            {
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadSongs();
                }
            }
        }

        private void btnAddPlaylist_Click(object sender, EventArgs e)
        {
            using (FormEditPlaylist formEdit = new FormEditPlaylist())
            {
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadPlaylists();
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

            string titleToDelete = (selectedCard is SongCard sc) ? sc.Title : (selectedCard as PlaylistCard)?.PlaylistName;

            if (UIMessageBox.ShowAsk($"Bạn có chắc chắn muốn xóa '{titleToDelete}' không?"))
            {
                if (selectedCard is SongCard songCard)
                {
                    var songToDelete = GetFullSongInfoFromDb(songCard.SongID);
                    if (songToDelete != null)
                    {
                        if (DeleteFromDatabase("Song", "SongID", songCard.SongID))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(songToDelete.FilePath) && File.Exists(songToDelete.FilePath)) File.Delete(songToDelete.FilePath);
                                if (!string.IsNullOrEmpty(songToDelete.CoverPath) && File.Exists(songToDelete.CoverPath)) File.Delete(songToDelete.CoverPath);
                            }
                            catch (Exception ex)
                            {
                                UIMessageBox.ShowError("Đã xóa khỏi thư viện nhưng không thể xóa file vật lý:\n" + ex.Message);
                            }

                            // *** SỬA LỖI Ở ĐÂY ***
                            // Thay vì chỉ LoadSongs(), hãy Load lại toàn bộ dữ liệu.
                            LoadData();
                        }
                    }
                }
                else if (selectedCard is PlaylistCard playlistCard)
                {
                    var playlistToDelete = GetFullPlaylistInfoFromDb(playlistCard.PlaylistID);
                    if (playlistToDelete != null)
                    {
                        if (DeleteFromDatabase("Playlist", "PlaylistID", playlistCard.PlaylistID))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(playlistToDelete.PlaylistImage) && File.Exists(playlistToDelete.PlaylistImage)) File.Delete(playlistToDelete.PlaylistImage);
                            }
                            catch (Exception ex)
                            {
                                UIMessageBox.ShowError("Đã xóa playlist nhưng không thể xóa file ảnh bìa:\n" + ex.Message);
                            }

                            // *** SỬA LỖI Ở ĐÂY ***
                            LoadData();
                        }
                    }
                }
            }
        }

        private bool DeleteFromDatabase(string tableName, string idColumnName, int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand($"DELETE FROM dbo.{tableName} WHERE {idColumnName} = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                try
                {
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        UIMessageBox.ShowSuccess("Xóa thành công!");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError("Lỗi khi xóa khỏi CSDL: " + ex.Message);
                }
            }
            return false;
        }

        private void uiPanel1_Click(object sender, EventArgs e)
        {
            // Có thể bỏ trống
        }
    }
}