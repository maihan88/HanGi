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
            this.uiSplitContainer1 = new Sunny.UI.UISplitContainer();
            this.avatarCover = new Sunny.UI.UIAvatar();
            this.lblPlaylistName = new Sunny.UI.UILabel();
            this.lstSongPreview = new Sunny.UI.UIListBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitContainer1)).BeginInit();
            this.uiSplitContainer1.Panel1.SuspendLayout();
            this.uiSplitContainer1.Panel2.SuspendLayout();
            this.uiSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiSplitContainer1
            // 
            this.uiSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSplitContainer1.IsSplitterFixed = true;
            this.uiSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.uiSplitContainer1.MinimumSize = new System.Drawing.Size(20, 20);
            this.uiSplitContainer1.Name = "uiSplitContainer1";
            // 
            // uiSplitContainer1.Panel1
            // 
            this.uiSplitContainer1.Panel1.Controls.Add(this.avatarCover);
            // 
            // uiSplitContainer1.Panel2
            // 
            this.uiSplitContainer1.Panel2.Controls.Add(this.lstSongPreview);
            this.uiSplitContainer1.Panel2.Controls.Add(this.lblPlaylistName);
            this.uiSplitContainer1.Size = new System.Drawing.Size(600, 250);
            this.uiSplitContainer1.SplitterDistance = 250;
            this.uiSplitContainer1.SplitterWidth = 11;
            this.uiSplitContainer1.TabIndex = 0;
            // 
            // avatarCover
            // 
            this.avatarCover.AvatarSize = 240;
            this.avatarCover.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.avatarCover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.avatarCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.avatarCover.FillColor = System.Drawing.Color.Transparent;
            this.avatarCover.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.avatarCover.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.avatarCover.Location = new System.Drawing.Point(0, 0);
            this.avatarCover.MinimumSize = new System.Drawing.Size(1, 1);
            this.avatarCover.Name = "avatarCover";
            this.avatarCover.Shape = Sunny.UI.UIShape.Square;
            this.avatarCover.Size = new System.Drawing.Size(250, 250);
            this.avatarCover.Symbol = 0;
            this.avatarCover.TabIndex = 0;
            // 
            // lblPlaylistName
            // 
            this.lblPlaylistName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlaylistName.Font = new System.Drawing.Font("Montserrat Medium", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylistName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPlaylistName.Location = new System.Drawing.Point(0, 0);
            this.lblPlaylistName.Name = "lblPlaylistName";
            this.lblPlaylistName.Size = new System.Drawing.Size(339, 42);
            this.lblPlaylistName.TabIndex = 0;
            this.lblPlaylistName.Text = "uiLabel1";
            // 
            // lstSongPreview
            // 
            this.lstSongPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSongPreview.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSongPreview.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lstSongPreview.ItemSelectForeColor = System.Drawing.Color.White;
            this.lstSongPreview.Location = new System.Drawing.Point(0, 42);
            this.lstSongPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstSongPreview.MinimumSize = new System.Drawing.Size(1, 1);
            this.lstSongPreview.Name = "lstSongPreview";
            this.lstSongPreview.Padding = new System.Windows.Forms.Padding(2);
            this.lstSongPreview.ShowText = false;
            this.lstSongPreview.Size = new System.Drawing.Size(339, 208);
            this.lstSongPreview.TabIndex = 1;
            this.lstSongPreview.Text = "uiListBox1";
            // 
            // PlaylistCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.uiSplitContainer1);
            this.Name = "PlaylistCard";
            this.Size = new System.Drawing.Size(600, 250);
            this.uiSplitContainer1.Panel1.ResumeLayout(false);
            this.uiSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitContainer1)).EndInit();
            this.uiSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UISplitContainer uiSplitContainer1;
        private Sunny.UI.UIAvatar avatarCover;
        private Sunny.UI.UILabel lblPlaylistName;
        private Sunny.UI.UIListBox lstSongPreview;
    }
}
