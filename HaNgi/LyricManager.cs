using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HaNgi
{
    /// <summary>
    /// Chịu trách nhiệm phân tích và quản lý lời bài hát.
    /// </summary>
    public class LyricManager
    {
        public List<LyricLine> ParsedLines { get; private set; }
        public bool IsTimedLyric { get; private set; }

        public LyricManager()
        {
            ParsedLines = new List<LyricLine>();
            IsTimedLyric = false;
        }

        public void Parse(string lyricText)
        {
            ParsedLines.Clear();
            if (string.IsNullOrWhiteSpace(lyricText))
            {
                IsTimedLyric = false;
                return;
            }

            // Regex để tìm các tag thời gian như [mm:ss.xx]
            var lrcRegex = new Regex(@"\[(\d{2}):(\d{2})\.(\d{2,3})\]");

            if (lrcRegex.IsMatch(lyricText))
            {
                IsTimedLyric = true;
                var regex = new Regex(@"\[(\d{2}):(\d{2})\.(\d{2,3})\](.*)");
                foreach (var line in lyricText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        int minutes = int.Parse(match.Groups[1].Value);
                        int seconds = int.Parse(match.Groups[2].Value);
                        // Đảm bảo milliseconds luôn có 3 chữ số
                        int milliseconds = int.Parse(match.Groups[3].Value.PadRight(3, '0'));

                        var time = new TimeSpan(0, 0, minutes, seconds, milliseconds);
                        var text = match.Groups[4].Value.Trim();

                        ParsedLines.Add(new LyricLine { Time = time, Text = text });
                    }
                }
            }
            else // Lời bài hát không có timing
            {
                IsTimedLyric = false;
                foreach (var line in lyricText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ParsedLines.Add(new LyricLine { Time = TimeSpan.Zero, Text = line });
                }
            }
        }

        public string GetFullLyricText()
        {
            if (!ParsedLines.Any()) return "Không có lời cho bài hát này.";

            var fullLyricsBuilder = new StringBuilder();
            foreach (var line in ParsedLines)
            {
                line.StartIndex = fullLyricsBuilder.Length;
                fullLyricsBuilder.Append(line.Text + "\n");
                line.Length = line.Text.Length;
            }
            return fullLyricsBuilder.ToString();
        }

        public int GetCurrentLineIndex(TimeSpan currentTime)
        {
            if (!IsTimedLyric || !ParsedLines.Any()) return -1;

            int newLyricIndex = -1;
            for (int i = 0; i < ParsedLines.Count; i++)
            {
                if (currentTime >= ParsedLines[i].Time)
                {
                    newLyricIndex = i;
                }
                else
                {
                    break;
                }
            }
            return newLyricIndex;
        }
    }
}
