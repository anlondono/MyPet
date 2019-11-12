using DryIoc;
using MyPet.Common.Helpers;
using MyPet.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPet.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenu));

        private async void SelectMenu()
        {
            if (PageName.Equals("LoginPage"))
            {
                Settings.IsRemembered = false;
                Settings.Owner = null;
                Settings.Pet = null;
                await _navigationService.NavigateAsync("/NavigationPage/LoginPage");
                return;
            }

            if (PageName.Equals("RequestHistoriesPage"))
            {
                Settings.Pet = null;
            }

            await _navigationService.NavigateAsync($"/PetMasterDetailPage/NavigationPage/{PageName}");

        }
    }
}
