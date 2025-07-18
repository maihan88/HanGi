using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace HaNgi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            PathHelper.EnsureAppFoldersExist(); 

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Đã có lỗi giao diện nghiêm trọng xảy ra:\n\n" +
                "Lỗi: " + e.Exception.Message + "\n\n" +
                "Chi tiết: \n" + e.Exception.StackTrace,
                "Lỗi Giao Diện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show("Đã có lỗi hệ thống nghiêm trọng xảy ra:\n\n" +
                "Lỗi: " + ex.Message + "\n\n" +
                "Chi tiết: \n" + ex.StackTrace,
                "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}