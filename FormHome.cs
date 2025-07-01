using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using System.IO;
using HaNgi;

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

        private void LoadData()
        {
            LoadSongs();
            LoadPlaylists();
            pnlSongDetails.Visible = false;
            pnlPlaylistDetails.Visible = false;
        }

        #region Tải dữ liệu từ CSDL
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

            // Bước 1: Gọi DataAccess để lấy tất cả dữ liệu cần thiết
            List<PlaylistCardData> allPlaylists = DataAccess.GetAllPlaylistsWithPreviews();

            // Bước 2: Duyệt qua danh sách và tạo Card
            foreach (var pData in allPlaylists)
            {
                var card = new PlaylistCard(); // Card này đã được sửa lỗi StackOverflow

                // Dữ liệu bây giờ đã được đóng gói sẵn
                card.SetData(pData.PlaylistID, pData.PlaylistName, pData.SongPreviews);

                string absolutePath = PathHelper.GetAbsoluteCoverPath(pData.PlaylistImage);
                card.SetCoverImage(absolutePath);

                card.Click += new EventHandler(PlaylistCard_Click);
                flpPlaylists.Controls.Add(card);
            }

            flpPlaylists.ResumeLayout(true);
        }

        private List<string> GetTop3SongsForPlaylist(int playlistId, SqlConnection conn)
        {
            var list = new List<string>();
            string query = "SELECT TOP 3 s.SongName, s.Artist FROM dbo.PlaylistSong ps JOIN dbo.Song s ON ps.SongID = s.SongID WHERE ps.PlaylistID = @ID ORDER BY ps.OrderIndex";
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", playlistId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) { list.Add($"{reader["SongName"]} - {reader["Artist"]}"); }
                }
            }
            return list;
        }
        #endregion

        #region Xử lý Sự kiện UI
        private void SongCard_Click(object sender, EventArgs e)
        {
            selectedCard = sender as SongCard;
            if (selectedCard == null) return;

            // Cập nhật dữ liệu cho panel bài hát
            UpdateSongDetailsPanel();

            // Hiện panel bài hát và ẩn panel playlist
            pnlSongDetails.Visible = true;
            pnlPlaylistDetails.Visible = false;
        }

        private void PlaylistCard_Click(object sender, EventArgs e)
        {
            selectedCard = sender as PlaylistCard;
            if (selectedCard == null) return;

            // Cập nhật dữ liệu cho panel playlist
            UpdatePlaylistDetailsPanel();

            // Hiện panel playlist và ẩn panel bài hát
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
            detailAvatar.Image = songCard.CoverAvatar.Image;
        }

        private void UpdatePlaylistDetailsPanel()
        {
            if (!(selectedCard is PlaylistCard playlistCard)) return;

            // Hàm này đã lấy về đầy đủ thông tin, bao gồm cả đường dẫn ảnh
            var playlist = GetFullPlaylistInfoFromDb(playlistCard.PlaylistID);
            if (playlist == null) return;

            // Cập nhật tên playlist (đã có)
            playlistDetailLblName.Text = playlist.PlaylistName;

            // Cập nhật danh sách bài hát (đã có)
            lstPlaylistSongsDetail.Items.Clear();
            foreach (var song in playlist.Songs)
            {
                lstPlaylistSongsDetail.Items.Add($"{song.SongName} - {song.Artist}");
            }

            // --- BẮT ĐẦU THÊM CODE MỚI ---
            // Cập nhật ảnh bìa cho avatar ở panel chi tiết
            // (tên mặc định của nó trong designer là uiAvatar1)
            if (!string.IsNullOrEmpty(playlist.PlaylistImage) && File.Exists(playlist.PlaylistImage))
            {
                // Dùng try-catch để phòng trường hợp file ảnh bị lỗi
                try
                {
                    // Tạo một Bitmap mới để tránh lỗi "file is being used by another process"
                    using (var originalImage = Image.FromFile(playlist.PlaylistImage))
                    {
                        uiAvatar1.Image = new Bitmap(originalImage);
                    }
                }
                catch
                {
                    uiAvatar1.Image = null;
                    uiAvatar1.Symbol = 61449; // Hiện biểu tượng lỗi
                }
            }
            else
            {
                // Nếu không có ảnh, hiện biểu tượng mặc định
                uiAvatar1.Image = null;
                uiAvatar1.Symbol = 61449;
            }
            // --- KẾT THÚC THÊM CODE MỚI ---
        }



        #region Các hàm hỗ trợ Truy vấn CSDL
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
                                FilePath = reader["FilePath"].ToString(),
                                CoverPath = reader["CoverPath"].ToString(),
                                FullLyric = reader["FullLyric"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex) { UIMessageBox.ShowError("Lỗi lấy thông tin bài hát: " + ex.Message); }
            }
            return song;
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
                if (shouldClose && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }
        #endregion

        private void FormHome_Load(object sender, EventArgs e)
        {
           LoadData(); 
        }

        private void uiPanel1_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnAddSong_Click(object sender, EventArgs e)
        {
            using (var formEdit = new FormEditSong())
            {
                // Khi formEdit đóng lại, chúng ta kiểm tra kết quả nó trả về
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    // THÊM DÒNG NÀY ĐỂ KIỂM TRA
                    UIMessageBox.Show("Đã nhận được DialogResult.OK, chuẩn bị tải lại bài hát!");

                    LoadSongs();    // Tải lại toàn bộ danh sách
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
                        // Nếu sửa thành công, tải lại dữ liệu và ẩn panel chi tiết
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
                        // Nếu sửa thành công, tải lại dữ liệu và ẩn panel chi tiết
                        LoadData();
                    }
                }
            }
        }

        private void btnPlayPlaylist_Click(object sender, EventArgs e)
        {
            if (selectedCard is SongCard songCard)
            {
                var song = GetFullSongInfoFromDb(songCard.SongID);
                if (song != null)
                {
                    // SỬA LẠI CHO ĐÚNG: Gửi yêu cầu phát chỉ một bài hát
                    PlayerService.RequestPlay(new List<Song> { song });
                }
            }
        }

        private void btnPlaySong_Click(object sender, EventArgs e)
        {
            if (selectedCard is PlaylistCard playlistCard)
            {
                var songs = GetSongsFromDbByPlaylistId(playlistCard.PlaylistID);
                if (songs.Any())
                {
                    // SỬA LẠI CHO ĐÚNG: Dùng PlayerService để gửi yêu cầu phát nhạc
                    PlayerService.RequestPlay(songs);
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
                    DeleteFromDatabase("Song", "SongID", songCard.SongID);
                    LoadSongs();
                    pnlSongDetails.Visible = false;
                }
                else if (selectedCard is PlaylistCard playlistCard)
                {
                    DeleteFromDatabase("Playlist", "PlaylistID", playlistCard.PlaylistID);
                    LoadPlaylists();
                    pnlPlaylistDetails.Visible = false;
                }
            }
        }

        private Playlist GetFullPlaylistInfoFromDb(int playlistId)
        {
            var playlist = new Playlist { PlaylistID = playlistId };
            using (var conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT PlaylistName, PlaylistImage FROM dbo.Playlist WHERE PlaylistID = @ID", conn))
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

        private void DeleteFromDatabase(string tableName, string idColumnName, int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand($"DELETE FROM dbo.{tableName} WHERE {idColumnName} = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                try
                {
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0) { UIMessageBox.ShowSuccess("Xóa thành công!"); }
                }
                catch (Exception ex) { UIMessageBox.ShowError("Lỗi khi xóa: " + ex.Message); }
            }
        }
    }
}
