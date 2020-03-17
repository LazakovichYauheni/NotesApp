using Notes.Services.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Services.Contracts
{
    public interface INotesService
    {
        Task<List<NotesDataModel>> GetNotesDataAsync();
        Task SaveNotesAsync(string title, string description, string isFavorite);
        Task DeleteNotesAsync(string name);
    }
}
