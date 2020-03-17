using Models;
using Caliburn.Micro;
using Notes.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Core.ViewModels
{
    public class FavoriteNotesViewModel : Screen
    {
        private readonly INotesProvider _notesProvider;
        private List<NotesModel> _favoriteNotes;
        public FavoriteNotesViewModel(INotesProvider notesProvider)
        {
            _notesProvider = notesProvider;
        }

        public List<NotesModel> FavoriteNotes
        {
            get { return _favoriteNotes; }
            set
            {
                _favoriteNotes = value;
                NotifyOfPropertyChange(() => FavoriteNotes);
            }
        }

        public async Task GetFavoriteNotesAsync()
        {
            var favoriteNotes = await _notesProvider.GetFavoriteList();
            if (favoriteNotes != null)
            {
                FavoriteNotes = new List<NotesModel>(favoriteNotes);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await GetFavoriteNotesAsync();
        }

    }
}
