using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString = @"Data Source=LAPTOP-JPBP45S2\SQLEXPRESS;Initial Catalog=MusicApp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tạo kết nối CSDL: " + ex.Message);
                return null;
            }
        }
    }
}
