using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HaNgi
{
    internal class LrcParser
    {
        public static List<LyricLine> Parse(string lyricText)
        {
            var resultLines = new List<LyricLine>();
            if (string.IsNullOrWhiteSpace(lyricText))
            {
                return resultLines; // Trả về danh sách rỗng nếu không có lời
            }

            // Biểu thức chính quy để tìm các dòng có dạng [mm:ss.xx]
            var lrcRegex = new Regex(@"\[(\d{2}):(\d{2})\.(\d{2,3})\]");

            // *** LOGIC MỚI: KIỂM TRA ĐỊNH DẠNG ***
            // Nếu văn bản chứa các mốc thời gian (là định dạng LRC)
            if (lrcRegex.IsMatch(lyricText))
            {
                var regex = new Regex(@"\[(\d{2}):(\d{2})\.(\d{2,3})\](.*)");
                foreach (var line in lyricText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        int minutes = int.Parse(match.Groups[1].Value);
                        int seconds = int.Parse(match.Groups[2].Value);
                        int milliseconds = int.Parse(match.Groups[3].Value.PadRight(3, '0'));

                        var time = new TimeSpan(0, 0, minutes, seconds, milliseconds);
                        var text = match.Groups[4].Value.Trim();

                        resultLines.Add(new LyricLine { Time = time, Text = text });
                    }
                }
            }
            // Nếu văn bản không chứa timestamp (là văn bản thô)
            else
            {
                // Chia toàn bộ văn bản thành từng dòng
                foreach (var line in lyricText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Thêm mỗi dòng vào danh sách với thời gian là 0
                    // Lời bài hát sẽ hiển thị toàn bộ ngay từ đầu
                    resultLines.Add(new LyricLine { Time = TimeSpan.Zero, Text = line });
                }
            }

            return resultLines;
        }
    }
}
