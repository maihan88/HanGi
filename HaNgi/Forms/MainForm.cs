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
    /// Form chính của ứng dụng.
    /// Đóng vai trò là "container" (thùng chứa) chính, quản lý việc hiển thị các trang con
    /// (Home, Player, Search) và điều hướng giữa chúng thông qua menu.
    /// </summary>
    public partial class MainForm : Sunny.UI.UIForm
    {
        // --- Các biến tham chiếu đến các trang con và control quan trọng ---
        private FormHome formHome;
        private FormPlayer formPlayer;
        private FormSearch formSearch;
        private TabPage homeTabPage;
        private TabPage playerTabPage;
        private TreeNode playerNode; // Lưu lại node "Nghe nhạc" để có thể chọn nó bằng code.

        /// <summary>
        /// Hàm khởi tạo của MainForm.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            // Khởi tạo các trang và menu điều hướng.
            InitializeAppPagesAndMenu();

            // Đăng ký lắng nghe sự kiện "PlayRequest" từ PlayerService.
            // Khi bất kỳ phần nào của ứng dụng (ví dụ: FormHome, FormSearch) muốn phát nhạc,
            // nó sẽ gửi yêu cầu qua PlayerService, và hàm HandlePlayRequest sẽ được gọi để xử lý.
            // Đây là một ví dụ về kiến trúc "event-driven" (hướng sự kiện) giúp các form không cần biết về nhau trực tiếp.
            PlayerService.PlayRequest += HandlePlayRequest;
        }

        /// <summary>
        /// Khởi tạo các trang con (UIPage), nhúng chúng vào các TabPage,
        /// và tạo ra menu điều hướng (UINavMenu) tương ứng.
        /// </summary>
        private void InitializeAppPagesAndMenu()
        {
            // 1. Tạo các instance (thể hiện) của các form trang con.
            formHome = new FormHome();
            formPlayer = new FormPlayer();
            formSearch = new FormSearch();

            // Truyền tham chiếu của formPlayer cho formHome để formHome có thể tương tác
            // trực tiếp với trình phát khi cần (ví dụ: kiểm tra bài hát đang phát trước khi xóa).
            formHome.SetPlayerFormReference(formPlayer);

            // 2. Tạo các TabPage để chứa các form con.
            homeTabPage = new TabPage("Trang chủ");
            EmbedPageIntoTab(formHome, homeTabPage);

            playerTabPage = new TabPage("Nghe nhạc");
            EmbedPageIntoTab(formPlayer, playerTabPage);

            TabPage searchTabPage = new TabPage("Tìm kiếm");
            EmbedPageIntoTab(formSearch, searchTabPage);

            // 3. Thêm các TabPage vào TabControl.
            uiTabControl1.Controls.Add(homeTabPage);
            uiTabControl1.Controls.Add(playerTabPage);
            uiTabControl1.Controls.Add(searchTabPage);

            // 4. Tạo các mục menu (TreeNode) và liên kết chúng với các TabPage.
            // 61461, 61515, 61442 là các mã biểu tượng (symbol) của thư viện SunnyUI.
            TreeNode homeNode = uiNavMenu1.CreateNode("Trang chủ", 61461);
            // Sử dụng thuộc tính `Tag` để lưu trữ một tham chiếu đến TabPage tương ứng.
            // Đây là một kỹ thuật phổ biến trong WinForms để liên kết dữ liệu với một control.
            homeNode.Tag = homeTabPage;

            playerNode = uiNavMenu1.CreateNode("Nghe nhạc", 61515);
            playerNode.Tag = playerTabPage;

            TreeNode searchNode = uiNavMenu1.CreateNode("Tìm kiếm", 61442);
            searchNode.Tag = searchTabPage;

            // Mặc định chọn mục menu đầu tiên khi ứng dụng khởi động.
            uiNavMenu1.SelectFirst();
        }

        /// <summary>
        /// Hàm tiện ích để nhúng một UIPage (hoặc Form) vào bên trong một TabPage.
        /// </summary>
        /// <param name="page">Trang con cần nhúng.</param>
        /// <param name="tabPage">Tab sẽ chứa trang con.</param>
        private void EmbedPageIntoTab(UIPage page, TabPage tabPage)
        {
            page.TopLevel = false; // Quan trọng: Cho phép form được chứa bên trong một control khác.
            page.Dock = DockStyle.Fill; // Form con sẽ lấp đầy toàn bộ không gian của TabPage.
            tabPage.Controls.Add(page); // Thêm form con vào TabPage.
            page.Show(); // Hiển thị form con.
        }

        /// <summary>
        /// Xử lý yêu cầu phát nhạc được gửi từ PlayerService.
        /// </summary>
        /// <param name="songs">Danh sách bài hát cần phát.</param>
        /// <param name="startIndex">Chỉ số của bài hát bắt đầu phát trong danh sách.</param>
        private void HandlePlayRequest(List<Song> songs, int startIndex)
        {
            // Kiểm tra xem danh sách có hợp lệ không.
            if (songs == null || !songs.Any()) return;

            // 1. Tự động chuyển sang tab "Nghe nhạc".
            uiTabControl1.SelectedTab = playerTabPage;

            // 2. Tự động chọn mục "Nghe nhạc" trên menu điều hướng.
            if (playerNode != null)
            {
                uiNavMenu1.SelectedNode = playerNode;
            }

            // 3. Gọi phương thức trên formPlayer để bắt đầu phát nhạc.
            formPlayer.NavigateToNowPlayingAndPlay(songs, startIndex);
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng click vào một mục trên menu điều hướng.
        /// </summary>
        private void uiNavMenu1_MenuItemClick_1(TreeNode node, NavMenuItem item, int pageIndex)
        {
            // Kiểm tra xem node.Tag có phải TabPage không
            if (node.Tag is TabPage pageToSelect)
            {
                uiTabControl1.SelectedTab = pageToSelect;
            }
        }

        // --- Các hàm xử lý sự kiện trống (có thể được sử dụng trong tương lai) ---

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Có thể thêm code khởi tạo tại đây nếu cần.
        }

        private void Aside_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
            // Dành cho menu phụ (nếu có).
        }

        private void uiTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Có thể xử lý logic khi người dùng tự chuyển tab bằng chuột.
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            // Đây là một trình xử lý sự kiện Load khác, có thể là dư thừa.
        }
    }
}
