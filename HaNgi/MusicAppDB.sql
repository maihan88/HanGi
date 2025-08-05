USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MusicApp')
BEGIN
    ALTER DATABASE MusicApp SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE MusicApp;
END
GO

CREATE DATABASE MusicApp
ON PRIMARY (
    NAME = N'MusicApp_Data',
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MusicApp_Data.mdf',
    SIZE = 100MB,
    MAXSIZE = 1024MB,
    FILEGROWTH = 50MB
)
LOG ON (
    NAME = N'MusicApp_Log',
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MusicApp_Log.ldf',
    SIZE = 50MB,
    MAXSIZE = 256MB,
    FILEGROWTH = 25MB
);

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