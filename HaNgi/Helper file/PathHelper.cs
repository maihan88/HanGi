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
        private static readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;

        public static readonly string MusicFolderPath = Path.Combine(basePath, "MusicFiles");
        public static readonly string CoversFolderPath = Path.Combine(basePath, "CoverFiles");

        public static void EnsureAppFoldersExist()
        {
            Directory.CreateDirectory(MusicFolderPath);
            Directory.CreateDirectory(CoversFolderPath);
        }

        public static string CopyFileToAppFolder(string sourceFilePath, string destinationFolder)
        {
            if (string.IsNullOrEmpty(sourceFilePath) || !File.Exists(sourceFilePath))
            {
                return string.Empty;
            }

            try
            {
                EnsureAppFoldersExist();
                string fileName = Path.GetFileName(sourceFilePath);
                string destinationPath = Path.Combine(destinationFolder, fileName);

                File.Copy(sourceFilePath, destinationPath, true);

                return fileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sao chép file: {ex.Message}");
                return string.Empty;
            }
        }

        public static string GetAbsoluteMusicPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            return Path.Combine(MusicFolderPath, fileName);
        }

        public static string GetAbsoluteCoverPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;
            return Path.Combine(CoversFolderPath, fileName);
        }
    }
}
