using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Notes.Services.Contracts;

namespace Notes.Infrastructure
{
    public class LocalStorage : ILocalStorage
    {
        private readonly Windows.Storage.StorageFolder _localFolder;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public List<string> FilesList { get; set; }

        public LocalStorage()
        {
            _localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            FilesList = new List<string>();
        }

        public async Task<List<string>> GetApplicationDataFromStorageAsync()
        {
            FilesList.Clear();
            var dataFile = await _localFolder.GetFilesAsync();
            foreach (var data in dataFile)
            {
                var text = await Windows.Storage.FileIO.ReadTextAsync(data);
                FilesList.Add(text);
            }
            return FilesList;
        }

        public async Task SaveFilesToStorageAsync(string title, string content)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var file = await _localFolder.CreateFileAsync(title, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                await Windows.Storage.FileIO.WriteTextAsync(file, content);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task DeleteFilesAsync(string name)
        {
            var file = await _localFolder.GetFileAsync(name);
            await file.DeleteAsync();
        }
    }
}
