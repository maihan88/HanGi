using System;
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
        private string musicFileName; // Chỉ lưu tên file, không lưu đường dẫn tuyệt đối
        private string coverFileName; // Chỉ lưu tên file

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
            var song = DataAccess.GetSongById(editingSongId);
            if (song == null)
            {
                UIMessageBox.ShowError("Không tìm thấy bài hát để chỉnh sửa.");
                this.Close();
                return;
            }

            txtName.Text = song.SongName;
            txtArtist.Text = song.Artist;
            txtLyric.Text = song.FullLyric;

            musicFileName = Path.GetFileName(song.FilePath);
            coverFileName = Path.GetFileName(song.CoverPath);

            txtFilePath.Text = musicFileName;

            btnOpenMusicFolder.Enabled = !string.IsNullOrEmpty(musicFileName) && File.Exists(song.FilePath);
            btnOpenCoverFolder.Enabled = !string.IsNullOrEmpty(coverFileName) && File.Exists(song.CoverPath);

            LoadPreviewImage(song.CoverPath);
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

            // Kiểm tra nếu người dùng chọn file nhạc mới
            if (txtFilePath.Tag is string newMusicFullPath && !string.IsNullOrEmpty(newMusicFullPath))
            {
                newMusicPathForCheck = Path.GetFileName(newMusicFullPath);
            }

            if (string.IsNullOrEmpty(newMusicPathForCheck))
            {
                UIMessageBox.ShowWarning("Vui lòng chọn một file nhạc.");
                return;
            }

            if (DataAccess.FilePathExists(newMusicPathForCheck, isEditMode ? (int?)editingSongId : null))
            {
                UIMessageBox.ShowWarning("File nhạc này đã được thêm vào thư viện!");
                return;
            }

            // Sao chép file mới nếu có
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
                UIMessageBox.ShowWarning("Không thể xử lý file nhạc. Vui lòng thử lại.");
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

            var songToSave = new Song
            {
                SongID = this.editingSongId,
                SongName = txtName.Text,
                Artist = txtArtist.Text,
                Duration = duration,
                FilePath = finalMusicFileName, // Chỉ lưu tên file
                CoverPath = finalCoverFileName, // Chỉ lưu tên file
                FullLyric = txtLyric.Text
            };

            if (DataAccess.SaveSong(songToSave, isEditMode))
            {
                UIMessageBox.ShowSuccess("Lưu thông tin thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // --- CÁC HÀM KHÁC GIỮ NGUYÊN ---
        #region Other Event Handlers and Helper Methods
        private void btnOpenMusicFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", PathHelper.MusicFolderPath); }
            catch (Exception ex) { UIMessageBox.ShowError("Không thể mở thư mục: " + ex.Message); }
        }

        private void btnOpenCoverFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", PathHelper.CoversFolderPath); }
            catch (Exception ex) { UIMessageBox.ShowError("Không thể mở thư mục: " + ex.Message); }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Audio Files (*.mp3;*.mp4;*.wav)|*.mp3;*.mp4;*.wav" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = Path.GetFileName(ofd.FileName);
                    txtFilePath.Tag = ofd.FileName; // Lưu đường dẫn đầy đủ vào Tag
                    btnOpenMusicFolder.Enabled = false;

                    try
                    {
                        var tagFile = TagLib.File.Create(ofd.FileName);
                        if (!string.IsNullOrEmpty(tagFile.Tag.Title)) txtName.Text = tagFile.Tag.Title;
                        if (!string.IsNullOrEmpty(tagFile.Tag.FirstPerformer)) txtArtist.Text = tagFile.Tag.FirstPerformer;
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
                    avatarPreview.Tag = ofd.FileName; // Lưu đường dẫn đầy đủ vào Tag
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
        #endregion
    }
}
