using MyPet.Common.Models;
using Prism.Navigation;

namespace MyPet.Prism.ViewModels
{
    public class RequestHistoryPageViewModel : ViewModelBase
    {
        private HistoryRequestResponse _history;

        public RequestHistoryPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "History";
        }

        public HistoryRequestResponse History
        {
            get => _history;
            set => SetProperty(ref _history, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("history"))
            {
                History = parameters.GetValue<HistoryRequestResponse>("history");
            }
        }
    }
}
