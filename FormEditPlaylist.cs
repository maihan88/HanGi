using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace HaNgi
{
    public partial class FormEditPlaylist : Sunny.UI.UIForm
    {
        private class SongListItem
        {
            public int SongID { get; set; }
            public string DisplayName { get; set; }
            public override string ToString() => DisplayName;
        }

        private bool isEditMode;
        private int editingPlaylistId;
        private string currentAbsoluteCoverPath;
        private string originalAbsoluteCoverPath;

        public FormEditPlaylist(int? playlistId = null)
        {
            InitializeComponent();
            if (playlistId.HasValue)
            {
                isEditMode = true;
                editingPlaylistId = playlistId.Value;
                this.Text = "Chỉnh sửa Playlist";
                LoadDataForEditing();
            }
            else
            {
                isEditMode = false;
                this.Text = "Tạo Playlist Mới";
                LoadAllSongsForNewPlaylist();
            }
        }

        private void LoadAllSongsForNewPlaylist()
        {
            uiTransferSongs.ItemsLeft.Clear();
            foreach (var song in GetAllSongsFromDB())
            {
                uiTransferSongs.ItemsLeft.Add(song);
            }
        }

        private void LoadDataForEditing()
        {
            var allSongs = GetAllSongsFromDB();
            var playlistSongs = new List<SongListItem>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT PlaylistName, PlaylistImage FROM dbo.Playlist WHERE PlaylistID = @ID";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", editingPlaylistId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtPlaylistName.Text = reader["PlaylistName"].ToString();
                            var coverFileName = reader["PlaylistImage"].ToString();
                            currentAbsoluteCoverPath = PathHelper.GetAbsoluteCoverPath(coverFileName);
                            originalAbsoluteCoverPath = currentAbsoluteCoverPath;
                            LoadPreviewImage(currentAbsoluteCoverPath);
                        }
                    }
                }

                query = @"SELECT s.SongID, s.SongName, s.Artist 
                          FROM dbo.PlaylistSong ps JOIN dbo.Song s ON ps.SongID = s.SongID 
                          WHERE ps.PlaylistID = @ID ORDER BY ps.OrderIndex";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", editingPlaylistId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playlistSongs.Add(new SongListItem
                            {
                                SongID = reader.GetInt32(reader.GetOrdinal("SongID")),
                                DisplayName = $"{reader["SongName"]} - {reader["Artist"]}"
                            });
                        }
                    }
                }
            }

            // Fill transfer lists
            var songsNotInPlaylist = allSongs.Where(s => !playlistSongs.Any(ps => ps.SongID == s.SongID)).ToList();
            uiTransferSongs.ItemsLeft.Clear();
            foreach (var s in songsNotInPlaylist)
                uiTransferSongs.ItemsLeft.Add(s);

            uiTransferSongs.ItemsRight.Clear();
            foreach (var s in playlistSongs)
                uiTransferSongs.ItemsRight.Add(s);
        }

        private List<SongListItem> GetAllSongsFromDB()
        {
            var list = new List<SongListItem>();
            string query = "SELECT SongID, SongName, Artist FROM dbo.Song";
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SongListItem
                            {
                                SongID = reader.GetInt32(reader.GetOrdinal("SongID")),
                                DisplayName = $"{reader["SongName"]} - {reader["Artist"]}"
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError("Lỗi tải danh sách bài hát: " + ex.Message);
                }
            }
            return list;
        }

        private void FormEditPlaylist_Load(object sender, EventArgs e)
        {
        }

        private void btnSelectCover_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    currentAbsoluteCoverPath = ofd.FileName;
                    LoadPreviewImage(currentAbsoluteCoverPath);
                }
            }
        }

        private void LoadPreviewImage(string absolutePath)
        {
            // Kiểm tra xem đường dẫn có hợp lệ và file có tồn tại không
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    // 1. Đọc tất cả nội dung của file ảnh vào một mảng byte trong bộ nhớ.
                    //    Thao tác này đọc xong sẽ tự động đóng file, giải phóng khóa.
                    byte[] imageBytes = File.ReadAllBytes(absolutePath);

                    // 2. Tạo một luồng dữ liệu (MemoryStream) từ mảng byte vừa đọc.
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        // 3. Tạo đối tượng Image từ luồng trong bộ nhớ.
                        //    Cách này hoàn toàn không đụng chạm gì đến file gốc trên ổ đĩa nữa.
                        avatarPreview.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi xảy ra (ví dụ file không phải là ảnh), hiển thị biểu tượng lỗi
                    UIMessageBox.ShowError("Không thể đọc file ảnh: " + ex.Message);
                    avatarPreview.Image = null;
                    avatarPreview.Symbol = 61635;
                }
            }
            else
            {
                // Nếu không có đường dẫn, hiển thị avatar trống
                avatarPreview.Image = null;
                avatarPreview.Symbol = 61635;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveItemInPlaylist(-1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveItemInPlaylist(1);
        }

        private void MoveItemInPlaylist(int direction)
        {
            var listBox = uiTransferSongs.ListBoxRight;
            if (listBox.SelectedItem == null || listBox.SelectedIndex < 0)
                return;

            int newIndex = listBox.SelectedIndex + direction;
            if (newIndex < 0 || newIndex >= listBox.Items.Count)
                return;

            object selected = listBox.SelectedItem;
            listBox.Items.Remove(selected);
            listBox.Items.Insert(newIndex, selected);
            listBox.SetSelected(newIndex, true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaylistName.Text))
            {
                UIMessageBox.ShowWarning("Tên playlist không được để trống!");
                return;
            }

            string coverFileNameToSave = "";

            // *** BẮT ĐẦU LOGIC MỚI ***
            // Chỉ sao chép nếu người dùng đã chọn một file ảnh mới (đường dẫn thay đổi)
            if (currentAbsoluteCoverPath != originalAbsoluteCoverPath && !string.IsNullOrEmpty(currentAbsoluteCoverPath))
            {
                coverFileNameToSave = PathHelper.CopyFileToAppFolder(currentAbsoluteCoverPath, PathHelper.CoversFolderPath);
                if (string.IsNullOrEmpty(coverFileNameToSave))
                {
                    return; // Dừng lại nếu sao chép lỗi
                }
            }
            // Nếu không, chỉ cần lấy lại tên file cũ
            else if (!string.IsNullOrEmpty(originalAbsoluteCoverPath))
            {
                coverFileNameToSave = Path.GetFileName(originalAbsoluteCoverPath);
            }
            // *** KẾT THÚC LOGIC MỚI ***

            var songsInPlaylist = uiTransferSongs.ItemsRight.Cast<SongListItem>().ToList();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    if (!isEditMode)
                    {
                        string query = "INSERT INTO dbo.Playlist (PlaylistName, PlaylistImage) OUTPUT INSERTED.PlaylistID VALUES (@Name, @Image);";
                        using (var cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtPlaylistName.Text);
                            cmd.Parameters.AddWithValue("@Image", string.IsNullOrEmpty(coverFileNameToSave) ? (object)DBNull.Value : coverFileNameToSave);
                            editingPlaylistId = (int)cmd.ExecuteScalar();
                        }
                    }
                    else
                    {
                        string query = "UPDATE dbo.Playlist SET PlaylistName = @Name, PlaylistImage = @Image WHERE PlaylistID = @ID;";
                        using (var cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtPlaylistName.Text);
                            cmd.Parameters.AddWithValue("@Image", string.IsNullOrEmpty(coverFileNameToSave) ? (object)DBNull.Value : coverFileNameToSave);
                            cmd.Parameters.AddWithValue("@ID", editingPlaylistId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Xóa các bài hát cũ và thêm lại danh sách mới (giữ nguyên)
                    using (var cmd = new SqlCommand("DELETE FROM dbo.PlaylistSong WHERE PlaylistID = @PlaylistID;", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@PlaylistID", editingPlaylistId);
                        cmd.ExecuteNonQuery();
                    }

                    for (int i = 0; i < songsInPlaylist.Count; i++)
                    {
                        string insertQuery = "INSERT INTO dbo.PlaylistSong (PlaylistID, SongID, OrderIndex) VALUES (@PlaylistID, @SongID, @OrderIndex);";
                        using (var cmd = new SqlCommand(insertQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@PlaylistID", editingPlaylistId);
                            cmd.Parameters.AddWithValue("@SongID", songsInPlaylist[i].SongID);
                            cmd.Parameters.AddWithValue("@OrderIndex", i);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    UIMessageBox.ShowSuccess("Lưu playlist thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    UIMessageBox.ShowError("Lỗi khi lưu playlist: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
