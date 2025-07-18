using System;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using Sunny.UI;

namespace HaNgi
{
    /// <summary>
    /// Enum tự định nghĩa để theo dõi trạng thái của trình phát.
    /// </summary>
    public enum PlayerState
    {
        Playing,
        Paused,
        Stopped,
        Error
    }

    /// <summary>
    /// Quản lý việc tương tác trực tiếp với MediaPlayer của VLC.
    /// </summary>
    public class PlayerManager : IDisposable
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private Media _currentMedia;

        // Sửa lỗi: Thay thế sự kiện không tồn tại bằng một sự kiện tùy chỉnh.
        public event Action<PlayerState> StateChanged;
        public event EventHandler<MediaPlayerTimeChangedEventArgs> TimeChanged;
        public event EventHandler<MediaPlayerLengthChangedEventArgs> LengthChanged;
        public event EventHandler EndReached;

        public bool IsPlaying => _mediaPlayer.IsPlaying;
        public bool IsSeekable => _mediaPlayer.IsSeekable;

        public PlayerManager(VideoView videoView)
        {
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView.MediaPlayer = _mediaPlayer;

            // Gắn các sự kiện của MediaPlayer vào các sự kiện của lớp Manager
            _mediaPlayer.TimeChanged += (s, e) => TimeChanged?.Invoke(s, e);
            _mediaPlayer.LengthChanged += (s, e) => LengthChanged?.Invoke(s, e);
            _mediaPlayer.EndReached += (s, e) => EndReached?.Invoke(s, e);

            // Sửa lỗi: Lắng nghe các sự kiện riêng lẻ thay vì StateChanged
            _mediaPlayer.Playing += (s, e) => StateChanged?.Invoke(PlayerState.Playing);
            _mediaPlayer.Paused += (s, e) => StateChanged?.Invoke(PlayerState.Paused);
            _mediaPlayer.Stopped += (s, e) => StateChanged?.Invoke(PlayerState.Stopped);
            _mediaPlayer.EncounteredError += (s, e) => {
                // Sửa lỗi: Xóa tham số thứ hai không hợp lệ
                UIMessageBox.ShowError("VLC gặp lỗi khi phát file này.");
                StateChanged?.Invoke(PlayerState.Error);
            };
        }

        public void Play(Song song)
        {
            if (song == null || string.IsNullOrEmpty(song.FilePath)) return;

            _currentMedia?.Dispose();
            _currentMedia = new Media(_libVLC, new Uri(song.FilePath));
            _mediaPlayer.Play(_currentMedia);
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
            // không dispose _currentMedia
        }

        public void StopAndRelease()
        {
            _mediaPlayer.Stop();
            _currentMedia?.Dispose();
            _currentMedia = null;
        }

        public void Seek(long time)
        {
            if (_mediaPlayer.IsSeekable)
            {
                _mediaPlayer.Time = time;
            }
        }

        public void SetVolume(int volume)
        {
            _mediaPlayer.Volume = volume;
        }

        public void Dispose()
        {
            _mediaPlayer?.Stop();
            _mediaPlayer?.Dispose();
            _libVLC?.Dispose();
            _currentMedia?.Dispose();
        }
    }
}
