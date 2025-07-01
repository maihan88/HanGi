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
            // Gọi hàm đệ quy để gán sự kiện cho tất cả control
            AttachClickEventToAllChildren(this);
        }

        // HÀM MỚI: Tự động gán sự kiện Click cho tất cả control con
        private void AttachClickEventToAllChildren(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Ngoại trừ UIListBox/UILabel hiển thị danh sách bài hát
                if (control.Name != "lblSongPreview")
                {
                    control.Click += (sender, e) => {
                        this.OnClick(e); // Kích hoạt sự kiện click của cha
                    };
                }
                // Nếu control này có con, tiếp tục đi sâu vào trong
                if (control.HasChildren)
                {
                    AttachClickEventToAllChildren(control);
                }
            }
        }

        // ... các hàm SetData và SetCoverImage của bạn giữ nguyên ...
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
                lblSongPreview.Text = "";
            }
        }

        public void SetCoverImage(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    using (Image originalImage = Image.FromFile(absolutePath))
                    {
                        avatarCover.Image = new Bitmap(originalImage);
                        avatarCover.FillColor = Color.Transparent;
                    }
                }
                catch
                {
                    avatarCover.Image = null;
                    avatarCover.Symbol = 61449;
                    avatarCover.FillColor = Color.Gainsboro;
                }
            }
            else
            {
                avatarCover.Image = null;
                avatarCover.Symbol = 61449;
                avatarCover.FillColor = Color.Gainsboro;
            }
        }

        private void lblPlaylistName_Click(object sender, EventArgs e)
        {

        }
    }
}
