using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Services.Contracts
{
    public interface ILocalStorage
    {
        Task<List<string>> GetApplicationDataFromStorageAsync();
        Task SaveFilesToStorageAsync(string title, string content);
        Task DeleteFilesAsync(string name);
    }
}
