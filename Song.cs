using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaNgi;

namespace HaNgi
{
    public class Song
    {
        public int SongID { get; set; }
        public string SongName { get; set; }
        public string Artist { get; set; }
        public int Duration { get; set; }
        public string FilePath { get; set; }
        public string CoverPath { get; set; }
        public string FullLyric { get; set; }
    }
}
