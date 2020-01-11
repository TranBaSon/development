using musicApp.Models;
using musicApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace musicApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterSong : Page
    {
        private ISongService _service;
        private string urlSong = "";
        StorageFile fileSlected;
        public RegisterSong()
        {
            this.InitializeComponent();
            _service = new SongService();
        }

        private async void Button_Register(object sender, RoutedEventArgs e)
        {
            var song = new Song
            {
                name = songName.Text,
                author = author.Text,
                singer = singerName.Text,
                thumbnail = thumbnail.Text,
            };
            var errors = song.CheckValidate();
            if(fileSlected == null)
            {
                errors.Add("ErrFile", "File is required!");
            }
            if (errors.Count > 0 && fileSlected == null)
            {
                ErSongName.Text = errors.ContainsKey("Name") ? errors["Name"] : "";
                ErrSingerName.Text = errors.ContainsKey("Singer") ? errors["Singer"] : "";
                ErrAuthor.Text = errors.ContainsKey("Author") ? errors["Author"] : "";
                ErrThumbnail.Text = errors.ContainsKey("Thumbnail") ? errors["Thumbnail"] : "";
                ErrFile.Text = errors.ContainsKey("ErrFile") ? errors["ErrFile"] : "";
            }
            else {
                var url = await _service.postFile(fileSlected);
                if (url.Equals(""))
                {
                    ErrFile.Text = "Error link!";
                    return;
                }
                song.link = url;
                bool status = _service.RegisterSong(song, App.token);
                if (status)
                {
                    Navigatior.GetCurrent().SetSelectedNavigationItem(3);
                }
            }

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Navigatior.GetCurrent().SetSelectedNavigationItem(2);
        }

        private async void getFile_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            //// Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("the song", new List<string>() { ".mp3" });
            //// Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";
            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Debug.WriteLine(file.ContentType);
                //var url = await _service.postFile(file);
                Debug.WriteLine("--------------------------------------");
                //Debug.WriteLine(url);
                fileName.Text = file.DisplayName;
                fileSlected = file;
            }
        }
    }
}
