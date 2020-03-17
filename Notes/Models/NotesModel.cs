using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models
{
    public class NotesModel : INotifyPropertyChanged
    {
        private bool _isFavorite;
        private bool _isSelected;
        private bool _checked;

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreationDate { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                _isFavorite = value;
                NotifyPropertyChanged(nameof(IsFavorite));
            }
        }

        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                NotifyPropertyChanged(nameof(Checked));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
