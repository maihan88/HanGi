using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HaNgi
{
    /// <summary>
    /// Một UserControl tùy chỉnh để hiển thị thông tin của một playlist dưới dạng một "thẻ" (card).
    /// Bao gồm ảnh bìa, tên playlist và một vài bài hát tiêu biểu.
    /// </summary>
    public partial class PlaylistCard : UserControl
    {
        public int PlaylistID { get; set; }
        public string PlaylistName { get; private set; }

        /// <summary>
        /// Cung cấp quyền truy cập công khai (public) vào control UIAvatar bên trong
        /// để các form khác có thể tương tác nếu cần (ví dụ: áp dụng hiệu ứng).
        /// </summary>
        public Sunny.UI.UIAvatar CoverAvatar => avatarCover;

        /// <summary>
        /// Hàm khởi tạo của PlaylistCard.
        /// </summary>
        public PlaylistCard()
        {
            InitializeComponent();
            // Gắn sự kiện Click cho toàn bộ card để người dùng có thể click vào bất kỳ đâu trên card.
            AttachClickEventToAllChildren(this);
        }

        /// <summary>
        /// Gắn sự kiện Click cho một control và tất cả các control con của nó một cách đệ quy.
        /// Mục đích: Khi người dùng click vào bất kỳ thành phần nào bên trong UserControl (như Label, PictureBox),
        /// sự kiện Click của chính UserControl (PlaylistCard) sẽ được kích hoạt.
        /// </summary>
        /// <param name="parent">Control cha để bắt đầu quá trình.</param>
        private void AttachClickEventToAllChildren(Control parent)
        {
            // Duyệt qua tất cả các control con trực tiếp của control cha.
            foreach (Control control in parent.Controls)
            {
                // Gắn sự kiện Click của control con. Khi được click, nó sẽ gọi hàm OnClick của PlaylistCard.
                control.Click += (sender, e) => { this.OnClick(e); };

                // Nếu control con này lại có các control con khác (đệ quy).
                if (control.HasChildren)
                {
                    // Gọi lại chính hàm này để xử lý cho các control cháu.
                    AttachClickEventToAllChildren(control);
                }
            }
        }

        /// <summary>
        /// Thiết lập dữ liệu văn bản cho card.
        /// </summary>
        /// <param name="playlistId">ID của playlist.</param>
        /// <param name="playlistName">Tên của playlist.</param>
        /// <param name="songPreviews">Danh sách tên một vài bài hát để hiển thị xem trước.</param>
        public void SetData(int playlistId, string playlistName, List<string> songPreviews)
        {
            this.PlaylistID = playlistId;
            this.PlaylistName = playlistName;
            lblPlaylistName.Text = playlistName;

            if (songPreviews != null && songPreviews.Any())
            {
                // Sử dụng string.Join để nối các tên bài hát lại với nhau, mỗi tên trên một dòng.
                lblSongPreview.Text = string.Join("\n", songPreviews);
            }
            else
            {
                lblSongPreview.Text = "Chưa có bài hát nào trong playlist này.";
            }
        }

        /// <summary>
        /// Tải và hiển thị ảnh bìa cho playlist một cách an toàn.
        /// </summary>
        /// <param name="absolutePath">Đường dẫn tuyệt đối đến file ảnh.</param>
        public void SetCoverImage(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    // Đọc file ảnh vào mảng byte và tải qua MemoryStream để tránh khóa file.
                    byte[] imageBytes = File.ReadAllBytes(absolutePath);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        avatarCover.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    // Nếu có lỗi (file hỏng, không phải ảnh), hiển thị biểu tượng mặc định.
                    avatarCover.Image = null;
                    avatarCover.Symbol = 61449; // Mã biểu tượng cho playlist/danh sách.
                }
            }
            else
            {
                // Nếu không có đường dẫn hoặc file không tồn tại, hiển thị biểu tượng mặc định.
                avatarCover.Image = null;
                avatarCover.Symbol = 61449;
            }
        }
    }
}
