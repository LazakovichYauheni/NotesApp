using Models;
using Notes.Services.Contracts;
using Notes.Services.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Notes.Services.Services
{
    public class NotesProvider : INotesProvider
    {
        private StorageFolder _folder;
        private readonly INotesService _notesService;

        private List<NotesModel> _notesList { get; set; } = new List<NotesModel>();

        public NotesProvider(INotesService notesService)
        {
            _notesService = notesService;
            _folder = ApplicationData.Current.LocalFolder;
        }

        public async Task<List<NotesModel>> GetNotesDataAsync()
        {
            _notesList.Clear();
            var data = await _notesService.GetNotesDataAsync();

            if (data != null && data.Any())
            {
                foreach (var note in data)
                {
                    _notesList.Add(GetNotes(note));
                }
            }
            return _notesList;
        }

        public async Task SaveNotesAsync(string title, string description, string isFavorite)
        {
            await _notesService.SaveNotesAsync(title, description, isFavorite);
        }

        public async Task DeleteNotesAsync(string name)
        {
            await _notesService.DeleteNotesAsync(name);
        }

        public async Task<IEnumerable<NotesModel>> GetFavoriteList()
        {
            return _notesList.Where(x => x.IsFavorite);
        }

        private NotesModel GetNotes(NotesDataModel notesData)
        {
            return new NotesModel()
            {
                Title = notesData.Title,
                Description = notesData.Description,
                CreationDate = System.IO.File.GetLastWriteTime(_folder.Path + "\\" + notesData.Title).ToString("dd MM H:mm:ss"),
                IsFavorite = Convert.ToBoolean(notesData.Favorite)
            };
        }
    }
}
