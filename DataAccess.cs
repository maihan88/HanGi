using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HaNgi;

// Lớp mới để chứa dữ liệu trả về cho playlist card
public class PlaylistCardData
{
    public int PlaylistID { get; set; }
    public string PlaylistName { get; set; }
    public string PlaylistImage { get; set; }
    public List<string> SongPreviews { get; set; }
}

public static class DataAccess
{
    // --- Hàm GetAllSongs của bạn giữ nguyên ---
    public static List<Song> GetAllSongs()
    {
        var songList = new List<Song>();
        string query = "SELECT SongID, SongName, Artist, CoverPath FROM dbo.Song";
        try
        {
            var dataTable = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            using (var adapter = new SqlDataAdapter(query, conn))
            {
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                songList.Add(new Song
                {
                    SongID = Convert.ToInt32(row["SongID"]),
                    SongName = row["SongName"].ToString(),
                    Artist = row["Artist"].ToString(),
                    CoverPath = row["CoverPath"].ToString()
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi nghiêm trọng khi lấy dữ liệu bài hát: " + ex.Message, "Lỗi Tầng Dữ Liệu");
        }
        return songList;
    }

    // --- HÀM MỚI CHO PLAYLIST ---
    public static List<PlaylistCardData> GetAllPlaylistsWithPreviews()
    {
        var playlistDataList = new List<PlaylistCardData>();
        string query = "SELECT PlaylistID, PlaylistName, PlaylistImage FROM dbo.Playlist";

        // Sử dụng DataTable để tránh lỗi nhiều DataReader
        var playlistTable = new DataTable();
        using (var conn = DatabaseHelper.GetConnection())
        using (var adapter = new SqlDataAdapter(query, conn))
        {
            try
            {
                // Bước 1: Lấy tất cả playlist về trước
                adapter.Fill(playlistTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách playlist: " + ex.Message, "Lỗi Tầng Dữ Liệu");
                return playlistDataList; // Trả về danh sách rỗng nếu có lỗi
            }
        }

        // Bước 2: Với mỗi playlist, lấy 3 bài hát preview
        foreach (DataRow row in playlistTable.Rows)
        {
            var pData = new PlaylistCardData
            {
                PlaylistID = Convert.ToInt32(row["PlaylistID"]),
                PlaylistName = row["PlaylistName"].ToString(),
                PlaylistImage = row["PlaylistImage"].ToString(),
                SongPreviews = GetTop3SongsForPlaylist_Internal(Convert.ToInt32(row["PlaylistID"]))
            };
            playlistDataList.Add(pData);
        }

        return playlistDataList;
    }

    // Hàm nội bộ để lấy song preview, được gọi bởi hàm ở trên
    private static List<string> GetTop3SongsForPlaylist_Internal(int playlistId)
    {
        var list = new List<string>();
        string query = "SELECT TOP 3 s.SongName, s.Artist FROM dbo.PlaylistSong ps JOIN dbo.Song s ON ps.SongID = s.SongID WHERE ps.PlaylistID = @ID ORDER BY ps.OrderIndex";

        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@ID", playlistId);
            try
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add($"{reader["SongName"]} - {reader["Artist"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi nhưng không dừng chương trình
                Console.WriteLine($"Lỗi khi lấy preview cho PlaylistID {playlistId}: {ex.Message}");
            }
        }
        return list;
    }

    // *** HÀM MỚI ĐỂ TÌM KIẾM ***
    public static List<Song> SearchSongs(string searchTerm)
    {
        var songList = new List<Song>();
        // Câu lệnh LIKE '%...%' sẽ tìm kiếm bất kỳ đâu trong tên bài hát hoặc tên nghệ sĩ
        string query = "SELECT * FROM dbo.Song WHERE SongName LIKE @SearchTerm OR Artist LIKE @SearchTerm";

        try
        {
            var dataTable = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            using (var adapter = new SqlDataAdapter(query, conn))
            {
                // Thêm dấu % vào hai đầu của từ khóa
                adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                songList.Add(new Song
                {
                    SongID = Convert.ToInt32(row["SongID"]),
                    SongName = row["SongName"].ToString(),
                    Artist = row["Artist"].ToString(),
                    // Quan trọng: Phải lấy đường dẫn đầy đủ
                    CoverPath = PathHelper.GetAbsoluteCoverPath(row["CoverPath"].ToString()),
                    FilePath = PathHelper.GetAbsoluteMusicPath(row["FilePath"].ToString())
                    // Các thuộc tính khác nếu cần
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi tìm kiếm bài hát: " + ex.Message, "Lỗi Tầng Dữ Liệu");
        }
        return songList;
    }

    // Kiểm tra xem file nhạc đã tồn tại chưa
    public static bool FilePathExists(string filePath, int? excludeSongId = null)
    {
        string query = "SELECT COUNT(1) FROM dbo.Song WHERE FilePath = @Path" + (excludeSongId.HasValue ? " AND SongID != @ID" : "");
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Path", filePath);
            if (excludeSongId.HasValue) cmd.Parameters.AddWithValue("@ID", excludeSongId.Value);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }
    }

    // Kiểm tra xem tên playlist đã tồn tại chưa
    public static bool PlaylistNameExists(string playlistName, int? excludePlaylistId = null)
    {
        string query = "SELECT COUNT(1) FROM dbo.Playlist WHERE PlaylistName = @Name" + (excludePlaylistId.HasValue ? " AND PlaylistID != @ID" : "");
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Name", playlistName);
            if (excludePlaylistId.HasValue) cmd.Parameters.AddWithValue("@ID", excludePlaylistId.Value);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}