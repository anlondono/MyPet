using Prism;
using Prism.Ioc;
using MyPet.Prism.ViewModels;
using MyPet.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using MyPet.Common.Models;
using MyPet.Common.Helpers;
using System;
using MyPet.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyPet.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            {

                await NavigationService.NavigateAsync("/PetMasterDetailPage/NavigationPage/PetsPage");
                await PetsPageViewModel.GetInstance().UpdateOwnerAsync();
            }
            else
            {
                await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<PetMasterDetailPage, PetMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<PetsPage, PetsPageViewModel>();
            containerRegistry.RegisterForNavigation<PetPage, PetPageViewModel>();
            containerRegistry.RegisterForNavigation<EditPetPage, EditPetPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterRequestPage, RegisterRequestPageViewModel>();
            containerRegistry.RegisterForNavigation<PetTabbedPage, PetTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<RequestHistoriesPage, RequestHistoriesPageViewModel>();
            containerRegistry.RegisterForNavigation<RequestHistoryPage, RequestHistoryPageViewModel>();
        }
    }
}
