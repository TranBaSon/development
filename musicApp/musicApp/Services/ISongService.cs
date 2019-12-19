using musicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicApp.Services
{
    interface ISongService
    {
        List<Song> GetSong();
        Song CreateSong(Song song);

    }
}
