// using: Khai báo sử dụng các "không gian tên" (namespace) chứa các lớp và hàm dựng sẵn.
// Điều này giống như việc "import" thư viện trong các ngôn ngữ khác.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq; // LINQ (Language Integrated Query) là một công nghệ mạnh mẽ để truy vấn dữ liệu từ các bộ sưu tập.
using System.Windows.Forms;
using Sunny.UI; // Thư viện giao diện người dùng của bên thứ ba.

// namespace: Tổ chức mã nguồn thành các nhóm logic, tránh xung đột tên.
namespace HaNgi
{
    /// <summary>
    /// Form này chịu trách nhiệm cho việc TẠO MỚI hoặc CHỈNH SỬA một Playlist.
    /// Nó hoạt động ở hai chế độ: "Tạo mới" (khi không có playlistId được cung cấp)
    /// và "Chỉnh sửa" (khi có playlistId).
    /// </summary>
    public partial class FormEditPlaylist : Sunny.UI.UIForm
    {
        /// <summary>
        /// Một lớp nội bộ (nested class) dùng để biểu diễn một bài hát trong các ListBox của UI.
        /// Việc tạo lớp riêng này giúp chúng ta tùy chỉnh cách thông tin bài hát được hiển thị
        /// mà không làm thay đổi lớp 'Song' gốc từ cơ sở dữ liệu.
        /// </summary>
        private class SongListItem
        {
            public int SongID { get; set; } // ID của bài hát, dùng để định danh.
            public string DisplayName { get; set; } // Tên hiển thị (ví dụ: "Tên Bài Hát - Tên Ca Sĩ").

            /// <summary>
            /// Ghi đè (override) phương thức ToString().
            /// Khi một đối tượng SongListItem được thêm vào ListBox, ListBox sẽ tự động gọi phương thức này
            /// để biết phải hiển thị chuỗi ký tự nào cho người dùng.
            /// </summary>
            /// <returns>Trả về tên hiển thị của bài hát.</returns>
            public override string ToString() => DisplayName;
        }

        // --- KHAI BÁO CÁC BIẾN THÀNH VIÊN (FIELDS) ---

        private bool isEditMode; // Biến cờ (flag) để xác định form đang ở chế độ chỉnh sửa (true) hay tạo mới (false).
        private int editingPlaylistId; // Lưu ID của playlist đang được chỉnh sửa.
        private string currentAbsoluteCoverPath; // Lưu đường dẫn ĐẦY ĐỦ đến file ảnh bìa MỚI mà người dùng chọn.
        private string originalAbsoluteCoverPath; // Lưu đường dẫn ĐẦY ĐỦ đến file ảnh bìa GỐC của playlist khi bắt đầu chỉnh sửa.

        /// <summary>
        /// Hàm khởi tạo (Constructor) của Form.
        /// </summary>
        /// <param name="playlistId">ID của playlist cần chỉnh sửa. Đây là một kiểu 'int?' (Nullable<int>),
        /// có nghĩa là nó có thể nhận giá trị số nguyên hoặc giá trị 'null'.
        /// Nếu là 'null', form sẽ ở chế độ tạo mới. Nếu có giá trị, form ở chế độ chỉnh sửa.</param>
        public FormEditPlaylist(int? playlistId = null)
        {
            InitializeComponent(); // Hàm này được Visual Studio tự động tạo để khởi tạo các thành phần trên giao diện.

            // Kiểm tra xem playlistId có giá trị hay không (tức là không phải null).
            if (playlistId.HasValue)
            {
                // --- CHẾ ĐỘ CHỈNH SỬA ---
                isEditMode = true;
                editingPlaylistId = playlistId.Value; // Lấy giá trị số nguyên từ kiểu nullable.
                this.Text = "Chỉnh sửa Playlist"; // Đặt tiêu đề cho cửa sổ form.
                LoadDataForEditing(); // Tải dữ liệu của playlist đã có để hiển thị lên form.
            }
            else
            {
                // --- CHẾ ĐỘ TẠO MỚI ---
                isEditMode = false;
                this.Text = "Tạo Playlist Mới";
                LoadAllSongsForNewPlaylist(); // Tải tất cả bài hát có sẵn để người dùng lựa chọn.
            }
        }

        /// <summary>
        /// Tải tất cả các bài hát từ cơ sở dữ liệu và hiển thị chúng
        /// vào danh sách bên trái (danh sách các bài hát có thể thêm vào playlist).
        /// Được gọi khi tạo một playlist mới.
        /// </summary>
        private void LoadAllSongsForNewPlaylist()
        {
            uiTransferSongs.ItemsLeft.Clear(); // Xóa sạch danh sách bên trái để đảm bảo không có dữ liệu cũ.
            var allSongs = DataAccess.GetAllSongs(); // Gọi lớp DataAccess để lấy danh sách tất cả bài hát.

            // Dùng vòng lặp 'foreach' để duyệt qua từng bài hát trong danh sách vừa lấy.
            foreach (var song in allSongs)
            {
                // Thêm một đối tượng SongListItem mới vào danh sách bên trái.
                uiTransferSongs.ItemsLeft.Add(new SongListItem
                {
                    SongID = song.SongID,
                    // Sử dụng "string interpolation" ($"...") để tạo chuỗi hiển thị gọn gàng.
                    DisplayName = $"{song.SongName} - {song.Artist}"
                });
            }
        }

        /// <summary>
        /// Tải dữ liệu chi tiết của một playlist đã tồn tại để người dùng chỉnh sửa.
        /// Bao gồm tên playlist, ảnh bìa, và danh sách các bài hát.
        /// </summary>
        private void LoadDataForEditing()
        {
            var playlist = DataAccess.GetPlaylistById(editingPlaylistId);
            if (playlist == null)
            {
                UIMessageBox.ShowError("Không tìm thấy playlist để chỉnh sửa.");
                this.Close(); // Đóng form nếu không tìm thấy playlist.
                return; // Kết thúc hàm ngay lập tức.
            }

            // Điền thông tin của playlist vào các control trên form.
            txtPlaylistName.Text = playlist.PlaylistName;
            currentAbsoluteCoverPath = playlist.PlaylistImage;
            originalAbsoluteCoverPath = currentAbsoluteCoverPath; // Lưu lại đường dẫn ảnh gốc.
            LoadPreviewImage(currentAbsoluteCoverPath); // Hiển thị ảnh bìa.

            // --- SỬ DỤNG LINQ ĐỂ XỬ LÝ DANH SÁCH BÀI HÁT ---

            // 1. Lấy tất cả bài hát và chuyển đổi chúng thành danh sách các đối tượng SongListItem.
            //    .Select() là một toán tử LINQ dùng để "chiếu" (project) mỗi phần tử của một tập hợp sang một dạng mới.
            var allSongs = DataAccess.GetAllSongs().Select(s => new SongListItem { SongID = s.SongID, DisplayName = $"{s.SongName} - {s.Artist}" }).ToList();

            // 2. Lấy các bài hát thuộc playlist này và cũng chuyển đổi chúng.
            var songsInPlaylist = DataAccess.GetSongsByPlaylistId(editingPlaylistId).Select(s => new SongListItem { SongID = s.SongID, DisplayName = $"{s.SongName} - {s.Artist}" }).ToList();

            // 3. Tìm các bài hát KHÔNG thuộc playlist này.
            //    .Where() là toán tử LINQ để lọc một tập hợp dựa trên một điều kiện.
            //    !songsInPlaylist.Any(ps => ps.SongID == s.SongID) có nghĩa là: "lọc những bài hát 's' mà không có BẤT KỲ ('Any')
            //    bài hát nào trong 'songsInPlaylist' có cùng SongID".
            var songsNotInPlaylist = allSongs.Where(s => !songsInPlaylist.Any(ps => ps.SongID == s.SongID)).ToList();


            // 4. Hiển thị các danh sách đã xử lý lên giao diện.
            uiTransferSongs.ItemsLeft.Clear();
            foreach (var s in songsNotInPlaylist) uiTransferSongs.ItemsLeft.Add(s);

            uiTransferSongs.ItemsRight.Clear();
            foreach (var s in songsInPlaylist) uiTransferSongs.ItemsRight.Add(s);
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút "Lưu".
        /// Thu thập dữ liệu từ form và gọi DataAccess để lưu vào cơ sở dữ liệu.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã nhập tên playlist chưa.
            // string.IsNullOrWhiteSpace kiểm tra cả chuỗi rỗng ("") và chuỗi chỉ chứa khoảng trắng ("   ").
            if (string.IsNullOrWhiteSpace(txtPlaylistName.Text))
            {
                UIMessageBox.ShowWarning("Tên playlist không được để trống!");
                return; // Dừng lại, không làm gì thêm.
            }

            string coverFileNameToSave = ""; // Biến để lưu TÊN FILE ảnh sẽ được lưu vào CSDL.

            // Kiểm tra xem người dùng có chọn ảnh bìa mới không.
            if (currentAbsoluteCoverPath != originalAbsoluteCoverPath && !string.IsNullOrEmpty(currentAbsoluteCoverPath))
            {
                // Người dùng đã chọn một ảnh mới.
                // Sao chép file ảnh từ vị trí người dùng chọn vào thư mục của ứng dụng.
                coverFileNameToSave = PathHelper.CopyFileToAppFolder(currentAbsoluteCoverPath, PathHelper.CoversFolderPath);
                if (string.IsNullOrEmpty(coverFileNameToSave)) return; // Dừng nếu có lỗi xảy ra khi sao chép file.
            }
            else if (!string.IsNullOrEmpty(originalAbsoluteCoverPath))
            {
                // Người dùng không chọn ảnh mới, giữ lại ảnh cũ.
                // Chỉ lấy tên file từ đường dẫn đầy đủ để lưu vào CSDL.
                coverFileNameToSave = Path.GetFileName(originalAbsoluteCoverPath);
            }

            // Tạo một đối tượng Playlist mới với thông tin từ form.
            var playlistToSave = new Playlist
            {
                PlaylistID = this.editingPlaylistId, // Nếu là tạo mới, ID này có thể là 0 hoặc sẽ được CSDL tự gán.
                PlaylistName = txtPlaylistName.Text,
                PlaylistImage = coverFileNameToSave // Lưu tên file, không phải đường dẫn đầy đủ.
            };

            // Lấy danh sách ID của các bài hát trong danh sách bên phải (các bài hát trong playlist).
            // uiTransferSongs.ItemsRight là một tập hợp các 'object', phải ép kiểu (Cast) chúng về 'SongListItem'
            // trước khi có thể truy cập thuộc tính 'SongID'.
            var songIdsInPlaylist = uiTransferSongs.ItemsRight.Cast<SongListItem>().Select(s => s.SongID).ToList();

            // Gọi hàm lưu của DataAccess.
            if (DataAccess.SavePlaylist(playlistToSave, songIdsInPlaylist))
            {
                UIMessageBox.ShowSuccess("Lưu playlist thành công!");
                this.DialogResult = DialogResult.OK; // Đặt kết quả của form là OK. Form cha có thể kiểm tra kết quả này.
                this.Close(); // Đóng form sau khi lưu thành công.
            }
            // Nếu lưu thất bại, DataAccess sẽ hiển thị thông báo lỗi.
        }

        // #region là một chỉ thị của C# để giúp gom nhóm các đoạn mã lại với nhau trong trình soạn thảo,
        // giúp mã nguồn gọn gàng hơn, có thể thu gọn/mở rộng khối này.
        #region Other Event Handlers and Helper Methods

        private void FormEditPlaylist_Load(object sender, EventArgs e) { }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút "Chọn ảnh bìa".
        /// Mở hộp thoại cho phép người dùng chọn một file ảnh.
        /// </summary>
        private void btnSelectCover_Click(object sender, EventArgs e)
        {
            // 'using' statement đảm bảo rằng đối tượng 'ofd' (OpenFileDialog) sẽ được giải phóng tài nguyên
            // một cách tự động ngay cả khi có lỗi xảy ra. Đây là cách làm tốt nhất.
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                // Hiển thị hộp thoại. Nếu người dùng chọn một file và nhấn OK...
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn đầy đủ của file đã chọn và cập nhật vào biến.
                    currentAbsoluteCoverPath = ofd.FileName;
                    // Tải ảnh lên để xem trước.
                    LoadPreviewImage(currentAbsoluteCoverPath);
                }
            }
        }

        /// <summary>
        /// Tải và hiển thị một ảnh từ một đường dẫn file đầy đủ.
        /// </summary>
        /// <param name="absolutePath">Đường dẫn đầy đủ đến file ảnh.</param>
        private void LoadPreviewImage(string absolutePath)
        {
            // Kiểm tra đường dẫn có hợp lệ và file có tồn tại không.
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                // try-catch block để bắt các lỗi có thể xảy ra khi đọc file (ví dụ: file bị hỏng, không có quyền đọc).
                try
                {
                    // Đọc toàn bộ nội dung file ảnh vào một mảng byte.
                    byte[] imageBytes = File.ReadAllBytes(absolutePath);
                    // Sử dụng MemoryStream để đọc dữ liệu ảnh từ mảng byte trong bộ nhớ.
                    // Cách này giúp tránh việc khóa file ảnh trên đĩa cứng.
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        avatarPreview.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError("Không thể đọc file ảnh: " + ex.Message);
                    avatarPreview.Image = null; // Xóa ảnh xem trước nếu có lỗi.
                }
            }
            else
            {
                avatarPreview.Image = null; // Xóa ảnh xem trước nếu đường dẫn không hợp lệ.
            }
        }

        // Các hàm xử lý sự kiện cho nút Lên/Xuống.
        // Sử dụng cú pháp "expression-bodied member" (=>) cho các hàm chỉ có một dòng lệnh.
        private void btnUp_Click(object sender, EventArgs e) => MoveItemInPlaylist(-1);
        private void btnDown_Click(object sender, EventArgs e) => MoveItemInPlaylist(1);

        /// <summary>
        /// Di chuyển mục được chọn trong danh sách bài hát của playlist lên hoặc xuống.
        /// </summary>
        /// <param name="direction">-1 để đi lên, 1 để đi xuống.</param>
        private void MoveItemInPlaylist(int direction)
        {
            var listBox = uiTransferSongs.ListBoxRight; // Lấy ListBox bên phải.
            // Kiểm tra xem có mục nào được chọn không.
            if (listBox.SelectedItem == null || listBox.SelectedIndex < 0) return;

            // Tính toán vị trí mới.
            int newIndex = listBox.SelectedIndex + direction;
            // Kiểm tra xem vị trí mới có nằm ngoài phạm vi của danh sách không.
            if (newIndex < 0 || newIndex >= listBox.Items.Count) return;

            // Thực hiện di chuyển
            object selected = listBox.SelectedItem; // Lưu lại mục đang được chọn.
            listBox.Items.Remove(selected); // Xóa nó khỏi vị trí hiện tại.
            listBox.Items.Insert(newIndex, selected); // Chèn nó vào vị trí mới.
            listBox.SetSelected(newIndex, true); // Chọn lại mục đó ở vị trí mới.
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút "Hủy".
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Đặt kết quả là Cancel.
            this.Close(); // Đóng form.
        }
        #endregion
    }
}
