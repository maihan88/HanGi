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

        // Thêm một thuộc tính public để FormHome có thể truy cập UIAvatar
        public Sunny.UI.UIAvatar CoverAvatar => avatarCover;

        public SongCard()
        {
            InitializeComponent();
            // Gán sự kiện click cho tất cả control con để dễ dàng click
            this.Click += (sender, e) => this.OnClick(e);
            foreach (Control control in this.Controls)
            {
                control.Click += (sender, e) => this.OnClick(e);
            }
        }

        /// <summary>
        /// Gán dữ liệu bài hát và cập nhật giao diện.
        /// </summary>
        public void SetData(int songId, string title, string artist)
        {
            this.SongID = songId;
            this.Title = title;

            lblSongName.Text = title;
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
                catch { avatarCover.Symbol = 61442; avatarCover.Image = null; }
            }
            else { avatarCover.Symbol = 61442; avatarCover.Image = null; }
        }

        private void avatarCover_Click(object sender, EventArgs e)
        {

        }
    }
}
