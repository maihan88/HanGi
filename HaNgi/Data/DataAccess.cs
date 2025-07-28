using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;
using HaNgi;
using Sunny.UI;

public class PlaylistCardData
{
    public int PlaylistID { get; set; }
    public string PlaylistName { get; set; }
    public string PlaylistImage { get; set; }
    public List<string> SongPreviews { get; set; } = new List<string>();
}

public static class DataAccess
{
    //================================================================//
    // PHẦN LẤY DỮ LIỆU (SELECT)
    //================================================================//

    /// <summary>
    /// Lấy danh sách tất cả bài hát.
    /// </summary>
    public static List<Song> GetAllSongs()
    {
        const string query = "SELECT SongID, SongName, Artist, CoverPath FROM dbo.Song";
        return ExecuteQuery(query, cmd => { }, reader => new Song
        {
            SongID = reader.GetInt32("SongID"),
            SongName = reader.GetString("SongName"),
            Artist = reader.GetString("Artist"),
            CoverPath = reader.GetString("CoverPath")
        });
    }

    /// <summary>
    /// Lấy thông tin bài hát theo ID.
    /// </summary>
    public static Song GetSongById(int songId)
    {
        const string query = "SELECT * FROM dbo.Song WHERE SongID = @ID";
        return ExecuteQuerySingle(query, cmd =>
        {
            cmd.Parameters.AddWithValue("@ID", songId);
        }, reader => new Song
        {
            SongID = songId,
            SongName = reader.GetString("SongName"),
            Artist = reader.GetString("Artist"),
            Duration = reader.GetInt32("Duration"),
            FilePath = PathHelper.GetAbsoluteMusicPath(reader.GetString("FilePath")),
            CoverPath = PathHelper.GetAbsoluteCoverPath(reader.GetString("CoverPath")),
            FullLyric = reader["FullLyric"] as string ?? string.Empty
        });
    }

    /// <summary>
    /// Lấy danh sách bài hát theo ID playlist.
    /// </summary>
    public static List<Song> GetSongsByPlaylistId(int playlistId)
    {
        const string query = @"
            SELECT s.* FROM dbo.Song s
            JOIN dbo.PlaylistSong ps ON s.SongID = ps.SongID
            WHERE ps.PlaylistID = @ID ORDER BY ps.OrderIndex";

        return ExecuteQuery(query, cmd =>
        {
            cmd.Parameters.AddWithValue("@ID", playlistId);
        }, reader => new Song
        {
            SongID = reader.GetInt32("SongID"),
            SongName = reader.GetString("SongName"),
            Artist = reader.GetString("Artist"),
            Duration = reader.GetInt32("Duration"),
            FilePath = PathHelper.GetAbsoluteMusicPath(reader.GetString("FilePath")),
            CoverPath = PathHelper.GetAbsoluteCoverPath(reader.GetString("CoverPath")),
            FullLyric = reader["FullLyric"] as string ?? string.Empty
        });
    }

    /// <summary>
    /// Lấy thông tin playlist theo ID.
    /// </summary>
    public static Playlist GetPlaylistById(int playlistId)
    {
        const string query = "SELECT PlaylistID, PlaylistName, PlaylistImage FROM dbo.Playlist WHERE PlaylistID = @ID";
        return ExecuteQuerySingle(query, cmd =>
        {
            cmd.Parameters.AddWithValue("@ID", playlistId);
        }, reader => new Playlist
        {
            PlaylistID = playlistId,
            PlaylistName = reader.GetString("PlaylistName"),
            PlaylistImage = PathHelper.GetAbsoluteCoverPath(reader.GetString("PlaylistImage"))
        });
    }

    /// <summary>
    /// Lấy danh sách tất cả playlist kèm tối đa 3 bài hát xem trước.
    /// </summary>
    /// <remarks>
    /// - Sử dụng CTE (Common Table Expression) `NumberedSongs` để đánh số thứ tự bài hát trong mỗi playlist.
    /// - Chỉ lấy tối đa 3 bài hát đầu tiên (hoặc không có bài hát nào nếu playlist rỗng).
    /// - Kết quả được nhóm lại bằng từ điển `playlistDictionary`.
    /// </remarks>
    public static List<PlaylistCardData> GetAllPlaylistsWithPreviews()
    {
        const string query = @"
            WITH NumberedSongs AS (
                SELECT 
                    p.PlaylistID, p.PlaylistName, p.PlaylistImage, s.SongName, s.Artist,
                    ROW_NUMBER() OVER(PARTITION BY p.PlaylistID ORDER BY ps.OrderIndex) as rn
                FROM dbo.Playlist p
                LEFT JOIN dbo.PlaylistSong ps ON p.PlaylistID = ps.PlaylistID
                LEFT JOIN dbo.Song s ON ps.SongID = s.SongID
            )
            SELECT PlaylistID, PlaylistName, PlaylistImage, SongName, Artist
            FROM NumberedSongs
            WHERE rn <= 3 OR SongName IS NULL;";

        // Từ điển để nhóm các bài hát theo playlist
        var playlistDictionary = new Dictionary<int, PlaylistCardData>();

        ExecuteQuery<PlaylistCardData>(query, cmd => { }, reader =>
        {
            int id = reader.GetInt32("PlaylistID");
            if (!playlistDictionary.ContainsKey(id))
            {
                // Tạo một playlist mới nếu chưa tồn tại trong từ điển
                playlistDictionary[id] = new PlaylistCardData
                {
                    PlaylistID = id,
                    PlaylistName = reader.GetString("PlaylistName"),
                    PlaylistImage = reader.GetString("PlaylistImage")
                };
            }

            // Thêm bài hát xem trước nếu có
            if (reader["SongName"] != DBNull.Value)
            {
                string preview = $"{reader.GetString("SongName")} - {reader.GetString("Artist")}";
                playlistDictionary[id].SongPreviews.Add(preview);
            }

            return playlistDictionary[id];
        });

        // Trả về danh sách playlist
        return playlistDictionary.Values.ToList();
    }

    /// <summary>
    /// Tìm kiếm bài hát theo từ khóa.
    /// </summary>
    public static List<Song> SearchSongs(string searchTerm)
    {
        const string query = "SELECT * FROM dbo.Song WHERE SongName LIKE @SearchTerm OR Artist LIKE @SearchTerm";
        return ExecuteQuery(query, cmd =>
        {
            cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
        }, reader => new Song
        {
            SongID = reader.GetInt32("SongID"),
            SongName = reader.GetString("SongName"),
            Artist = reader.GetString("Artist"),
            CoverPath = PathHelper.GetAbsoluteCoverPath(reader.GetString("CoverPath")),
            FilePath = PathHelper.GetAbsoluteMusicPath(reader.GetString("FilePath"))
        });
    }

    public static bool DeleteSong(int songId)
    {
        const string query = "DELETE FROM dbo.Song WHERE SongID = @ID";
        return ExecuteNonQuery(query, cmd =>
        {
            cmd.Parameters.AddWithValue("@ID", songId);
        }) > 0;
    }

    public static bool DeletePlaylist(int playlistId)
    {
        const string query = "DELETE FROM dbo.Playlist WHERE PlaylistID = @ID";
        return ExecuteNonQuery(query, cmd =>
        {
            cmd.Parameters.AddWithValue("@ID", playlistId);
        }) > 0;
    }

    public static bool SaveSong(Song song)
    {
        const string query = @"
    IF EXISTS (SELECT 1 FROM dbo.Song WHERE SongID = @ID)
        UPDATE dbo.Song
        -- THÊM FullLyric VÀO ĐÂY
        SET SongName = @Name, Artist = @Artist, Duration = @Duration, FilePath = @FilePath, CoverPath = @CoverPath, FullLyric = @Lyric
        WHERE SongID = @ID
    ELSE
        -- THÊM FullLyric VÀO ĐÂY
        INSERT INTO dbo.Song (SongName, Artist, Duration, FilePath, CoverPath, FullLyric)
        VALUES (@Name, @Artist, @Duration, @FilePath, @CoverPath, @Lyric)";

        return ExecuteNonQuery(query, cmd =>
        {
            cmd.Parameters.AddWithValue("@ID", song.SongID);
            cmd.Parameters.AddWithValue("@Name", song.SongName);
            cmd.Parameters.AddWithValue("@Artist", song.Artist);
            cmd.Parameters.AddWithValue("@Duration", song.Duration);
            cmd.Parameters.AddWithValue("@FilePath", song.FilePath);
            cmd.Parameters.AddWithValue("@CoverPath", song.CoverPath);
            // VÀ THÊM THAM SỐ @Lyric
            cmd.Parameters.AddWithValue("@Lyric", (object)song.FullLyric ?? DBNull.Value);
        }) > 0;
    }

    /// <summary>
    /// Lưu thông tin playlist và danh sách bài hát của nó.
    /// </summary>
    /// <param name="playlist">Đối tượng playlist cần lưu.</param>
    /// <param name="songIds">Danh sách ID bài hát thuộc playlist.</param>
    /// <returns>Trả về `true` nếu lưu thành công, ngược lại `false`.</returns>
    /// <remarks>
    /// - Sử dụng transaction để đảm bảo tính toàn vẹn dữ liệu.
    /// - Gồm 3 bước chính:
    ///   1. Cập nhật hoặc thêm mới playlist (`UpsertPlaylist`).
    ///   2. Xóa các bài hát cũ trong playlist (`DeletePlaylistSongs`).
    ///   3. Thêm các bài hát mới vào playlist (`InsertPlaylistSongs`).
    /// </remarks>
    public static bool SavePlaylist(Playlist playlist, List<int> songIds)
    {
        using (var conn = DatabaseHelper.GetConnection())
        {
            conn.Open();
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // 1. Cập nhật hoặc thêm mới playlist
                    int playlistId = UpsertPlaylist(playlist, conn, transaction);
                    playlist.PlaylistID = playlistId;

                    // 2. Xóa các bài hát cũ trong playlist
                    DeletePlaylistSongs(playlistId, conn, transaction);

                    // 3. Thêm các bài hát mới vào playlist
                    if (songIds != null && songIds.Any())
                    {
                        InsertPlaylistSongs(playlistId, songIds, conn, transaction);
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }

    private static int UpsertPlaylist(Playlist playlist, SqlConnection conn, SqlTransaction transaction)
    {
        const string upsertSql = @"
IF EXISTS (SELECT 1 FROM dbo.Playlist WHERE PlaylistID = @ID)
    UPDATE dbo.Playlist
    SET PlaylistName = @Name, PlaylistImage = @Image
    WHERE PlaylistID = @ID;
ELSE
    INSERT INTO dbo.Playlist (PlaylistName, PlaylistImage)
    VALUES (@Name, @Image);
-- Return the ID (existing or newly generated)
SELECT ISNULL(@ID, SCOPE_IDENTITY());";

        using (var cmd = new SqlCommand(upsertSql, conn, transaction))
        {
            cmd.Parameters.AddWithValue("@ID", playlist.PlaylistID > 0 ? (object)playlist.PlaylistID : DBNull.Value);
            cmd.Parameters.AddWithValue("@Name", playlist.PlaylistName);
            cmd.Parameters.AddWithValue("@Image", (object)playlist.PlaylistImage ?? DBNull.Value);

            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }

    private static void DeletePlaylistSongs(int playlistId, SqlConnection conn, SqlTransaction transaction)
    {
        const string deleteSql = "DELETE FROM dbo.PlaylistSong WHERE PlaylistID = @PlaylistID";
        using (var cmd = new SqlCommand(deleteSql, conn, transaction))
        {
            cmd.Parameters.AddWithValue("@PlaylistID", playlistId);
            cmd.ExecuteNonQuery();
        }
    }

    private static void InsertPlaylistSongs(int playlistId, List<int> songIds, SqlConnection conn, SqlTransaction transaction)
    {
        const string insertSql =
            "INSERT INTO dbo.PlaylistSong (PlaylistID, SongID, OrderIndex) VALUES (@PlaylistID, @SongID, @OrderIndex)";

        for (int i = 0; i < songIds.Count; i++)
        {
            using (var cmd = new SqlCommand(insertSql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@PlaylistID", playlistId);
                cmd.Parameters.AddWithValue("@SongID", songIds[i]);
                cmd.Parameters.AddWithValue("@OrderIndex", i);
                cmd.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Kiểm tra xem filePath đã tồn tại trong bảng Song (ngoại trừ songId hiện tại, nếu có).
    /// </summary>
    public static bool FilePathExists(string filePath, int? excludeSongId = null)
    {
        string query = excludeSongId.HasValue
            ? "SELECT COUNT(1) FROM dbo.Song WHERE FilePath = @FilePath AND SongID <> @ID"
            : "SELECT COUNT(1) FROM dbo.Song WHERE FilePath = @FilePath";

        return ExecuteScalar<int>(query, cmd =>
        {
            cmd.Parameters.AddWithValue("@FilePath", filePath);
            if (excludeSongId.HasValue)
                cmd.Parameters.AddWithValue("@ID", excludeSongId.Value);
        }) > 0;
    }

    /// <summary>
    /// Thực thi truy vấn SQL và trả về danh sách kết quả.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối tượng trả về.</typeparam>
    /// <param name="query">Câu lệnh SQL cần thực thi.</param>
    /// <param name="parameterize">Hàm để thêm tham số vào câu lệnh SQL.</param>
    /// <param name="map">Hàm ánh xạ từ `SqlDataReader` sang đối tượng kiểu `T`.</param>
    /// <returns>Danh sách các đối tượng kiểu `T`.</returns>
    /// <remarks>
    /// - Hàm này giúp tái sử dụng mã khi thực thi các truy vấn SQL.
    /// - Sử dụng `try-catch` để xử lý lỗi và hiển thị thông báo lỗi.
    /// </remarks>
    private static List<T> ExecuteQuery<T>(string query, Action<SqlCommand> parameterize, Func<SqlDataReader, T> map)
    {
        var results = new List<T>();
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            parameterize?.Invoke(cmd);
            try
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(map(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Lỗi khi thực thi truy vấn: " + ex.Message);
            }
        }
        return results;
    }

    /// <summary>
    /// Thực thi truy vấn trả về một đối tượng duy nhất.
    /// </summary>
    private static T ExecuteQuerySingle<T>(string query, Action<SqlCommand> parameterize, Func<SqlDataReader, T> map)
    {
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            parameterize?.Invoke(cmd);
            try
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return map(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Lỗi khi thực thi truy vấn: " + ex.Message);
            }
        }
        return default;
    }

    private static int ExecuteNonQuery(string query, Action<SqlCommand> parameterize)
    {
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            parameterize?.Invoke(cmd);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }

    private static T ExecuteScalar<T>(string query, Action<SqlCommand> parameterize)
    {
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            parameterize?.Invoke(cmd);
            conn.Open();
            return (T)cmd.ExecuteScalar();
        }
    }
}
