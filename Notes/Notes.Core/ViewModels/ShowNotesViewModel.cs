using Caliburn.Micro;
using Models;
using Notes.Core.Contracts;
using Notes.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Core.ViewModels
{
    public class ShowNotesViewModel : Screen
    {
        private readonly INotesProvider _notesProvider;
        private readonly IInteractionService _interactionService;
        private const string _oldest = "Oldest";
        private const string _newest = "Newest";
        private string _selectedItem;
        private bool _favorite;
        private NotesModel _selected;
        private NotesModel _checked;
        private List<NotesModel> _notes;

        public List<string> ComboItems { get; set; } = new List<string>
        {
            _oldest,
            _newest
        };

        public List<NotesModel> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                NotifyOfPropertyChange(nameof(Notes));
            }
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _selectedItem = value;
                    SortByOldest(value);
                    NotifyOfPropertyChange(() => SelectedItem);
                }
            }
        }

        public bool Favorite
        {
            get { return _favorite; }
            set
            {
                _favorite = value;
                NotifyOfPropertyChange(() => Favorite);
            }
        }

        public NotesModel Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                NotifyOfPropertyChange(() => Selected);
            }
        }

        public NotesModel Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                SetChecked();
                NotifyOfPropertyChange(() => Checked);
            }
        }

        public ShowNotesViewModel(INotesProvider notesProvider, IInteractionService interactionService)
        {
            _notesProvider = notesProvider;
            _interactionService = interactionService;

            _interactionService.ItemClicked += SetFavorite;
        }

        public async Task GetNotesDataAsync()
        {
            var notes = await _notesProvider.GetNotesDataAsync();

            if (notes != null)
            {
                Notes = new List<NotesModel>(notes);
            }
        }

        public void SelectNote()
        {
            SetSelectedItems(true);
        }

        public void CancelEditing()
        {
            SetSelectedItems(false);
        }

        public async Task DeleteNote()
        {
            var notes = _notes.Where(x => x.Checked);

            foreach (var note in notes)
            {
                await _notesProvider.DeleteNotesAsync(note.Title);
            }

            _notes.RemoveAll(x => x.Checked);
            Notes = new List<NotesModel>(_notes);
        }

        private void SetSelectedItems(bool isSelected)
        {
            if (Notes != null)
            {
                foreach (var item in Notes)
                {
                    item.IsSelected = isSelected;
                }
            }
        }

        private async void SetFavorite(object sender, NotesModel notes)
        {
            notes.IsFavorite = !notes.IsFavorite;
            await _notesProvider.SaveNotesAsync(notes.Title, notes.Description, notes.IsFavorite.ToString());
        }

        private void SetChecked()
        {
            _selected.Checked = true;
        }

        private void SortByOldest(string item)
        {
            if (item == "Oldest")
                Notes = Notes.OrderByDescending(x => x.CreationDate).Reverse().ToList();
            else if (item == "Newest")
                Notes = Notes.OrderByDescending(x => x.CreationDate).ToList();

        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await GetNotesDataAsync();
        }
    }
}
