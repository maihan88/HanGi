namespace HaNgi
{
    partial class FormEditSong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditSong));
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.avatarPreview = new Sunny.UI.UIAvatar();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.btnOpenCoverFolder = new Sunny.UI.UISymbolButton();
            this.btnOpenMusicFolder = new Sunny.UI.UISymbolButton();
            this.txtFilePath = new Sunny.UI.UIRichTextBox();
            this.btnCancel = new Sunny.UI.UIButton();
            this.btnSave = new Sunny.UI.UIButton();
            this.btnSelectFile = new Sunny.UI.UIButton();
            this.btnSelectCover = new Sunny.UI.UIButton();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.txtArtist = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.txtName = new Sunny.UI.UITextBox();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.btnImportLyric = new Sunny.UI.UIButton();
            this.txtLyric = new Sunny.UI.UIRichTextBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiToolTip1 = new Sunny.UI.UIToolTip(this.components);
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            resources.ApplyResources(this.uiLabel1, "uiLabel1");
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel1.Name = "uiLabel1";
            this.uiToolTip1.SetToolTip(this.uiLabel1, resources.GetString("uiLabel1.ToolTip"));
            // 
            // avatarPreview
            // 
            resources.ApplyResources(this.avatarPreview, "avatarPreview");
            this.avatarPreview.AvatarSize = 150;
            this.avatarPreview.BackColor = System.Drawing.Color.Transparent;
            this.avatarPreview.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.avatarPreview.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.avatarPreview.Name = "avatarPreview";
            this.avatarPreview.Shape = Sunny.UI.UIShape.Square;
            this.avatarPreview.Symbol = 558373;
            this.avatarPreview.SymbolSize = 50;
            this.uiToolTip1.SetToolTip(this.avatarPreview, resources.GetString("avatarPreview.ToolTip"));
            // 
            // uiPanel1
            // 
            resources.ApplyResources(this.uiPanel1, "uiPanel1");
            this.uiPanel1.Controls.Add(this.btnOpenCoverFolder);
            this.uiPanel1.Controls.Add(this.btnOpenMusicFolder);
            this.uiPanel1.Controls.Add(this.txtFilePath);
            this.uiPanel1.Controls.Add(this.btnCancel);
            this.uiPanel1.Controls.Add(this.btnSave);
            this.uiPanel1.Controls.Add(this.btnSelectFile);
            this.uiPanel1.Controls.Add(this.btnSelectCover);
            this.uiPanel1.Controls.Add(this.uiLabel3);
            this.uiPanel1.Controls.Add(this.txtArtist);
            this.uiPanel1.Controls.Add(this.uiLabel2);
            this.uiPanel1.Controls.Add(this.txtName);
            this.uiPanel1.Controls.Add(this.avatarPreview);
            this.uiPanel1.Controls.Add(this.uiLabel1);
            this.uiPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiToolTip1.SetToolTip(this.uiPanel1, resources.GetString("uiPanel1.ToolTip"));
            // 
            // btnOpenCoverFolder
            // 
            resources.ApplyResources(this.btnOpenCoverFolder, "btnOpenCoverFolder");
            this.btnOpenCoverFolder.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenCoverFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenCoverFolder.FillColor = System.Drawing.Color.Transparent;
            this.btnOpenCoverFolder.ForeDisableColor = System.Drawing.Color.Transparent;
            this.btnOpenCoverFolder.Name = "btnOpenCoverFolder";
            this.btnOpenCoverFolder.RectColor = System.Drawing.Color.Transparent;
            this.btnOpenCoverFolder.ShowTips = true;
            this.btnOpenCoverFolder.Symbol = 61564;
            this.btnOpenCoverFolder.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnOpenCoverFolder.TipsColor = System.Drawing.Color.Transparent;
            this.btnOpenCoverFolder.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnOpenCoverFolder.TipsText = "Mở thư mục chứa ảnh bìa";
            this.uiToolTip1.SetToolTip(this.btnOpenCoverFolder, resources.GetString("btnOpenCoverFolder.ToolTip"));
            // 
            // btnOpenMusicFolder
            // 
            resources.ApplyResources(this.btnOpenMusicFolder, "btnOpenMusicFolder");
            this.btnOpenMusicFolder.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenMusicFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenMusicFolder.FillColor = System.Drawing.Color.Transparent;
            this.btnOpenMusicFolder.Name = "btnOpenMusicFolder";
            this.btnOpenMusicFolder.RectColor = System.Drawing.Color.Transparent;
            this.btnOpenMusicFolder.ShowTips = true;
            this.btnOpenMusicFolder.Symbol = 61564;
            this.btnOpenMusicFolder.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnOpenMusicFolder.TipsColor = System.Drawing.Color.Transparent;
            this.btnOpenMusicFolder.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnOpenMusicFolder.TipsForeColor = System.Drawing.Color.Black;
            this.btnOpenMusicFolder.TipsText = "Mở thư mục chứa file nhạc";
            this.uiToolTip1.SetToolTip(this.btnOpenMusicFolder, resources.GetString("btnOpenMusicFolder.ToolTip"));
            // 
            // txtFilePath
            // 
            resources.ApplyResources(this.txtFilePath, "txtFilePath");
            this.txtFilePath.BackColor = System.Drawing.Color.Transparent;
            this.txtFilePath.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.txtFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.txtFilePath.ShowText = false;
            this.txtFilePath.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiToolTip1.SetToolTip(this.txtFilePath, resources.GetString("txtFilePath.ToolTip"));
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Radius = 50;
            this.btnCancel.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiToolTip1.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnSave.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127)))));
            this.btnSave.FillColorGradient = true;
            this.btnSave.FillColorGradientDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.btnSave.Name = "btnSave";
            this.btnSave.Radius = 50;
            this.btnSave.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiToolTip1.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelectFile
            // 
            resources.ApplyResources(this.btnSelectFile, "btnSelectFile");
            this.btnSelectFile.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectFile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnSelectFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Radius = 25;
            this.btnSelectFile.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiToolTip1.SetToolTip(this.btnSelectFile, resources.GetString("btnSelectFile.ToolTip"));
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnSelectCover
            // 
            resources.ApplyResources(this.btnSelectCover, "btnSelectCover");
            this.btnSelectCover.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectCover.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnSelectCover.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSelectCover.Name = "btnSelectCover";
            this.btnSelectCover.Radius = 25;
            this.btnSelectCover.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiToolTip1.SetToolTip(this.btnSelectCover, resources.GetString("btnSelectCover.ToolTip"));
            this.btnSelectCover.Click += new System.EventHandler(this.btnSelectCover_Click);
            // 
            // uiLabel3
            // 
            resources.ApplyResources(this.uiLabel3, "uiLabel3");
            this.uiLabel3.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel3.Name = "uiLabel3";
            this.uiToolTip1.SetToolTip(this.uiLabel3, resources.GetString("uiLabel3.ToolTip"));
            // 
            // txtArtist
            // 
            resources.ApplyResources(this.txtArtist, "txtArtist");
            this.txtArtist.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtArtist.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.txtArtist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtArtist.Multiline = true;
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Radius = 20;
            this.txtArtist.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.txtArtist.ShowText = false;
            this.txtArtist.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiToolTip1.SetToolTip(this.txtArtist, resources.GetString("txtArtist.ToolTip"));
            this.txtArtist.Watermark = "";
            // 
            // uiLabel2
            // 
            resources.ApplyResources(this.uiLabel2, "uiLabel2");
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel2.Name = "uiLabel2";
            this.uiToolTip1.SetToolTip(this.uiLabel2, resources.GetString("uiLabel2.ToolTip"));
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Radius = 20;
            this.txtName.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.txtName.ShowText = false;
            this.txtName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiToolTip1.SetToolTip(this.txtName, resources.GetString("txtName.ToolTip"));
            this.txtName.Watermark = "";
            // 
            // uiPanel2
            // 
            resources.ApplyResources(this.uiPanel2, "uiPanel2");
            this.uiPanel2.Controls.Add(this.btnImportLyric);
            this.uiPanel2.Controls.Add(this.txtLyric);
            this.uiPanel2.Controls.Add(this.uiLabel4);
            this.uiPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiToolTip1.SetToolTip(this.uiPanel2, resources.GetString("uiPanel2.ToolTip"));
            // 
            // btnImportLyric
            // 
            resources.ApplyResources(this.btnImportLyric, "btnImportLyric");
            this.btnImportLyric.BackColor = System.Drawing.Color.Transparent;
            this.btnImportLyric.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportLyric.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.btnImportLyric.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnImportLyric.Name = "btnImportLyric";
            this.btnImportLyric.Radius = 25;
            this.btnImportLyric.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiToolTip1.SetToolTip(this.btnImportLyric, resources.GetString("btnImportLyric.ToolTip"));
            this.btnImportLyric.Click += new System.EventHandler(this.btnImportLyric_Click);
            // 
            // txtLyric
            // 
            resources.ApplyResources(this.txtLyric, "txtLyric");
            this.txtLyric.BackColor = System.Drawing.Color.Transparent;
            this.txtLyric.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.txtLyric.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLyric.Name = "txtLyric";
            this.txtLyric.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(84)))));
            this.txtLyric.ShowText = false;
            this.txtLyric.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiToolTip1.SetToolTip(this.txtLyric, resources.GetString("txtLyric.ToolTip"));
            // 
            // uiLabel4
            // 
            resources.ApplyResources(this.uiLabel4, "uiLabel4");
            this.uiLabel4.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiLabel4.Name = "uiLabel4";
            this.uiToolTip1.SetToolTip(this.uiLabel4, resources.GetString("uiLabel4.ToolTip"));
            // 
            // uiToolTip1
            // 
            this.uiToolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiToolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.uiToolTip1.OwnerDraw = true;
            // 
            // FormEditSong
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(56)))));
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiPanel1);
            this.Name = "FormEditSong";
            this.TitleFont = new System.Drawing.Font("Lora Medium", 12F, System.Drawing.FontStyle.Bold);
            this.uiToolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel1.PerformLayout();
            this.uiPanel2.ResumeLayout(false);
            this.uiPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIAvatar avatarPreview;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox txtName;
        private Sunny.UI.UITextBox txtArtist;
        private Sunny.UI.UIButton btnSelectCover;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIButton btnSelectFile;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIRichTextBox txtLyric;
        private Sunny.UI.UIButton btnImportLyric;
        private Sunny.UI.UIRichTextBox txtFilePath;
        private Sunny.UI.UISymbolButton btnOpenCoverFolder;
        private Sunny.UI.UISymbolButton btnOpenMusicFolder;
        private Sunny.UI.UIToolTip uiToolTip1;
    }
}