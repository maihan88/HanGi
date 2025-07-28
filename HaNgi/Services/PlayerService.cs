using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    /// <summary>
    /// Cung cấp một cơ chế giao tiếp trung tâm để xử lý các yêu cầu phát nhạc từ khắp nơi trong ứng dụng.
    /// Đây là một lớp "tĩnh" (static), hoạt động như một cầu nối (service locator/event aggregator) đơn giản.
    /// Nó cho phép các form khác nhau (ví dụ: FormHome, FormSearch) gửi yêu cầu phát nhạc
    /// mà không cần phải có tham chiếu trực tiếp đến FormPlayer hay MainForm.
    /// </summary>
    internal class PlayerService
    {
        /// <summary>
        /// Một sự kiện (event) tĩnh (static). Bất kỳ lớp nào cũng có thể đăng ký lắng nghe sự kiện này.
        /// Khi được kích hoạt, nó sẽ thông báo cho tất cả các "người nghe" (ở đây là MainForm)
        /// rằng có một yêu cầu phát nhạc mới.
        ///
        /// `Action<List<Song>, int>` là một delegate định nghĩa một "chữ ký" hàm:
        /// một hàm nhận tham số đầu tiên là `List<Song>` và tham số thứ hai là `int`.
        /// </summary>
        public static event Action<List<Song>, int> PlayRequest;

        /// <summary>
        /// Phương thức tĩnh (static) mà các lớp khác sẽ gọi để gửi yêu cầu phát nhạc.
        /// Việc là phương thức tĩnh cho phép gọi trực tiếp từ tên lớp mà không cần tạo đối tượng: `PlayerService.RequestPlay(...)`.
        /// </summary>
        /// <param name="songsToPlay">Danh sách các bài hát cần được phát.</param>
        /// <param name="startIndex">Chỉ số của bài hát trong danh sách sẽ được phát đầu tiên (mặc định là 0).</param>
        public static void RequestPlay(List<Song> songsToPlay, int startIndex = 0)
        {
            // Kích hoạt sự kiện PlayRequest.
            // Toán tử `?.Invoke` (null-conditional operator) là một cách viết an toàn và ngắn gọn.
            // Nó kiểm tra xem `PlayRequest` có `null` hay không (tức là có ai đăng ký lắng nghe sự kiện này không).
            // Nếu có, nó sẽ gọi `Invoke` để kích hoạt sự kiện và truyền các tham số.
            // Nếu không có ai đăng ký, nó sẽ không làm gì cả và tránh được lỗi `NullReferenceException`.
            PlayRequest?.Invoke(songsToPlay, startIndex);
        }
    }
}
