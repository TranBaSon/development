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
using Windows.Storage;
using Windows.Web.Http;

namespace musicApp.Services
{
    class SongService : ISongService
    {
        private readonly string APILOADSONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        private readonly string APILOADMYSONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-mine";
        private readonly string APIPOSTSONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        private readonly string APIPOSTFILE = "https://2-dot-backup-server-003.appspot.com/upload-my-file-handle";
        private static string CONTENT_TYPE = "application/json";
        private static string CLOUDNAME = "genson1808";
        private static string APIKEY = "786551593633328";
        private static string APISECRET = "b33rVC3vCkwn2ZyxO3HPAImI9y4";


        public ObservableCollection<Song> LoadSongs(string token)
        {

            Task<ObservableCollection<Song>> list = Task.Run(async () => await LoadSongAPI(token));
            return list.Result;
        }

        public async Task<ObservableCollection<Song>> LoadSongAPI(string token)
        {            
            var httpClient = new System.Net.Http.HttpClient();
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
            var httpClient = new System.Net.Http.HttpClient();
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
            var httpClient = new System.Net.Http.HttpClient();
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

        public async Task<string> postFile(StorageFile file)
        {
            var httpClient = new Windows.Web.Http.HttpClient();
     
            var formContent = new HttpMultipartFormDataContent();
            var fileContent = new HttpStreamContent(await file.OpenReadAsync());
            fileContent.Headers.ContentType = new Windows.Web.Http.Headers.HttpMediaTypeHeaderValue("mp3");
            var apiKeyContent = new HttpStringContent(APIKEY);
            var apiSecretContent = new HttpStringContent(APISECRET);
            var cloudNameContent = new HttpStringContent(CLOUDNAME);
            formContent.Add(fileContent, "myFile", file.Name);
        
            formContent.Add(apiKeyContent, "apiKey");
            formContent.Add(apiSecretContent, "apiSecret");
            formContent.Add(cloudNameContent, "cloudName");
           
            var response = await httpClient.PostAsync(new Uri(APIPOSTFILE), formContent);
            var stringContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(stringContent);
            string url = JObject.Parse(stringContent)["url"].ToString();
            if(url.Length > 0)
            {
                return url;
            }
            return "";

        }
    }
   
}
