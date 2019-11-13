using MyPet.Common.Helpers;
using MyPet.Common.Models;
using MyPet.Common.Services;
using MyPet.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyPet.Prism.ViewModels
{
    public class RequestHistoriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private PetResponse _pet;
        private ObservableCollection<HistoryItemViewModel> _histories;
        private TemporaryOwnerResponse _owner;

        public RequestHistoriesPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Pet = JsonConvert.DeserializeObject<PetResponse>(Settings.Pet);
            _owner = JsonConvert.DeserializeObject<TemporaryOwnerResponse>(Settings.Owner);
            Title = Languages.Requests;
            LoadHistories();
        }
        public ObservableCollection<HistoryItemViewModel> Histories
        {
            get => _histories;
            set => SetProperty(ref _histories, value);
        }

        public PetResponse Pet
        {
            get => _pet;
            set => SetProperty(ref _pet, value);
        }

        private async void LoadHistories()
        {
            IEnumerable<HistoryRequestResponse> data;
            if (Pet != null)
            {
                data = Pet.HistoryRequests;
            }
            else
            {
                var url = App.Current.Resources["UrlAPI"].ToString();
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                var tipo = _owner.IsAdopter ? $"Adopter/{_owner.Id}" : $"Owner/{_owner.Id}";
                var response = await _apiService.GetListAsync<HistoryRequestResponse>(
                    url,
                    "api",
                    $"/Requests/{tipo}",
                    "bearer",
                    token.Token);

                if (response.IsSuccess)
                {
                    data = (List<HistoryRequestResponse>)response.Result;
                }
                else
                {
                    data = new List<HistoryRequestResponse>();
                }
            }
            Histories = new ObservableCollection<HistoryItemViewModel>(
                   data
                    .Select(h => new HistoryItemViewModel(_navigationService)
                    {
                        Date = h.Date,
                        Observation = h.Observation,
                        Active = h.Active,
                        Adopter = h.Adopter,
                        Denied = h.Denied,
                        HasKids = h.HasKids,
                        HasPets = h.HasPets,
                        HouseType = h.HouseType,
                        Pet = h.Pet,
                        RequestId = h.RequestId,
                        Telephone = h.Telephone,
                    }).ToList());
        }
    }
}
