using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Sunny.UI;
using System.Diagnostics;

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

            this.btnOpenMusicFolder.Click += new System.EventHandler(this.btnOpenMusicFolder_Click);
            this.btnOpenCoverFolder.Click += new System.EventHandler(this.btnOpenCoverFolder_Click);

            if (this.uiToolTip1 != null)
            {
                this.uiToolTip1.SetToolTip(this.btnOpenMusicFolder, "Mở thư mục chứa file nhạc của ứng dụng");
                this.uiToolTip1.SetToolTip(this.btnOpenCoverFolder, "Mở thư mục chứa ảnh bìa của ứng dụng");
            }

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
                btnOpenMusicFolder.Enabled = false;
                btnOpenCoverFolder.Enabled = false;
            }
        }

        private void LoadSongDataForEditing()
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM dbo.Song WHERE SongID = @SongID", conn))
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

                            txtFilePath.Text = musicFileName;

                            string absoluteMusicPath = PathHelper.GetAbsoluteMusicPath(musicFileName);
                            btnOpenMusicFolder.Enabled = !string.IsNullOrEmpty(musicFileName) && File.Exists(absoluteMusicPath);

                            string absoluteCoverPath = PathHelper.GetAbsoluteCoverPath(coverFileName);
                            btnOpenCoverFolder.Enabled = !string.IsNullOrEmpty(coverFileName) && File.Exists(absoluteCoverPath);

                            LoadPreviewImage(absoluteCoverPath);
                        }
                    }
                }
                catch (Exception ex) { UIMessageBox.ShowError("Lỗi tải dữ liệu bài hát: " + ex.Message); this.Close(); }
            }
        }

        private void btnOpenMusicFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", PathHelper.MusicFolderPath);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Không thể mở thư mục: " + ex.Message);
            }
        }

        private void btnOpenCoverFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", PathHelper.CoversFolderPath);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Không thể mở thư mục: " + ex.Message);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Audio Files (*.mp3;*.mp4;*.wav)|*.mp3;*.mp4;*.wav" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = Path.GetFileName(ofd.FileName);
                    txtFilePath.Tag = ofd.FileName;

                    btnOpenMusicFolder.Enabled = false;

                    try
                    {
                        var tagFile = TagLib.File.Create(ofd.FileName);
                        if (!string.IsNullOrEmpty(tagFile.Tag.Title))
                        {
                            txtName.Text = tagFile.Tag.Title;
                            txtName.ReadOnly = true;
                        }

                        if (!string.IsNullOrEmpty(tagFile.Tag.FirstPerformer))
                        {
                            txtArtist.Text = tagFile.Tag.FirstPerformer;
                            txtArtist.ReadOnly = true;
                        }
                    }
                    catch { /* Bỏ qua nếu không đọc được tag */ }

                    TryToAutoLoadLyrics(ofd.FileName);
                }
            }
        }

        private void btnSelectCover_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    avatarPreview.Tag = ofd.FileName;
                    LoadPreviewImage(ofd.FileName);
                    btnOpenCoverFolder.Enabled = false;
                }
            }
        }

        private void LoadPreviewImage(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(absolutePath);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        avatarPreview.Image = Image.FromStream(ms);
                    }
                }
                catch { avatarPreview.Image = null; }
            }
            else { avatarPreview.Image = null; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtArtist.Text))
            {
                UIMessageBox.ShowWarning("Vui lòng điền đầy đủ Tên và Nghệ sĩ.");
                return;
            }

            string finalMusicFileName = this.musicFileName;
            string finalCoverFileName = this.coverFileName;
            string newMusicPathForCheck = this.musicFileName;

            if (txtFilePath.Tag is string newMusicPath && !string.IsNullOrEmpty(newMusicPath))
            {
                finalMusicFileName = Path.GetFileName(newMusicPath);
                newMusicPathForCheck = finalMusicFileName;
            }

            if (DataAccess.FilePathExists(newMusicPathForCheck, isEditMode ? (int?)editingSongId : null))
            {
                UIMessageBox.ShowWarning("File nhạc này đã được thêm vào thư viện!");
                return;
            }

            if (txtFilePath.Tag is string newMusicPathToCopy && !string.IsNullOrEmpty(newMusicPathToCopy))
            {
                finalMusicFileName = PathHelper.CopyFileToAppFolder(newMusicPathToCopy, PathHelper.MusicFolderPath);
            }
            if (avatarPreview.Tag is string newCoverPath && !string.IsNullOrEmpty(newCoverPath))
            {
                finalCoverFileName = PathHelper.CopyFileToAppFolder(newCoverPath, PathHelper.CoversFolderPath);
            }

            if (string.IsNullOrEmpty(finalMusicFileName))
            {
                UIMessageBox.ShowWarning("Vui lòng chọn một file nhạc.");
                return;
            }

            int duration = 0;
            try
            {
                using (var tagFile = TagLib.File.Create(PathHelper.GetAbsoluteMusicPath(finalMusicFileName)))
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
                cmd.Parameters.AddWithValue("@FilePath", finalMusicFileName);
                cmd.Parameters.AddWithValue("@CoverPath", (object)finalCoverFileName ?? DBNull.Value);
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
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadLyricsFromFile(ofd.FileName);
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
    }
}