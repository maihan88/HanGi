using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace HaNgi
{
    public partial class FormSearch : Sunny.UI.UIPage
    {
        public FormSearch()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchQuery.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                UIMessageBox.ShowWarning("Vui lòng nhập từ khóa tìm kiếm.");
                return;
            }

            // Xóa kết quả cũ
            flpResults.Controls.Clear();

            // Tìm kiếm bài hát
            List<Song> songResults = DataAccess.SearchSongs(searchTerm);

            // Hiển thị kết quả tìm được
            if (songResults.Any())
            {
                foreach (var song in songResults)
                {
                    var card = new SongCard();
                    card.SetData(song.SongID, song.SongName, song.Artist);
                    card.SetCoverImage(song.CoverPath);

                    // Thêm sự kiện click cho mỗi card để có thể phát nhạc
                    card.Click += (s, ev) => {
                        PlayerService.RequestPlay(new List<Song> { song });
                    };

                    flpResults.Controls.Add(card);
                }
            }
            else
            {
                // Thông báo nếu không tìm thấy kết quả
                var lblNoResult = new UILabel();
                lblNoResult.Text = "Không tìm thấy kết quả nào.";
                lblNoResult.AutoSize = true;
                flpResults.Controls.Add(lblNoResult);
            }
        }

        private void FormSearch_Initialize(object sender, EventArgs e)
        {
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
        }
    }
}
