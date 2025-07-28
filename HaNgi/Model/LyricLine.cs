using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    public class LyricLine
    {
        public TimeSpan Time { get; set; }
        public string Text { get; set; }
        public int StartIndex { get; set; }
        public int Length { get; set; }
    }
}
