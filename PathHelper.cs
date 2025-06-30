using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaNgi
{
    internal class PathHelper
    {
        // Đường dẫn gốc của ứng dụng (nơi chứa file .exe)
        private static readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;

        // Định nghĩa đường dẫn tới các thư mục lưu trữ chuyên dụng
        public static readonly string MusicFolderPath = Path.Combine(basePath, "MusicFiles");
        public static readonly string CoversFolderPath = Path.Combine(basePath, "CoverFiles");

        /// <summary>
        /// Đảm bảo các thư mục lưu trữ tồn tại. Sẽ tự động tạo nếu chưa có.
        /// </summary>
        public static void EnsureAppFoldersExist()
        {
            Directory.CreateDirectory(MusicFolderPath);
            Directory.CreateDirectory(CoversFolderPath);
        }

        /// <summary>
        /// Sao chép một file từ bên ngoài vào thư mục của ứng dụng và trả về chỉ tên file.
        /// </summary>
        /// <param name="sourceFilePath">Đường dẫn đầy đủ của file gốc.</param>
        /// <param name="destinationFolder">Thư mục đích (dùng MusicFolderPath hoặc CoversFolderPath).</param>
        /// <returns>Chỉ tên file để lưu vào CSDL, hoặc string rỗng nếu thất bại.</returns>
        public static string CopyFileToAppFolder(string sourceFilePath, string destinationFolder)
        {
            if (string.IsNullOrEmpty(sourceFilePath) || !File.Exists(sourceFilePath))
            {
                return string.Empty;
            }

            try
            {
                EnsureAppFoldersExist(); // Đảm bảo thư mục đích tồn tại
                string fileName = Path.GetFileName(sourceFilePath);
                string destinationPath = Path.Combine(destinationFolder, fileName);

                // Sao chép file, cho phép ghi đè nếu file đã tồn tại
                File.Copy(sourceFilePath, destinationPath, true);

                return fileName; // Chỉ trả về tên file
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ví dụ: không có quyền ghi file)
                MessageBox.Show($"Lỗi khi sao chép file: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Lấy đường dẫn tuyệt đối của một file nhạc từ tên file.
        /// </summary>
        public static string GetAbsoluteMusicPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            return Path.Combine(MusicFolderPath, fileName);
        }

        /// <summary>
        /// Lấy đường dẫn tuyệt đối của một ảnh bìa từ tên file.
        /// </summary>
        public static string GetAbsoluteCoverPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            return Path.Combine(CoversFolderPath, fileName);
        }
    }
}
