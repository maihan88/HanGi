using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Sunny.UI;

namespace HaNgi
{
    public partial class FormEditPlaylist : Sunny.UI.UIForm
    {
        private class SongListItem
        {
            public int SongID { get; set; }
            public string DisplayName { get; set; }
            public override string ToString() => DisplayName;
        }

        private bool isEditMode;
        private int editingPlaylistId;
        private string currentAbsoluteCoverPath;
        private string originalAbsoluteCoverPath;

        public FormEditPlaylist(int? playlistId = null)
        {
            InitializeComponent();
            if (playlistId.HasValue)
            {
                isEditMode = true;
                editingPlaylistId = playlistId.Value;
                this.Text = "Chỉnh sửa Playlist";
                LoadDataForEditing();
            }
            else
            {
                isEditMode = false;
                this.Text = "Tạo Playlist Mới";
                LoadAllSongsForNewPlaylist();
            }
        }

        private void LoadAllSongsForNewPlaylist()
        {
            uiTransferSongs.ItemsLeft.Clear();
            var allSongs = DataAccess.GetAllSongs(); // Lấy danh sách Song cơ bản
            foreach (var song in allSongs)
            {
                uiTransferSongs.ItemsLeft.Add(new SongListItem
                {
                    SongID = song.SongID,
                    DisplayName = $"{song.SongName} - {song.Artist}"
                });
            }
        }

        private void LoadDataForEditing()
        {
            var playlist = DataAccess.GetPlaylistById(editingPlaylistId);
            if (playlist == null)
            {
                UIMessageBox.ShowError("Không tìm thấy playlist để chỉnh sửa.");
                this.Close();
                return;
            }

            txtPlaylistName.Text = playlist.PlaylistName;
            currentAbsoluteCoverPath = playlist.PlaylistImage;
            originalAbsoluteCoverPath = currentAbsoluteCoverPath;
            LoadPreviewImage(currentAbsoluteCoverPath);

            var allSongs = DataAccess.GetAllSongs().Select(s => new SongListItem { SongID = s.SongID, DisplayName = $"{s.SongName} - {s.Artist}" }).ToList();
            var songsInPlaylist = DataAccess.GetSongsByPlaylistId(editingPlaylistId).Select(s => new SongListItem { SongID = s.SongID, DisplayName = $"{s.SongName} - {s.Artist}" }).ToList();

            var songsNotInPlaylist = allSongs.Where(s => !songsInPlaylist.Any(ps => ps.SongID == s.SongID)).ToList();

            uiTransferSongs.ItemsLeft.Clear();
            foreach (var s in songsNotInPlaylist) uiTransferSongs.ItemsLeft.Add(s);

            uiTransferSongs.ItemsRight.Clear();
            foreach (var s in songsInPlaylist) uiTransferSongs.ItemsRight.Add(s);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaylistName.Text))
            {
                UIMessageBox.ShowWarning("Tên playlist không được để trống!");
                return;
            }

            string coverFileNameToSave = "";
            if (currentAbsoluteCoverPath != originalAbsoluteCoverPath && !string.IsNullOrEmpty(currentAbsoluteCoverPath))
            {
                coverFileNameToSave = PathHelper.CopyFileToAppFolder(currentAbsoluteCoverPath, PathHelper.CoversFolderPath);
                if (string.IsNullOrEmpty(coverFileNameToSave)) return; // Lỗi sao chép file
            }
            else if (!string.IsNullOrEmpty(originalAbsoluteCoverPath))
            {
                coverFileNameToSave = Path.GetFileName(originalAbsoluteCoverPath);
            }

            var playlistToSave = new Playlist
            {
                PlaylistID = this.editingPlaylistId,
                PlaylistName = txtPlaylistName.Text,
                PlaylistImage = coverFileNameToSave
            };

            var songIdsInPlaylist = uiTransferSongs.ItemsRight.Cast<SongListItem>().Select(s => s.SongID).ToList();

            if (DataAccess.SavePlaylist(playlistToSave, songIdsInPlaylist, isEditMode))
            {
                UIMessageBox.ShowSuccess("Lưu playlist thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // --- CÁC HÀM KHÁC GIỮ NGUYÊN ---
        #region Other Event Handlers and Helper Methods
        private void FormEditPlaylist_Load(object sender, EventArgs e) { }

        private void btnSelectCover_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    currentAbsoluteCoverPath = ofd.FileName;
                    LoadPreviewImage(currentAbsoluteCoverPath);
                }
            }
        }

        private void LoadPreviewImage(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath) && File.Exists(absolutePath))
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(absolutePath);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        avatarPreview.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError("Không thể đọc file ảnh: " + ex.Message);
                    avatarPreview.Image = null;
                }
            }
            else
            {
                avatarPreview.Image = null;
            }
        }

        private void btnUp_Click(object sender, EventArgs e) => MoveItemInPlaylist(-1);
        private void btnDown_Click(object sender, EventArgs e) => MoveItemInPlaylist(1);

        private void MoveItemInPlaylist(int direction)
        {
            var listBox = uiTransferSongs.ListBoxRight;
            if (listBox.SelectedItem == null || listBox.SelectedIndex < 0) return;

            int newIndex = listBox.SelectedIndex + direction;
            if (newIndex < 0 || newIndex >= listBox.Items.Count) return;

            object selected = listBox.SelectedItem;
            listBox.Items.Remove(selected);
            listBox.Items.Insert(newIndex, selected);
            listBox.SetSelected(newIndex, true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
