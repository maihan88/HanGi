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

    public partial class SongCard : UserControl
    {
        public int SongID { get; set; }
        public string Title { get; private set; }

        public Sunny.UI.UIAvatar CoverAvatar => avatarCover;

        public SongCard()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Click += (sender, e) => {
                    this.OnClick(e);
                };
            }
        }

        public void SetData(int songId, string title, string artist)
        {
            this.SongID = songId;
            this.Title = title;

            lblSongName.Text = title;
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
                    avatarCover.Symbol = 61442;
                    avatarCover.Image = null;
                }
            }
            else
            {
                avatarCover.Symbol = 61442;
                avatarCover.Image = null;
            }
        }

        private void avatarCover_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void SongCard_MouseEnter(object sender, EventArgs e)
        {
            
        }
    }
}
