USE MusicApp;
GO

CREATE TABLE dbo.Playlist (
    PlaylistID   INT IDENTITY(1,1) PRIMARY KEY,
    PlaylistName         NVARCHAR(200)     NOT NULL,
	PlaylistImage	NVARCHAR(500)	NOT NULL,
);
GO

CREATE TABLE dbo.Song (
    SongID       INT IDENTITY(1,1) PRIMARY KEY,
    SongName     NVARCHAR(200)     NOT NULL,
    Artist       NVARCHAR(200)     NOT NULL,
    Duration     INT               NOT NULL,
    FilePath     NVARCHAR(500)     NOT NULL,
    CoverPath    NVARCHAR(500)     NOT NULL,
    FullLyric    NVARCHAR(MAX)     NULL,
	AlternativeName	NVARCHAR(200)	NULL,
);
GO

CREATE TABLE dbo.PlaylistSong (
    PlaylistID   INT   NOT NULL,
    SongID       INT   NOT NULL,
    OrderIndex   INT   NOT NULL,
    CONSTRAINT PK_PlaylistSong PRIMARY KEY (PlaylistID, SongID),
    CONSTRAINT FK_PS_Playlist FOREIGN KEY (PlaylistID)
      REFERENCES dbo.Playlist(PlaylistID)
      ON DELETE CASCADE,
    CONSTRAINT FK_PS_Song FOREIGN KEY (SongID)
      REFERENCES dbo.Song(SongID)
      ON DELETE CASCADE
);
GO