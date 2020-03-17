using Caliburn.Micro;
using Notes.Services.Contracts;
using System.Threading.Tasks;

namespace Notes.Core.ViewModels
{
    public class CreateNoteViewModel : Screen
    {
        private string _title;
        private string _description;
        private string _isFavorite = "False";
        private readonly INotesProvider _notesProvider;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }
        
        public CreateNoteViewModel(INotesProvider notesProvider)
        {
            _notesProvider = notesProvider;
        }

        public async Task SaveNoteButtonAsync()
        {
            await _notesProvider.SaveNotesAsync(Title, Description, _isFavorite);
        }
    }
}
