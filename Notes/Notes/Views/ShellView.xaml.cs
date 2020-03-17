using Caliburn.Micro;
using Notes.Core.ViewModels;
using Notes.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Notes.Views
{
    public sealed partial class ShellView : Page
    {
        private readonly AppNavigationService _navigationService;
        private ShellViewModel _viewModel;

        public ShellView()
        {
            this.InitializeComponent();

            _navigationService = IoC.Get<AppNavigationService>();
        }

        private void OnRootFrameLoaded(object sender, RoutedEventArgs e)
        {
            _navigationService.SetShellFrame(SplitViewFrame);
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            _viewModel = DataContext as ShellViewModel;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!MySplitView.IsPaneOpen)
            {
                MySplitView.IsPaneOpen = true;
            }
            else
                MySplitView.IsPaneOpen = false;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Create.IsSelected)
            {
                SplitViewFrame.Navigate(typeof(CreateNoteView));
            }
            else if (Favorite.IsSelected)
            {
                SplitViewFrame.Navigate(typeof(FavoriteNotesView));
            }
            else if (Show.IsSelected)
            {
                SplitViewFrame.Navigate(typeof(ShowNotesView));
            }
        }
    }
}
