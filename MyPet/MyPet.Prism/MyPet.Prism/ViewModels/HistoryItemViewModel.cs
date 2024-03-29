﻿using MyPet.Common.Models;
using Prism.Commands;
using Prism.Navigation;

namespace MyPet.Prism.ViewModels
{
    public class HistoryItemViewModel : HistoryRequestResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectHistoryCommand;

        public HistoryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectHistoryCommand => _selectHistoryCommand ??
            (_selectHistoryCommand = new DelegateCommand(SelectHistory));

        private async void SelectHistory()
        {
            var parameters = new NavigationParameters
            {
                { "history", this }
            };

            await _navigationService.NavigateAsync("RequestHistoryPage", parameters);
        }
    }
}
