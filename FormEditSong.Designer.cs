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
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.avatarPreview = new Sunny.UI.UIAvatar();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.txtCoverPath = new Sunny.UI.UITextBox();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.btnCancel = new Sunny.UI.UIButton();
            this.btnSave = new Sunny.UI.UIButton();
            this.btnSelectFile = new Sunny.UI.UIButton();
            this.txtFilePath = new Sunny.UI.UITextBox();
            this.btnSelectCover = new Sunny.UI.UIButton();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.txtArtist = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.txtName = new Sunny.UI.UITextBox();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.txtLyric = new Sunny.UI.UIRichTextBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.btnImportLyric = new Sunny.UI.UIButton();
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
            this.uiPanel1.Controls.Add(this.txtCoverPath);
            this.uiPanel1.Controls.Add(this.uiLabel5);
            this.uiPanel1.Controls.Add(this.btnCancel);
            this.uiPanel1.Controls.Add(this.btnSave);
            this.uiPanel1.Controls.Add(this.btnSelectFile);
            this.uiPanel1.Controls.Add(this.txtFilePath);
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
            // txtCoverPath
            // 
            this.txtCoverPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCoverPath.Enabled = false;
            this.txtCoverPath.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoverPath.Location = new System.Drawing.Point(38, 425);
            this.txtCoverPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCoverPath.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtCoverPath.Multiline = true;
            this.txtCoverPath.Name = "txtCoverPath";
            this.txtCoverPath.Padding = new System.Windows.Forms.Padding(5);
            this.txtCoverPath.Radius = 20;
            this.txtCoverPath.ReadOnly = true;
            this.txtCoverPath.ShowText = false;
            this.txtCoverPath.Size = new System.Drawing.Size(458, 37);
            this.txtCoverPath.TabIndex = 13;
            this.txtCoverPath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtCoverPath.Watermark = "";
            // 
            // uiLabel5
            // 
            this.uiLabel5.AutoSize = true;
            this.uiLabel5.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel5.Location = new System.Drawing.Point(33, 373);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(141, 28);
            this.uiLabel5.TabIndex = 12;
            this.uiLabel5.Text = "File ảnh bìa:";
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.Black;
            this.btnCancel.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(382, 515);
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
            this.btnSave.Location = new System.Drawing.Point(139, 515);
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
            this.btnSelectFile.Location = new System.Drawing.Point(527, 313);
            this.btnSelectFile.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Radius = 25;
            this.btnSelectFile.Size = new System.Drawing.Size(126, 37);
            this.btnSelectFile.TabIndex = 9;
            this.btnSelectFile.Text = "Chọn file";
            this.btnSelectFile.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(38, 313);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilePath.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtFilePath.Multiline = true;
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Padding = new System.Windows.Forms.Padding(5);
            this.txtFilePath.Radius = 20;
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.ShowText = false;
            this.txtFilePath.Size = new System.Drawing.Size(458, 37);
            this.txtFilePath.TabIndex = 8;
            this.txtFilePath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFilePath.Watermark = "";
            // 
            // btnSelectCover
            // 
            this.btnSelectCover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectCover.FillColor = System.Drawing.Color.Black;
            this.btnSelectCover.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectCover.Location = new System.Drawing.Point(38, 192);
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
        private Sunny.UI.UITextBox txtFilePath;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIRichTextBox txtLyric;
        private Sunny.UI.UITextBox txtCoverPath;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UIButton btnImportLyric;
    }
}