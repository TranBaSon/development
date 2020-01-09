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
using Windows.Media.Core;
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
    public sealed partial class MySong : Page
    {
        private ISongService _songService;
        private Song currentSong;
        private bool _isPlaying = false;

        [Obsolete]
        public MySong()
        {
            this.InitializeComponent();
        
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        [Obsolete]
        private void timer_Tick(object sender, object e)
        {
            if(MyPlayer.Source != null )
            {
                timelineSlider.Minimum = 0;
                timelineSlider.Maximum = MyPlayer.MediaPlayer.NaturalDuration.TotalSeconds;
                timelineSlider.Value = MyPlayer.MediaPlayer.Position.TotalSeconds;

                totalTime.Text = MyPlayer.MediaPlayer.NaturalDuration.ToString(@"mm\:ss");
                currentTime.Text = MyPlayer.MediaPlayer.Position.ToString(@"mm\:ss");
                
            }
        }

        [Obsolete]
        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            _songService = new SongService();
            Songs.ItemsSource = null;
            Songs.ItemsSource = _songService.LoadMySongs(App.token);
            
        }

        [Obsolete]
        private void Songs_OnItemClick(object sender, ItemClickEventArgs e)
        {
            currentSong = e.ClickedItem as Song;
            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            MyPlayer.MediaPlayer.Play();
            PlayButton.Icon = new SymbolIcon(Symbol.Pause);
            _isPlaying = true;
            StatusText.Text = "Now Playing: " + currentSong.name;
            Debug.WriteLine(MyPlayer.MediaPlayer.Position.TotalMilliseconds.ToString());
        }

        [Obsolete]
        private void PlayButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (Songs.ItemsSource == null) return;
            if (currentSong == null)
            {
                currentSong = _songService.LoadMySongs(App.token).FirstOrDefault();
                MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
                Songs.SelectedIndex = 0;
            }

            if (_isPlaying)
            {
                MyPlayer.MediaPlayer.Pause();
                PlayButton.Icon = new SymbolIcon(Symbol.Play);
                StatusText.Text = "Paused";
                _isPlaying = false;
            }
            else
            {
                MyPlayer.MediaPlayer.Play();
                PlayButton.Icon = new SymbolIcon(Symbol.Pause);
                StatusText.Text = "Now Playing: " + currentSong.name;
                _isPlaying = true;
            }

        }

        private void Next_OnClick(object sender, RoutedEventArgs e)
        {
            var currentIndex = Songs.SelectedIndex;
            currentIndex++;
            if (currentIndex >= Songs.Items.Count)
            {
                currentIndex = 0;
            }
            currentSong = Songs.Items[currentIndex] as Song;
            Songs.SelectedIndex = currentIndex;
            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            MyPlayer.MediaPlayer.Play();
            PlayButton.Icon = new SymbolIcon(Symbol.Pause);
            _isPlaying = true;
            StatusText.Text = "Now Playing: " + currentSong.name;
        }

        private void Previous_OnClick(object sender, RoutedEventArgs e)
        {
            var currentIndex = Songs.SelectedIndex;
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = Songs.Items.Count - 1;
            }
            currentSong = Songs.Items[currentIndex] as Song;
            Songs.SelectedIndex = currentIndex;
            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            MyPlayer.MediaPlayer.Play();
            PlayButton.Icon = new SymbolIcon(Symbol.Pause);
            _isPlaying = true;
            StatusText.Text = "Now Playing: " + currentSong.name;
        }

        [Obsolete]
        private void timelineSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(MyPlayer.Source != null)
            {
                MyPlayer.MediaPlayer.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            }
        }

      
    }
}

