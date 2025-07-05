using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    internal class LyricLine
    {
        public TimeSpan Time { get; set; }
        public string Text { get; set; }
        // Thêm 2 thuộc tính mới để lưu vị trí trong RichTextBox
        public int StartIndex { get; set; }
        public int Length { get; set; }
    }
}
