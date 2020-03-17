using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Services.Contracts
{
    public interface INotesProvider
    {
        Task<List<NotesModel>> GetNotesDataAsync();
        Task SaveNotesAsync(string title, string description, string isFavorite);
        Task DeleteNotesAsync(string name);
        Task<IEnumerable<NotesModel>> GetFavoriteList();
    }
}
