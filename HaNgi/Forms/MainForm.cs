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
        private FormSearch formSearch;
        private TabPage homeTabPage;
        private TabPage playerTabPage;
        private TreeNode playerNode;

        public MainForm()
        {
            InitializeComponent();
            InitializeAppPagesAndMenu();

            PlayerService.PlayRequest += HandlePlayRequest;
        }

        private void InitializeAppPagesAndMenu()
        {
            formHome = new FormHome();
            formPlayer = new FormPlayer();
            formSearch = new FormSearch();

            homeTabPage = new TabPage("Trang chủ");
            EmbedPageIntoTab(formHome, homeTabPage);

            playerTabPage = new TabPage("Nghe nhạc");
            EmbedPageIntoTab(formPlayer, playerTabPage);

            TabPage searchTabPage = new TabPage("Tìm kiếm");
            EmbedPageIntoTab(formSearch, searchTabPage);

            uiTabControl1.Controls.Add(homeTabPage);
            uiTabControl1.Controls.Add(playerTabPage);
            uiTabControl1.Controls.Add(searchTabPage);

            TreeNode homeNode = uiNavMenu1.CreateNode("Trang chủ", 61461);
            homeNode.Tag = homeTabPage;

            playerNode = uiNavMenu1.CreateNode("Nghe nhạc", 61515);
            playerNode.Tag = playerTabPage;

            TreeNode searchNode = uiNavMenu1.CreateNode("Tìm kiếm", 61442);
            searchNode.Tag = searchTabPage;

            uiNavMenu1.SelectFirst();
        }

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

            uiTabControl1.SelectedTab = playerTabPage;

            if (playerNode != null)
            {
                uiNavMenu1.SelectedNode = playerNode;
            }

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
