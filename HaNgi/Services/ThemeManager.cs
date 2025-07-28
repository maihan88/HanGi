using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    /// <summary>
    /// Định nghĩa cấu trúc của một chủ đề (theme) màu sắc.
    /// `struct` là một kiểu dữ liệu giá trị (value type), thích hợp cho các đối tượng nhỏ, bất biến.
    /// </summary>
    public struct Theme
    {
        public string Name { get; set; }
        public Color GradientStart { get; set; } // Màu bắt đầu của dải màu gradient.
        public Color GradientEnd { get; set; }   // Màu kết thúc của dải màu gradient.
        public Color AccentColor { get; set; }   // Màu nhấn, dùng cho các thành phần quan trọng (nút, highlight).
        public Color TextColor { get; set; }     // Màu chữ chính.
        public Color SubtleColor { get; set; }   // Một màu nhẹ nhàng, dùng cho nền của các panel phụ.

        /// <summary>
        /// Hàm khởi tạo (constructor) để dễ dàng tạo một đối tượng Theme mới.
        /// </summary>
        public Theme(string name, Color gradStart, Color gradEnd, Color accent, Color text, Color subtle)
        {
            Name = name;
            GradientStart = gradStart;
            GradientEnd = gradEnd;
            AccentColor = accent;
            TextColor = text;
            SubtleColor = subtle;
        }
    }

    /// <summary>
    /// Lớp tĩnh (static class) để quản lý các chủ đề màu sắc được định nghĩa sẵn trong ứng dụng.
    /// Một lớp tĩnh không thể tạo đối tượng (instance) và tất cả các thành viên của nó cũng phải là tĩnh.
    /// </summary>
    public static class ThemeManager
    {
        // `private static readonly`: Khai báo một danh sách chỉ có thể đọc (readonly) và chỉ thuộc về lớp (static).
        // Danh sách này được khởi tạo một lần duy nhất khi chương trình bắt đầu.
        private static readonly List<Theme> PredefinedThemes = new List<Theme>
        {
            // Theme mặc định "HanGi" (Tên có thể là "Hàn Gì" hoặc "Hang-i")
            new Theme("HanGi",
                Color.FromArgb(44, 44, 84),      // GradientStart: Tím than đậm
                Color.FromArgb(25, 25, 56),      // GradientEnd: Tím than rất đậm
                Color.FromArgb(255, 0, 127),     // AccentColor: Hồng neon
                Color.FromArgb(240, 240, 240),   // TextColor: Trắng ngà
                Color.FromArgb(58, 58, 90)),     // SubtleColor: Tím than nhạt

            // Theme "Sky" (Bầu trời)
            new Theme("Sky",
                Color.FromArgb(78, 142, 221),    // GradientStart: Xanh da trời
                Color.FromArgb(29, 68, 115),     // GradientEnd: Xanh dương đậm
                Color.FromArgb(255, 193, 7),     // AccentColor: Vàng hổ phách (màu mặt trời)
                Color.White,                     // TextColor: Trắng
                Color.FromArgb(49, 112, 185)),   // SubtleColor: Xanh dương vừa

            // Theme "Sunset" (Hoàng hôn)
            new Theme("Sunset",
                Color.FromArgb(243, 156, 18),    // GradientStart: Vàng cam
                Color.FromArgb(211, 84, 0),      // GradientEnd: Cam cháy
                Color.FromArgb(255, 235, 59),    // AccentColor: Vàng chanh
                Color.White,                     // TextColor: Trắng
                Color.FromArgb(230, 126, 34))    // SubtleColor: Cam vừa
        };

        /// <summary>
        /// Lấy một đối tượng Theme dựa vào tên của nó.
        /// </summary>
        /// <param name="themeName">Tên của theme cần tìm.</param>
        /// <returns>Đối tượng Theme tương ứng, hoặc theme "HanGi" mặc định nếu không tìm thấy.</returns>
        public static Theme GetTheme(string themeName)
        {
            // Sử dụng LINQ `FirstOrDefault` để tìm theme đầu tiên trong danh sách có Name khớp với themeName.
            // `t => t.Name == themeName` là một biểu thức lambda, định nghĩa điều kiện tìm kiếm.
            var theme = PredefinedThemes.FirstOrDefault(t => t.Name == themeName);

            // Vì `Theme` là một struct, `FirstOrDefault` sẽ trả về một `Theme` mặc định (với Name là null) nếu không tìm thấy.
            if (theme.Name == null)
            {
                // Nếu không tìm thấy, trả về theme "HanGi" làm phương án dự phòng.
                // `First` được sử dụng ở đây vì ta chắc chắn theme "HanGi" tồn tại.
                return PredefinedThemes.First(t => t.Name == "HanGi");
            }
            return theme;
        }

        /// <summary>
        /// Lấy danh sách tên của tất cả các theme đã được định nghĩa.
        /// </summary>
        /// <returns>Một danh sách các chuỗi chứa tên theme.</returns>
        public static List<string> GetThemeNames()
        {
            // Sử dụng LINQ `Select` để chuyển đổi (project) danh sách `List<Theme>` thành một danh sách `IEnumerable<string>`
            // chỉ chứa thuộc tính `Name` của mỗi theme.
            // `ToList()` sau đó chuyển đổi kết quả `IEnumerable` thành một `List<string>`.
            return PredefinedThemes.Select(t => t.Name).ToList();
        }
    }
}
