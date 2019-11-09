using System.Globalization;

namespace MyPet.Prism.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}
