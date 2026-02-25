namespace Silksprite.Loch.Core
{
    public interface ILocalizationProvider
    {
        string LocaleIsoCode { get; }

        string GetLocalizedString(string original);
    }
}
