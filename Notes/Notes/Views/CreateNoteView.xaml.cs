using Caliburn.Micro;
using Notes.Core.ViewModels;
using Notes.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Notes.Views
{
    public sealed partial class CreateNoteView : Page
    {
        private AppNavigationService _navigationService;
        public CreateNoteView()
        {
            InitializeComponent();
            _navigationService = IoC.Get<AppNavigationService>();
        }

        private void SaveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateToViewModel<ShowNotesViewModel>();
        }
    }
}
