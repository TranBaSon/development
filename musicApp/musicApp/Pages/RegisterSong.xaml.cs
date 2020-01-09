using musicApp.Models;
using musicApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public RegisterSong()
        {
            this.InitializeComponent();
            _service = new SongService();
        }

        private void Button_Register(object sender, RoutedEventArgs e)
        {
            var song = new Song
            {
                name = songName.Text,
                author = author.Text,
                singer = singerName.Text,
                thumbnail = thumbnail.Text,
                link = link.Text,
            };
            var errors = song.CheckValidate();

            if (errors.Count > 0)
            {
                ErSongName.Text = errors.ContainsKey("Name") ? errors["Name"] : "";
                ErrSingerName.Text = errors.ContainsKey("Singer") ? errors["Singer"] : "";
                ErrLink.Text = errors.ContainsKey("Link") ? errors["Link"] : "";
                ErrAuthor.Text = errors.ContainsKey("Author") ? errors["Author"] : "";
                ErrThumbnail.Text = errors.ContainsKey("Thumbnail") ? errors["Thumbnail"] : "";
            }
            else {
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
    }
}
