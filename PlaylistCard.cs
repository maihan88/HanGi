using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            this.Click += (sender, e) => this.OnClick(e);
            foreach (Control control in this.Controls)
            {
                // Ngăn ListBox nhận sự kiện click để cả card được click
                if (!(control is Sunny.UI.UIListBox))
                {
                    control.Click += (sender, e) => this.OnClick(e);
                }
            }
        }

        /// <summary>
        /// Gán dữ liệu playlist và danh sách bài hát xem trước.
        /// </summary>
        public void SetData(int playlistId, string playlistName, List<string> songPreviews)
        {
            this.PlaylistID = playlistId;
            this.PlaylistName = playlistName;

            lblPlaylistName.Text = playlistName;

            // Hiển thị danh sách xem trước
            lstSongPreview.Items.Clear();
            if (songPreviews != null)
            {
                foreach (var songPreview in songPreviews)
                {
                    lstSongPreview.Items.Add(songPreview);
                }
            }
        }

        /// <summary>
        /// Gán ảnh bìa cho card.
        /// </summary>
        public void SetCoverImage(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    using (Image originalImage = Image.FromFile(absolutePath))
                    {
                        avatarCover.Image = new Bitmap(originalImage);
                    }
                }
                catch { avatarCover.Symbol = 61449; avatarCover.Image = null; }
            }
            else { avatarCover.Symbol = 61449; avatarCover.Image = null; }
        }
    }
}
