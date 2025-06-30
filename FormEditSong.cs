using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Sunny.UI;
// TagLib is used for reading audio file properties like duration.
// We are giving it an alias 'TagFile' to avoid conflict with System.IO.File
using TagFile = TagLib.File;

namespace HaNgi
{
    public partial class FormEditSong : UIForm
    {
        private bool isEditMode;
        private int editingSongId;

        private string musicFileName;
        private string coverFileName;

        public FormEditSong(int? songId = null)
        {
            InitializeComponent();
            PathHelper.EnsureAppFoldersExist();

            if (songId.HasValue)
            {
                isEditMode = true;
                editingSongId = songId.Value;
                this.Text = "Chỉnh sửa thông tin bài hát";
                LoadSongDataForEditing();
            }
            else
            {
                isEditMode = false;
                this.Text = "Thêm bài hát mới";
            }
        }

        private void LoadSongDataForEditing()
        {
            string query = "SELECT * FROM dbo.Song WHERE SongID = @SongID";
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@SongID", editingSongId);
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["SongName"].ToString();
                            txtArtist.Text = reader["Artist"].ToString();
                            txtLyric.Text = reader["FullLyric"].ToString();

                            musicFileName = reader["FilePath"].ToString();
                            coverFileName = reader["CoverPath"].ToString();

                            // Cập nhật text cho các textbox để người dùng xem
                            txtFilePath.Text = musicFileName;
                            txtCoverPath.Text = coverFileName;

                            LoadPreviewImage(PathHelper.GetAbsoluteCoverPath(coverFileName));
                        }
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError("Lỗi tải dữ liệu bài hát: " + ex.Message);
                    this.Close();
                }
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Audio Files (*.mp3;*.mp4;*.wav)|*.mp3;*.mp4;*.wav" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Chỉ hiển thị tên file cho người dùng
                    txtFilePath.Text = Path.GetFileName(ofd.FileName);
                    // Dùng Tag để lưu tạm đường dẫn tuyệt đối của file mới chọn
                    txtFilePath.Tag = ofd.FileName;
                    // Tự động tìm lời khi chọn file nhạc mới
                    TryToAutoLoadLyrics(ofd.FileName);
                }
            }
        }

        private void TryToAutoLoadLyrics(string audioFilePath)
        {
            string lrcFilePath = Path.ChangeExtension(audioFilePath, ".lrc");
            if (File.Exists(lrcFilePath))
            {
                LoadLyricsFromFile(lrcFilePath);
                this.ShowSuccessTip("Đã tự động tìm thấy và nhập lời từ file .lrc!");
                return;
            }
            string txtFilePath = Path.ChangeExtension(audioFilePath, ".txt");
            if (File.Exists(txtFilePath))
            {
                LoadLyricsFromFile(txtFilePath);
                this.ShowSuccessTip("Đã tự động tìm thấy và nhập lời từ file .txt!");
            }
        }

        private void LoadLyricsFromFile(string filePath)
        {
            try { txtLyric.Text = File.ReadAllText(filePath, Encoding.UTF8); }
            catch (Exception ex) { UIMessageBox.ShowError("Không thể đọc file lời bài hát: " + ex.Message); }
        }

        private void btnSelectCover_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Chỉ hiển thị tên file cho người dùng
                    txtCoverPath.Text = Path.GetFileName(ofd.FileName);
                    // Dùng Tag của avatar để lưu tạm đường dẫn tuyệt đối của file mới chọn
                    avatarPreview.Tag = ofd.FileName;
                    // Hiển thị ảnh xem trước ngay lập tức
                    LoadPreviewImage(ofd.FileName);
                }
            }
        }

        private void LoadPreviewImage(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try { using (var img = Image.FromFile(absolutePath)) { avatarPreview.Image = new Bitmap(img); } }
                catch { avatarPreview.Image = null; avatarPreview.Symbol = 61635; }
            }
            else { avatarPreview.Image = null; avatarPreview.Symbol = 61635; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtArtist.Text))
            {
                UIMessageBox.ShowWarning("Vui lòng điền đầy đủ Tên và Nghệ sĩ.");
                return;
            }

            // Sao chép file và lấy tên file
            // Nếu người dùng đã chọn file nhạc mới (đường dẫn được lưu trong Tag của textbox)
            if (txtFilePath.Tag is string newMusicPath && !string.IsNullOrEmpty(newMusicPath))
            {
                musicFileName = PathHelper.CopyFileToAppFolder(newMusicPath, PathHelper.MusicFolderPath);
            }
            // Tương tự cho ảnh bìa (đường dẫn được lưu trong Tag của avatar)
            if (avatarPreview.Tag is string newCoverPath && !string.IsNullOrEmpty(newCoverPath))
            {
                coverFileName = PathHelper.CopyFileToAppFolder(newCoverPath, PathHelper.CoversFolderPath);
            }

            // Kiểm tra lại xem có file nhạc không (hoặc là file cũ, hoặc là file mới vừa copy)
            if (string.IsNullOrEmpty(musicFileName))
            {
                UIMessageBox.ShowWarning("Vui lòng chọn một file nhạc.");
                return;
            }

            // Lấy thời lượng từ file nhạc trong thư mục của ứng dụng
            int duration = 0;
            try
            {
                using (var tagFile = TagLib.File.Create(PathHelper.GetAbsoluteMusicPath(musicFileName)))
                {
                    duration = (int)tagFile.Properties.Duration.TotalSeconds;
                }
            }
            catch
            {
                UIMessageBox.ShowWarning("Không thể đọc file nhạc. File có thể bị lỗi hoặc không được hỗ trợ.");
                return;
            }

            string query = isEditMode
                ? "UPDATE dbo.Song SET SongName=@SongName, Artist=@Artist, Duration=@Duration, FilePath=@FilePath, CoverPath=@CoverPath, FullLyric=@FullLyric WHERE SongID=@SongID"
                : "INSERT INTO dbo.Song (SongName, Artist, Duration, FilePath, CoverPath, FullLyric) VALUES (@SongName, @Artist, @Duration, @FilePath, @CoverPath, @FullLyric)";

            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@SongName", txtName.Text);
                cmd.Parameters.AddWithValue("@Artist", txtArtist.Text);
                cmd.Parameters.AddWithValue("@Duration", duration);
                cmd.Parameters.AddWithValue("@FilePath", musicFileName); // Lưu chỉ tên file
                cmd.Parameters.AddWithValue("@CoverPath", (object)coverFileName ?? DBNull.Value); // Lưu chỉ tên file
                cmd.Parameters.AddWithValue("@FullLyric", string.IsNullOrWhiteSpace(txtLyric.Text) ? (object)DBNull.Value : txtLyric.Text);

                if (isEditMode) { cmd.Parameters.AddWithValue("@SongID", editingSongId); }

                try
                {
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        UIMessageBox.ShowSuccess("Lưu thông tin thành công!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex) { UIMessageBox.ShowError("Lỗi khi lưu vào CSDL: " + ex.Message); }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnImportLyric_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Lyric Files (*.lrc;*.txt)|*.lrc;*.txt" })
            {
                if (ofd.ShowDialog() == DialogResult.OK) { LoadLyricsFromFile(ofd.FileName); }
            }
        }
    }
}
