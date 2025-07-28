using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HaNgi
{
    /// <summary>
    /// Chịu trách nhiệm phân tích, quản lý và cung cấp thông tin về lời bài hát.
    /// Hỗ trợ cả lời bài hát có định dạng thời gian (LRC) và lời bài hát văn bản thuần túy.
    /// </summary>
    public class LyricManager
    {
        /// <summary>
        /// Danh sách các dòng lyric đã được phân tích. Mỗi phần tử là một đối tượng LyricLine.
        /// </summary>
        public List<LyricLine> ParsedLines { get; private set; }

        /// <summary>
        /// Cờ (flag) cho biết lời bài hát có chứa thông tin thời gian (timing) hay không.
        /// </summary>
        public bool IsTimedLyric { get; private set; }

        /// <summary>
        /// Hàm khởi tạo của LyricManager.
        /// </summary>
        public LyricManager()
        {
            ParsedLines = new List<LyricLine>();
            IsTimedLyric = false;
        }

        /// <summary>
        /// Phân tích một chuỗi văn bản lời bài hát.
        /// Tự động phát hiện xem đó là định dạng LRC hay văn bản thuần túy.
        /// </summary>
        /// <param name="lyricText">Toàn bộ nội dung lời bài hát dưới dạng một chuỗi.</param>
        public void Parse(string lyricText)
        {
            ParsedLines.Clear(); // Xóa kết quả phân tích cũ trước khi bắt đầu.

            // Kiểm tra xem chuỗi có null, rỗng, hoặc chỉ chứa khoảng trắng không.
            if (string.IsNullOrWhiteSpace(lyricText))
            {
                IsTimedLyric = false;
                return; // Không có gì để phân tích, kết thúc hàm.
            }

            // --- Sử dụng Biểu thức chính quy (Regular Expression - Regex) để phát hiện định dạng LRC ---
            // Regex này tìm kiếm các tag thời gian có dạng [mm:ss.xx] hoặc [mm:ss.xxx].
            // \[: ký tự mở ngoặc vuông.
            // (\d{2}): một nhóm (group 1) chứa đúng 2 chữ số (phút).
            // : : ký tự hai chấm.
            // (\d{2}): một nhóm (group 2) chứa đúng 2 chữ số (giây).
            // \.: ký tự dấu chấm.
            // (\d{2,3}): một nhóm (group 3) chứa từ 2 đến 3 chữ số (mili giây).
            // \]: ký tự đóng ngoặc vuông.
            var lrcRegex = new Regex(@"\[(\d{2}):(\d{2})\.(\d{2,3})\]");

            // Kiểm tra xem có bất kỳ dòng nào khớp với định dạng LRC không.
            if (lrcRegex.IsMatch(lyricText))
            {
                IsTimedLyric = true;
                // Regex này phức tạp hơn, nó bắt cả tag thời gian và phần nội dung lyric theo sau.
                // (.*): một nhóm (group 4) bắt tất cả các ký tự còn lại trên dòng.
                var regex = new Regex(@"\[(\d{2}):(\d{2})\.(\d{2,3})\](.*)");

                // Tách toàn bộ văn bản thành các dòng riêng lẻ, loại bỏ các dòng trống.
                foreach (var line in lyricText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var match = regex.Match(line);
                    if (match.Success) // Nếu dòng hiện tại khớp với mẫu LRC
                    {
                        // Lấy giá trị từ các nhóm (group) đã bắt được và chuyển đổi sang số.
                        int minutes = int.Parse(match.Groups[1].Value);
                        int seconds = int.Parse(match.Groups[2].Value);
                        // Đảm bảo mili giây luôn có 3 chữ số bằng cách thêm '0' vào bên phải nếu cần.
                        // Ví dụ: "45" -> "450".
                        int milliseconds = int.Parse(match.Groups[3].Value.PadRight(3, '0'));

                        // Tạo đối tượng TimeSpan từ các thành phần thời gian.
                        var time = new TimeSpan(0, 0, minutes, seconds, milliseconds);
                        // Lấy nội dung lyric và xóa khoảng trắng thừa.
                        var text = match.Groups[4].Value.Trim();

                        ParsedLines.Add(new LyricLine { Time = time, Text = text });
                    }
                }
            }
            else // Nếu không phải là lời bài hát có timing (LRC)
            {
                IsTimedLyric = false;
                // Chỉ cần tách thành các dòng và thêm vào danh sách.
                foreach (var line in lyricText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Thời gian được đặt là Zero vì không có timing.
                    ParsedLines.Add(new LyricLine { Time = TimeSpan.Zero, Text = line });
                }
            }
        }

        /// <summary>
        /// Tạo và trả về một chuỗi duy nhất chứa toàn bộ lời bài hát để hiển thị.
        /// Đồng thời, tính toán và cập nhật vị trí bắt đầu (StartIndex) và độ dài (Length) cho mỗi dòng.
        /// </summary>
        /// <returns>Toàn bộ lời bài hát dưới dạng một chuỗi, hoặc thông báo nếu không có lời.</returns>
        public string GetFullLyricText()
        {
            // Sử dụng LINQ's Any() để kiểm tra danh sách có rỗng không.
            if (!ParsedLines.Any()) return "Không có lời cho bài hát này.";

            // StringBuilder hiệu quả hơn việc cộng chuỗi liên tục vì nó không tạo ra các đối tượng chuỗi mới.
            var fullLyricsBuilder = new StringBuilder();
            foreach (var line in ParsedLines)
            {
                // Ghi lại vị trí bắt đầu của dòng này trong chuỗi lớn.
                line.StartIndex = fullLyricsBuilder.Length;
                // Nối dòng lyric và ký tự xuống dòng vào chuỗi lớn.
                fullLyricsBuilder.Append(line.Text + "\n");
                // Ghi lại độ dài của dòng lyric (không tính ký tự xuống dòng).
                line.Length = line.Text.Length;
            }
            return fullLyricsBuilder.ToString();
        }

        /// <summary>
        /// Tìm chỉ số (index) của dòng lyric tương ứng với thời gian phát hiện tại.
        /// </summary>
        /// <param name="currentTime">Thời gian hiện tại của bài hát.</param>
        /// <returns>Chỉ số của dòng lyric hiện tại, hoặc -1 nếu không tìm thấy.</returns>
        public int GetCurrentLineIndex(TimeSpan currentTime)
        {
            if (!IsTimedLyric || !ParsedLines.Any()) return -1;

            int newLyricIndex = -1;
            // Duyệt qua danh sách các dòng lyric (đã được sắp xếp theo thời gian).
            for (int i = 0; i < ParsedLines.Count; i++)
            {
                // Nếu thời gian hiện tại lớn hơn hoặc bằng thời gian của dòng lyric này...
                if (currentTime >= ParsedLines[i].Time)
                {
                    // ...thì đây có thể là dòng lyric hiện tại.
                    newLyricIndex = i;
                }
                else
                {
                    // Nếu thời gian hiện tại nhỏ hơn, có nghĩa là ta đã đi qua dòng lyric cần tìm.
                    // Vì danh sách đã được sắp xếp, ta có thể dừng vòng lặp sớm để tối ưu.
                    break;
                }
            }
            return newLyricIndex;
        }
    }
}
