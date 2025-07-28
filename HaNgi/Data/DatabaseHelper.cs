using System;
using System.Collections.Generic;
using System.Data.SqlClient; // 1. Namespace để làm việc với SQL Server
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{
    /// <summary>
    /// Lớp hỗ trợ các hoạt động liên quan đến cơ sở dữ liệu (CSDL).
    /// Đây là một lớp 'helper', chuyên cung cấp các hàm tiện ích để tái sử dụng,
    /// trong trường hợp này là để quản lý kết nối đến CSDL.
    /// </summary>
    internal class DatabaseHelper
    {
        // 2. Chuỗi kết nối (Connection String)
        /// <summary>
        /// Chuỗi kết nối chứa thông tin cần thiết để kết nối đến CSDL SQL Server.
        /// private: Chỉ có thể truy cập bên trong lớp DatabaseHelper.
        /// static: Biến này thuộc về lớp chứ không thuộc về một đối tượng cụ thể nào.
        ///         Ta có thể truy cập trực tiếp qua tên lớp (DatabaseHelper.connectionString).
        /// readonly: Giá trị của biến này chỉ có thể được gán một lần (khi khai báo hoặc trong constructor).
        ///           Điều này đảm bảo chuỗi kết nối không bị thay đổi trong quá trình chạy chương trình.
        /// </summary>
        private static readonly string connectionString = @"Data Source=LAPTOP-JPBP45S2\SQLEXPRESS;Initial Catalog=MusicApp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        // 3. Phương thức lấy kết nối
        /// <summary>
        /// Tạo và trả về một đối tượng SqlConnection mới để tương tác với CSDL.
        /// </summary>
        /// <returns>
        /// Một đối tượng SqlConnection nếu tạo thành công; ngược lại, trả về null nếu có lỗi.
        /// </returns>
        public static SqlConnection GetConnection()
        {
            // 4. Khối try-catch để xử lý lỗi
            try
            {
                // 5. Khởi tạo đối tượng SqlConnection
                // Lệnh 'new' tạo ra một đối tượng (instance) mới của lớp SqlConnection.
                // Constructor của lớp này nhận vào chuỗi kết nối để biết phải kết nối đến server nào, CSDL nào.
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                // 6. Bắt và xử lý ngoại lệ (Exception)
                // Nếu có bất kỳ lỗi nào xảy ra trong khối 'try' (ví dụ: chuỗi kết nối sai, server không tồn tại),
                // chương trình sẽ nhảy vào khối 'catch' này.
                // 'ex' là một đối tượng chứa thông tin về lỗi vừa xảy ra.
                Console.WriteLine("Lỗi khi tạo kết nối CSDL: " + ex.Message); // Ghi thông báo lỗi ra màn hình console.

                // Trả về 'null' để báo hiệu cho nơi gọi hàm rằng kết nối đã thất bại.
                return null;
            }
        }
    }
}