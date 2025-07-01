namespace HaNgi
{
    partial class PlaylistCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlLeft = new Sunny.UI.UIPanel();
            this.avatarCover = new Sunny.UI.UIAvatar();
            this.pnlRight = new Sunny.UI.UIPanel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.lblPlaylistName = new Sunny.UI.UILabel();
            this.lblSongPreview = new Sunny.UI.UILabel();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.avatarCover);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLeft.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(250, 250);
            this.pnlLeft.TabIndex = 0;
            this.pnlLeft.Text = "uiPanel1";
            this.pnlLeft.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // avatarCover
            // 
            this.avatarCover.AvatarSize = 200;
            this.avatarCover.BackColor = System.Drawing.Color.Transparent;
            this.avatarCover.FillColor = System.Drawing.Color.DarkGray;
            this.avatarCover.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.avatarCover.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.avatarCover.Location = new System.Drawing.Point(0, 0);
            this.avatarCover.MinimumSize = new System.Drawing.Size(1, 1);
            this.avatarCover.Name = "avatarCover";
            this.avatarCover.Radius = 0;
            this.avatarCover.Shape = Sunny.UI.UIShape.Square;
            this.avatarCover.Size = new System.Drawing.Size(250, 250);
            this.avatarCover.TabIndex = 0;
            this.avatarCover.Text = "uiAvatar1";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.uiPanel2);
            this.pnlRight.Controls.Add(this.uiPanel1);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pnlRight.Location = new System.Drawing.Point(250, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlRight.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(350, 250);
            this.pnlRight.TabIndex = 1;
            this.pnlRight.Text = "uiPanel1";
            this.pnlRight.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.lblPlaylistName);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(350, 51);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = "uiPanel1";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.lblSongPreview);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(0, 51);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(350, 199);
            this.uiPanel2.TabIndex = 1;
            this.uiPanel2.Text = "uiPanel2";
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlaylistName
            // 
            this.lblPlaylistName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlaylistName.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylistName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPlaylistName.Location = new System.Drawing.Point(0, 0);
            this.lblPlaylistName.Name = "lblPlaylistName";
            this.lblPlaylistName.Size = new System.Drawing.Size(350, 51);
            this.lblPlaylistName.TabIndex = 0;
            this.lblPlaylistName.Text = "uiLabel1";
            this.lblPlaylistName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSongPreview
            // 
            this.lblSongPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSongPreview.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblSongPreview.Location = new System.Drawing.Point(0, 0);
            this.lblSongPreview.Name = "lblSongPreview";
            this.lblSongPreview.Size = new System.Drawing.Size(350, 199);
            this.lblSongPreview.TabIndex = 0;
            this.lblSongPreview.Text = "uiLabel2";
            // 
            // PlaylistCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Name = "PlaylistCard";
            this.Size = new System.Drawing.Size(600, 250);
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel pnlLeft;
        private Sunny.UI.UIAvatar avatarCover;
        private Sunny.UI.UIPanel pnlRight;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UILabel lblSongPreview;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UILabel lblPlaylistName;
    }
}
