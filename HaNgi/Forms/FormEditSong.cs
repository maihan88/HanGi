// using: Khai báo sử dụng các "không gian tên" (namespace) chứa các lớp và hàm dựng sẵn.
using System;
using System.Drawing;
using System.IO; // Cung cấp các lớp để làm việc với file và thư mục (như File, Path).
using System.Text; // Cung cấp các lớp để mã hóa ký tự (như Encoding.UTF8).
using System.Windows.Forms;
using Sunny.UI; // Thư viện giao diện người dùng của bên thứ ba.
using System.Diagnostics; // Cung cấp lớp Process để tương tác với các tiến trình hệ thống (ví dụ: mở thư mục).

namespace HaNgi
{
    /// <summary>
    /// Form này chịu trách nhiệm cho việc TẠO MỚI hoặc CHỈNH SỬA thông tin chi tiết của một Bài hát (Song).
    /// Nó quản lý việc cập nhật tên, nghệ sĩ, lời bài hát, cũng như các file nhạc và ảnh bìa liên quan.
    /// </summary>
    public partial class FormEditSong : UIForm
    {
        // --- KHAI BÁO CÁC BIẾN THÀNH VIÊN (FIELDS) ---
        private bool isEditMode; // Biến cờ (flag) để xác định form đang ở chế độ chỉnh sửa (true) hay tạo mới (false).
        private int editingSongId; // Lưu ID của bài hát đang được chỉnh sửa.
        private string musicFileName; // Chỉ lưu TÊN FILE nhạc (ví dụ: "baihat.mp3"), không phải đường dẫn đầy đủ.
        private string coverFileName; // Chỉ lưu TÊN FILE ảnh bìa (ví dụ: "bia.jpg").
        private string oldAbsoluteMusicPath, oldAbsoluteCoverPath; // Lưu đường dẫn ĐẦY ĐỦ đến các file CŨ để xử lý việc xóa file sau này.

        /// <summary>
        /// Hàm khởi tạo (Constructor) của Form.
        /// </summary>
        /// <param name="songId">ID của bài hát cần chỉnh sửa. Là kiểu 'int?' (Nullable),
        /// cho phép giá trị là null, tương ứng với chế độ tạo mới.</param>
        public FormEditSong(int? songId = null)
        {
            InitializeComponent(); // Khởi tạo các control trên giao diện.
            PathHelper.EnsureAppFoldersExist(); // Đảm bảo các thư mục cần thiết của ứng dụng (như Music, Covers) đã tồn tại.

            // Gán sự kiện Click cho các nút bằng code thay vì qua giao diện Properties của Visual Studio.
            this.btnOpenMusicFolder.Click += new System.EventHandler(this.btnOpenMusicFolder_Click);
            this.btnOpenCoverFolder.Click += new System.EventHandler(this.btnOpenCoverFolder_Click);

            // Kiểm tra xem form đang ở chế độ nào.
            if (songId.HasValue)
            {
                // --- CHẾ ĐỘ CHỈNH SỬA ---
                isEditMode = true;
                editingSongId = songId.Value;
                this.Text = "Chỉnh sửa thông tin bài hát";
                LoadSongDataForEditing(); // Tải dữ liệu của bài hát đã có.
            }
            else
            {
                // --- CHẾ ĐỘ TẠO MỚI ---
                isEditMode = false;
                this.Text = "Thêm bài hát mới";
                // Vô hiệu hóa các nút mở thư mục vì chưa có file nào được lưu.
                btnOpenMusicFolder.Enabled = false;
                btnOpenCoverFolder.Enabled = false;
            }
        }

        /// <summary>
        /// Tải dữ liệu của một bài hát đã tồn tại từ CSDL và hiển thị lên các control trên form.
        /// </summary>
        private void LoadSongDataForEditing()
        {
            var song = DataAccess.GetSongById(editingSongId);
            if (song == null)
            {
                UIMessageBox.ShowError("Không tìm thấy bài hát.");
                Close();
                return;
            }

            // Lưu lại đường dẫn tuyệt đối của các file cũ để so sánh và xóa nếu cần.
            oldAbsoluteMusicPath = song.FilePath;
            oldAbsoluteCoverPath = song.CoverPath;

            // Điền thông tin vào các TextBox.
            txtName.Text = song.SongName;
            txtArtist.Text = song.Artist;
            txtLyric.Text = song.FullLyric;

            // Lấy tên file từ đường dẫn đầy đủ và lưu vào biến thành viên.
            musicFileName = Path.GetFileName(oldAbsoluteMusicPath);
            coverFileName = Path.GetFileName(oldAbsoluteCoverPath);

            // Hiển thị tên file và ảnh bìa.
            txtFilePath.Text = musicFileName;
            LoadPreviewImage(oldAbsoluteCoverPath);

            // Kích hoạt/Vô hiệu hóa nút mở thư mục dựa trên việc file có thực sự tồn tại trên đĩa hay không.
            btnOpenMusicFolder.Enabled = File.Exists(oldAbsoluteMusicPath);
            btnOpenCoverFolder.Enabled = File.Exists(oldAbsoluteCoverPath);

            // Nếu bài hát chưa có lời, thử tự động tìm file lời (.lrc, .txt).
            if (string.IsNullOrWhiteSpace(song.FullLyric))
            {
                TryToAutoLoadLyrics(oldAbsoluteMusicPath);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút "Lưu".
        /// Thu thập dữ liệu, xử lý file, và lưu thông tin bài hát vào CSDL.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtArtist.Text))
            {
                UIMessageBox.ShowWarning("Vui lòng điền Tên và Nghệ sĩ.");
                return;
            }

            // Chuẩn bị tên file cuối cùng sẽ được lưu vào CSDL.
            var finalMusicName = musicFileName;
            var finalCoverName = coverFileName;

            // --- XỬ LÝ VIỆC THAY ĐỔI FILE ---
            // Kiểm tra xem người dùng có chọn file nhạc mới không.
            // 'is string newMusicFull' là một dạng "pattern matching". Nó vừa kiểm tra kiểu của Tag,
            // vừa khai báo biến 'newMusicFull' để sử dụng ngay nếu kiểm tra thành công.
            if (txtFilePath.Tag is string newMusicFull && !string.IsNullOrEmpty(newMusicFull))
            {
                // Nếu có, sao chép file mới vào thư mục của ứng dụng và lấy tên file mới.
                finalMusicName = PathHelper.CopyFileToAppFolder(newMusicFull, PathHelper.MusicFolderPath);
            }

            // Tương tự, kiểm tra và xử lý ảnh bìa mới.
            if (avatarPreview.Tag is string newCoverFull && !string.IsNullOrEmpty(newCoverFull))
            {
                finalCoverName = PathHelper.CopyFileToAppFolder(newCoverFull, PathHelper.CoversFolderPath);
            }

            // Lấy thời lượng (duration) của bài hát bằng thư viện TagLib.
            int duration = 0;
            try
            {
                // Lấy đường dẫn đầy đủ của file nhạc cuối cùng.
                string musicPathForTag = PathHelper.GetAbsoluteMusicPath(finalMusicName);
                // Dùng 'using' để đảm bảo tài nguyên file được giải phóng.
                using (var tf = TagLib.File.Create(musicPathForTag))
                    duration = (int)tf.Properties.Duration.TotalSeconds; // Lấy tổng số giây.
            }
            catch
            {
                UIMessageBox.ShowWarning("Không thể đọc thông tin file nhạc. Vui lòng chọn lại file.");
                return;
            }

            // Tạo đối tượng Song để lưu.
            var songToSave = new Song
            {
                SongID = editingSongId,
                SongName = txtName.Text,
                Artist = txtArtist.Text,
                Duration = duration,
                FilePath = finalMusicName, // Lưu tên file
                CoverPath = finalCoverName, // Lưu tên file
                FullLyric = txtLyric.Text
            };

            if (DataAccess.SaveSong(songToSave))
            {
                // --- DỌN DẸP FILE CŨ SAU KHI LƯU THÀNH CÔNG ---
                var newAbsoluteMusic = PathHelper.GetAbsoluteMusicPath(finalMusicName);
                var newAbsoluteCover = PathHelper.GetAbsoluteCoverPath(finalCoverName);

                // Xóa file nhạc cũ nếu nó khác file mới VÀ nó thực sự tồn tại.
                if (!string.IsNullOrEmpty(oldAbsoluteMusicPath) &&
                    oldAbsoluteMusicPath != newAbsoluteMusic &&
                    File.Exists(oldAbsoluteMusicPath))
                {
                    File.Delete(oldAbsoluteMusicPath);
                }
                // Tương tự, xóa file ảnh bìa cũ.
                if (!string.IsNullOrEmpty(oldAbsoluteCoverPath) &&
                    oldAbsoluteCoverPath != newAbsoluteCover &&
                    File.Exists(oldAbsoluteCoverPath))
                {
                    File.Delete(oldAbsoluteCoverPath);
                }

                UIMessageBox.ShowSuccess("Lưu thành công!");
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        #region Other Event Handlers and Helper Methods
        /// <summary>
        /// Mở thư mục chứa các file nhạc của ứng dụng bằng File Explorer.
        /// </summary>
        private void btnOpenMusicFolder_Click(object sender, EventArgs e)
        {
            try
            {
                // Bắt đầu một tiến trình mới là "explorer.exe" và truyền đường dẫn thư mục làm đối số.
                Process.Start("explorer.exe", PathHelper.MusicFolderPath);
            }
            catch (Exception ex) { UIMessageBox.ShowError("Không thể mở thư mục: " + ex.Message); }
        }

        /// <summary>
        /// Mở thư mục chứa các file ảnh bìa của ứng dụng bằng File Explorer.
        /// </summary>
        private void btnOpenCoverFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", PathHelper.CoversFolderPath); }
            catch (Exception ex) { UIMessageBox.ShowError("Không thể mở thư mục: " + ex.Message); }
        }

        /// <summary>
        /// Xử lý sự kiện chọn file nhạc mới.
        /// </summary>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Audio Files (*.mp3;*.mp4;*.wav)|*.mp3;*.mp4;*.wav" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Hiển thị tên file lên TextBox.
                    txtFilePath.Text = Path.GetFileName(ofd.FileName);
                    // Lưu đường dẫn ĐẦY ĐỦ vào thuộc tính Tag của TextBox.
                    // đi kèm với một control mà không cần tạo biến riêng.
                    txtFilePath.Tag = ofd.FileName;
                    btnOpenMusicFolder.Enabled = false; // Vô hiệu hóa nút mở vì đây là file tạm, chưa được chép.

                    // Cố gắng tự động điền tên bài hát và nghệ sĩ từ metadata (tags) của file nhạc.
                    try
                    {
                        var tagFile = TagLib.File.Create(ofd.FileName);
                        if (!string.IsNullOrEmpty(tagFile.Tag.Title)) txtName.Text = tagFile.Tag.Title;
                        if (!string.IsNullOrEmpty(tagFile.Tag.FirstPerformer)) txtArtist.Text = tagFile.Tag.FirstPerformer;
                    }
                    catch { /* Bỏ qua nếu không đọc được tag */ }

                    // Cố gắng tự động tải lời bài hát từ file .lrc/.txt cùng tên.
                    TryToAutoLoadLyrics(ofd.FileName);
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện chọn ảnh bìa mới.
        /// </summary>
        private void btnSelectCover_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Lưu đường dẫn đầy đủ vào Tag của PictureBox.
                    avatarPreview.Tag = ofd.FileName;
                    LoadPreviewImage(ofd.FileName);
                    btnOpenCoverFolder.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Tải và hiển thị một ảnh từ một đường dẫn file đầy đủ.
        /// </summary>
        private void LoadPreviewImage(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    // Đọc file vào mảng byte rồi tạo ảnh từ MemoryStream để tránh khóa file.
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

        /// <summary>
        /// Hủy bỏ các thay đổi và đóng form.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Mở hộp thoại để người dùng chọn file lời bài hát (.lrc hoặc .txt).
        /// </summary>
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

        /// <summary>
        /// Tự động tìm và tải lời bài hát từ file có cùng tên với file nhạc nhưng khác phần mở rộng (.lrc, .txt).
        /// </summary>
        private void TryToAutoLoadLyrics(string audioFilePath)
        {
            // Thử tìm file .lrc trước.
            string lrcFilePath = Path.ChangeExtension(audioFilePath, ".lrc");
            if (File.Exists(lrcFilePath))
            {
                LoadLyricsFromFile(lrcFilePath);
                this.ShowSuccessTip("Đã tự động tìm thấy và nhập lời từ file .lrc!");
                return; // Tìm thấy thì dừng lại.
            }
            // Nếu không có, thử tìm file .txt.
            string txtFilePath = Path.ChangeExtension(audioFilePath, ".txt");
            if (File.Exists(txtFilePath))
            {
                LoadLyricsFromFile(txtFilePath);
                this.ShowSuccessTip("Đã tự động tìm thấy và nhập lời từ file .txt!");
            }
        }

        /// <summary>
        /// Đọc nội dung từ một file text và hiển thị vào TextBox lời bài hát.
        /// </summary>
        private void LoadLyricsFromFile(string filePath)
        {
            try
            {
                // Đọc toàn bộ file text với mã hóa UTF-8 để hỗ trợ tiếng Việt.
                txtLyric.Text = File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (Exception ex) { UIMessageBox.ShowError("Không thể đọc file lời bài hát: " + ex.Message); }
        }
        #endregion
    }
}
