using System;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using Sunny.UI;

namespace HaNgi
{
    /// <summary>
    /// Enum (kiểu liệt kê) tự định nghĩa để theo dõi và quản lý trạng thái của trình phát một cách rõ ràng.
    /// </summary>
    public enum PlayerState
    {
        Playing,    // Đang phát
        Paused,     // Đang tạm dừng
        Stopped,    // Đã dừng hẳn
        Error       // Gặp lỗi
    }

    /// <summary>
    /// Quản lý việc tương tác trực tiếp với MediaPlayer của thư viện LibVLCSharp.
    /// Đóng vai trò là một lớp "wrapper" (lớp bao bọc), che giấu sự phức tạp của thư viện VLC
    /// và cung cấp một giao diện (API) đơn giản và nhất quán cho phần còn lại của ứng dụng.
    /// Implement IDisposable để đảm bảo các tài nguyên không được quản lý (unmanaged) của VLC được giải phóng đúng cách.
    /// </summary>
    public class PlayerManager : IDisposable
    {
        // --- Các đối tượng cốt lõi của LibVLCSharp ---
        private LibVLC _libVLC;             // Đối tượng chính của thư viện VLC.
        private MediaPlayer _mediaPlayer;   // Đối tượng chịu trách nhiệm phát media.
        private Media _currentMedia;        // Đối tượng đại diện cho file media đang được xử lý.

        // --- Các sự kiện (Events) của lớp Manager ---
        // Lớp này "phát lại" các sự kiện từ _mediaPlayer để các lớp khác (như FormPlayer) có thể lắng nghe.

        /// <summary>
        /// Sự kiện được kích hoạt khi trạng thái của trình phát thay đổi (Playing, Paused, Stopped, Error).
        /// Đây là một sự kiện tùy chỉnh sử dụng enum PlayerState để đơn giản hóa việc xử lý trạng thái.
        /// </summary>
        public event Action<PlayerState> StateChanged;
        public event EventHandler<MediaPlayerTimeChangedEventArgs> TimeChanged;
        public event EventHandler<MediaPlayerLengthChangedEventArgs> LengthChanged;
        public event EventHandler EndReached;

        // --- Các thuộc tính (Properties) ---
        public bool IsPlaying => _mediaPlayer.IsPlaying; // Cách viết ngắn gọn cho một property chỉ có getter.
        public bool IsSeekable => _mediaPlayer.IsSeekable;

        /// <summary>
        /// Hàm khởi tạo của PlayerManager.
        /// </summary>
        /// <param name="videoView">Control VideoView từ UI để VLC có thể vẽ video lên đó.</param>
        public PlayerManager(VideoView videoView)
        {
            Core.Initialize(); // Khởi tạo các thành phần cốt lõi của LibVLCSharp.
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView.MediaPlayer = _mediaPlayer; // Gán MediaPlayer cho control VideoView.

            // --- Gắn các sự kiện của MediaPlayer vào các sự kiện của lớp Manager ---
            // Sử dụng lambda expression `(s, e) => ...` để chuyển tiếp sự kiện.
            // Toán tử `?.Invoke` (null-conditional) đảm bảo rằng sự kiện chỉ được kích hoạt nếu có đối tượng nào đó đã đăng ký lắng nghe.
            _mediaPlayer.TimeChanged += (s, e) => TimeChanged?.Invoke(s, e);
            _mediaPlayer.LengthChanged += (s, e) => LengthChanged?.Invoke(s, e);
            _mediaPlayer.EndReached += (s, e) => EndReached?.Invoke(s, e);

            // Lắng nghe các sự kiện riêng lẻ của MediaPlayer và ánh xạ chúng vào sự kiện StateChanged tùy chỉnh.
            _mediaPlayer.Playing += (s, e) => StateChanged?.Invoke(PlayerState.Playing);
            _mediaPlayer.Paused += (s, e) => StateChanged?.Invoke(PlayerState.Paused);
            _mediaPlayer.Stopped += (s, e) => StateChanged?.Invoke(PlayerState.Stopped);
            _mediaPlayer.EncounteredError += (s, e) => {
                UIMessageBox.ShowError("VLC gặp lỗi khi phát file này.");
                StateChanged?.Invoke(PlayerState.Error);
            };
        }

        /// <summary>
        /// Phát một bài hát.
        /// </summary>
        /// <param name="song">Đối tượng Song chứa thông tin bài hát, quan trọng nhất là FilePath.</param>
        public void Play(Song song)
        {
            if (song == null || string.IsNullOrEmpty(song.FilePath)) return;

            // Giải phóng media cũ trước khi tạo media mới để tránh rò rỉ bộ nhớ.
            _currentMedia?.Dispose();
            // Tạo một đối tượng Media mới từ đường dẫn file.
            _currentMedia = new Media(_libVLC, new Uri(song.FilePath));
            // Ra lệnh cho MediaPlayer phát Media này.
            _mediaPlayer.Play(_currentMedia);
        }

        /// <summary>
        /// Tạm dừng hoặc tiếp tục phát.
        /// </summary>
        public void Pause()
        {
            _mediaPlayer.Pause(); // Hàm này của VLC tự động chuyển đổi giữa play và pause.
        }

        /// <summary>
        /// Dừng phát nhưng giữ lại media.
        /// Cho phép người dùng có thể tua hoặc phát lại từ đầu mà không cần tải lại file.
        /// </summary>
        public void Stop()
        {
            _mediaPlayer.Stop();
            // Không dispose _currentMedia ở đây.
        }

        /// <summary>
        /// Dừng phát và giải phóng hoàn toàn tài nguyên media hiện tại.
        /// Được sử dụng khi chuyển bài hoặc đóng trình phát.
        /// </summary>
        public void StopAndRelease()
        {
            _mediaPlayer.Stop();
            _currentMedia?.Dispose();
            _currentMedia = null;
        }

        /// <summary>
        /// Tua đến một thời điểm cụ thể trong media.
        /// </summary>
        /// <param name="time">Thời gian cần tua đến, tính bằng mili giây.</param>
        public void Seek(long time)
        {
            if (_mediaPlayer.IsSeekable)
            {
                _mediaPlayer.Time = time;
            }
        }

        /// <summary>
        /// Đặt mức âm lượng.
        /// </summary>
        /// <param name="volume">Âm lượng từ 0 đến 100.</param>
        public void SetVolume(int volume)
        {
            // Âm lượng của VLC cũng được tính từ 0-100.
            _mediaPlayer.Volume = volume;
        }

        /// <summary>
        /// Thực hiện giải phóng tài nguyên khi đối tượng PlayerManager không còn được sử dụng.
        /// Đây là một phần của pattern IDisposable.
        /// </summary>
        public void Dispose()
        {
            _mediaPlayer?.Stop();
            _mediaPlayer?.Dispose();
            _libVLC?.Dispose();
            _currentMedia?.Dispose();
        }
    }
}
