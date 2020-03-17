using Caliburn.Micro;
using Notes.Core.Navigation;

namespace Notes.Core.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(IAppNavigationService navigationService)
        {
            navigationService.NavigateToViewModel<ShowNotesViewModel>();
        }
    }
}
