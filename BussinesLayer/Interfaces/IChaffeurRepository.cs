namespace BussinesLayer.Interfaces
{
    public interface IChaffeurRepository
    {
        bool BestaatChauffeur(string personeelsnummer);
        Chauffeur VoegChauffeurToe(Chauffeur chauffeur);
        Chauffeur UpdateChaffeur(Chauffeur chauffeur);
        void DeleteChaffeur(int personeelsnummer);
        ICollection<Chauffeur> ZoekChauffeurs(int? personeelsnummer, string naam, DateTime? geboortedatum, Boolean? internationaal);
        Chauffeur SelecteerChauffeur(int personeelsnummer);
    }
}
