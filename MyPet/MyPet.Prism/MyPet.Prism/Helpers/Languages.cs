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

        public static string Confirm => Resource.Confirm;
        public static string Yes => Resource.Yes;
        public static string No => Resource.No;
        public static string NewPet => Resource.NewPet;
        public static string CheckConnection => Resource.CheckConnection;
        public static string GettingPetTypes => Resource.GettingPetTypes;
        public static string Wherepicturefrom => Resource.Wherepicturefrom;
        public static string FromGallery => Resource.FromGallery;
        public static string FromCamera => Resource.FromCamera;
        public static string EnteranEmail => Resource.EnteranEmail;
        public static string EnteraPassword => Resource.EnteraPassword;
        public static string Details => Resource.Details;
        public static string Pets => Resource.Pets;
        public static string PetsAvailable => Resource.PetsAvailable;
        public static string EnteraDocument => Resource.EnteraDocument;
        public static string EnteraName => Resource.EnteraName;
        public static string EnterLastName => Resource.EnterLastName;
        public static string EnterAddress => Resource.EnterAddress;
        public static string EenterValidEmail => Resource.EenterValidEmail;
        public static string EnteraPhonenumber => Resource.EnteraPhonenumber;
        public static string SelectaRole => Resource.SelectaRole;
        public static string PasswordCharacteres => Resource.PasswordCharacteres;
        public static string EnteraPasswordConfirm => Resource.EnteraPasswordConfirm;
        public static string PasswordandConfirmDoesNotMatch => Resource.PasswordandConfirmDoesNotMatch;
        public static string RemembePassword => Resource.RemembePassword;
        public static string Password => Resource.Password;
        public static string Email => Resource.Email;
        public static string ForgotyourPassword => Resource.ForgotyourPassword;
        public static string Login => Resource.Login;
        public static string Register => Resource.Register;
        public static string EditPet => Resource.EditPet;
        public static string BigproblemCallSupport => Resource.BigproblemCallSupport;
        public static string MyPets => Resource.MyPets;
        public static string ModifyProfile => Resource.ModifyProfile;
        public static string Logout => Resource.Logout;
        public static string SuccessfulRequest => Resource.SuccessfulRequest;
        public static string SelectHousing => Resource.SelectHousing;
        public static string RegisterNewUser => Resource.RegisterNewUser;
        public static string Document => Resource.RegisterNewUser;









    }
}
