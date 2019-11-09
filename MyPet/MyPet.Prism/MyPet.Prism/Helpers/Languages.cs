using MyPet.Prism.Interfaces;
using MyPet.Prism.Resources;
using Xamarin.Forms;


namespace MyPet.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string Cancel => Resource.Cancel;
        public static string CheckConection => Resource.CheckConection;

        public static string EmailorPassword => Resource.EmailorPassword;



    }
}
