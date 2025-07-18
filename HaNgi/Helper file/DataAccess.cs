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
            // Sửa lỗi: Xóa tham số thứ hai không hợp lệ
            UIMessageBox.ShowError("Lỗi nghiêm trọng khi lấy dữ liệu bài hát: " + ex.Message);
        }
        return songList;
    }

    public static Song GetSongById(int songId)
    {
        Song song = null;
        string query = "SELECT * FROM dbo.Song WHERE SongID = @ID";
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@ID", songId);
            try
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        song = new Song
                        {
                            SongID = songId,
                            SongName = reader["SongName"].ToString(),
                            Artist = reader["Artist"].ToString(),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            FilePath = PathHelper.GetAbsoluteMusicPath(reader["FilePath"].ToString()),
                            CoverPath = PathHelper.GetAbsoluteCoverPath(reader["CoverPath"].ToString()),
                            FullLyric = reader["FullLyric"] == DBNull.Value ? "" : reader["FullLyric"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex) { UIMessageBox.ShowError("Lỗi lấy thông tin bài hát: " + ex.Message); }
        }
        return song;
    }

    public static List<Song> GetSongsByPlaylistId(int playlistId)
    {
        var list = new List<Song>();
        string query = @"SELECT s.* FROM dbo.Song s
                         JOIN dbo.PlaylistSong ps ON s.SongID = ps.SongID
                         WHERE ps.PlaylistID = @ID ORDER BY ps.OrderIndex";
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
                        list.Add(new Song
                        {
                            SongID = reader.GetInt32(reader.GetOrdinal("SongID")),
                            SongName = reader["SongName"].ToString(),
                            Artist = reader["Artist"].ToString(),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            FilePath = PathHelper.GetAbsoluteMusicPath(reader["FilePath"].ToString()),
                            CoverPath = PathHelper.GetAbsoluteCoverPath(reader["CoverPath"].ToString()),
                            FullLyric = reader["FullLyric"] == DBNull.Value ? "" : reader["FullLyric"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Lỗi tải các bài hát trong playlist: " + ex.Message);
            }
        }
        return list;
    }

    public static Playlist GetPlaylistById(int playlistId)
    {
        Playlist playlist = null;
        string query = "SELECT PlaylistID, PlaylistName, PlaylistImage FROM dbo.Playlist WHERE PlaylistID = @ID";
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@ID", playlistId);
            try
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        playlist = new Playlist
                        {
                            PlaylistID = playlistId,
                            PlaylistName = reader["PlaylistName"].ToString(),
                            PlaylistImage = PathHelper.GetAbsoluteCoverPath(reader["PlaylistImage"].ToString())
                        };
                    }
                }
            }
            catch (Exception ex) { UIMessageBox.ShowError("Lỗi tải thông tin playlist: " + ex.Message); }
        }
        return playlist;
    }

    public static List<PlaylistCardData> GetAllPlaylistsWithPreviews()
    {
        var playlistDictionary = new Dictionary<int, PlaylistCardData>();
        string query = @"
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

        try
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["PlaylistID"]);
                        if (!playlistDictionary.ContainsKey(id))
                        {
                            playlistDictionary[id] = new PlaylistCardData
                            {
                                PlaylistID = id,
                                PlaylistName = reader["PlaylistName"].ToString(),
                                PlaylistImage = reader["PlaylistImage"].ToString()
                            };
                        }

                        if (reader["SongName"] != DBNull.Value)
                        {
                            string preview = $"{reader["SongName"]} - {reader["Artist"]}";
                            playlistDictionary[id].SongPreviews.Add(preview);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Sửa lỗi: Xóa tham số thứ hai không hợp lệ
            UIMessageBox.ShowError("Lỗi khi tải danh sách playlist: " + ex.Message);
        }
        return playlistDictionary.Values.ToList();
    }

    public static List<Song> SearchSongs(string searchTerm)
    {
        var songList = new List<Song>();
        string query = "SELECT * FROM dbo.Song WHERE SongName LIKE @SearchTerm OR Artist LIKE @SearchTerm";

        try
        {
            var dataTable = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            using (var adapter = new SqlDataAdapter(query, conn))
            {
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
                    CoverPath = PathHelper.GetAbsoluteCoverPath(row["CoverPath"].ToString()),
                    FilePath = PathHelper.GetAbsoluteMusicPath(row["FilePath"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            // Sửa lỗi: Xóa tham số thứ hai không hợp lệ
            UIMessageBox.ShowError("Lỗi khi tìm kiếm bài hát: " + ex.Message);
        }
        return songList;
    }

    //================================================================//
    // PHẦN KIỂM TRA (VALIDATION)
    //================================================================//

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

    //================================================================//
    // PHẦN LƯU DỮ LIỆU (INSERT / UPDATE)
    //================================================================//

    public static bool SaveSong(Song song, bool isEditMode)
    {
        string query = isEditMode
            ? "UPDATE dbo.Song SET SongName=@SongName, Artist=@Artist, Duration=@Duration, FilePath=@FilePath, CoverPath=@CoverPath, FullLyric=@FullLyric WHERE SongID=@SongID"
            : "INSERT INTO dbo.Song (SongName, Artist, Duration, FilePath, CoverPath, FullLyric) VALUES (@SongName, @Artist, @Duration, @FilePath, @CoverPath, @FullLyric)";

        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@SongName", song.SongName);
            cmd.Parameters.AddWithValue("@Artist", song.Artist);
            cmd.Parameters.AddWithValue("@Duration", song.Duration);
            cmd.Parameters.AddWithValue("@FilePath", System.IO.Path.GetFileName(song.FilePath));
            cmd.Parameters.AddWithValue("@CoverPath", (object)System.IO.Path.GetFileName(song.CoverPath) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FullLyric", string.IsNullOrWhiteSpace(song.FullLyric) ? (object)DBNull.Value : song.FullLyric);

            if (isEditMode)
            {
                cmd.Parameters.AddWithValue("@SongID", song.SongID);
            }

            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Lỗi khi lưu bài hát vào CSDL: " + ex.Message);
                return false;
            }
        }
    }

    public static bool SavePlaylist(Playlist playlist, List<int> songIds, bool isEditMode)
    {
        using (var conn = DatabaseHelper.GetConnection())
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {
                int playlistId = playlist.PlaylistID;
                if (!isEditMode)
                {
                    string query = "INSERT INTO dbo.Playlist (PlaylistName, PlaylistImage) OUTPUT INSERTED.PlaylistID VALUES (@Name, @Image);";
                    using (var cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Name", playlist.PlaylistName);
                        cmd.Parameters.AddWithValue("@Image", string.IsNullOrEmpty(playlist.PlaylistImage) ? (object)DBNull.Value : System.IO.Path.GetFileName(playlist.PlaylistImage));
                        playlistId = (int)cmd.ExecuteScalar();
                    }
                }
                else
                {
                    string query = "UPDATE dbo.Playlist SET PlaylistName = @Name, PlaylistImage = @Image WHERE PlaylistID = @ID;";
                    using (var cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Name", playlist.PlaylistName);
                        cmd.Parameters.AddWithValue("@Image", string.IsNullOrEmpty(playlist.PlaylistImage) ? (object)DBNull.Value : System.IO.Path.GetFileName(playlist.PlaylistImage));
                        cmd.Parameters.AddWithValue("@ID", playlistId);
                        cmd.ExecuteNonQuery();
                    }
                }

                using (var cmd = new SqlCommand("DELETE FROM dbo.PlaylistSong WHERE PlaylistID = @PlaylistID;", conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@PlaylistID", playlistId);
                    cmd.ExecuteNonQuery();
                }

                for (int i = 0; i < songIds.Count; i++)
                {
                    string insertQuery = "INSERT INTO dbo.PlaylistSong (PlaylistID, SongID, OrderIndex) VALUES (@PlaylistID, @SongID, @OrderIndex);";
                    using (var cmd = new SqlCommand(insertQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@PlaylistID", playlistId);
                        cmd.Parameters.AddWithValue("@SongID", songIds[i]);
                        cmd.Parameters.AddWithValue("@OrderIndex", i);
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                UIMessageBox.ShowError("Lỗi khi lưu playlist: " + ex.Message);
                return false;
            }
        }
    }

    //================================================================//
    // PHẦN XÓA DỮ LIỆU (DELETE)
    //================================================================//

    public static bool DeleteSong(int songId)
    {
        string query = "DELETE FROM dbo.Song WHERE SongID = @ID";
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@ID", songId);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Lỗi khi xóa bài hát khỏi CSDL: " + ex.Message);
                return false;
            }
        }
    }

    public static bool DeletePlaylist(int playlistId)
    {
        string query = "DELETE FROM dbo.Playlist WHERE PlaylistID = @ID";
        using (var conn = DatabaseHelper.GetConnection())
        using (var cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@ID", playlistId);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("Lỗi khi xóa playlist khỏi CSDL: " + ex.Message);
                return false;
            }
        }
    }
}
