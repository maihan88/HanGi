using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HaNgi
{
    /// <summary>
    /// Một UserControl tùy chỉnh để hiển thị thông tin của một bài hát dưới dạng "thẻ" (card).
    /// Thường bao gồm ảnh bìa, tên bài hát và nghệ sĩ.
    /// </summary>
    public partial class SongCard : UserControl
    {
        public int SongID { get; set; }
        public string Title { get; private set; }

        /// <summary>
        /// Cung cấp quyền truy cập công khai vào control UIAvatar bên trong
        /// để các form khác có thể tương tác nếu cần.
        /// </summary>
        public Sunny.UI.UIAvatar CoverAvatar => avatarCover;

        /// <summary>
        /// Hàm khởi tạo của SongCard.
        /// </summary>
        public SongCard()
        {
            InitializeComponent();

            // --- Gắn sự kiện Click cho toàn bộ card ---
            // Vòng lặp này duyệt qua tất cả các control con trực tiếp (như Label, PictureBox)
            // và gắn sự kiện Click của chúng.
            foreach (Control control in this.Controls)
            {
                // Khi bất kỳ control con nào được click, nó sẽ kích hoạt sự kiện OnClick của chính SongCard.
                // Điều này tạo ra cảm giác toàn bộ card là một thể thống nhất và có thể click được.
                control.Click += (sender, e) => {
                    this.OnClick(e);
                };
            }
        }

        /// <summary>
        /// Thiết lập dữ liệu văn bản cho card.
        /// </summary>
        /// <param name="songId">ID của bài hát.</param>
        /// <param name="title">Tên bài hát.</param>
        /// <param name="artist">Tên nghệ sĩ.</param>
        public void SetData(int songId, string title, string artist)
        {
            this.SongID = songId;
            this.Title = title;

            lblSongName.Text = title;
            // Giả sử bạn có một label cho nghệ sĩ tên là lblArtist.
            // lblArtist.Text = artist;
        }

        /// <summary>
        /// Tải và hiển thị ảnh bìa cho bài hát một cách an toàn.
        /// </summary>
        /// <param name="absolutePath">Đường dẫn tuyệt đối đến file ảnh.</param>
        public void SetCoverImage(string absolutePath)
        {
            // Kiểm tra xem đường dẫn có hợp lệ và file có tồn tại không.
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    // Đọc toàn bộ file ảnh vào một mảng byte.
                    byte[] imageBytes = File.ReadAllBytes(absolutePath);
                    // Sử dụng MemoryStream để tạo đối tượng Image từ mảng byte.
                    // Cách này giúp tránh việc khóa file ảnh, cho phép các tiến trình khác có thể truy cập file.
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        avatarCover.Image = Image.FromStream(ms);
                    }
                }
                catch // Bắt tất cả các lỗi có thể xảy ra khi đọc file (file hỏng, không phải định dạng ảnh...).
                {
                    // Nếu có lỗi, hiển thị biểu tượng mặc định.
                    avatarCover.Symbol = 61442; // Mã biểu tượng nốt nhạc.
                    avatarCover.Image = null;
                }
            }
            else
            {
                // Nếu đường dẫn không hợp lệ, hiển thị biểu tượng mặc định.
                avatarCover.Symbol = 61442;
                avatarCover.Image = null;
            }
        }

        // Các hàm xử lý sự kiện trống, có thể thêm logic cho hiệu ứng hover (di chuột qua) tại đây.
        private void avatarCover_MouseEnter(object sender, EventArgs e)
        {

        }

        private void SongCard_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}