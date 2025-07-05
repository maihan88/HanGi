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
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(203, 21);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(136, 28);
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.Text = "Tên bài hát:";
            // 
            // avatarPreview
            // 
            this.avatarPreview.AvatarSize = 150;
            this.avatarPreview.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.avatarPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.avatarPreview.FillColor = System.Drawing.Color.Transparent;
            this.avatarPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.avatarPreview.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.avatarPreview.Location = new System.Drawing.Point(26, 21);
            this.avatarPreview.MinimumSize = new System.Drawing.Size(1, 1);
            this.avatarPreview.Name = "avatarPreview";
            this.avatarPreview.Shape = Sunny.UI.UIShape.Square;
            this.avatarPreview.Size = new System.Drawing.Size(150, 150);
            this.avatarPreview.Symbol = 558373;
            this.avatarPreview.SymbolSize = 50;
            this.avatarPreview.TabIndex = 2;
            // 
            // uiPanel1
            // 
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
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(0, 35);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(685, 665);
            this.uiPanel1.TabIndex = 3;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpenCoverFolder
            // 
            this.btnOpenCoverFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenCoverFolder.FillColor = System.Drawing.Color.Transparent;
            this.btnOpenCoverFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnOpenCoverFolder.Location = new System.Drawing.Point(158, 192);
            this.btnOpenCoverFolder.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOpenCoverFolder.Name = "btnOpenCoverFolder";
            this.btnOpenCoverFolder.ShowTips = true;
            this.btnOpenCoverFolder.Size = new System.Drawing.Size(46, 35);
            this.btnOpenCoverFolder.Symbol = 61564;
            this.btnOpenCoverFolder.SymbolColor = System.Drawing.Color.Olive;
            this.btnOpenCoverFolder.TabIndex = 15;
            this.btnOpenCoverFolder.TipsColor = System.Drawing.Color.Transparent;
            this.btnOpenCoverFolder.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnOpenCoverFolder.TipsText = "Mở thư mục chứa ảnh bìa";
            // 
            // btnOpenMusicFolder
            // 
            this.btnOpenMusicFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenMusicFolder.FillColor = System.Drawing.Color.Transparent;
            this.btnOpenMusicFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnOpenMusicFolder.Location = new System.Drawing.Point(618, 315);
            this.btnOpenMusicFolder.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOpenMusicFolder.Name = "btnOpenMusicFolder";
            this.btnOpenMusicFolder.ShowTips = true;
            this.btnOpenMusicFolder.Size = new System.Drawing.Size(46, 35);
            this.btnOpenMusicFolder.Symbol = 61564;
            this.btnOpenMusicFolder.SymbolColor = System.Drawing.Color.Olive;
            this.btnOpenMusicFolder.TabIndex = 14;
            this.btnOpenMusicFolder.TipsColor = System.Drawing.Color.Transparent;
            this.btnOpenMusicFolder.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnOpenMusicFolder.TipsForeColor = System.Drawing.Color.Black;
            this.btnOpenMusicFolder.TipsText = "Mở thư mục chứa file nhạc";
            // 
            // txtFilePath
            // 
            this.txtFilePath.FillColor = System.Drawing.Color.White;
            this.txtFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtFilePath.Location = new System.Drawing.Point(38, 313);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilePath.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Padding = new System.Windows.Forms.Padding(2);
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.ShowText = false;
            this.txtFilePath.Size = new System.Drawing.Size(411, 37);
            this.txtFilePath.TabIndex = 13;
            this.txtFilePath.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.Black;
            this.btnCancel.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(377, 446);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Radius = 50;
            this.btnCancel.Size = new System.Drawing.Size(138, 50);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.Black;
            this.btnSave.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(134, 446);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Radius = 50;
            this.btnSave.Size = new System.Drawing.Size(138, 50);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu";
            this.btnSave.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectFile.FillColor = System.Drawing.Color.Black;
            this.btnSelectFile.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFile.Location = new System.Drawing.Point(484, 313);
            this.btnSelectFile.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Radius = 25;
            this.btnSelectFile.Size = new System.Drawing.Size(113, 37);
            this.btnSelectFile.TabIndex = 9;
            this.btnSelectFile.Text = "Chọn file";
            this.btnSelectFile.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnSelectCover
            // 
            this.btnSelectCover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectCover.FillColor = System.Drawing.Color.Black;
            this.btnSelectCover.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectCover.Location = new System.Drawing.Point(26, 192);
            this.btnSelectCover.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSelectCover.Name = "btnSelectCover";
            this.btnSelectCover.Radius = 25;
            this.btnSelectCover.Size = new System.Drawing.Size(126, 32);
            this.btnSelectCover.TabIndex = 7;
            this.btnSelectCover.Text = "Thêm ảnh";
            this.btnSelectCover.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSelectCover.Click += new System.EventHandler(this.btnSelectCover_Click);
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel3.Location = new System.Drawing.Point(33, 268);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(114, 28);
            this.uiLabel3.TabIndex = 6;
            this.uiLabel3.Text = "File nhạc:";
            // 
            // txtArtist
            // 
            this.txtArtist.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtArtist.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArtist.Location = new System.Drawing.Point(208, 134);
            this.txtArtist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtArtist.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtArtist.Multiline = true;
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Padding = new System.Windows.Forms.Padding(5);
            this.txtArtist.Radius = 20;
            this.txtArtist.ShowText = false;
            this.txtArtist.Size = new System.Drawing.Size(445, 37);
            this.txtArtist.TabIndex = 5;
            this.txtArtist.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtArtist.Watermark = "";
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(203, 96);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(98, 28);
            this.uiLabel2.TabIndex = 4;
            this.uiLabel2.Text = "Nghệ sĩ:";
            // 
            // txtName
            // 
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(208, 54);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Padding = new System.Windows.Forms.Padding(5);
            this.txtName.Radius = 20;
            this.txtName.ShowText = false;
            this.txtName.Size = new System.Drawing.Size(445, 37);
            this.txtName.TabIndex = 3;
            this.txtName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtName.Watermark = "";
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.btnImportLyric);
            this.uiPanel2.Controls.Add(this.txtLyric);
            this.uiPanel2.Controls.Add(this.uiLabel4);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(685, 35);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(488, 665);
            this.uiPanel2.TabIndex = 4;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnImportLyric
            // 
            this.btnImportLyric.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportLyric.FillColor = System.Drawing.Color.Black;
            this.btnImportLyric.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportLyric.Location = new System.Drawing.Point(169, 12);
            this.btnImportLyric.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnImportLyric.Name = "btnImportLyric";
            this.btnImportLyric.Radius = 25;
            this.btnImportLyric.Size = new System.Drawing.Size(126, 37);
            this.btnImportLyric.TabIndex = 10;
            this.btnImportLyric.Text = "Chọn file";
            this.btnImportLyric.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnImportLyric.Click += new System.EventHandler(this.btnImportLyric_Click);
            // 
            // txtLyric
            // 
            this.txtLyric.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtLyric.FillColor = System.Drawing.Color.White;
            this.txtLyric.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtLyric.Location = new System.Drawing.Point(21, 57);
            this.txtLyric.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLyric.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtLyric.Name = "txtLyric";
            this.txtLyric.Padding = new System.Windows.Forms.Padding(2);
            this.txtLyric.ShowText = false;
            this.txtLyric.Size = new System.Drawing.Size(446, 584);
            this.txtLyric.TabIndex = 3;
            this.txtLyric.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel4.Location = new System.Drawing.Point(16, 21);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(129, 28);
            this.uiLabel4.TabIndex = 2;
            this.uiLabel4.Text = "Lời bài hát:";
            // 
            // uiToolTip1
            // 
            this.uiToolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiToolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.uiToolTip1.OwnerDraw = true;
            // 
            // FormEditSong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1173, 700);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiPanel1);
            this.Name = "FormEditSong";
            this.Text = "FormEditSong";
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