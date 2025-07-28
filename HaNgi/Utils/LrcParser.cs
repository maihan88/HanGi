using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HaNgi
{
    /// <summary>
    /// Lớp LrcParser chịu trách nhiệm phân tích (parse) văn bản lời bài hát.
    /// Nó có thể xử lý cả lời bài hát dạng văn bản thuần túy (không có mốc thời gian)
    /// và định dạng LRC tiêu chuẩn (có mốc thời gian dạng [phút:giây.mili giây]).
    /// </summary>
    internal class LrcParser
    {
        /// <summary>
        /// Phương thức chính để phân tích một chuỗi văn bản lời bài hát.
        /// Tự động phát hiện định dạng (có timestamp hay không) để xử lý cho phù hợp.
        /// </summary>
        /// <param name="lyricText">Chuỗi (string) chứa toàn bộ lời bài hát cần phân tích.</param>
        /// <returns>
        /// Một danh sách (List) các đối tượng LyricLine. Mỗi LyricLine chứa thời gian (Time)
        /// và nội dung lời (Text) tương ứng. Nếu là văn bản thuần túy, Time sẽ là TimeSpan.Zero.
        /// </returns>
        public static List<LyricLine> Parse(string lyricText)
        {
            // Khởi tạo một danh sách rỗng để chứa kết quả các dòng lời bài hát sau khi xử lý.
            var resultLines = new List<LyricLine>();

            // Kiểm tra xem chuỗi đầu vào có phải là null, rỗng, hoặc chỉ chứa khoảng trắng không.
            // Nếu đúng, trả về danh sách rỗng luôn để tránh lỗi và xử lý không cần thiết.
            if (string.IsNullOrWhiteSpace(lyricText))
                return resultLines;

            // Định nghĩa một Biểu thức chính quy (Regular Expression - Regex) để tìm kiếm các mốc thời gian (timestamp)
            // theo định dạng chuẩn của LRC.
            // Phân tích Regex: \[(\d{2}):(\d{2})\.(\d{2,3})\]
            // - \[ và \]: Tìm ký tự "[" và "]" thật. (Dấu \ để thoát ký tự đặc biệt)
            // - (\d{2}): Tìm và "bắt lấy" (capture) chính xác 2 chữ số (cho phút và giây). \d là chữ số, {2} là 2 lần.
            // - \.: Tìm ký tự "." thật.
            // - (\d{2,3}): Tìm và "bắt lấy" từ 2 đến 3 chữ số (cho mili giây).
            var lrcRegex = new Regex(@"\[(\d{2}):(\d{2})\.(\d{2,3})\]");

            // Sử dụng Regex vừa tạo để kiểm tra xem trong toàn bộ chuỗi lyricText có tồn tại ít nhất một mốc thời gian nào không.
            if (lrcRegex.IsMatch(lyricText))
            {
                // Nếu có, gọi phương thức chuyên xử lý định dạng LRC.
                ProcessLrcFormat(lyricText, resultLines);
            }
            else
            {
                // Nếu không tìm thấy bất kỳ mốc thời gian nào, coi nó là văn bản thuần túy.
                ProcessPlainText(lyricText, resultLines);
            }

            // Trả về danh sách kết quả đã được điền dữ liệu.
            return resultLines;
        }

        /// <summary>
        /// Xử lý lời bài hát có định dạng LRC (có chứa timestamp).
        /// </summary>
        /// <param name="lyricText">Chuỗi lời bài hát đầy đủ.</param>
        /// <param name="resultLines">Danh sách để thêm các dòng LyricLine đã xử lý vào.</param>
        private static void ProcessLrcFormat(string lyricText, List<LyricLine> resultLines)
        {
            // Regex này nâng cấp hơn regex ở trên một chút. Nó không chỉ tìm timestamp mà còn "bắt lấy"
            // phần nội dung lời bài hát theo sau timestamp.
            // Phân tích phần thêm vào: (.*)
            // - .: Đại diện cho bất kỳ ký tự nào (trừ ký tự xuống dòng).
            // - *: Lặp lại ký tự trước đó 0 hoặc nhiều lần.
            // -> (.*) sẽ "bắt lấy" tất cả mọi thứ cho đến cuối dòng.
            var regex = new Regex(@"\[(\d{2}):(\d{2})\.(\d{2,3})\](.*)");

            // Tách toàn bộ chuỗi lyricText thành một mảng các dòng riêng lẻ.
            // - new[] { '\r', '\n' }: Tách dòng dựa trên cả hai loại ký tự xuống dòng phổ biến (\r\n của Windows và \n của Unix/Mac).
            // - StringSplitOptions.RemoveEmptyEntries: Một tùy chọn rất hữu ích, nó sẽ tự động loại bỏ các dòng trống
            //   (ví dụ khi có 2 lần xuống dòng liên tiếp), giúp ta không phải xử lý chúng.
            foreach (var line in lyricText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                // Với mỗi dòng, thử áp dụng regex để xem nó có khớp với định dạng [time]text không.
                var match = regex.Match(line);

                // Nếu khớp (match.Success là true), tức là dòng này là một dòng lời hợp lệ.
                if (match.Success)
                {
                    // `match.Groups` chứa các phần đã được "bắt lấy" (nằm trong dấu ngoặc đơn `()`) từ regex.
                    // - Groups[0] là toàn bộ chuỗi khớp được (ví dụ: "[01:23.45]Hello world").
                    // - Groups[1] là nhóm bắt được đầu tiên (\d{2}) -> "01" (phút).
                    // - Groups[2] là nhóm bắt được thứ hai (\d{2}) -> "23" (giây).
                    // - Groups[3] là nhóm bắt được thứ ba (\d{2,3}) -> "45" (mili giây).
                    // - Groups[4] là nhóm bắt được thứ tư (.*) -> "Hello world".
                    var time = ParseTime(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value);

                    // Lấy nội dung lời và dùng .Trim() để xóa các khoảng trắng thừa ở đầu và cuối chuỗi.
                    var text = match.Groups[4].Value.Trim();

                    // Tạo đối tượng LyricLine mới và thêm vào danh sách kết quả.
                    resultLines.Add(new LyricLine { Time = time, Text = text });
                }
            }
        }

        /// <summary>
        /// Xử lý lời bài hát dạng văn bản thuần túy (không có timestamp).
        /// </summary>
        private static void ProcessPlainText(string lyricText, List<LyricLine> resultLines)
        {
            // Tương tự như trên, tách chuỗi thành các dòng.
            foreach (var line in lyricText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                // Vì không có thời gian, ta gán cho mỗi dòng một mốc thời gian mặc định là 0 (TimeSpan.Zero).
                // Và nội dung chính là cả dòng đó.
                resultLines.Add(new LyricLine { Time = TimeSpan.Zero, Text = line });
            }
        }

        /// <summary>
        /// Hàm tiện ích giúp chuyển đổi các thành phần thời gian (dưới dạng chuỗi) thành một đối tượng TimeSpan.
        /// TimeSpan là một cấu trúc của .NET để biểu diễn một khoảng thời gian.
        /// </summary>
        private static TimeSpan ParseTime(string minutes, string seconds, string milliseconds)
        {
            // Dùng int.Parse để chuyển chuỗi số (ví dụ: "59") thành số nguyên (integer: 59).
            int parsedMinutes = int.Parse(minutes);
            int parsedSeconds = int.Parse(seconds);

            // Xử lý mili giây:
            // Định dạng LRC có thể có 2 hoặc 3 chữ số cho mili giây (ví dụ: .95 hoặc .950).
            // Tuy nhiên, hàm tạo của TimeSpan cần giá trị mili giây đầy đủ (3 chữ số).
            // `PadRight(3, '0')` sẽ đảm bảo chuỗi luôn dài 3 ký tự.
            // - Nếu chuỗi là "95" (dài 2), nó sẽ thêm '0' vào bên phải -> "950".
            // - Nếu chuỗi là "951" (dài 3), nó sẽ giữ nguyên.
            int parsedMilliseconds = int.Parse(milliseconds.PadRight(3, '0'));

            // Tạo và trả về một đối tượng TimeSpan mới.
            // Các tham số lần lượt là: (days, hours, minutes, seconds, milliseconds).
            // Ta không cần ngày và giờ nên để là 0.
            return new TimeSpan(0, 0, parsedMinutes, parsedSeconds, parsedMilliseconds);
        }
    }
}
