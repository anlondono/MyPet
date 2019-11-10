using MyPet.Common.Helpers;
using MyPet.Common.Models;
using MyPet.Common.Services;
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
    public class PetsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TemporaryOwnerResponse _owner;
        private ObservableCollection<PetItemViewModel> _pets;
        private DelegateCommand _addPetCommand;
        private static PetsPageViewModel _instance;

        public PetsPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _instance = this;
            Title = "Pets";
            LoadOwner();
        }

        public DelegateCommand AddPetCommand => _addPetCommand ?? (_addPetCommand = new DelegateCommand(AddPet));

        public ObservableCollection<PetItemViewModel> Pets
        {
            get => _pets;
            set => SetProperty(ref _pets, value);
        }

        public static PetsPageViewModel GetInstance()
        {
            return _instance;
        }

        public async Task UpdateOwnerAsync()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await _apiService.GetOwnerByEmailAsync(
                url,
                "api",
                "/TemporaryOwners/GetTemporaryOwnerByEmail",
                "bearer",
                token.Token,
               _owner.Email);

            if (response.IsSuccess)
            {
                var owner = response.Result;
                Settings.Owner = JsonConvert.SerializeObject(owner);
                _owner = owner;
                LoadOwner();
            }
        }

        private void LoadOwner()
        {
            _owner = JsonConvert.DeserializeObject<TemporaryOwnerResponse>(Settings.Owner);
            if (_owner.IsOwner)
            {
                Title = $"Pets of: {_owner.FullName}";
            }
            else
            {
                Title = "Chandas dispobibles";
            }

            Pets = new ObservableCollection<PetItemViewModel>(_owner.Pets.Select(
                p => new PetItemViewModel(_navigationService)
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    PetType = p.PetType,
                    Race = p.Race,
                    Description = p.Description,
                    Age = p.Age,
                }).ToList());
        }

        private async void AddPet()
        {
            await _navigationService.NavigateAsync("EditPet");
        }
    }
}
