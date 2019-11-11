using MyPet.Common.Helpers;
using MyPet.Common.Models;
using MyPet.Common.Services;
using MyPet.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyPet.Prism.ViewModels
{
    public class RegisterRequestPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _saveCommand;
        private PetResponse _pet;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _hasKids;
        private bool _hasPets;
        private string _observation;
        private ObservableCollection<HouseTypeResponse> _houseTypes;
        private HouseTypeResponse _houseType;

        public RegisterRequestPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
        }

        public DelegateCommand SaveCommand => _saveCommand ??
            (_saveCommand = new DelegateCommand(SaveAsync));

        public PetResponse Pet
        {
            get => _pet;
            set => SetProperty(ref _pet, value);
        }

        public ObservableCollection<HouseTypeResponse> HouseTypes
        {
            get => _houseTypes;
            set => SetProperty(ref _houseTypes, value);
        }

        public HouseTypeResponse HouseType
        {
            get => _houseType;
            set => SetProperty(ref _houseType, value);
        }

        public string Observation
        {
            get => _observation;
            set => SetProperty(ref _observation, value);
        }

        public bool HasKids
        {
            get => _hasKids;
            set => SetProperty(ref _hasKids, value);
        }

        public bool HasPets
        {
            get => _hasPets;
            set => SetProperty(ref _hasPets, value);
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


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("pet"))
            {
                Pet = parameters.GetValue<PetResponse>("pet");
                Title = "Solicitar al perro que tradusca esto";
            }
            else
            {
                await _navigationService.GoBackAsync();
            }

            LoadHouseTypesAsync();
        }

        private async void LoadHouseTypesAsync()
        {
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();

            var connection = await _apiService.CheckConnection(url);
            if (!connection)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CheckConection,
                    Languages.Accept);
                await _navigationService.GoBackAsync();
                return;
            }

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await _apiService.GetListAsync<HouseTypeResponse>(
                url,
                "/api",
                "/HouseTypes",
                "bearer",
                token.Token);

            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.GettingPetTypes,
                    Languages.Accept);
                await _navigationService.GoBackAsync();
                return;
            }

            var houseTypes = (List<HouseTypeResponse>)response.Result;
            HouseTypes = new ObservableCollection<HouseTypeResponse>(houseTypes);
        }

        private async void SaveAsync()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var owner = JsonConvert.DeserializeObject<TemporaryOwnerResponse>(Settings.Owner);

            var petRequest = new AskingPetRequest
            {
                HasKids = HasKids,
                HasOthePets = HasPets,
                HouseTypeId = HouseType.Id,
                Observation = Observation,
                PetId = Pet.Id,
                AdopterId = owner.Id,
            };

            var response = await _apiService.PostAsync(
                    url, "/api", "/Requests/Create", petRequest, "bearer", token.Token);


            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.SuccessfulRequest,
                Languages.Accept);

            await PetsPageViewModel.GetInstance().UpdateOwnerAsync();

            await _navigationService.GoBackToRootAsync();
        }

        private async Task<bool> ValidateData()
        {
            if (HouseType == null)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SelectHousing, 
                    Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
