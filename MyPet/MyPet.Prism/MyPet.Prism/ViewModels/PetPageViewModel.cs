using MyPet.Common.Helpers;
using MyPet.Common.Models;
using MyPet.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyPet.Prism.ViewModels
{
    public class PetPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private PetResponse _pet;
        private DelegateCommand _editPetCommand;
        private DelegateCommand _requestPetCommand;
        private bool _isOwner;
        private bool _isAdopter;

        public PetPageViewModel(
                 INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.Details;
        }

        public DelegateCommand EditPetCommand => _editPetCommand ??
            (_editPetCommand = new DelegateCommand(EditPetAsync));
        public DelegateCommand RequestPetCommand => _requestPetCommand ??
            (_requestPetCommand = new DelegateCommand(RequestPetAsync));

        public PetResponse Pet
        {
            get => _pet;
            set => SetProperty(ref _pet, value);
        }

        public bool IsOwner
        {
            get => _isOwner;
            set => SetProperty(ref _isOwner, value);
        }

        public bool IsAdopter
        {
            get => _isAdopter;
            set => SetProperty(ref _isAdopter, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Pet = JsonConvert.DeserializeObject<PetResponse>(Settings.Pet);
            var owner = JsonConvert.DeserializeObject<TemporaryOwnerResponse>(
               Settings.Owner);
            IsAdopter = owner.IsAdopter;
            IsOwner = owner.IsOwner;
        }

        private async void EditPetAsync()
        {
            if (!IsOwner)
            {
                return;
            }
            var parameters = new NavigationParameters
            {
                { "pet", Pet }
            };

            await _navigationService.NavigateAsync("EditPetPage", parameters);
        }

        private async void RequestPetAsync()
        {
            if (!IsAdopter)
            {
                return;
            }
            var parameters = new NavigationParameters
            {
                { "pet", Pet }
            };

            await _navigationService.NavigateAsync("RegisterRequestPage", parameters);
        }
    }
}
