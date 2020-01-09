using musicApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace musicApp.Services
{
    class SongService : ISongService
    {
        private readonly string APILOADSONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        private readonly string APILOADMYSONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-mine";
        private readonly string APIPOSTSONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        private static string CONTENT_TYPE = "application/json";


        public ObservableCollection<Song> LoadSongs(string token)
        {

            Task<ObservableCollection<Song>> list = Task.Run(async () => await LoadSongAPI(token));
            return list.Result;
        }

        public async Task<ObservableCollection<Song>> LoadSongAPI(string token)
        {            
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = await httpClient.GetAsync(APILOADSONG);
    
            var stringContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ObservableCollection<Song>>(stringContent);
            return data;
        }

        public ObservableCollection<Song> LoadMySongs(string token)
        {

            Task<ObservableCollection<Song>> list = Task.Run(async () => await LoadMySongAPI(token));
            return list.Result;
        }

        public async Task<ObservableCollection<Song>> LoadMySongAPI(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = await httpClient.GetAsync(APILOADMYSONG);

            var stringContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ObservableCollection<Song>>(stringContent);

            return data;
        }

        public bool RegisterSong(Song song, string token)
        {
            Task<bool> status = Task.Run(async () => await RegisterSongAPI(song, token));
            return status.Result;
        }

        
        public async Task<bool> RegisterSongAPI(Song song, string token)
        {
            var songJson = JsonConvert.SerializeObject(song);
            HttpContent contentToSend = new StringContent(songJson, Encoding.UTF8, CONTENT_TYPE);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = await httpClient.PostAsync(APIPOSTSONG, contentToSend);
            var stringContent = await response.Content.ReadAsStringAsync();
            string status = JObject.Parse(stringContent)["status"].ToString();
            if (status.Equals("1"))
            {
                return true;
            }
            return false;
        }
        
    }
   
}
