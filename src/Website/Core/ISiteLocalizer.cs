namespace mbsoft.BrewClub.Website
{
    public interface ISiteLocalizer
    {
        string Localize(string s);
        string LocalizeFormat(string formatKey, params object[] args);
    }
}