namespace HaNgi
{
    partial class FormHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHome));
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.flpPlaylists = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSongs = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRightDetails = new Sunny.UI.UIPanel();
            this.pnlPlaylistDetails = new Sunny.UI.UIPanel();
            this.lstPlaylistSongsDetail = new Sunny.UI.UIListBox();
            this.btnPlayPlaylist = new Sunny.UI.UIButton();
            this.playlistDetailLblName = new Sunny.UI.UILabel();
            this.uiAvatar1 = new Sunny.UI.UIAvatar();
            this.pnlSongDetails = new Sunny.UI.UIPanel();
            this.btnPlaySong = new Sunny.UI.UIButton();
            this.detailLblDuration = new Sunny.UI.UILabel();
            this.detailLblArtist = new Sunny.UI.UILabel();
            this.detailLblSongName = new Sunny.UI.UILabel();
            this.detailAvatar = new Sunny.UI.UIAvatar();
            this.uiNavBar1 = new Sunny.UI.UINavBar();
            this.btnDelete = new Sunny.UI.UIImageButton();
            this.btnEdit = new Sunny.UI.UIImageButton();
            this.btnAddPlaylist = new Sunny.UI.UIImageButton();
            this.btnAddSong = new Sunny.UI.UIImageButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiPanel1.SuspendLayout();
            this.pnlRightDetails.SuspendLayout();
            this.pnlPlaylistDetails.SuspendLayout();
            this.pnlSongDetails.SuspendLayout();
            this.uiNavBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPlaylist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddSong)).BeginInit();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(61)))));
            this.uiPanel1.Controls.Add(this.flpPlaylists);
            this.uiPanel1.Controls.Add(this.flpSongs);
            this.uiPanel1.Controls.Add(this.pnlRightDetails);
            this.uiPanel1.Controls.Add(this.uiNavBar1);
            this.uiPanel1.Controls.Add(this.uiLabel2);
            this.uiPanel1.Controls.Add(this.uiLabel1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.uiPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.uiPanel1.FillColorGradient = true;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RectColor = System.Drawing.Color.Transparent;
            this.uiPanel1.Size = new System.Drawing.Size(973, 665);
            this.uiPanel1.TabIndex = 1;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.Click += new System.EventHandler(this.uiPanel1_Click);
            // 
            // flpPlaylists
            // 
            this.flpPlaylists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpPlaylists.AutoScroll = true;
            this.flpPlaylists.BackColor = System.Drawing.Color.Transparent;
            this.flpPlaylists.ForeColor = System.Drawing.Color.White;
            this.flpPlaylists.Location = new System.Drawing.Point(17, 340);
            this.flpPlaylists.Name = "flpPlaylists";
            this.flpPlaylists.Size = new System.Drawing.Size(600, 250);
            this.flpPlaylists.TabIndex = 8;
            this.flpPlaylists.WrapContents = false;
            // 
            // flpSongs
            // 
            this.flpSongs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpSongs.AutoScroll = true;
            this.flpSongs.BackColor = System.Drawing.Color.Transparent;
            this.flpSongs.Location = new System.Drawing.Point(17, 37);
            this.flpSongs.Name = "flpSongs";
            this.flpSongs.Size = new System.Drawing.Size(600, 250);
            this.flpSongs.TabIndex = 7;
            // 
            // pnlRightDetails
            // 
            this.pnlRightDetails.Controls.Add(this.pnlPlaylistDetails);
            this.pnlRightDetails.Controls.Add(this.pnlSongDetails);
            this.pnlRightDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRightDetails.FillColor = System.Drawing.Color.Transparent;
            this.pnlRightDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlRightDetails.Location = new System.Drawing.Point(668, 0);
            this.pnlRightDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlRightDetails.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlRightDetails.Name = "pnlRightDetails";
            this.pnlRightDetails.RectColor = System.Drawing.Color.Transparent;
            this.pnlRightDetails.Size = new System.Drawing.Size(305, 596);
            this.pnlRightDetails.TabIndex = 6;
            this.pnlRightDetails.Text = null;
            this.pnlRightDetails.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPlaylistDetails
            // 
            this.pnlPlaylistDetails.Controls.Add(this.lstPlaylistSongsDetail);
            this.pnlPlaylistDetails.Controls.Add(this.btnPlayPlaylist);
            this.pnlPlaylistDetails.Controls.Add(this.playlistDetailLblName);
            this.pnlPlaylistDetails.Controls.Add(this.uiAvatar1);
            this.pnlPlaylistDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlaylistDetails.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.pnlPlaylistDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlPlaylistDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlPlaylistDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlPlaylistDetails.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlPlaylistDetails.Name = "pnlPlaylistDetails";
            this.pnlPlaylistDetails.Radius = 20;
            this.pnlPlaylistDetails.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.pnlPlaylistDetails.Size = new System.Drawing.Size(305, 596);
            this.pnlPlaylistDetails.TabIndex = 7;
            this.pnlPlaylistDetails.Text = null;
            this.pnlPlaylistDetails.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlPlaylistDetails.Visible = false;
            // 
            // lstPlaylistSongsDetail
            // 
            this.lstPlaylistSongsDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPlaylistSongsDetail.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.lstPlaylistSongsDetail.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaylistSongsDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lstPlaylistSongsDetail.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lstPlaylistSongsDetail.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.lstPlaylistSongsDetail.ItemSelectForeColor = System.Drawing.Color.White;
            this.lstPlaylistSongsDetail.Location = new System.Drawing.Point(34, 259);
            this.lstPlaylistSongsDetail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstPlaylistSongsDetail.MinimumSize = new System.Drawing.Size(1, 1);
            this.lstPlaylistSongsDetail.Name = "lstPlaylistSongsDetail";
            this.lstPlaylistSongsDetail.Padding = new System.Windows.Forms.Padding(2);
            this.lstPlaylistSongsDetail.RectColor = System.Drawing.Color.Transparent;
            this.lstPlaylistSongsDetail.ShowText = false;
            this.lstPlaylistSongsDetail.Size = new System.Drawing.Size(239, 212);
            this.lstPlaylistSongsDetail.TabIndex = 5;
            this.lstPlaylistSongsDetail.Text = "uiListBox1";
            // 
            // btnPlayPlaylist
            // 
            this.btnPlayPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlayPlaylist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayPlaylist.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnPlayPlaylist.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.btnPlayPlaylist.FillColorGradient = true;
            this.btnPlayPlaylist.FillColorGradientDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.btnPlayPlaylist.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayPlaylist.Location = new System.Drawing.Point(77, 491);
            this.btnPlayPlaylist.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPlayPlaylist.Name = "btnPlayPlaylist";
            this.btnPlayPlaylist.Size = new System.Drawing.Size(150, 51);
            this.btnPlayPlaylist.TabIndex = 4;
            this.btnPlayPlaylist.Text = "Nghe";
            this.btnPlayPlaylist.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPlayPlaylist.Click += new System.EventHandler(this.btnPlayPlaylist_Click);
            // 
            // playlistDetailLblName
            // 
            this.playlistDetailLblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playlistDetailLblName.BackColor = System.Drawing.Color.Transparent;
            this.playlistDetailLblName.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playlistDetailLblName.ForeColor = System.Drawing.Color.White;
            this.playlistDetailLblName.Location = new System.Drawing.Point(0, 203);
            this.playlistDetailLblName.Name = "playlistDetailLblName";
            this.playlistDetailLblName.Size = new System.Drawing.Size(305, 41);
            this.playlistDetailLblName.TabIndex = 1;
            this.playlistDetailLblName.Text = "Tên playlist";
            this.playlistDetailLblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiAvatar1
            // 
            this.uiAvatar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiAvatar1.AvatarSize = 150;
            this.uiAvatar1.BackColor = System.Drawing.Color.Transparent;
            this.uiAvatar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.uiAvatar1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.uiAvatar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiAvatar1.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.uiAvatar1.Location = new System.Drawing.Point(77, 37);
            this.uiAvatar1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAvatar1.Name = "uiAvatar1";
            this.uiAvatar1.Radius = 20;
            this.uiAvatar1.Shape = Sunny.UI.UIShape.Square;
            this.uiAvatar1.Size = new System.Drawing.Size(150, 150);
            this.uiAvatar1.Symbol = 0;
            this.uiAvatar1.TabIndex = 0;
            this.uiAvatar1.Text = "uiAvatar1";
            // 
            // pnlSongDetails
            // 
            this.pnlSongDetails.Controls.Add(this.btnPlaySong);
            this.pnlSongDetails.Controls.Add(this.detailLblDuration);
            this.pnlSongDetails.Controls.Add(this.detailLblArtist);
            this.pnlSongDetails.Controls.Add(this.detailLblSongName);
            this.pnlSongDetails.Controls.Add(this.detailAvatar);
            this.pnlSongDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSongDetails.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.pnlSongDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlSongDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlSongDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSongDetails.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlSongDetails.Name = "pnlSongDetails";
            this.pnlSongDetails.Radius = 20;
            this.pnlSongDetails.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.pnlSongDetails.Size = new System.Drawing.Size(305, 596);
            this.pnlSongDetails.TabIndex = 6;
            this.pnlSongDetails.Text = null;
            this.pnlSongDetails.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlSongDetails.Visible = false;
            // 
            // btnPlaySong
            // 
            this.btnPlaySong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaySong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlaySong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnPlaySong.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.btnPlaySong.FillColorGradient = true;
            this.btnPlaySong.FillColorGradientDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.btnPlaySong.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlaySong.Location = new System.Drawing.Point(77, 408);
            this.btnPlaySong.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPlaySong.Name = "btnPlaySong";
            this.btnPlaySong.Size = new System.Drawing.Size(150, 51);
            this.btnPlaySong.TabIndex = 4;
            this.btnPlaySong.Text = "Nghe";
            this.btnPlaySong.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPlaySong.Click += new System.EventHandler(this.btnPlaySong_Click);
            // 
            // detailLblDuration
            // 
            this.detailLblDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailLblDuration.BackColor = System.Drawing.Color.Transparent;
            this.detailLblDuration.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLblDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.detailLblDuration.Location = new System.Drawing.Point(0, 320);
            this.detailLblDuration.Name = "detailLblDuration";
            this.detailLblDuration.Size = new System.Drawing.Size(302, 30);
            this.detailLblDuration.TabIndex = 3;
            this.detailLblDuration.Text = "Thời lượng";
            this.detailLblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailLblArtist
            // 
            this.detailLblArtist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailLblArtist.BackColor = System.Drawing.Color.Transparent;
            this.detailLblArtist.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLblArtist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.detailLblArtist.Location = new System.Drawing.Point(-3, 274);
            this.detailLblArtist.Name = "detailLblArtist";
            this.detailLblArtist.Size = new System.Drawing.Size(305, 30);
            this.detailLblArtist.TabIndex = 2;
            this.detailLblArtist.Text = "Nghệ sĩ";
            this.detailLblArtist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailLblSongName
            // 
            this.detailLblSongName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailLblSongName.BackColor = System.Drawing.Color.Transparent;
            this.detailLblSongName.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLblSongName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.detailLblSongName.Location = new System.Drawing.Point(3, 214);
            this.detailLblSongName.Name = "detailLblSongName";
            this.detailLblSongName.Size = new System.Drawing.Size(299, 60);
            this.detailLblSongName.TabIndex = 1;
            this.detailLblSongName.Text = "Tên bài hát";
            this.detailLblSongName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailAvatar
            // 
            this.detailAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.detailAvatar.AvatarSize = 150;
            this.detailAvatar.BackColor = System.Drawing.Color.Transparent;
            this.detailAvatar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.detailAvatar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.detailAvatar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.detailAvatar.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.detailAvatar.Location = new System.Drawing.Point(77, 37);
            this.detailAvatar.MinimumSize = new System.Drawing.Size(1, 1);
            this.detailAvatar.Name = "detailAvatar";
            this.detailAvatar.Radius = 20;
            this.detailAvatar.Shape = Sunny.UI.UIShape.Square;
            this.detailAvatar.Size = new System.Drawing.Size(150, 150);
            this.detailAvatar.Symbol = 0;
            this.detailAvatar.TabIndex = 0;
            this.detailAvatar.Text = "uiAvatar1";
            // 
            // uiNavBar1
            // 
            this.uiNavBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.uiNavBar1.Controls.Add(this.btnDelete);
            this.uiNavBar1.Controls.Add(this.btnEdit);
            this.uiNavBar1.Controls.Add(this.btnAddPlaylist);
            this.uiNavBar1.Controls.Add(this.btnAddSong);
            this.uiNavBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiNavBar1.DropMenuFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiNavBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiNavBar1.ForeColor = System.Drawing.Color.DimGray;
            this.uiNavBar1.Location = new System.Drawing.Point(0, 596);
            this.uiNavBar1.MenuHoverColor = System.Drawing.Color.Silver;
            this.uiNavBar1.MenuSelectedColor = System.Drawing.Color.Silver;
            this.uiNavBar1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavBar1.Name = "uiNavBar1";
            this.uiNavBar1.Size = new System.Drawing.Size(973, 69);
            this.uiNavBar1.TabIndex = 5;
            this.uiNavBar1.Text = "uiNavBar1";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Font = new System.Drawing.Font("Lora Medium", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageHover")));
            this.btnDelete.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImagePress")));
            this.btnDelete.ImageSelected = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageSelected")));
            this.btnDelete.Location = new System.Drawing.Point(576, 17);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 40);
            this.btnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDelete.TabIndex = 3;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Font = new System.Drawing.Font("Lora Medium", 12F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageHover")));
            this.btnEdit.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImagePress")));
            this.btnEdit.ImageSelected = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageSelected")));
            this.btnEdit.Location = new System.Drawing.Point(453, 17);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(86, 40);
            this.btnEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnEdit.TabIndex = 2;
            this.btnEdit.TabStop = false;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAddPlaylist
            // 
            this.btnAddPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddPlaylist.BackColor = System.Drawing.Color.Transparent;
            this.btnAddPlaylist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddPlaylist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddPlaylist.Font = new System.Drawing.Font("Lora Medium", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddPlaylist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnAddPlaylist.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPlaylist.Image")));
            this.btnAddPlaylist.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnAddPlaylist.ImageHover")));
            this.btnAddPlaylist.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnAddPlaylist.ImagePress")));
            this.btnAddPlaylist.ImageSelected = ((System.Drawing.Image)(resources.GetObject("btnAddPlaylist.ImageSelected")));
            this.btnAddPlaylist.Location = new System.Drawing.Point(234, 17);
            this.btnAddPlaylist.Name = "btnAddPlaylist";
            this.btnAddPlaylist.Size = new System.Drawing.Size(184, 40);
            this.btnAddPlaylist.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAddPlaylist.TabIndex = 1;
            this.btnAddPlaylist.TabStop = false;
            this.btnAddPlaylist.Text = "Thêm playlist";
            this.btnAddPlaylist.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddPlaylist.Click += new System.EventHandler(this.btnAddPlaylist_Click);
            // 
            // btnAddSong
            // 
            this.btnAddSong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddSong.BackColor = System.Drawing.Color.Transparent;
            this.btnAddSong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddSong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddSong.Font = new System.Drawing.Font("Lora Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnAddSong.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSong.Image")));
            this.btnAddSong.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnAddSong.ImageHover")));
            this.btnAddSong.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnAddSong.ImagePress")));
            this.btnAddSong.ImageSelected = ((System.Drawing.Image)(resources.GetObject("btnAddSong.ImageSelected")));
            this.btnAddSong.Location = new System.Drawing.Point(17, 17);
            this.btnAddSong.Name = "btnAddSong";
            this.btnAddSong.Size = new System.Drawing.Size(183, 40);
            this.btnAddSong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAddSong.TabIndex = 0;
            this.btnAddSong.TabStop = false;
            this.btnAddSong.Text = "Thêm bài hát";
            this.btnAddSong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddSong.Click += new System.EventHandler(this.btnAddSong_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel2.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel2.Location = new System.Drawing.Point(12, 303);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(84, 30);
            this.uiLabel2.TabIndex = 2;
            this.uiLabel2.Text = "Playlist";
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel1.Font = new System.Drawing.Font("Lora Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel1.Location = new System.Drawing.Point(12, 4);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(84, 30);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "Bài hát";
            // 
            // FormHome
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(973, 665);
            this.Controls.Add(this.uiPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHome";
            this.PageIndex = 0;
            this.Tag = "1001";
            this.Text = "Trang chủ";
            this.Load += new System.EventHandler(this.FormHome_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel1.PerformLayout();
            this.pnlRightDetails.ResumeLayout(false);
            this.pnlPlaylistDetails.ResumeLayout(false);
            this.pnlSongDetails.ResumeLayout(false);
            this.uiNavBar1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPlaylist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddSong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UINavBar uiNavBar1;
        private Sunny.UI.UIPanel pnlSongDetails;
        private Sunny.UI.UIAvatar detailAvatar;
        private Sunny.UI.UILabel detailLblArtist;
        private Sunny.UI.UILabel detailLblSongName;
        private Sunny.UI.UILabel detailLblDuration;
        private Sunny.UI.UIImageButton btnAddSong;
        private Sunny.UI.UIImageButton btnDelete;
        private Sunny.UI.UIImageButton btnEdit;
        private Sunny.UI.UIImageButton btnAddPlaylist;
        private Sunny.UI.UIPanel pnlRightDetails;
        private Sunny.UI.UIButton btnPlaySong;
        private Sunny.UI.UIPanel pnlPlaylistDetails;
        private Sunny.UI.UIButton btnPlayPlaylist;
        private Sunny.UI.UILabel playlistDetailLblName;
        private Sunny.UI.UIAvatar uiAvatar1;
        private Sunny.UI.UIListBox lstPlaylistSongsDetail;
        private System.Windows.Forms.FlowLayoutPanel flpPlaylists;
        private System.Windows.Forms.FlowLayoutPanel flpSongs;
    }
}

