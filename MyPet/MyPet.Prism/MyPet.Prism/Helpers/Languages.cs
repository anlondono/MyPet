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
        public static string ChangePassword => Resource.ChangePassword;
        public static string ThePasswordsDoesNotMatch => Resource.ThePasswordsDoesNotMatch;

        public static string UserUpdatedSuccesfully => Resource.UserUpdatedSuccesfully;
        public static string Ok => Resource.Ok;
        public static string RequestAdoption => Resource.RequestAdoption;
        public static string Requests => Resource.Requests;
        public static string History => Resource.History;
        public static string SuccessfullyAccepted => Resource.SuccessfullyAccepted;
        public static string CurrentPassword => Resource.CurrentPassword;
        public static string NewPassword => Resource.NewPassword;
        public static string RememberMeInThisDevice => Resource.RememberMeInThisDevice;
        public static string Firstname => Resource.Firstname;
        public static string Lastame => Resource.Lastname;
        public static string Address => Resource.Address;
        public static string Phone => Resource.Phone;
        public static string RegisterAs => Resource.RegisterAs;
        public static string PasswordConfirm => Resource.PasswordConfirm;
        public static string SelectRole => Resource.SelectRole;
        public static string Race => Resource.Race;
        public static string Age => Resource.Age;
        public static string Name => Resource.Name;
        public static string PetType => Resource.PetType;
        public static string IsAvailable => Resource.IsAvailable;
        public static string Description => Resource.Description;
        public static string Save => Resource.Save;
        public static string ChangeImage => Resource.ChangeImage;
        public static string Date => Resource.Date;
        public static string Children => Resource.Children;
        public static string HasPets => Resource.HasPets;
        public static string TypeHousing => Resource.TypeHousing;
        public static string Adopter => Resource.Adopter;
        public static string ItIsActive => Resource.ItIsActive;
        public static string Rejected => Resource.Rejected;

        public static string MakeRequest => Resource.MakeRequest;
        public static string Observations => Resource.Observations;
        public static string ToRefuse => Resource.ToRefuse;
        public static string Home => Resource.Home;
        



















    }
}
