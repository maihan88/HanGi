namespace HaNgi
{
    partial class FormPlayer
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
            this.components = new System.ComponentModel.Container();
            this.timer = new Sunny.UI.UIMillisecondTimer(this.components);
            this.pnlBottomControls = new Sunny.UI.UIPanel();
            this.trackVolume = new Sunny.UI.UITrackBar();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.btnSettings = new Sunny.UI.UISymbolButton();
            this.btnLoop = new Sunny.UI.UISymbolButton();
            this.btnNext = new Sunny.UI.UISymbolButton();
            this.btnPlayPause = new Sunny.UI.UISymbolButton();
            this.btnPrevious = new Sunny.UI.UISymbolButton();
            this.btnShuffle = new Sunny.UI.UISymbolButton();
            this.lblTotalTime = new Sunny.UI.UILabel();
            this.lblCurrentTime = new Sunny.UI.UILabel();
            this.trackProgress = new Sunny.UI.UITrackBar();
            this.pnlQueue = new Sunny.UI.UIPanel();
            this.lstNowPlayingQueue = new Sunny.UI.UIListBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.pnlRightInfo = new Sunny.UI.UIPanel();
            this.uiTabControl1 = new Sunny.UI.UITabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblInfoArtist = new Sunny.UI.UILabel();
            this.lblInfoSongName = new Sunny.UI.UILabel();
            this.avatarInfo = new Sunny.UI.UIAvatar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbLyrics = new Sunny.UI.UIRichTextBox();
            this.pnlCenterDisplay = new Sunny.UI.UIPanel();
            this.videoView = new LibVLCSharp.WinForms.VideoView();
            this.avatarCenterDisplay = new Sunny.UI.UIAvatar();
            this.pnlBottomControls.SuspendLayout();
            this.pnlQueue.SuspendLayout();
            this.pnlRightInfo.SuspendLayout();
            this.uiTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlCenterDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoView)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pnlBottomControls
            // 
            this.pnlBottomControls.Controls.Add(this.trackVolume);
            this.pnlBottomControls.Controls.Add(this.uiSymbolButton1);
            this.pnlBottomControls.Controls.Add(this.btnSettings);
            this.pnlBottomControls.Controls.Add(this.btnLoop);
            this.pnlBottomControls.Controls.Add(this.btnNext);
            this.pnlBottomControls.Controls.Add(this.btnPlayPause);
            this.pnlBottomControls.Controls.Add(this.btnPrevious);
            this.pnlBottomControls.Controls.Add(this.btnShuffle);
            this.pnlBottomControls.Controls.Add(this.lblTotalTime);
            this.pnlBottomControls.Controls.Add(this.lblCurrentTime);
            this.pnlBottomControls.Controls.Add(this.trackProgress);
            this.pnlBottomControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomControls.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.pnlBottomControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlBottomControls.Location = new System.Drawing.Point(0, 476);
            this.pnlBottomControls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlBottomControls.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlBottomControls.Name = "pnlBottomControls";
            this.pnlBottomControls.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.pnlBottomControls.Size = new System.Drawing.Size(973, 189);
            this.pnlBottomControls.TabIndex = 1;
            this.pnlBottomControls.Text = null;
            this.pnlBottomControls.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackVolume
            // 
            this.trackVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.trackVolume.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(61)))));
            this.trackVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackVolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.trackVolume.Location = new System.Drawing.Point(61, 8);
            this.trackVolume.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackVolume.Name = "trackVolume";
            this.trackVolume.Size = new System.Drawing.Size(113, 29);
            this.trackVolume.TabIndex = 10;
            this.trackVolume.Text = "uiTrackBar1";
            this.trackVolume.ValueChanged += new System.EventHandler(this.trackVolume_ValueChanged);
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiSymbolButton1.BackColor = System.Drawing.Color.Transparent;
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.FillColor = System.Drawing.Color.Transparent;
            this.uiSymbolButton1.FillColor2 = System.Drawing.Color.Transparent;
            this.uiSymbolButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiSymbolButton1.ForeColor = System.Drawing.Color.Black;
            this.uiSymbolButton1.Location = new System.Drawing.Point(5, 3);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.RectColor = System.Drawing.Color.Transparent;
            this.uiSymbolButton1.Size = new System.Drawing.Size(50, 50);
            this.uiSymbolButton1.Symbol = 57449;
            this.uiSymbolButton1.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiSymbolButton1.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.uiSymbolButton1.SymbolSize = 35;
            this.uiSymbolButton1.TabIndex = 9;
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.FillColor = System.Drawing.Color.Transparent;
            this.btnSettings.FillColor2 = System.Drawing.Color.Transparent;
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSettings.ForeColor = System.Drawing.Color.Black;
            this.btnSettings.Location = new System.Drawing.Point(911, 127);
            this.btnSettings.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.RectColor = System.Drawing.Color.Transparent;
            this.btnSettings.Size = new System.Drawing.Size(50, 50);
            this.btnSettings.Symbol = 61459;
            this.btnSettings.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSettings.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnSettings.SymbolSize = 35;
            this.btnSettings.TabIndex = 8;
            this.btnSettings.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnLoop
            // 
            this.btnLoop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLoop.BackColor = System.Drawing.Color.Transparent;
            this.btnLoop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoop.FillColor = System.Drawing.Color.Transparent;
            this.btnLoop.FillColor2 = System.Drawing.Color.Transparent;
            this.btnLoop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLoop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnLoop.Location = new System.Drawing.Point(724, 94);
            this.btnLoop.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLoop.Name = "btnLoop";
            this.btnLoop.RectColor = System.Drawing.Color.Transparent;
            this.btnLoop.Size = new System.Drawing.Size(50, 50);
            this.btnLoop.Symbol = 557408;
            this.btnLoop.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnLoop.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnLoop.SymbolSize = 35;
            this.btnLoop.TabIndex = 7;
            this.btnLoop.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnLoop.Click += new System.EventHandler(this.btnLoop_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FillColor = System.Drawing.Color.Transparent;
            this.btnNext.FillColor2 = System.Drawing.Color.Transparent;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(603, 94);
            this.btnNext.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.RectColor = System.Drawing.Color.Transparent;
            this.btnNext.Size = new System.Drawing.Size(50, 50);
            this.btnNext.Symbol = 61518;
            this.btnNext.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNext.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnNext.SymbolSize = 35;
            this.btnNext.TabIndex = 6;
            this.btnNext.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPlayPause.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayPause.FillColor = System.Drawing.Color.Transparent;
            this.btnPlayPause.FillColor2 = System.Drawing.Color.Transparent;
            this.btnPlayPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnPlayPause.ForeColor = System.Drawing.Color.Black;
            this.btnPlayPause.Location = new System.Drawing.Point(478, 94);
            this.btnPlayPause.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.RectColor = System.Drawing.Color.Transparent;
            this.btnPlayPause.Size = new System.Drawing.Size(50, 50);
            this.btnPlayPause.Symbol = 61515;
            this.btnPlayPause.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPlayPause.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnPlayPause.SymbolSize = 35;
            this.btnPlayPause.TabIndex = 5;
            this.btnPlayPause.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.FillColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FillColor2 = System.Drawing.Color.Transparent;
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnPrevious.ForeColor = System.Drawing.Color.Black;
            this.btnPrevious.Location = new System.Drawing.Point(340, 94);
            this.btnPrevious.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.RectColor = System.Drawing.Color.Transparent;
            this.btnPrevious.Size = new System.Drawing.Size(50, 50);
            this.btnPrevious.Symbol = 61514;
            this.btnPrevious.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPrevious.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnPrevious.SymbolSize = 35;
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnShuffle
            // 
            this.btnShuffle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnShuffle.BackColor = System.Drawing.Color.Transparent;
            this.btnShuffle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShuffle.FillColor = System.Drawing.Color.Transparent;
            this.btnShuffle.FillColor2 = System.Drawing.Color.Transparent;
            this.btnShuffle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnShuffle.ForeColor = System.Drawing.Color.Black;
            this.btnShuffle.Location = new System.Drawing.Point(213, 94);
            this.btnShuffle.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.RectColor = System.Drawing.Color.Transparent;
            this.btnShuffle.Size = new System.Drawing.Size(50, 50);
            this.btnShuffle.Symbol = 361556;
            this.btnShuffle.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnShuffle.SymbolHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnShuffle.SymbolSize = 35;
            this.btnShuffle.TabIndex = 3;
            this.btnShuffle.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalTime.Font = new System.Drawing.Font("Lora Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTotalTime.Location = new System.Drawing.Point(722, 23);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(75, 30);
            this.lblTotalTime.TabIndex = 2;
            this.lblTotalTime.Text = "00:00";
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentTime.Font = new System.Drawing.Font("Lora Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCurrentTime.Location = new System.Drawing.Point(195, 24);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(75, 30);
            this.lblCurrentTime.TabIndex = 1;
            this.lblCurrentTime.Text = "00:00";
            // 
            // trackProgress
            // 
            this.trackProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackProgress.BackColor = System.Drawing.Color.Transparent;
            this.trackProgress.FillColor = System.Drawing.Color.DarkSlateBlue;
            this.trackProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.trackProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.trackProgress.Location = new System.Drawing.Point(269, 23);
            this.trackProgress.MinimumSize = new System.Drawing.Size(1, 1);
            this.trackProgress.Name = "trackProgress";
            this.trackProgress.Size = new System.Drawing.Size(447, 29);
            this.trackProgress.TabIndex = 0;
            this.trackProgress.Text = "uiTrackBar1";
            // 
            // pnlQueue
            // 
            this.pnlQueue.Controls.Add(this.lstNowPlayingQueue);
            this.pnlQueue.Controls.Add(this.uiLabel1);
            this.pnlQueue.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlQueue.FillColor = System.Drawing.Color.Transparent;
            this.pnlQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlQueue.Location = new System.Drawing.Point(0, 0);
            this.pnlQueue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlQueue.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlQueue.Name = "pnlQueue";
            this.pnlQueue.RectColor = System.Drawing.Color.Transparent;
            this.pnlQueue.Size = new System.Drawing.Size(239, 476);
            this.pnlQueue.TabIndex = 2;
            this.pnlQueue.Text = "uiPanel1";
            this.pnlQueue.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlQueue.Visible = false;
            // 
            // lstNowPlayingQueue
            // 
            this.lstNowPlayingQueue.BackColor = System.Drawing.Color.Transparent;
            this.lstNowPlayingQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNowPlayingQueue.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.lstNowPlayingQueue.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstNowPlayingQueue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lstNowPlayingQueue.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.lstNowPlayingQueue.ItemSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.lstNowPlayingQueue.ItemSelectForeColor = System.Drawing.Color.White;
            this.lstNowPlayingQueue.Location = new System.Drawing.Point(0, 31);
            this.lstNowPlayingQueue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstNowPlayingQueue.MinimumSize = new System.Drawing.Size(1, 1);
            this.lstNowPlayingQueue.Name = "lstNowPlayingQueue";
            this.lstNowPlayingQueue.Padding = new System.Windows.Forms.Padding(2);
            this.lstNowPlayingQueue.ShowText = false;
            this.lstNowPlayingQueue.Size = new System.Drawing.Size(239, 445);
            this.lstNowPlayingQueue.TabIndex = 1;
            this.lstNowPlayingQueue.Text = "uiListBox1";
            this.lstNowPlayingQueue.DoubleClick += new System.EventHandler(this.lstNowPlayingQueue_DoubleClick);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiLabel1.Font = new System.Drawing.Font("Lora", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel1.Location = new System.Drawing.Point(0, 0);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(239, 31);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "Danh sách phát";
            // 
            // pnlRightInfo
            // 
            this.pnlRightInfo.Controls.Add(this.uiTabControl1);
            this.pnlRightInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRightInfo.FillColor = System.Drawing.Color.Transparent;
            this.pnlRightInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlRightInfo.Location = new System.Drawing.Point(724, 0);
            this.pnlRightInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlRightInfo.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlRightInfo.Name = "pnlRightInfo";
            this.pnlRightInfo.RectColor = System.Drawing.Color.Transparent;
            this.pnlRightInfo.Size = new System.Drawing.Size(249, 476);
            this.pnlRightInfo.TabIndex = 3;
            this.pnlRightInfo.Text = "uiPanel1";
            this.pnlRightInfo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiTabControl1
            // 
            this.uiTabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.uiTabControl1.Controls.Add(this.tabPage1);
            this.uiTabControl1.Controls.Add(this.tabPage2);
            this.uiTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControl1.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTabControl1.ItemSize = new System.Drawing.Size(123, 40);
            this.uiTabControl1.Location = new System.Drawing.Point(0, 0);
            this.uiTabControl1.MainPage = "";
            this.uiTabControl1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiTabControl1.Name = "uiTabControl1";
            this.uiTabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiTabControl1.SelectedIndex = 0;
            this.uiTabControl1.Size = new System.Drawing.Size(249, 476);
            this.uiTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControl1.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.uiTabControl1.TabIndex = 0;
            this.uiTabControl1.TabPageTextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.uiTabControl1.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.uiTabControl1.TabSelectedForeColor = System.Drawing.Color.White;
            this.uiTabControl1.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.tabPage1.Controls.Add(this.lblInfoArtist);
            this.tabPage1.Controls.Add(this.lblInfoSongName);
            this.tabPage1.Controls.Add(this.avatarInfo);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(249, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông tin";
            // 
            // lblInfoArtist
            // 
            this.lblInfoArtist.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfoArtist.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoArtist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblInfoArtist.Location = new System.Drawing.Point(0, 284);
            this.lblInfoArtist.Name = "lblInfoArtist";
            this.lblInfoArtist.Size = new System.Drawing.Size(249, 52);
            this.lblInfoArtist.TabIndex = 2;
            this.lblInfoArtist.Text = "Nghệ sĩ";
            this.lblInfoArtist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInfoSongName
            // 
            this.lblInfoSongName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfoSongName.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoSongName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblInfoSongName.Location = new System.Drawing.Point(0, 217);
            this.lblInfoSongName.Name = "lblInfoSongName";
            this.lblInfoSongName.Size = new System.Drawing.Size(249, 54);
            this.lblInfoSongName.TabIndex = 1;
            this.lblInfoSongName.Text = "Tên bài hát";
            this.lblInfoSongName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfoSongName.Click += new System.EventHandler(this.lblInfoSongName_Click);
            // 
            // avatarInfo
            // 
            this.avatarInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.avatarInfo.AvatarSize = 150;
            this.avatarInfo.BackColor = System.Drawing.Color.Transparent;
            this.avatarInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.avatarInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.avatarInfo.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.avatarInfo.Location = new System.Drawing.Point(52, 31);
            this.avatarInfo.MinimumSize = new System.Drawing.Size(1, 1);
            this.avatarInfo.Name = "avatarInfo";
            this.avatarInfo.Shape = Sunny.UI.UIShape.Square;
            this.avatarInfo.Size = new System.Drawing.Size(150, 150);
            this.avatarInfo.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.tabPage2.Controls.Add(this.rtbLyrics);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(200, 60);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Lời bài hát";
            // 
            // rtbLyrics
            // 
            this.rtbLyrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLyrics.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.rtbLyrics.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLyrics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.rtbLyrics.Location = new System.Drawing.Point(0, 0);
            this.rtbLyrics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtbLyrics.MinimumSize = new System.Drawing.Size(1, 1);
            this.rtbLyrics.Name = "rtbLyrics";
            this.rtbLyrics.Padding = new System.Windows.Forms.Padding(2);
            this.rtbLyrics.ReadOnly = true;
            this.rtbLyrics.ShowText = false;
            this.rtbLyrics.Size = new System.Drawing.Size(200, 60);
            this.rtbLyrics.TabIndex = 0;
            this.rtbLyrics.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCenterDisplay
            // 
            this.pnlCenterDisplay.BackColor = System.Drawing.Color.Transparent;
            this.pnlCenterDisplay.Controls.Add(this.videoView);
            this.pnlCenterDisplay.Controls.Add(this.avatarCenterDisplay);
            this.pnlCenterDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenterDisplay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.pnlCenterDisplay.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.pnlCenterDisplay.FillColorGradient = true;
            this.pnlCenterDisplay.FillColorGradientDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.pnlCenterDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlCenterDisplay.Location = new System.Drawing.Point(239, 0);
            this.pnlCenterDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlCenterDisplay.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlCenterDisplay.Name = "pnlCenterDisplay";
            this.pnlCenterDisplay.RectColor = System.Drawing.Color.Transparent;
            this.pnlCenterDisplay.Size = new System.Drawing.Size(485, 476);
            this.pnlCenterDisplay.TabIndex = 4;
            this.pnlCenterDisplay.Text = null;
            this.pnlCenterDisplay.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // videoView
            // 
            this.videoView.BackColor = System.Drawing.Color.Black;
            this.videoView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoView.Location = new System.Drawing.Point(0, 0);
            this.videoView.MediaPlayer = null;
            this.videoView.Name = "videoView";
            this.videoView.Size = new System.Drawing.Size(485, 476);
            this.videoView.TabIndex = 2;
            this.videoView.Text = "videoView1";
            // 
            // avatarCenterDisplay
            // 
            this.avatarCenterDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.avatarCenterDisplay.AvatarSize = 400;
            this.avatarCenterDisplay.BackColor = System.Drawing.Color.Transparent;
            this.avatarCenterDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.avatarCenterDisplay.FillColor = System.Drawing.Color.Transparent;
            this.avatarCenterDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.avatarCenterDisplay.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.avatarCenterDisplay.Location = new System.Drawing.Point(48, 36);
            this.avatarCenterDisplay.MinimumSize = new System.Drawing.Size(1, 1);
            this.avatarCenterDisplay.Name = "avatarCenterDisplay";
            this.avatarCenterDisplay.Size = new System.Drawing.Size(400, 400);
            this.avatarCenterDisplay.TabIndex = 1;
            this.avatarCenterDisplay.Text = "uiAvatar1";
            // 
            // FormPlayer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(973, 665);
            this.Controls.Add(this.pnlCenterDisplay);
            this.Controls.Add(this.pnlRightInfo);
            this.Controls.Add(this.pnlQueue);
            this.Controls.Add(this.pnlBottomControls);
            this.Name = "FormPlayer";
            this.Text = "FormPlayer";
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.Load += new System.EventHandler(this.FormPlayer_Load);
            this.pnlBottomControls.ResumeLayout(false);
            this.pnlBottomControls.PerformLayout();
            this.pnlQueue.ResumeLayout(false);
            this.pnlRightInfo.ResumeLayout(false);
            this.uiTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.pnlCenterDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.videoView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIMillisecondTimer timer;
        private Sunny.UI.UIPanel pnlBottomControls;
        private Sunny.UI.UITrackBar trackProgress;
        private Sunny.UI.UISymbolButton btnShuffle;
        private Sunny.UI.UILabel lblTotalTime;
        private Sunny.UI.UILabel lblCurrentTime;
        private Sunny.UI.UISymbolButton btnNext;
        private Sunny.UI.UISymbolButton btnPlayPause;
        private Sunny.UI.UISymbolButton btnPrevious;
        private Sunny.UI.UISymbolButton btnSettings;
        private Sunny.UI.UISymbolButton btnLoop;
        private Sunny.UI.UITrackBar trackVolume;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UIPanel pnlQueue;
        private Sunny.UI.UIListBox lstNowPlayingQueue;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIPanel pnlRightInfo;
        private Sunny.UI.UITabControl uiTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Sunny.UI.UILabel lblInfoArtist;
        private Sunny.UI.UILabel lblInfoSongName;
        private Sunny.UI.UIAvatar avatarInfo;
        private Sunny.UI.UIPanel pnlCenterDisplay;
        private Sunny.UI.UIAvatar avatarCenterDisplay;
        private LibVLCSharp.WinForms.VideoView videoView;
        private Sunny.UI.UIRichTextBox rtbLyrics;
    }
}