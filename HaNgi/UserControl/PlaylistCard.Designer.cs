namespace HaNgi
{
    partial class PlaylistCard
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.avatarCover = new Sunny.UI.UIAvatar();
            this.lblSongPreview = new Sunny.UI.UILabel();
            this.lblPlaylistName = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // avatarCover
            // 
            this.avatarCover.AvatarSize = 150;
            this.avatarCover.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.avatarCover.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.avatarCover.Location = new System.Drawing.Point(5, 5);
            this.avatarCover.MinimumSize = new System.Drawing.Size(1, 1);
            this.avatarCover.Name = "avatarCover";
            this.avatarCover.Shape = Sunny.UI.UIShape.Square;
            this.avatarCover.Size = new System.Drawing.Size(150, 150);
            this.avatarCover.TabIndex = 0;
            this.avatarCover.Text = "uiAvatar1";
            // 
            // lblSongPreview
            // 
            this.lblSongPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSongPreview.Font = new System.Drawing.Font("Lora", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblSongPreview.Location = new System.Drawing.Point(165, 40);
            this.lblSongPreview.Name = "lblSongPreview";
            this.lblSongPreview.Size = new System.Drawing.Size(330, 115);
            this.lblSongPreview.TabIndex = 2;
            this.lblSongPreview.Text = "Song previews...";
            // 
            // lblPlaylistName
            // 
            this.lblPlaylistName.Font = new System.Drawing.Font("Lora", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylistName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblPlaylistName.Location = new System.Drawing.Point(165, 5);
            this.lblPlaylistName.Name = "lblPlaylistName";
            this.lblPlaylistName.Size = new System.Drawing.Size(330, 35);
            this.lblPlaylistName.TabIndex = 1;
            this.lblPlaylistName.Text = "Playlist Name";
            this.lblPlaylistName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlaylistCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(90)))));
            this.Controls.Add(this.lblSongPreview);
            this.Controls.Add(this.lblPlaylistName);
            this.Controls.Add(this.avatarCover);
            this.Name = "PlaylistCard";
            this.Size = new System.Drawing.Size(500, 160);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIAvatar avatarCover;
        private Sunny.UI.UILabel lblSongPreview;
        private Sunny.UI.UILabel lblPlaylistName;
    }
}