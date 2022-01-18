namespace BussinesLayer.Interfaces
{
    public interface IChaffeurRepository
    {
        bool BestaatChauffeur(string personeelsnummer);
        Chauffeur VoegChauffeurToe(Chauffeur chauffeur);
        Chauffeur UpdateChaffeur(Chauffeur chauffeur);
        void DeleteChaffeur(Chauffeur chauffeur);
        IReadOnlyCollection<Chauffeur> ZoekChauffeurs(string personeelsnummer, string naam, DateTime? geboortedatum, Boolean? internationaal);
        Chauffeur SelecteerChauffeur(string personeelsnummer);
    }
}
