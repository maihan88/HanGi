using System;
using System.Collections.Generic;
using System.Linq;

namespace HaNgi
{
    /// <summary>
    /// Enum để định nghĩa các chế độ lặp lại.
    /// </summary>
    public enum LoopMode { None, LoopOne }

    /// <summary>
    /// Quản lý logic của danh sách phát (playlist).
    /// Chịu trách nhiệm tải danh sách, theo dõi bài hát hiện tại, chuyển bài (next/previous),
    /// và xử lý các chế độ như xáo trộn (shuffle) và lặp lại (loop).
    /// </summary>
    public class PlaylistManager
    {
        // Danh sách lưu trữ thứ tự gốc của các bài hát.
        private List<Song> _originalPlaylist = new List<Song>();
        // Chỉ số (index) của bài hát hiện tại trong danh sách.
        private int _currentIndex = -1;
        // Đối tượng để sinh số ngẫu nhiên cho chức năng shuffle.
        private Random _random = new Random();

        /// <summary>
        /// Cho biết chế độ xáo trộn có đang được bật hay không.
        /// </summary>
        public bool IsShuffled { get; private set; } = false;

        /// <summary>
        /// Chế độ lặp lại hiện tại.
        /// </summary>
        public LoopMode LoopMode { get; private set; } = LoopMode.None;

        /// <summary>
        /// Luôn trả về danh sách bài hát với thứ tự gốc.
        /// Dùng để hiển thị trên giao diện người dùng (UI) một cách nhất quán,
        /// ngay cả khi chế độ shuffle đang bật.
        /// `=>` là cách viết tắt cho một property chỉ có getter (expression-bodied property).
        /// </summary>
        public List<Song> DisplayPlaylist => _originalPlaylist;

        /// <summary>
        /// Kiểm tra xem danh sách phát có rỗng không.
        /// </summary>
        public bool IsPlaylistEmpty => !_originalPlaylist.Any();

        /// <summary>
        /// Lấy số lượng bài hát trong danh sách.
        /// </summary>
        public int PlaylistCount => _originalPlaylist.Count;

        /// <summary>
        /// Tải một danh sách bài hát mới vào trình quản lý.
        /// </summary>
        /// <param name="songs">Danh sách các đối tượng Song cần tải.</param>
        /// <param name="startIndex">Chỉ số của bài hát sẽ được phát đầu tiên.</param>
        public void LoadPlaylist(List<Song> songs, int startIndex = 0)
        {
            if (songs == null || !songs.Any())
            {
                // Nếu danh sách không hợp lệ hoặc rỗng, xóa sạch playlist hiện tại.
                _originalPlaylist.Clear();
                _currentIndex = -1;
                return;
            }
            // Tạo một bản sao của danh sách để tránh thay đổi danh sách gốc từ bên ngoài.
            _originalPlaylist = new List<Song>(songs);
            // Sử dụng toán tử ba ngôi để kiểm tra và gán _currentIndex một cách an toàn.
            // Điều kiện ? giá_trị_nếu_đúng : giá_trị_nếu_sai
            _currentIndex = startIndex >= 0 && startIndex < songs.Count ? startIndex : 0;
        }

        /// <summary>
        /// Lấy đối tượng Song hiện tại đang được trỏ tới bởi _currentIndex.
        /// </summary>
        /// <returns>Đối tượng Song hiện tại, hoặc null nếu có lỗi.</returns>
        public Song GetCurrentSong()
        {
            if (IsPlaylistEmpty || _currentIndex < 0 || _currentIndex >= _originalPlaylist.Count)
            {
                return null;
            }
            return _originalPlaylist[_currentIndex];
        }

        /// <summary>
        /// Chuyển đến bài hát tiếp theo trong danh sách.
        /// </summary>
        /// <returns>Đối tượng Song tiếp theo.</returns>
        public Song GoToNext()
        {
            if (IsPlaylistEmpty || PlaylistCount <= 1) return GetCurrentSong();

            if (IsShuffled)
            {
                // Logic cho chế độ xáo trộn:
                // Chọn một bài hát ngẫu nhiên khác với bài hiện tại để tránh lặp lại ngay lập tức.
                int newIndex;
                do
                {
                    newIndex = _random.Next(0, PlaylistCount); // Sinh số ngẫu nhiên từ 0 đến (số lượng - 1).
                } while (newIndex == _currentIndex); // Lặp lại nếu bài hát mới trùng với bài cũ.
                _currentIndex = newIndex;
            }
            else
            {
                // Logic cho chế độ phát tuần tự:
                _currentIndex++;
                if (_currentIndex >= PlaylistCount)
                {
                    _currentIndex = 0; // Quay về đầu danh sách nếu đã hết bài.
                }
            }
            return GetCurrentSong();
        }

        /// <summary>
        /// Chuyển về bài hát phía trước trong danh sách.
        /// </summary>
        /// <returns>Đối tượng Song phía trước.</returns>
        public Song GoToPrevious()
        {
            if (IsPlaylistEmpty || PlaylistCount <= 1) return GetCurrentSong();

            // Nút "Previous" luôn hoạt động tuần tự để người dùng dễ dàng điều hướng,
            // ngay cả khi đang ở chế độ shuffle.
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = PlaylistCount - 1; // Chuyển đến cuối danh sách nếu đang ở đầu.
            }
            return GetCurrentSong();
        }

        /// <summary>
        /// Chuyển đổi giữa các chế độ lặp lại (None <-> LoopOne).
        /// </summary>
        public void ToggleLoopMode()
        {
            // Sử dụng toán tử ba ngôi để chuyển đổi giá trị.
            LoopMode = (LoopMode == LoopMode.None) ? LoopMode.LoopOne : LoopMode.None;
        }

        /// <summary>
        /// Bật hoặc tắt chế độ xáo trộn (shuffle).
        /// </summary>
        public void ToggleShuffle()
        {
            IsShuffled = !IsShuffled;
        }

        /// <summary>
        /// Lấy chỉ số của bài hát hiện tại trong danh sách.
        /// </summary>
        public int GetCurrentSongIndex() => _currentIndex;
    }
}
