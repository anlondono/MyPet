using MyPet.Common.Helpers;
using MyPet.Common.Models;
using MyPet.Common.Services;
using Newtonsoft.Json;
using Prism.Navigation;
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
        public RequestHistoriesPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Pet = JsonConvert.DeserializeObject<PetResponse>(Settings.Pet);
            Title = "Solicitudes";
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

        private void LoadHistories()
        {
            Histories = new ObservableCollection<HistoryItemViewModel>(
                Pet.HistoryRequests
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
