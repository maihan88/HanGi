namespace HaNgi
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiNavMenu1 = new Sunny.UI.UINavMenu();
            this.uiTabControl1 = new Sunny.UI.UITabControl();
            this.pnNavMenu = new Sunny.UI.UIPanel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.NameApp = new Sunny.UI.UISymbolLabel();
            this.pnNavMenu.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiNavMenu1
            // 
            this.uiNavMenu1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uiNavMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiNavMenu1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.uiNavMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiNavMenu1.FullRowSelect = true;
            this.uiNavMenu1.HotTracking = true;
            this.uiNavMenu1.ItemHeight = 50;
            this.uiNavMenu1.Location = new System.Drawing.Point(0, 0);
            this.uiNavMenu1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavMenu1.Name = "uiNavMenu1";
            this.uiNavMenu1.ShowLines = false;
            this.uiNavMenu1.ShowPlusMinus = false;
            this.uiNavMenu1.ShowRootLines = false;
            this.uiNavMenu1.Size = new System.Drawing.Size(200, 545);
            this.uiNavMenu1.TabControl = this.uiTabControl1;
            this.uiNavMenu1.TabIndex = 0;
            this.uiNavMenu1.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiNavMenu1.MenuItemClick += new Sunny.UI.UINavMenu.OnMenuItemClick(this.uiNavMenu1_MenuItemClick_1);
            // 
            // uiTabControl1
            // 
            this.uiTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiTabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.uiTabControl1.Location = new System.Drawing.Point(200, 35);
            this.uiTabControl1.MainPage = "";
            this.uiTabControl1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiTabControl1.Name = "uiTabControl1";
            this.uiTabControl1.SelectedIndex = 0;
            this.uiTabControl1.Size = new System.Drawing.Size(973, 665);
            this.uiTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControl1.TabIndex = 0;
            this.uiTabControl1.TabVisible = false;
            this.uiTabControl1.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiTabControl1.SelectedIndexChanged += new System.EventHandler(this.uiTabControl1_SelectedIndexChanged);
            // 
            // pnNavMenu
            // 
            this.pnNavMenu.Controls.Add(this.uiPanel2);
            this.pnNavMenu.Controls.Add(this.uiPanel1);
            this.pnNavMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnNavMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnNavMenu.Location = new System.Drawing.Point(0, 35);
            this.pnNavMenu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnNavMenu.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnNavMenu.Name = "pnNavMenu";
            this.pnNavMenu.Size = new System.Drawing.Size(200, 665);
            this.pnNavMenu.TabIndex = 1;
            this.pnNavMenu.Text = null;
            this.pnNavMenu.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiNavMenu1);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(0, 120);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(200, 545);
            this.uiPanel2.TabIndex = 1;
            this.uiPanel2.Text = "uiPanel2";
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.NameApp);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(200, 120);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NameApp
            // 
            this.NameApp.BackColor = System.Drawing.Color.DimGray;
            this.NameApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.NameApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameApp.Font = new System.Drawing.Font("Lucida Handwriting", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameApp.ForeColor = System.Drawing.Color.White;
            this.NameApp.Location = new System.Drawing.Point(0, 0);
            this.NameApp.MinimumSize = new System.Drawing.Size(1, 1);
            this.NameApp.Name = "NameApp";
            this.NameApp.Size = new System.Drawing.Size(200, 120);
            this.NameApp.Symbol = 361441;
            this.NameApp.SymbolColor = System.Drawing.Color.White;
            this.NameApp.TabIndex = 2;
            this.NameApp.Text = "HanGi";
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1173, 700);
            this.Controls.Add(this.uiTabControl1);
            this.Controls.Add(this.pnNavMenu);
            this.Name = "MainForm";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "HaNgi";
            this.TitleColor = System.Drawing.Color.White;
            this.TitleForeColor = System.Drawing.Color.Black;
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 480);
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            this.pnNavMenu.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UINavMenu uiNavMenu1;
        private Sunny.UI.UIPanel pnNavMenu;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UISymbolLabel NameApp;
        private Sunny.UI.UITabControl uiTabControl1;
    }
}