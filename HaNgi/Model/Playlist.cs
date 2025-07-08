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

        public List<Song> Songs { get; set; }

        public Playlist()
        {
            Songs = new List<Song>();
        }
    }
}
