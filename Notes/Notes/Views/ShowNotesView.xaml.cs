using Caliburn.Micro;
using Models;
using Notes.Core.Contracts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Notes.Views
{
    public sealed partial class ShowNotesView : Page
    {
        private readonly IInteractionService _interactionService;

        public ShowNotesView()
        {
            InitializeComponent();
            _interactionService = IoC.Get<IInteractionService>();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            SelectNote.Visibility = Visibility.Collapsed;
            CancelEditing.Visibility = Visibility.Visible;
            DeleteNote.Visibility = Visibility.Visible;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SelectNote.Visibility = Visibility.Visible;
            CancelEditing.Visibility = Visibility.Collapsed;
            DeleteNote.Visibility = Visibility.Collapsed;
        }

        private void Image_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var originalSource = (Image)e.OriginalSource;
            var noteFavorite = (NotesModel)originalSource.DataContext;
            _interactionService.ItemClicked(sender, noteFavorite);
        }
    }
}
