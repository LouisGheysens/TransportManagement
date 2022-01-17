namespace BussinesLayer.Interfaces
{
    public interface IVrachtwagenRepository
    {
        bool BestaatVrachtwagen(string chassisnummer);
        Vrachtwagen VoegVrachtwagenToe(Vrachtwagen vrachtwagen);
        Vrachtwagen UpdateVrachtwagen(Vrachtwagen vrachtwagen);
        void VerwijderVrachtwagen(string chassisnummer);
        Vrachtwagen SelecteerVrachtwagen(Vrachtwagen vrachtwagen);
        ICollection<Vrachtwagen> ZoekVrachtwagen(string chassisnummer, string merk, string model, Brandstof brandstof, float gewicht);
    }
}
