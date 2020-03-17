using Newtonsoft.Json;
using Notes.Services.Contracts;
using Notes.Services.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Services.Services
{
    public class NotesService : INotesService
    {
        private readonly ILocalStorage _localStorage;

        public NotesService(ILocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<NotesDataModel>> GetNotesDataAsync()
        {
            var data = "{data:[" + String.Join(",", await _localStorage.GetApplicationDataFromStorageAsync()) + "]}";
            if (data != null)
            {
                return JsonConvert.DeserializeObject<NotesRootObject>(data).NotesData;
            }
            return null;
        }

        public async Task SaveNotesAsync(string title, string description, string isFavorite)
        {
            var newNotesDataModel = new NotesDataModel
            {
                Title = title,
                Description = description,
                Favorite = isFavorite
            };
            var content = JsonConvert.SerializeObject(newNotesDataModel);
            await _localStorage.SaveFilesToStorageAsync(title, content);
        }

        public async Task DeleteNotesAsync(string name)
        {
            await _localStorage.DeleteFilesAsync(name);
        }
    }
}
