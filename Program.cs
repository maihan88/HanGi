using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaNgi
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Thư mục gốc của ứng dụng
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Định nghĩa các thư mục con cho media
            string audioDir = Path.Combine(baseDir, "Media", "Audio");
            string videoDir = Path.Combine(baseDir, "Media", "Video");
            string imageDir = Path.Combine(baseDir, "Media", "Images");

            // Tự động tạo các thư mục nếu chưa tồn tại
            Directory.CreateDirectory(audioDir);
            Directory.CreateDirectory(videoDir);
            Directory.CreateDirectory(imageDir);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
