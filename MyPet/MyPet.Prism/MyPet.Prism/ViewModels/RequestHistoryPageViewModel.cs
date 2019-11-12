using System;
using MyPet.Common.Helpers;
using MyPet.Common.Models;
using MyPet.Common.Services;
using MyPet.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyPet.Prism.ViewModels
{
    public class RequestHistoryPageViewModel : ViewModelBase
    {
        private HistoryRequestResponse _history;
        private DelegateCommand _denyCommand;
        private DelegateCommand _acceptCommand;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isOwner;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;


        public RequestHistoryPageViewModel(INavigationService navigationService,
             IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "History";
            IsEnabled = true;
        }
        public DelegateCommand DenyCommand => _denyCommand ??
          (_denyCommand = new DelegateCommand(Deny));



        public DelegateCommand AcceptCommand => _acceptCommand ??
          (_acceptCommand = new DelegateCommand(Accept));


        public HistoryRequestResponse History
        {
            get => _history;
            set => SetProperty(ref _history, value);
        }

        public bool IsOwner
        {
            get => _isOwner;
            set => SetProperty(ref _isOwner, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("history"))
            {
                var _history = parameters.GetValue<HistoryRequestResponse>("history");
                _history.WasDenied = _history.Denied ? Languages.Yes : Languages.No;
                _history.IsActive = _history.Active ? Languages.Yes : Languages.No;
                _history.HasKidsStr = _history.HasKids ? Languages.Yes : Languages.No;
                _history.HasPetsStr = _history.HasPets ? Languages.Yes : Languages.No;
                History = _history;

                IsOwner = JsonConvert.DeserializeObject<TemporaryOwnerResponse>
                    (Settings.Owner).IsOwner;
            }
        }

        private async void Deny()
        {
            IsEnabled = false;
            IsRunning = true;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await _apiService.PostAsync(
                     url,
                     "/api",
                     $"/Requests/Deny/{History.RequestId}",
                     "",
                     "bearer",
                     token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                Languages.Accept,
                "Rechazado con exito",
                Languages.Accept);

            await PetsPageViewModel.GetInstance().UpdateOwnerAsync();

            await _navigationService.GoBackToRootAsync();
        }


        private async void Accept()
        {
            IsEnabled = false;
            IsRunning = true;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await _apiService.PostAsync(
                     url,
                     "/api",
                     $"/Requests/Accept/{History.RequestId}",
                     "",
                     "bearer",
                     token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                Languages.Accept,
                "Aceptado con exito",
                Languages.Accept);

            await PetsPageViewModel.GetInstance().UpdateOwnerAsync();

            await _navigationService.GoBackToRootAsync();
        }
    }
}
