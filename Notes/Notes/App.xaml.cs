using Caliburn.Micro;
using Notes.Core.Contracts;
using Notes.Core.Navigation;
using Notes.Core.ViewModels;
using Notes.Infrastructure;
using Notes.Navigation;
using Notes.Services.Contracts;
using Notes.Services.Services;
using Notes.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Notes
{
    public sealed partial class App
    {
        private WinRTContainer _container;
        public App()
        {
            Initialize();
            InitializeComponent();
        }

        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            _container.Singleton<INotesService, NotesService>();
            _container.Singleton<INotesProvider, NotesProvider>();
            _container.Singleton<ILocalStorage, LocalStorage>();
            _container.Singleton<AppNavigationService>();
            _container.Singleton<IInteractionService, InteractionService>();
            _container.Handler<IAppNavigationService>(cnt => cnt.GetInstance<AppNavigationService>());


            _container.PerRequest<ShellViewModel>();
            _container.PerRequest<ShowNotesViewModel>();
            _container.PerRequest<CreateNoteViewModel>();
            _container.PerRequest<FavoriteNotesViewModel>();

            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "Notes.Views",
                DefaultSubNamespaceForViewModels = "Notes.Core.ViewModels"
            };

            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            DisplayRootView<ShellView>();
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.CacheSize = 3;
            Window.Current.Activate();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = base.SelectAssemblies().ToList();
            assemblies.Add(typeof(ShowNotesViewModel).GetTypeInfo().Assembly);

            return assemblies;
        }

        protected override void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
        }
    }
}
