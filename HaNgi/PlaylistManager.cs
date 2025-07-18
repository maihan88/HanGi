using System;
using System.Collections.Generic;
using System.Linq;

namespace HaNgi
{
    public enum LoopMode { None, LoopOne }

    public class PlaylistManager
    {
        private List<Song> _originalPlaylist = new List<Song>();
        private int _currentIndex = -1;
        private Random _random = new Random();

        public bool IsShuffled { get; private set; } = false;
        public LoopMode LoopMode { get; private set; } = LoopMode.None;

        // Thuộc tính này luôn trả về danh sách gốc để hiển thị trên UI
        public List<Song> DisplayPlaylist => _originalPlaylist;

        public bool IsPlaylistEmpty => !_originalPlaylist.Any();
        public int PlaylistCount => _originalPlaylist.Count;

        public void LoadPlaylist(List<Song> songs, int startIndex = 0)
        {
            if (songs == null || !songs.Any())
            {
                _originalPlaylist.Clear();
                _currentIndex = -1;
                return;
            }
            _originalPlaylist = new List<Song>(songs);
            _currentIndex = startIndex >= 0 && startIndex < songs.Count ? startIndex : 0;
        }

        public Song GetCurrentSong()
        {
            if (IsPlaylistEmpty || _currentIndex < 0 || _currentIndex >= _originalPlaylist.Count)
            {
                return null;
            }
            return _originalPlaylist[_currentIndex];
        }

        public Song GoToNext()
        {
            if (IsPlaylistEmpty || PlaylistCount <= 1) return GetCurrentSong();

            if (IsShuffled)
            {
                // <<< SỬA LỖI 2: Logic shuffle "bất ngờ"
                // Chọn một bài hát ngẫu nhiên khác với bài hiện tại.
                int newIndex;
                do
                {
                    newIndex = _random.Next(0, PlaylistCount);
                } while (newIndex == _currentIndex);
                _currentIndex = newIndex;
            }
            else
            {
                // Phát tuần tự
                _currentIndex++;
                if (_currentIndex >= PlaylistCount)
                {
                    _currentIndex = 0; // Quay về đầu
                }
            }
            return GetCurrentSong();
        }

        public Song GoToPrevious()
        {
            if (IsPlaylistEmpty || PlaylistCount <= 1) return GetCurrentSong();

            // Nút Previous sẽ luôn hoạt động tuần tự cho đơn giản
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = PlaylistCount - 1;
            }
            return GetCurrentSong();
        }

        public void ToggleLoopMode()
        {
            LoopMode = (LoopMode == LoopMode.None) ? LoopMode.LoopOne : LoopMode.None;
        }

        public void ToggleShuffle()
        {
            IsShuffled = !IsShuffled;
        }

        public int GetCurrentSongIndex() => _currentIndex;
    }
}
