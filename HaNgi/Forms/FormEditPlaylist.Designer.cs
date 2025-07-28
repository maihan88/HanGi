namespace HaNgi
{
    partial class FormEditPlaylist
    {
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditPlaylist));
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.avatarPreview = new Sunny.UI.UIAvatar();
            this.uiPanel5 = new Sunny.UI.UIPanel();
            this.btnCancel = new Sunny.UI.UIButton();
            this.btnSave = new Sunny.UI.UIButton();
            this.btnSelectCover = new Sunny.UI.UIButton();
            this.txtPlaylistName = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiPanel4 = new Sunny.UI.UIPanel();
            this.uiTransferSongs = new Sunny.UI.UITransfer();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.btnDown = new Sunny.UI.UISymbolButton();
            this.btnUp = new Sunny.UI.UISymbolButton();
            this.uiPanel1.SuspendLayout();
            this.uiPanel5.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiPanel4.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.uiPanel1.Controls.Add(this.avatarPreview);
            this.uiPanel1.Controls.Add(this.uiPanel5);
            this.uiPanel1.Controls.Add(this.btnSelectCover);
            this.uiPanel1.Controls.Add(this.txtPlaylistName);
            this.uiPanel1.Controls.Add(this.uiLabel1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.FillColor = System.Drawing.Color.Transparent;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(0, 35);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(1173, 208);
            this.uiPanel1.TabIndex = 1;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // avatarPreview
            // 
            this.avatarPreview.AvatarSize = 150;
            this.avatarPreview.BackColor = System.Drawing.Color.Transparent;
            this.avatarPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.avatarPreview.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.avatarPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.avatarPreview.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.avatarPreview.Location = new System.Drawing.Point(37, 26);
            this.avatarPreview.MinimumSize = new System.Drawing.Size(1, 1);
            this.avatarPreview.Name = "avatarPreview";
            this.avatarPreview.Shape = Sunny.UI.UIShape.Square;
            this.avatarPreview.Size = new System.Drawing.Size(150, 150);
            this.avatarPreview.TabIndex = 6;
            this.avatarPreview.Text = "uiAvatar1";
            // 
            // uiPanel5
            // 
            this.uiPanel5.Controls.Add(this.btnCancel);
            this.uiPanel5.Controls.Add(this.btnSave);
            this.uiPanel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiPanel5.FillColor = System.Drawing.Color.Transparent;
            this.uiPanel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel5.Location = new System.Drawing.Point(937, 0);
            this.uiPanel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel5.Name = "uiPanel5";
            this.uiPanel5.Size = new System.Drawing.Size(236, 208);
            this.uiPanel5.TabIndex = 4;
            this.uiPanel5.Text = null;
            this.uiPanel5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnCancel.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnCancel.Location = new System.Drawing.Point(57, 116);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Radius = 50;
            this.btnCancel.Size = new System.Drawing.Size(118, 51);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnSave.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.btnSave.FillColorGradient = true;
            this.btnSave.FillColorGradientDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.btnSave.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(57, 40);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Radius = 50;
            this.btnSave.Size = new System.Drawing.Size(118, 51);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu";
            this.btnSave.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelectCover
            // 
            this.btnSelectCover.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectCover.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnSelectCover.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectCover.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSelectCover.Location = new System.Drawing.Point(212, 141);
            this.btnSelectCover.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSelectCover.Name = "btnSelectCover";
            this.btnSelectCover.Radius = 25;
            this.btnSelectCover.Size = new System.Drawing.Size(118, 35);
            this.btnSelectCover.TabIndex = 3;
            this.btnSelectCover.Text = "Chọn ảnh";
            this.btnSelectCover.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSelectCover.Click += new System.EventHandler(this.btnSelectCover_Click);
            // 
            // txtPlaylistName
            // 
            this.txtPlaylistName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlaylistName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtPlaylistName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPlaylistName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.txtPlaylistName.Font = new System.Drawing.Font("Lora", 12F);
            this.txtPlaylistName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtPlaylistName.Location = new System.Drawing.Point(212, 91);
            this.txtPlaylistName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPlaylistName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPlaylistName.Multiline = true;
            this.txtPlaylistName.Name = "txtPlaylistName";
            this.txtPlaylistName.Padding = new System.Windows.Forms.Padding(5);
            this.txtPlaylistName.Radius = 20;
            this.txtPlaylistName.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.txtPlaylistName.ShowText = false;
            this.txtPlaylistName.Size = new System.Drawing.Size(402, 34);
            this.txtPlaylistName.TabIndex = 2;
            this.txtPlaylistName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPlaylistName.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("Lora SemiBold", 12F, System.Drawing.FontStyle.Bold);
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel1.Location = new System.Drawing.Point(207, 49);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(128, 30);
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.Text = "Tên playlist:";
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiPanel4);
            this.uiPanel2.Controls.Add(this.uiPanel3);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(0, 243);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(1173, 457);
            this.uiPanel2.TabIndex = 2;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel4
            // 
            this.uiPanel4.Controls.Add(this.uiTransferSongs);
            this.uiPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel4.Location = new System.Drawing.Point(0, 0);
            this.uiPanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel4.Name = "uiPanel4";
            this.uiPanel4.Size = new System.Drawing.Size(1082, 457);
            this.uiPanel4.TabIndex = 1;
            this.uiPanel4.Text = null;
            this.uiPanel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiTransferSongs
            // 
            this.uiTransferSongs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.uiTransferSongs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTransferSongs.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.uiTransferSongs.FillColor2 = System.Drawing.Color.White;
            this.uiTransferSongs.FillDisableColor = System.Drawing.Color.White;
            this.uiTransferSongs.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTransferSongs.Location = new System.Drawing.Point(0, 0);
            this.uiTransferSongs.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.uiTransferSongs.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTransferSongs.Name = "uiTransferSongs";
            this.uiTransferSongs.Padding = new System.Windows.Forms.Padding(1);
            this.uiTransferSongs.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiTransferSongs.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.uiTransferSongs.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiTransferSongs.ShowText = false;
            this.uiTransferSongs.Size = new System.Drawing.Size(1082, 457);
            this.uiTransferSongs.TabIndex = 4;
            this.uiTransferSongs.Text = null;
            this.uiTransferSongs.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel3
            // 
            this.uiPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.uiPanel3.Controls.Add(this.btnDown);
            this.uiPanel3.Controls.Add(this.btnUp);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiPanel3.FillColor = System.Drawing.Color.Transparent;
            this.uiPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel3.Location = new System.Drawing.Point(1082, 0);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Size = new System.Drawing.Size(91, 457);
            this.uiPanel3.TabIndex = 0;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDown.Location = new System.Drawing.Point(25, 252);
            this.btnDown.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(47, 33);
            this.btnDown.Symbol = 61703;
            this.btnDown.TabIndex = 1;
            this.btnDown.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUp.Location = new System.Drawing.Point(25, 168);
            this.btnUp.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(47, 33);
            this.btnUp.Symbol = 61702;
            this.btnUp.TabIndex = 0;
            this.btnUp.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // FormEditPlaylist
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(1173, 700);
            this.ControlBox = false;
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiPanel1);
            this.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditPlaylist";
            this.Text = "FormEditPlaylist";
            this.TitleFont = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.Load += new System.EventHandler(this.FormEditPlaylist_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel1.PerformLayout();
            this.uiPanel5.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiPanel4.ResumeLayout(false);
            this.uiPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UITextBox txtPlaylistName;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIButton btnSelectCover;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIPanel uiPanel4;
        private Sunny.UI.UITransfer uiTransferSongs;
        private Sunny.UI.UIPanel uiPanel5;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UISymbolButton btnDown;
        private Sunny.UI.UISymbolButton btnUp;
        private Sunny.UI.UIAvatar avatarPreview;
    }
}