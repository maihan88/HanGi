using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    /// <summary>
    /// Lớp tĩnh trung tâm, hoạt động như một "tổng đài"
    /// để nhận và điều phối các yêu cầu phát nhạc trong toàn bộ ứng dụng.
    /// </summary>
    internal class PlayerService
    {
        // Định nghĩa một sự kiện (event). 
        // Bất kỳ ai cũng có thể đăng ký lắng nghe sự kiện này.
        // Nó sẽ mang theo một danh sách bài hát và vị trí bắt đầu.
        public static event Action<List<Song>, int> PlayRequest;

        /// <summary>
        /// Phương thức để các form khác (như FormHome) gọi khi muốn phát nhạc.
        /// </summary>
        /// <param name="songsToPlay">Danh sách các bài hát cần phát.</param>
        /// <param name="startIndex">Vị trí của bài hát bắt đầu phát trong danh sách.</param>
        public static void RequestPlay(List<Song> songsToPlay, int startIndex = 0)
        {
            // Kích hoạt sự kiện PlayRequest.
            // Bất kỳ ai đã đăng ký lắng nghe (như FormPlayer) sẽ nhận được thông báo này.
            // Dấu ? để đảm bảo an toàn nếu không có ai lắng nghe.
            PlayRequest?.Invoke(songsToPlay, startIndex);
        }
    }
}
