using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HaNgi
{
    public partial class PlaylistCard : UserControl
    {
        public int PlaylistID { get; set; }
        public string PlaylistName { get; private set; }
        public Sunny.UI.UIAvatar CoverAvatar => avatarCover;

        public PlaylistCard()
        {
            InitializeComponent();
            AttachClickEventToAllChildren(this);
        }

        private void AttachClickEventToAllChildren(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.Click += (sender, e) => { this.OnClick(e); };
                if (control.HasChildren)
                {
                    AttachClickEventToAllChildren(control);
                }
            }
        }

        public void SetData(int playlistId, string playlistName, List<string> songPreviews)
        {
            this.PlaylistID = playlistId;
            this.PlaylistName = playlistName;
            lblPlaylistName.Text = playlistName;
            if (songPreviews != null && songPreviews.Any())
            {
                lblSongPreview.Text = string.Join("\n", songPreviews);
            }
            else
            {
                lblSongPreview.Text = "Chưa có bài hát nào trong playlist này.";
            }
        }

        public void SetCoverImage(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(absolutePath);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        avatarCover.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    avatarCover.Image = null;
                    avatarCover.Symbol = 61449;
                }
            }
            else
            {
                avatarCover.Image = null;
                avatarCover.Symbol = 61449;
            }
        }
    }
}