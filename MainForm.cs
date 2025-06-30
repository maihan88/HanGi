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
    public partial class MainForm : Sunny.UI.UIForm
    {
        private FormHome formHome;
        private FormPlayer formPlayer;
        private TabPage homeTabPage;
        private TabPage playerTabPage;

        public MainForm()
        {
            InitializeComponent();
            InitializeAppPagesAndMenu();

            // Đăng ký lắng nghe yêu cầu phát nhạc từ bất cứ đâu trong ứng dụng
            PlayerService.PlayRequest += HandlePlayRequest;
        }

        private void InitializeAppPagesAndMenu()
        {
            // --- Bước 1: Khởi tạo các trang con (UIPage) ---
            formHome = new FormHome();
            formPlayer = new FormPlayer();

            // --- Bước 2: Nhúng các trang con vào các TabPage ---
            homeTabPage = new TabPage("Trang chủ");
            EmbedPageIntoTab(formHome, homeTabPage);

            playerTabPage = new TabPage("Nghe nhạc");
            EmbedPageIntoTab(formPlayer, playerTabPage);

            // --- Bước 3: Thêm các TabPage vào TabControl ---
            // SỬA LỖI: Dùng đúng tên control từ Designer, có thể là 'uiTabControl1'
            uiTabControl1.Controls.Add(homeTabPage);
            uiTabControl1.Controls.Add(playerTabPage);

            // --- Bước 4: Tạo các mục Menu và Liên kết với TabPage ---
            // SỬA LỖI: Dùng phương thức AddNode của UINavMenu để tạo node có cả Symbol
            TreeNode homeNode = uiNavMenu1.CreateNode("Trang chủ", 61461);
            homeNode.Tag = homeTabPage;

            TreeNode playerNode = uiNavMenu1.CreateNode("Nghe nhạc", 61515);
            playerNode.Tag = playerTabPage;

            // --- Bước 5: Chọn mục menu đầu tiên làm mặc định ---
            uiNavMenu1.SelectFirst();
        }

        /// <summary>
        /// Hàm hỗ trợ để nhúng một UIPage vào một TabPage.
        /// </summary>
        private void EmbedPageIntoTab(UIPage page, TabPage tabPage)
        {
            page.TopLevel = false;
            page.Dock = DockStyle.Fill;
            tabPage.Controls.Add(page);
            page.Show();
        }

        private void HandlePlayRequest(List<Song> songs, int startIndex)
        {
            if (songs == null || !songs.Any()) return;

            // 1. Chuyển sang Tab "Nghe nhạc"
            uiTabControl1.SelectedTab = playerTabPage;

            // 2. Gọi đúng tên phương thức trên trang nghe nhạc để bắt đầu phát
            formPlayer.NavigateToNowPlayingAndPlay(songs, startIndex);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void uiNavMenu1_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {

        }

        private void Aside_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {

        }

        private void uiNavMenu1_MenuItemClick_1(TreeNode node, NavMenuItem item, int pageIndex)
        {
            if (node.Tag is TabPage pageToSelect)
            {
                // SỬA LỖI: Dùng đúng tên control từ Designer
                uiTabControl1.SelectedTab = pageToSelect;
            }
        }

        private void uiTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
