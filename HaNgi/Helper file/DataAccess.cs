using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;
using HaNgi;

public class PlaylistCardData
{
    public int PlaylistID { get; set; }
    public string PlaylistName { get; set; }
    public string PlaylistImage { get; set; }
    public List<string> SongPreviews { get; set; } = new List<string>();
}

public static class DataAccess
{
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

    public static List<PlaylistCardData> GetAllPlaylistsWithPreviews()
    {
        var playlistDictionary = new Dictionary<int, PlaylistCardData>();

        string query = @"
        WITH NumberedSongs AS (
            SELECT 
                p.PlaylistID, 
                p.PlaylistName, 
                p.PlaylistImage, 
                s.SongName, 
                s.Artist,
                ROW_NUMBER() OVER(PARTITION BY p.PlaylistID ORDER BY ps.OrderIndex) as rn
            FROM 
                dbo.Playlist p
            LEFT JOIN 
                dbo.PlaylistSong ps ON p.PlaylistID = ps.PlaylistID
            LEFT JOIN 
                dbo.Song s ON ps.SongID = s.SongID
        )
        SELECT 
            PlaylistID, 
            PlaylistName, 
            PlaylistImage, 
            SongName, 
            Artist
        FROM 
            NumberedSongs
        WHERE 
            rn <= 3 OR SongName IS NULL;
        ";

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
            MessageBox.Show("Lỗi khi tải danh sách playlist: " + ex.Message, "Lỗi Tầng Dữ Liệu");
        }

        return playlistDictionary.Values.ToList();
    }

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
                Console.WriteLine($"Lỗi khi lấy preview cho PlaylistID {playlistId}: {ex.Message}");
            }
        }
        return list;
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
            MessageBox.Show("Lỗi khi tìm kiếm bài hát: " + ex.Message, "Lỗi Tầng Dữ Liệu");
        }
        return songList;
    }

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
}