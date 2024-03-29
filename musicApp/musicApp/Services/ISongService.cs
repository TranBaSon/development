﻿using musicApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace musicApp.Services
{
    interface ISongService
    {
        ObservableCollection<Song> LoadSongs(string token);
        ObservableCollection<Song> LoadMySongs(string token);
        bool RegisterSong(Song song, string token);
        Task<string> postFile(StorageFile file);
    }
}
