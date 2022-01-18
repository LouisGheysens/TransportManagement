namespace BussinesLayer.Interfaces
{
    public interface IVrachtwagenRepository
    {
        bool BestaatVrachtwagen(string chassisnummer);
        Vrachtwagen VoegVrachtwagenToe(Vrachtwagen vrachtwagen);
        Vrachtwagen UpdateVrachtwagen(Vrachtwagen vrachtwagen);
        void VerwijderVrachtwagen(Vrachtwagen vrachtwagen);
        Vrachtwagen SelecteerVrachtwagen(string chassisnummer);
        IReadOnlyCollection<Vrachtwagen> ZoekVrachtwagen(string chassisnummer, string merk, string model, Brandstof brandstof, float gewicht);
    }
}
