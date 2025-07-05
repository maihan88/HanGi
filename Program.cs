using System;
using System.IO;
using System.Windows.Forms;
using System.Threading; // Thêm using này

namespace HaNgi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // *** BẮT ĐẦU THÊM CODE MỚI ***
            // Thiết lập bộ xử lý lỗi cho các luồng giao diện (UI Threads)
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            // Thiết lập bộ xử lý lỗi cho các luồng nền (Non-UI Threads)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            // *** KẾT THÚC THÊM CODE MỚI ***

            PathHelper.EnsureAppFoldersExist(); // Giữ lại dòng này (sửa từ code cũ của bạn)

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        // Phương thức xử lý lỗi từ luồng giao diện
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Hiển thị một hộp thoại lỗi chi tiết
            MessageBox.Show("Đã có lỗi giao diện nghiêm trọng xảy ra:\n\n" +
                "Lỗi: " + e.Exception.Message + "\n\n" +
                "Chi tiết: \n" + e.Exception.StackTrace,
                "Lỗi Giao Diện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Phương thức xử lý lỗi từ các luồng khác (quan trọng cho lỗi video)
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Lấy đối tượng lỗi
            Exception ex = e.ExceptionObject as Exception;
            // Hiển thị một hộp thoại lỗi chi tiết
            MessageBox.Show("Đã có lỗi hệ thống nghiêm trọng xảy ra:\n\n" +
                "Lỗi: " + ex.Message + "\n\n" +
                "Chi tiết: \n" + ex.StackTrace,
                "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}