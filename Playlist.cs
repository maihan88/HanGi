using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    internal class Playlist
    {
        public int PlaylistID { get; set; }
        public string PlaylistName { get; set; }
        public string PlaylistImage { get; set; }

        // Một playlist sẽ chứa một danh sách các bài hát
        public List<Song> Songs { get; set; }

        public Playlist()
        {
            // Khởi tạo danh sách để tránh lỗi null
            Songs = new List<Song>();
        }
    }
}
