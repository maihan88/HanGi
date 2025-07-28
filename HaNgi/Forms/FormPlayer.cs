using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using Sunny.UI;

namespace HaNgi
{
    /// <summary>
    /// Giao diện chính của trình phát nhạc.
    /// Quản lý việc phát media (nhạc/video), hiển thị lời bài hát, danh sách phát, và các điều khiển media.
    /// Tương tác với các lớp Manager để xử lý logic nền.
    /// </summary>
    public partial class FormPlayer : Sunny.UI.UIPage
    {
        // --- Các lớp quản lý logic nền ---

        /// <summary>
        /// Quản lý trình phát media VLC (phát, dừng, tua, âm lượng...).
        /// </summary>
        private readonly PlayerManager _playerManager;

        /// <summary>
        /// Quản lý danh sách bài hát đang phát (playlist), bao gồm chuyển bài, lặp lại, xáo trộn.
        /// </summary>
        private readonly PlaylistManager _playlistManager;

        /// <summary>
        /// Quản lý việc phân tích và hiển thị lời bài hát (lyrics).
        /// </summary>
        private readonly LyricManager _lyricManager;


        // --- Các biến trạng thái của giao diện ---

        /// <summary>
        /// Cờ (flag) để xác định người dùng có đang giữ và kéo thanh tiến trình (trackbar) hay không.
        /// Dùng để ngăn sự kiện TimeChanged cập nhật thanh tiến trình trong khi người dùng đang tua.
        /// </summary>
        private bool _isUserSeeking = false;

        /// <summary>
        /// Lưu chỉ số (index) của dòng lyric đang được highlight.
        /// Giúp tránh việc vẽ lại không cần thiết nếu dòng lyric không thay đổi.
        /// </summary>
        private int _currentLyricIndex = -1;

        /// <summary>
        /// Lưu theme (chủ đề màu sắc) hiện tại đang được áp dụng cho giao diện.
        /// </summary>
        private Theme _currentTheme;

        /// <summary>
        /// Tham chiếu đến form cài đặt, để đảm bảo chỉ có một cửa sổ cài đặt được mở tại một thời điểm.
        /// </summary>
        private FormPlayerSettings _settingsFormInstance;

        /// <summary>
        /// Lưu trạng thái cuối cùng của trình phát (Đang phát, Tạm dừng, Dừng hẳn...).
        /// Hữu ích để xử lý logic cho nút Play/Pause.
        /// </summary>
        private PlayerState _lastPlayerState = PlayerState.Stopped;

        /// <summary>
        /// Cờ (flag) để tạm thời vô hiệu hóa sự kiện SelectedIndexChanged của danh sách phát.
        /// Dùng khi ta muốn thay đổi mục được chọn bằng code mà không kích hoạt logic dành cho người dùng.
        /// </summary>
        private bool _suppressSelectionEvent = false;

        /// <summary>
        /// Lấy ID của bài hát đang được phát. Trả về -1 nếu không có bài nào đang phát.
        /// Thuộc tính này có thể được truy cập từ các form khác (ví dụ: FormHome) để kiểm tra.
        /// </summary>
        public int CurrentlyPlayingSongId { get; private set; } = -1;


        /// <summary>
        /// Hàm khởi tạo của FormPlayer.
        /// </summary>
        public FormPlayer()
        {
            InitializeComponent(); // Hàm do Visual Studio tự sinh để tạo các control trên form.

            // Khởi tạo các lớp quản lý, truyền vào các control cần thiết (như videoView).
            _playerManager = new PlayerManager(this.videoView);
            _playlistManager = new PlaylistManager();
            _lyricManager = new LyricManager();

            // Đăng ký các hàm xử lý sự kiện.
            SubscribeToEvents();

            // Đặt các giá trị ban đầu cho giao diện.
            _playerManager.SetVolume(trackVolume.Value);
            SetInitialUIState();

            // Đăng ký sự kiện StateChanged của PlayerManager bằng một lambda expression.
            // Mỗi khi trạng thái trình phát thay đổi (Playing, Paused, Stopped...), hàm này sẽ được gọi.
            _playerManager.StateChanged += state =>
            {
                _lastPlayerState = state; // Lưu lại trạng thái mới nhất.
                PlayerManager_StateChanged(state); // Gọi hàm cập nhật UI.
            };
        }

        /// <summary>
        /// Nơi tập trung đăng ký tất cả các sự kiện từ các lớp Manager và các control trên UI.
        /// Event (sự kiện) là một cơ chế cho phép một đối tượng thông báo cho các đối tượng khác khi có điều gì đó xảy ra.
        /// Toán tử `+=` dùng để "đăng ký" một hàm (event handler) vào một sự kiện.
        /// </summary>
        private void SubscribeToEvents()
        {
            // Đăng ký các sự kiện từ PlayerManager
            _playerManager.TimeChanged += PlayerManager_TimeChanged;         // Khi thời gian phát thay đổi.
            _playerManager.LengthChanged += PlayerManager_LengthChanged;     // Khi tổng thời lượng bài hát được xác định.
            _playerManager.EndReached += PlayerManager_EndReached;           // Khi bài hát kết thúc.
            _playerManager.StateChanged += PlayerManager_StateChanged;       // Khi trạng thái (play/pause/stop) thay đổi.

            // Đăng ký sự kiện chuột cho thanh tiến trình (progress bar)
            // Dùng lambda expression `(s, e) => { ... }` để viết một hàm xử lý sự kiện ngắn gọn ngay tại chỗ.
            trackProgress.MouseDown += (s, e) => { _isUserSeeking = true; }; // Khi người dùng nhấn chuột xuống, bắt đầu tua.
            trackProgress.MouseUp += TrackProgress_MouseUp;                  // Khi người dùng nhả chuột, kết thúc tua.

            // Khi vùng chọn trong RichTextBox thay đổi, nếu không phải lyric có timing, ta bỏ chọn tất cả.
            rtbLyrics.SelectionChanged += (s, e) => { if (!_lyricManager.IsTimedLyric) rtbLyrics.DeselectAll(); };
        }

        /// <summary>
        /// Hàm được gọi khi Form được tải lần đầu tiên.
        /// </summary>
        private void FormPlayer_Load(object sender, EventArgs e)
        {
            ApplyTheme("HanGi"); // Áp dụng theme mặc định.

            // Xử lý logic cho danh sách phát (Now Playing Queue).
            // Mục đích: Ngăn người dùng tự ý click để chuyển bài hát trong danh sách.
            // Việc chuyển bài phải thông qua nút Next/Previous hoặcดับเบิลคลิก.
            lstNowPlayingQueue.SelectedIndexChanged += (s, ev) =>
            {
                // Nếu cờ _suppressSelectionEvent đang bật, nghĩa là code đang tự thay đổi index,
                // nên ta bỏ qua, không làm gì cả.
                if (_suppressSelectionEvent) return;

                // Lấy index của bài hát thực sự đang phát.
                var currentSongIndex = _playlistManager.GetCurrentSongIndex();

                // Nếu index người dùng chọn khác với index đang phát...
                if (lstNowPlayingQueue.SelectedIndex != currentSongIndex)
                {
                    // ...thì ta buộc selection quay trở lại đúng bài hát đang phát.
                    lstNowPlayingQueue.SelectedIndex = currentSongIndex;
                }
            };
        }

        /// <summary>
        /// Phương thức công khai (public) được gọi từ bên ngoài (ví dụ: FormHome) để bắt đầu phát một danh sách bài hát.
        /// </summary>
        /// <param name="songsToPlay">Danh sách các đối tượng Song cần phát.</param>
        /// <param name="startIndex">Chỉ số của bài hát trong danh sách sẽ được phát đầu tiên.</param>
        public void NavigateToNowPlayingAndPlay(List<Song> songsToPlay, int startIndex = 0)
        {
            // Kiểm tra xem danh sách có hợp lệ và có bài hát nào không.
            // Toàn tử `!` là "not". `Any()` là một phương thức của LINQ, kiểm tra xem collection có phần tử nào không.
            if (songsToPlay == null || !songsToPlay.Any()) return;

            // Kích hoạt các nút điều khiển ở dưới.
            pnlBottomControls.Enabled = true;

            // --- SỬA LỖI: Vô hiệu hóa các nút không cần thiết khi chỉ có 1 bài hát ---
            bool isPlaylist = songsToPlay.Count > 1;
            btnNext.Enabled = isPlaylist;
            btnPrevious.Enabled = isPlaylist;
            btnShuffle.Enabled = isPlaylist;
            pnlQueue.Visible = isPlaylist; // Ẩn/hiện cả panel danh sách phát.

            // Tải danh sách vào PlaylistManager.
            _playlistManager.LoadPlaylist(songsToPlay, startIndex);

            // Cập nhật giao diện danh sách phát.
            UpdateNowPlayingQueueUI();

            // Bắt đầu phát bài hát hiện tại.
            PlaySong(_playlistManager.GetCurrentSong());
        }

        /// <summary>
        /// Hàm trung tâm để xử lý việc phát một bài hát cụ thể.
        /// </summary>
        /// <param name="song">Đối tượng Song cần phát.</param>
        private void PlaySong(Song song)
        {
            if (song == null)
            {
                // Nếu không có bài hát nào để phát (ví dụ: hết playlist).
                CurrentlyPlayingSongId = -1;
                SetInitialUIState(); // Reset giao diện về trạng thái ban đầu.
                return; // Kết thúc hàm.
            };

            // Cập nhật ID bài hát đang phát.
            CurrentlyPlayingSongId = song.SongID;

            // Yêu cầu PlayerManager phát bài hát.
            _playerManager.Play(song);

            // Yêu cầu LyricManager phân tích lời bài hát.
            _lyricManager.Parse(song.FullLyric);

            // Cập nhật toàn bộ giao diện cho bài hát mới.
            UpdateAllUIForNewSong(song);
        }

        /// <summary>
        /// THÊM HÀM MỚI: Dừng trình phát hoàn toàn và reset lại mọi thứ.
        /// Hàm này được gọi từ FormHome trước khi thực hiện các hành động như xóa file nhạc,
        /// để đảm bảo file không bị khóa bởi trình phát.
        /// </summary>
        public void StopPlayerAndReset()
        {
            _playerManager.StopAndRelease(); // Dừng và giải phóng tài nguyên media.
            _playlistManager.LoadPlaylist(new List<Song>()); // Xóa sạch playlist.
            CurrentlyPlayingSongId = -1; // Reset ID.
            SetInitialUIState(); // Reset giao diện.
        }

        #region UI Update Methods (Các phương thức cập nhật giao diện)

        /// <summary>
        /// Cập nhật tất cả các thành phần UI liên quan khi một bài hát mới bắt đầu.
        /// </summary>
        private void UpdateAllUIForNewSong(Song song)
        {
            // --- Xử lý đa luồng (Multi-threading) ---
            // Trình phát media (VLC) chạy trên một luồng (thread) khác với luồng của giao diện (UI thread).
            // Việc truy cập trực tiếp các control của UI từ một luồng khác sẽ gây ra lỗi.
            // `this.InvokeRequired` kiểm tra xem ta có đang ở trên một luồng khác hay không.
            if (this.InvokeRequired)
            {
                // Nếu đúng, ta dùng `this.Invoke` để yêu cầu UI thread thực thi hàm này một cách an toàn.
                // `new Action(() => ...)` tạo một "hành động" (delegate) để truyền cho Invoke.
                this.Invoke(new Action(() => UpdateAllUIForNewSong(song)));
                return; // Dừng việc thực thi ở luồng hiện tại.
            }

            // Kiểm tra xem file là video (.mp4) hay audio.
            bool isVideo = Path.GetExtension(song.FilePath ?? "").ToLower() == ".mp4";
            videoView.Visible = isVideo; // Hiện VideoView nếu là video.
            avatarCenterDisplay.Visible = !isVideo; // Hiện ảnh bìa nếu là audio.
            SetImage(avatarCenterDisplay, song.CoverPath, 61442); // 61442 là mã icon nốt nhạc.

            // Cập nhật thông tin bài hát ở panel bên phải.
            lblInfoSongName.Text = song.SongName;
            lblInfoArtist.Text = song.Artist;
            SetImage(avatarInfo, song.CoverPath, 61442);

            // Cập nhật và hiển thị lời bài hát.
            UpdateLyricsUI();

            // --- Cập nhật mục được chọn trong danh sách phát ---
            var idx = _playlistManager.GetCurrentSongIndex();
            if (idx >= 0 && idx < lstNowPlayingQueue.Items.Count)
            {
                // Tạm thời tắt event để tránh vòng lặp vô tận.
                _suppressSelectionEvent = true;
                // Chọn (highlight) đúng bài hát đang phát trong danh sách.
                lstNowPlayingQueue.SelectedIndex = idx;
                // Bật lại event.
                _suppressSelectionEvent = false;
                // Cuộn danh sách để đảm bảo bài hát đang phát luôn trong tầm nhìn.
                lstNowPlayingQueue.TopIndex = idx;
            }
        }

        /// <summary>
        /// Cập nhật RichTextBox hiển thị lời bài hát.
        /// </summary>
        private void UpdateLyricsUI()
        {
            rtbLyrics.Text = _lyricManager.GetFullLyricText(); // Lấy toàn bộ text lời bài hát.
            _currentLyricIndex = -1; // Reset chỉ số dòng lyric hiện tại.

            // Nếu có lời bài hát đã được phân tích (có timing).
            if (_lyricManager.ParsedLines.Any())
            {
                // Định dạng lại toàn bộ text về kiểu chữ và màu sắc mặc định.
                rtbLyrics.SelectAll();
                rtbLyrics.SelectionFont = new Font("Lora", 16F, FontStyle.Regular);
                rtbLyrics.SelectionColor = _currentTheme.TextColor;
                rtbLyrics.DeselectAll(); // Bỏ chọn để áp dụng thay đổi.
            }
        }

        /// <summary>
        /// Highlight dòng lyric tương ứng với thời gian phát hiện tại.
        /// </summary>
        /// <param name="currentTime">Thời gian hiện tại của bài hát.</param>
        private void HighlightCurrentLyric(TimeSpan currentTime)
        {
            // Tìm index của dòng lyric tương ứng với thời gian hiện tại.
            int newIndex = _lyricManager.GetCurrentLineIndex(currentTime);

            // Nếu tìm thấy một dòng mới (khác với dòng đang được highlight).
            if (newIndex != -1 && newIndex != _currentLyricIndex)
            {
                _currentLyricIndex = newIndex; // Cập nhật index hiện tại.

                // Sử dụng Invoke để cập nhật UI một cách an toàn từ luồng của player.
                this.Invoke(new Action(() =>
                {
                    // 1. Reset màu của tất cả các dòng về màu mặc định.
                    rtbLyrics.SelectAll();
                    rtbLyrics.SelectionColor = _currentTheme.TextColor;

                    // 2. Chọn và tô màu (highlight) dòng lyric hiện tại.
                    var currentLine = _lyricManager.ParsedLines[_currentLyricIndex];
                    rtbLyrics.Select(currentLine.StartIndex, currentLine.Length);
                    rtbLyrics.SelectionColor = _currentTheme.AccentColor; // Màu nhấn của theme.

                    // 3. Cuộn RichTextBox đến dòng hiện tại.
                    rtbLyrics.ScrollToCaret();

                    // 4. Bỏ chọn tất cả.
                    rtbLyrics.DeselectAll();
                }));
            }
        }

        /// <summary>
        /// Đặt lại giao diện về trạng thái ban đầu khi không có bài hát nào được phát.
        /// </summary>
        private void SetInitialUIState()
        {
            pnlBottomControls.Enabled = false;
            lblInfoSongName.Text = "Chưa có bài hát nào";
            lblInfoArtist.Text = "Vui lòng chọn một bài hát từ Trang chủ";
            lblCurrentTime.Text = "00:00";
            lblTotalTime.Text = "00:00";
            trackProgress.Value = 0;
            trackVolume.Value = 100;
            videoView.Visible = false;
            avatarCenterDisplay.Visible = true;
            avatarCenterDisplay.Image = null;
            avatarCenterDisplay.Symbol = 61442;
            avatarInfo.Image = null;
            avatarInfo.Symbol = 61442;
            rtbLyrics.Text = "";
            lstNowPlayingQueue.Items.Clear();
        }

        /// <summary>
        /// Cập nhật lại danh sách bài hát (Now Playing Queue) trên giao diện.
        /// </summary>
        private void UpdateNowPlayingQueueUI()
        {
            lstNowPlayingQueue.Items.Clear(); // Xóa các mục cũ.

            // --- SỬA LỖI 2: Luôn hiển thị danh sách gốc, không thay đổi khi shuffle ---
            // Ta duyệt qua `DisplayPlaylist` thay vì playlist đang phát thực sự.
            // `DisplayPlaylist` luôn giữ thứ tự gốc của các bài hát.
            foreach (var song in _playlistManager.DisplayPlaylist)
            {
                lstNowPlayingQueue.Items.Add($"{song.SongName} - {song.Artist}");
            }

            // Sau khi tải xong, highlight đúng bài hát hiện tại.
            var idx = _playlistManager.GetCurrentSongIndex();
            _suppressSelectionEvent = true;
            lstNowPlayingQueue.SelectedIndex = idx;
            _suppressSelectionEvent = false;
        }

        /// <summary>
        /// Hàm tiện ích để tải và hiển thị ảnh cho control UIAvatar một cách an toàn.
        /// </summary>
        /// <param name="avatar">Control UIAvatar cần set ảnh.</param>
        /// <param name="imagePath">Đường dẫn đến file ảnh.</param>
        /// <param name="defaultSymbol">Mã icon sẽ hiển thị nếu không tải được ảnh.</param>
        private void SetImage(UIAvatar avatar, string imagePath, int defaultSymbol)
        {
            // Kiểm tra đường dẫn có tồn tại và hợp lệ không.
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    // Đọc toàn bộ file ảnh vào một mảng byte.
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    // Sử dụng MemoryStream để tạo đối tượng Image từ mảng byte.
                    // Cách này giúp tránh việc khóa file ảnh, cho phép các tiến trình khác (như xóa file) có thể thực hiện được.
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        avatar.Image = Image.FromStream(ms);
                    }
                }
                // Nếu có lỗi xảy ra (file hỏng, không phải định dạng ảnh...), ta bắt lỗi và hiển thị icon mặc định.
                catch { avatar.Image = null; avatar.Symbol = defaultSymbol; }
            }
            else
            {
                // Nếu đường dẫn không hợp lệ, hiển thị icon mặc định.
                avatar.Image = null; avatar.Symbol = defaultSymbol;
            }
        }

        #endregion

        #region PlayerManager Event Handlers (Hàm xử lý sự kiện từ PlayerManager)

        /// <summary>
        /// Được gọi khi trạng thái của trình phát thay đổi (Playing, Paused, Stopped...).
        /// </summary>
        private void PlayerManager_StateChanged(PlayerState state)
        {
            // Cập nhật biểu tượng của nút Play/Pause.
            // Dùng Invoke để đảm bảo an toàn luồng.
            this.Invoke(new Action(() => {
                // Sử dụng toán tử ba ngôi (ternary operator) cho ngắn gọn:
                // condition ? value_if_true : value_if_false
                btnPlayPause.Symbol = (state == PlayerState.Playing) ? 61516 : 61515; // 61516: Pause, 61515: Play
            }));
        }

        /// <summary>
        /// Được gọi khi bài hát hiện tại kết thúc.
        /// `async void` thường được sử dụng cho các trình xử lý sự kiện cần chạy bất đồng bộ.
        /// </summary>
        private async void PlayerManager_EndReached(object sender, EventArgs e)
        {
            // Chờ một khoảng thời gian rất ngắn (100ms) để trình phát ổn định trạng thái.
            // `await` sẽ tạm dừng hàm này, trả lại quyền điều khiển cho UI, và quay lại sau khi Task.Delay hoàn thành.
            await Task.Delay(100);

            Song nextSongToPlay = null;

            // Xử lý logic dựa trên chế độ lặp (LoopMode).
            if (_playlistManager.LoopMode == LoopMode.LoopOne)
            {
                // Nếu đang lặp 1 bài, bài tiếp theo chính là bài hiện tại.
                nextSongToPlay = _playlistManager.GetCurrentSong();
            }
            else // Nếu không lặp 1 bài (LoopMode.None)
            {
                // Chỉ chuyển bài nếu có nhiều hơn 1 bài trong playlist.
                if (_playlistManager.PlaylistCount > 1)
                {
                    nextSongToPlay = _playlistManager.GoToNext();
                }
            }

            // Đưa hành động cuối cùng (phát bài mới hoặc dừng) về lại luồng UI.
            this.Invoke(new Action(() => {
                if (nextSongToPlay != null)
                {
                    // Nếu có bài hát tiếp theo, phát nó.
                    PlaySong(nextSongToPlay);
                }
                else
                {
                    // Nếu không, dừng trình phát.
                    _playerManager.Stop();
                    // Cập nhật lại UI: đặt progress = 0, thời gian hiện tại = 00:00, nút thành Play.
                    trackProgress.Value = 0;
                    lblCurrentTime.Text = "00:00";
                    btnPlayPause.Symbol = 61515; // Biểu tượng Play
                }
            }));
        }

        /// <summary>
        /// Được gọi khi trình phát xác định được tổng thời lượng của media.
        /// </summary>
        private void PlayerManager_LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            this.Invoke(new Action(() => {
                // `e.Length` là mili-giây, ta chuyển sang giây cho trackProgress.
                trackProgress.Maximum = (int)(e.Length / 1000);
                // Định dạng thời gian thành chuỗi "phút:giây".
                lblTotalTime.Text = TimeSpan.FromMilliseconds(e.Length).ToString(@"mm\:ss");
            }));
        }

        /// <summary>
        /// Được gọi liên tục trong khi media đang phát để cập nhật thời gian hiện tại.
        /// </summary>
        private void PlayerManager_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            // Nếu người dùng đang kéo thanh progress, ta không làm gì cả để tránh xung đột.
            if (_isUserSeeking) return;

            this.Invoke(new Action(() => {
                long currentSeconds = e.Time / 1000;
                // Kiểm tra để đảm bảo giá trị không vượt quá Maximum của trackbar.
                if (currentSeconds >= 0 && currentSeconds <= trackProgress.Maximum)
                {
                    trackProgress.Value = (int)currentSeconds;
                }
                lblCurrentTime.Text = TimeSpan.FromMilliseconds(e.Time).ToString(@"mm\:ss");
            }));

            // Cập nhật highlight cho lời bài hát.
            HighlightCurrentLyric(TimeSpan.FromMilliseconds(e.Time));
        }

        #endregion

        #region UI Control Event Handlers (Hàm xử lý sự kiện từ các Control)

        // Hàm này có thể không cần thiết nữa nếu logic đã được chuyển đi.
        private void timer_Tick(object sender, EventArgs e) { }
        private void lblInfoSongName_Click(object sender, EventArgs e) { }

        /// <summary>
        /// Được gọi khi người dùng nhả chuột ra khỏi thanh tiến trình.
        /// </summary>
        private void TrackProgress_MouseUp(object sender, MouseEventArgs e)
        {
            // Yêu cầu player tua (seek) đến vị trí mới (tính bằng mili-giây).
            _playerManager.Seek(trackProgress.Value * 1000);
            _isUserSeeking = false; // Đánh dấu đã tua xong.
        }

        /// <summary>
        /// Xử lý sự kiện click nút Play/Pause.
        /// </summary>
        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            // Trường hợp đặc biệt: bài hát đã kết thúc (trạng thái Stopped) và người dùng nhấn Play.
            if (_lastPlayerState == PlayerState.Stopped && CurrentlyPlayingSongId != -1)
            {
                // Ta cần phát lại bài hát đó từ đầu.
                var song = _playlistManager.GetCurrentSong();
                PlaySong(song);
            }
            else
            {
                // Trường hợp thông thường: chuyển đổi giữa Play và Pause.
                _playerManager.Pause();
            }
        }

        // Các hàm xử lý sự kiện click cho các nút khác, viết ngắn gọn bằng lambda expression.
        private void btnNext_Click(object sender, EventArgs e) => PlaySong(_playlistManager.GoToNext());
        private void btnPrevious_Click(object sender, EventArgs e) => PlaySong(_playlistManager.GoToPrevious());
        private void trackVolume_ValueChanged(object sender, EventArgs e) => _playerManager.SetVolume(trackVolume.Value);

        /// <summary>
        /// Xử lý sự kiện vào một mục trong danh sách phát.
        /// </summary>
        private void lstNowPlayingQueue_DoubleClick(object sender, EventArgs e)
        {
            if (lstNowPlayingQueue.SelectedIndex >= 0)
            {
                //
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút Lặp lại (Loop).
        /// </summary>
        private void btnLoop_Click(object sender, EventArgs e)
        {
            // Chuyển đổi chế độ lặp trong PlaylistManager.
            _playlistManager.ToggleLoopMode();
            var currentMode = _playlistManager.LoopMode;

            if (currentMode == LoopMode.LoopOne)
            {
                btnLoop.Symbol = 61469; // Icon lặp lại 1 bài.
                btnLoop.FillColor = _currentTheme.AccentColor; // Tô màu nút để báo hiệu đang bật.
                this.ShowInfoTip("Lặp lại bài hát hiện tại");
                // Khi lặp 1 bài, vô hiệu hóa nút Next/Previous.
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else // LoopMode.None
            {
                btnLoop.Symbol = 61470; // Icon không lặp.
                btnLoop.FillColor = Color.Transparent; // Bỏ tô màu.
                this.ShowInfoTip("Tắt lặp lại");
                // Kích hoạt lại nút Next/Previous nếu có nhiều hơn 1 bài hát.
                if (_playlistManager.PlaylistCount > 1)
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút Xáo trộn (Shuffle).
        /// </summary>
        private void btnShuffle_Click(object sender, EventArgs e)
        {
            _playlistManager.ToggleShuffle(); // Bật/tắt chế độ xáo trộn.

            // Cập nhật màu nút và hiển thị thông báo.
            btnShuffle.FillColor = _playlistManager.IsShuffled ? _currentTheme.AccentColor : Color.Transparent;
            this.ShowInfoTip(_playlistManager.IsShuffled ? "Bật phát ngẫu nhiên" : "Tắt phát ngẫu nhiên");

            // Cập nhật lại giao diện danh sách phát để highlight đúng bài hát hiện tại
            // sau khi thứ tự phát đã bị thay đổi.
            UpdateNowPlayingQueueUI();
        }

        /// <summary>
        /// Mở form cài đặt.
        /// </summary>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form cài đặt đã được tạo hoặc đã bị đóng chưa.
            if (_settingsFormInstance == null || _settingsFormInstance.IsDisposed)
            {
                // Nếu chưa, tạo một instance mới.
                _settingsFormInstance = new FormPlayerSettings(pnlQueue.Visible, pnlRightInfo.Visible, _currentTheme.Name);

                // Đăng ký các sự kiện từ form cài đặt để cập nhật lại giao diện của FormPlayer.
                _settingsFormInstance.QueueVisibilityChanged += (isVisible) => { pnlQueue.Visible = isVisible; };
                _settingsFormInstance.InfoVisibilityChanged += (isVisible) => { pnlRightInfo.Visible = isVisible; };
                _settingsFormInstance.ThemeNameChanged += ApplyTheme;

                _settingsFormInstance.Show(this.FindForm()); // Hiển thị form.
            }
            else
            {
                // Nếu form đã tồn tại, chỉ cần đưa nó lên trên cùng.
                _settingsFormInstance.BringToFront();
            }
        }

        #endregion

        #region Theme Management (Quản lý Chủ đề)

        /// <summary>
        /// Áp dụng một chủ đề (theme) màu sắc cho toàn bộ giao diện.
        /// </summary>
        /// <param name="themeName">Tên của theme cần áp dụng.</param>
        private void ApplyTheme(string themeName)
        {
            _currentTheme = ThemeManager.GetTheme(themeName);

            // Áp dụng các màu từ đối tượng _currentTheme cho từng control.
            pnlCenterDisplay.FillColor = _currentTheme.GradientStart;
            pnlCenterDisplay.FillColor2 = _currentTheme.GradientEnd;
            this.BackColor = _currentTheme.GradientEnd;
            pnlQueue.BackColor = _currentTheme.GradientEnd;
            pnlRightInfo.BackColor = _currentTheme.GradientEnd;
            pnlBottomControls.FillColor = _currentTheme.SubtleColor;
            foreach (TabPage page in uiTabControl1.TabPages) page.BackColor = _currentTheme.GradientEnd;
            lstNowPlayingQueue.FillColor = _currentTheme.GradientEnd;
            lstNowPlayingQueue.RectColor = _currentTheme.SubtleColor;
            trackProgress.ForeColor = _currentTheme.AccentColor;
            trackVolume.ForeColor = _currentTheme.AccentColor;
            lstNowPlayingQueue.ItemSelectBackColor = _currentTheme.AccentColor;
            uiTabControl1.TabSelectedColor = _currentTheme.AccentColor;

            // Cập nhật lại màu cho các nút đang ở trạng thái "active".
            if (_playlistManager.LoopMode != LoopMode.None) btnLoop.FillColor = _currentTheme.AccentColor;
            if (_playlistManager.IsShuffled) btnShuffle.FillColor = _currentTheme.AccentColor;

            // Tự động chọn màu chữ (đen hoặc trắng) để tương phản tốt nhất với màu nền (AccentColor).
            Color contrastColor = (_currentTheme.AccentColor.GetBrightness() < 0.5) ? Color.White : Color.Black;
            lstNowPlayingQueue.ItemSelectForeColor = contrastColor;
            uiTabControl1.TabSelectedForeColor = contrastColor;

            // Áp dụng màu chữ (TextColor).
            uiLabel1.ForeColor = _currentTheme.TextColor;
            lblCurrentTime.ForeColor = _currentTheme.TextColor;
            lblTotalTime.ForeColor = _currentTheme.TextColor;
            lblInfoSongName.ForeColor = _currentTheme.TextColor;
            lblInfoArtist.ForeColor = _currentTheme.TextColor;
            lstNowPlayingQueue.ForeColor = _currentTheme.TextColor;
            rtbLyrics.FillColor = _currentTheme.GradientEnd;
            rtbLyrics.ForeColor = _currentTheme.TextColor;

            // Đặt màu cho các icon nút điều khiển.
            foreach (Control ctrl in pnlBottomControls.Controls)
            {
                if (ctrl is UISymbolButton button) button.SymbolColor = _currentTheme.TextColor;
            }

            // Cập nhật lại màu sắc cho lời bài hát sau khi đổi theme.
            UpdateLyricsUI();

            // Yêu cầu panel vẽ lại để áp dụng màu gradient mới.
            pnlCenterDisplay.Invalidate();
        }
        #endregion
    }
}
