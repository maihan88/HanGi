using Sunny.UI;
using System;
using System.Windows.Forms;

namespace HaNgi
{
    /// <summary>
    /// Form dùng để cấu hình các tùy chọn cho trình phát nhạc, như hiển thị panel, đổi theme.
    /// Form này giao tiếp với FormPlayer thông qua các "event" (sự kiện).
    /// </summary>
    public partial class FormPlayerSettings : UIForm
    {
        // --- Định nghĩa các Events ---
        // Event là một cơ chế để lớp này "thông báo" cho các lớp khác (ở đây là FormPlayer)
        // rằng có một sự kiện đã xảy ra (ví dụ: người dùng đã thay đổi một cài đặt).
        // Action<T> là một delegate (đại biểu) đại diện cho một hàm không trả về giá trị và có tham số kiểu T.

        /// <summary>
        /// Sự kiện được kích hoạt khi người dùng thay đổi trạng thái hiển thị của danh sách phát (Queue).
        /// Tham số `bool` sẽ là `true` nếu switch được bật (hiển thị), và `false` nếu tắt (ẩn).
        /// </summary>
        public event Action<bool> QueueVisibilityChanged;

        /// <summary>
        /// Sự kiện được kích hoạt khi người dùng thay đổi trạng thái hiển thị của panel thông tin (Info).
        /// </summary>
        public event Action<bool> InfoVisibilityChanged;

        /// <summary>
        /// Sự kiện được kích hoạt khi người dùng chọn một theme mới.
        /// Tham số `string` sẽ là tên của theme được chọn (ví dụ: "HanGi", "Sky").
        /// </summary>
        public event Action<string> ThemeNameChanged;

        /// <summary>
        /// Hàm khởi tạo (Constructor) của Form.
        /// Nó được gọi khi một đối tượng mới của FormPlayerSettings được tạo.
        /// </summary>
        /// <param name="isQueueVisible">Trạng thái hiển thị ban đầu của danh sách phát.</param>
        /// <param name="isInfoVisible">Trạng thái hiển thị ban đầu của panel thông tin.</param>
        /// <param name="currentThemeName">Tên của theme hiện tại đang được sử dụng.</param>
        public FormPlayerSettings(bool isQueueVisible, bool isInfoVisible, string currentThemeName)
        {
            InitializeComponent();

            // --- Pattern: Unsubscribe/Subscribe ---
            // Tạm thời hủy đăng ký các sự kiện để tránh chúng bị kích hoạt trong lúc ta đang
            // thiết lập giá trị ban đầu cho các control.
            UnsubscribeEvents();

            // Thiết lập trạng thái ban đầu cho các switch dựa trên giá trị được truyền vào từ FormPlayer.
            switchQueue.Active = isQueueVisible;
            switchInfo.Active = isInfoVisible;

            // Sử dụng câu lệnh `switch` để chọn đúng RadioButton tương ứng với theme hiện tại.
            switch (currentThemeName)
            {
                case "Sky":
                    radioSky.Checked = true;
                    break;
                case "Sunset":
                    radioSunset.Checked = true;
                    break;
                default: // Nếu không phải các trường hợp trên, mặc định là "HanGi".
                    radioHanGi.Checked = true;
                    break;
            }

            // Sau khi đã thiết lập xong giá trị ban đầu, đăng ký lại các sự kiện.
            // Bây giờ, chỉ những thay đổi do người dùng thực hiện mới kích hoạt các sự kiện này.
            SubscribeEvents();
        }

        /// <summary>
        /// Đăng ký các hàm xử lý sự kiện cho các control.
        /// Toán tử `+=` dùng để gắn một hàm (event handler) vào một sự kiện (event).
        /// </summary>
        private void SubscribeEvents()
        {
            switchQueue.ValueChanged += switchQueue_ValueChanged;
            switchInfo.ValueChanged += switchInfo_ValueChanged;
            radioHanGi.ValueChanged += RadioButton_Theme_Changed;
            radioSky.ValueChanged += RadioButton_Theme_Changed;
            radioSunset.ValueChanged += RadioButton_Theme_Changed;
        }

        /// <summary>
        /// Hủy đăng ký các hàm xử lý sự kiện.
        /// Toán tử `-=` dùng để gỡ một hàm ra khỏi sự kiện.
        /// Việc này quan trọng để tránh lỗi hoặc hành vi không mong muốn, đặc biệt là khi khởi tạo form.
        /// </summary>
        private void UnsubscribeEvents()
        {
            switchQueue.ValueChanged -= switchQueue_ValueChanged;
            switchInfo.ValueChanged -= switchInfo_ValueChanged;
            radioHanGi.ValueChanged -= RadioButton_Theme_Changed;
            radioSky.ValueChanged -= RadioButton_Theme_Changed;
            radioSunset.ValueChanged -= RadioButton_Theme_Changed;
        }

        /// <summary>
        /// Hàm xử lý sự kiện chung cho tất cả các RadioButton chọn theme.
        /// </summary>
        /// <param name="sender">Đối tượng đã kích hoạt sự kiện (chính là RadioButton được click).</param>
        /// <param name="value">Trạng thái mới của RadioButton (`true` nếu được chọn, `false` nếu bị bỏ chọn).</param>
        private void RadioButton_Theme_Changed(object sender, bool value)
        {
            // Ta chỉ xử lý khi một RadioButton được chọn (`value` là `true`).
            if (value)
            {
                // Ép kiểu (cast) `sender` về kiểu UIRadioButton để có thể truy cập các thuộc tính của nó.
                // Toán tử `as` sẽ trả về `null` nếu việc ép kiểu thất bại, an toàn hơn việc ép kiểu trực tiếp `(UIRadioButton)sender`.
                var selectedRadioButton = sender as UIRadioButton;
                if (selectedRadioButton != null)
                {
                    // Xác định tên theme dựa trên RadioButton nào đã được chọn.
                    string themeName = "HanGi"; // Mặc định
                    if (selectedRadioButton == radioSky)
                    {
                        themeName = "Sky";
                    }
                    else if (selectedRadioButton == radioSunset)
                    {
                        themeName = "Sunset";
                    }

                    // Kích hoạt sự kiện ThemeNameChanged và gửi tên theme mới về cho FormPlayer.
                    // Toán tử `?.` (null-conditional operator) là cách viết tắt để kiểm tra xem
                    // `ThemeNameChanged` có khác `null` không (tức là có ai đăng ký sự kiện này không)
                    // trước khi gọi `Invoke`. Nếu là `null`, nó sẽ không làm gì cả và tránh được lỗi.
                    ThemeNameChanged?.Invoke(themeName);
                }
            }
        }

        /// <summary>
        /// Hàm xử lý sự kiện khi giá trị của switch "Hiển thị danh sách phát" thay đổi.
        /// </summary>
        private void switchQueue_ValueChanged(object sender, bool value)
        {
            // Kích hoạt sự kiện QueueVisibilityChanged và gửi giá trị mới (`true` hoặc `false`).
            QueueVisibilityChanged?.Invoke(value);
        }

        /// <summary>
        /// Hàm xử lý sự kiện khi giá trị của switch "Hiển thị thông tin" thay đổi.
        /// </summary>
        private void switchInfo_ValueChanged(object sender, bool value)
        {
            // Kích hoạt sự kiện InfoVisibilityChanged và gửi giá trị mới.
            InfoVisibilityChanged?.Invoke(value);
        }

        // Hàm này được Visual Studio tạo sẵn, có thể để trống nếu không cần xử lý gì khi form được tải.
        private void FormPlayerSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
