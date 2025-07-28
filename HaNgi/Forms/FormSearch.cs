using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace HaNgi
{
    /// <summary>
    /// Giao diện trang tìm kiếm.
    /// Cho phép người dùng nhập từ khóa, tìm kiếm bài hát trong cơ sở dữ liệu
    /// và hiển thị kết quả dưới dạng các thẻ (cards).
    /// </summary>
    public partial class FormSearch : Sunny.UI.UIPage
    {
        /// <summary>
        /// Hàm khởi tạo mặc định của Form.
        /// </summary>
        public FormSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút "Tìm kiếm".
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox và loại bỏ các khoảng trắng thừa ở đầu và cuối.
            string searchTerm = txtSearchQuery.Text.Trim();

            // Kiểm tra xem người dùng đã nhập gì chưa.
            // string.IsNullOrEmpty là cách kiểm tra an toàn và hiệu quả cho cả chuỗi null và chuỗi rỗng ("").
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Hiển thị hộp thoại cảnh báo nếu chưa nhập từ khóa.
                UIMessageBox.ShowWarning("Vui lòng nhập từ khóa tìm kiếm.");
                return; // Dừng thực thi hàm.
            }

            // Xóa tất cả các kết quả tìm kiếm cũ khỏi FlowLayoutPanel.
            // FlowLayoutPanel là một container tự động sắp xếp các control con bên trong nó
            // theo một hướng nhất định (ví dụ: từ trái sang phải, trên xuống dưới).
            flpResults.Controls.Clear();

            // Gọi phương thức từ lớp DataAccess để thực hiện tìm kiếm trong cơ sở dữ liệu.
            List<Song> songResults = DataAccess.SearchSongs(searchTerm);

            // Sử dụng phương thức Any() của LINQ để kiểm tra xem danh sách kết quả có phần tử nào không.
            // Đây là cách viết ngắn gọn và dễ đọc hơn so với `songResults.Count > 0`.
            if (songResults.Any())
            {
                // Nếu có kết quả, duyệt qua từng bài hát trong danh sách.
                foreach (var song in songResults)
                {
                    // Tạo một đối tượng SongCard mới (đây là một UserControl tùy chỉnh).
                    var card = new SongCard();
                    // Thiết lập dữ liệu cho card.
                    card.SetData(song.SongID, song.SongName, song.Artist);
                    // Thiết lập ảnh bìa cho card.
                    card.SetCoverImage(song.CoverPath);

                    // Đăng ký sự kiện Click cho card vừa tạo bằng một lambda expression.
                    // Khi người dùng click vào card này...
                    card.Click += (s, ev) => {
                        // ...gọi PlayerService để yêu cầu phát bài hát tương ứng.
                        // Ta tạo một danh sách mới chỉ chứa bài hát này để phát riêng lẻ.
                        PlayerService.RequestPlay(new List<Song> { song });
                    };

                    // Thêm card đã hoàn thiện vào FlowLayoutPanel để hiển thị.
                    flpResults.Controls.Add(card);
                }
            }
            else
            {
                // Nếu không tìm thấy kết quả nào.
                // Tạo một UILabel mới để hiển thị thông báo.
                var lblNoResult = new UILabel();
                lblNoResult.Text = "Không tìm thấy kết quả nào.";
                lblNoResult.AutoSize = true; // Tự động điều chỉnh kích thước label cho vừa với nội dung.
                // Thêm label thông báo vào FlowLayoutPanel.
                flpResults.Controls.Add(lblNoResult);
            }
        }

        /// <summary>
        /// Hàm này thường được Visual Studio Designer sử dụng để khởi tạo.
        /// Việc gán sự kiện Click ở đây có thể là dư thừa nếu nó đã được gán trong file Designer.cs.
        /// </summary>
        private void FormSearch_Initialize(object sender, EventArgs e)
        {
            // Dòng này đảm bảo rằng phương thức btnSearch_Click được gắn với sự kiện Click của btnSearch.
            // Thông thường, việc này đã được thực hiện tự động trong file FormSearch.Designer.cs.
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
        }
    }
}
