using System;
using System.Collections.Generic;
using System.IO; // Thư viện để làm việc với file và thư mục (Input/Output)
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // Thư viện để hiển thị các thành phần UI như MessageBox

namespace HaNgi
{
    /// <summary>
    /// Lớp PathHelper cung cấp các phương thức và thuộc tính tiện ích để quản lý
    /// các đường dẫn file và thư mục cho ứng dụng. Nó giúp tập trung logic xử lý
    /// đường dẫn vào một nơi, làm cho code dễ bảo trì hơn.
    /// </summary>
    internal class PathHelper
    {
        // `private static readonly string basePath`:
        // - `private`: Chỉ có thể truy cập bên trong lớp PathHelper.
        // - `static`: Biến này thuộc về chính lớp PathHelper, không phải của một đối tượng cụ thể nào.
        //   Ta có thể truy cập nó mà không cần tạo mới `new PathHelper()`.
        // - `readonly`: Giá trị của biến này chỉ có thể được gán một lần duy nhất khi khai báo hoặc trong hàm dựng static.
        //   Sau đó không thể thay đổi được nữa, đảm bảo đường dẫn gốc luôn ổn định.
        // `AppDomain.CurrentDomain.BaseDirectory`: Lấy đường dẫn đến thư mục chứa file thực thi (.exe) của ứng dụng.
        //   Đây là cách đáng tin cậy để biết ứng dụng đang chạy từ đâu.
        private static readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Đường dẫn tuyệt đối đến thư mục chứa các file nhạc của ứng dụng.
        /// `Path.Combine` là cách an toàn để nối các phần của một đường dẫn. Nó tự động xử lý
        /// các dấu gạch chéo ('\' hoặc '/') cho phù hợp với hệ điều hành (Windows, MacOS, Linux).
        /// </summary>
        public static readonly string MusicFolderPath = Path.Combine(basePath, "MusicFiles");

        /// <summary>
        /// Đường dẫn tuyệt đối đến thư mục chứa các file ảnh bìa của ứng dụng.
        /// </summary>
        public static readonly string CoversFolderPath = Path.Combine(basePath, "CoverFiles");

        /// <summary>
        /// Đảm bảo rằng các thư mục cần thiết cho ứng dụng (`MusicFiles`, `CoverFiles`) đã tồn tại.
        /// Nếu chưa có, phương thức này sẽ tự động tạo chúng.
        /// </summary>
        public static void EnsureAppFoldersExist()
        {
            // `Directory.CreateDirectory` sẽ tạo một thư mục nếu nó chưa tồn tại.
            // Một điểm hay là nếu thư mục đã tồn tại rồi, lệnh này sẽ không làm gì cả và không báo lỗi.
            Directory.CreateDirectory(MusicFolderPath);
            Directory.CreateDirectory(CoversFolderPath);
        }

        /// <summary>
        /// Sao chép một file từ đường dẫn nguồn vào một thư mục đích của ứng dụng.
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn đầy đủ đến file cần sao chép.</param>
        /// <param name="destinationFolder">Thư mục đích trong ứng dụng (ví dụ: MusicFolderPath).</param>
        /// <returns>Tên của file nếu sao chép thành công; ngược lại trả về chuỗi rỗng (string.Empty).</returns>
        public static string CopyFileToAppFolder(string sourceFilePath, string destinationFolder)
        {
            // Kiểm tra đầu vào: đảm bảo đường dẫn nguồn không rỗng và file thực sự tồn tại.
            if (string.IsNullOrEmpty(sourceFilePath) || !File.Exists(sourceFilePath))
            {
                // Nếu không hợp lệ, trả về chuỗi rỗng để báo hiệu thất bại.
                return string.Empty;
            }

            // `try-catch` là một khối xử lý lỗi. Code bên trong `try` sẽ được thực thi.
            // Nếu có bất kỳ lỗi nào xảy ra (ví dụ: không có quyền ghi file, ổ cứng đầy),
            // chương trình sẽ không bị "crash" mà sẽ nhảy vào khối `catch` để xử lý.
            try
            {
                // Đảm bảo thư mục đích tồn tại trước khi sao chép.
                EnsureAppFoldersExist();

                // `Path.GetFileName` trích xuất tên file và phần mở rộng từ một đường dẫn đầy đủ.
                // Ví dụ: "C:\Users\Admin\Music\song.mp3" -> "song.mp3"
                string fileName = Path.GetFileName(sourceFilePath);

                // Tạo đường dẫn đích hoàn chỉnh.
                string destinationPath = Path.Combine(destinationFolder, fileName);

                // Thực hiện sao chép file.
                // Tham số thứ ba `true` có nghĩa là "overwrite": nếu file đích đã tồn tại, ghi đè lên nó.
                File.Copy(sourceFilePath, destinationPath, true);

                // Nếu mọi thứ thành công, trả về tên file đã được sao chép.
                return fileName;
            }
            catch (Exception ex) // Bắt tất cả các loại ngoại lệ (lỗi) và lưu thông tin vào biến `ex`.
            {
                // Hiển thị một hộp thoại thông báo lỗi cho người dùng.
                MessageBox.Show($"Lỗi khi sao chép file: {ex.Message}");
                // Trả về chuỗi rỗng để báo hiệu việc sao chép đã thất bại.
                return string.Empty;
            }
        }

        /// <summary>
        /// Lấy đường dẫn tuyệt đối đến một file nhạc dựa vào tên file.
        /// </summary>
        /// <param name="fileName">Tên file nhạc (ví dụ: "mysong.mp3").</param>
        /// <returns>Đường dẫn đầy đủ đến file nhạc đó trong thư mục MusicFiles.</returns>
        public static string GetAbsoluteMusicPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            return Path.Combine(MusicFolderPath, fileName);
        }

        /// <summary>
        /// Lấy đường dẫn tuyệt đối đến một file ảnh bìa dựa vào tên file.
        /// </summary>
        /// <param name="fileName">Tên file ảnh (ví dụ: "cover.jpg").</param>
        /// <returns>Đường dẫn đầy đủ đến file ảnh đó trong thư mục CoverFiles.</returns>
        public static string GetAbsoluteCoverPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            return Path.Combine(CoversFolderPath, fileName);
        }
    }
}
