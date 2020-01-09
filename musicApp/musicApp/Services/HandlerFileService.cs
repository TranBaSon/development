using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace musicApp.Services
{
    public class HandlerFileService
    {
        public static async Task<string> ReadFile(string fileName)
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var sampleFile = await storageFolder.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.OpenIfExists);
            return await FileIO.ReadTextAsync(sampleFile);
        }

        public static async Task<bool> WriteFile(string fileName, string content) 
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var sampleFile = await storageFolder.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, content);
            return true;
        }
    }
}
