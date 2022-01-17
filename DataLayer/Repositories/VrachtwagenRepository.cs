namespace DataLayer.Repositories
{
    public class VrachtwagenRepository : IVrachtwagenRepository
    {
        public bool BestaatVrachtwagen(string chassisnummer)
        {
            throw new NotImplementedException();
        }

        public Vrachtwagen SelecteerVrachtwagen(Vrachtwagen vrachtwagen)
        {
            throw new NotImplementedException();
        }

        public Vrachtwagen UpdateVrachtwagen(Vrachtwagen vrachtwagen)
        {
            throw new NotImplementedException();
        }

        public void VerwijderVrachtwagen(string chassisnummer)
        {
            throw new NotImplementedException();
        }

        public Vrachtwagen VoegVrachtwagenToe(Vrachtwagen vrachtwagen)
        {
            throw new NotImplementedException();
        }

        public ICollection<Vrachtwagen> ZoekVrachtwagen(string chassisnummer, string merk, string model, Brandstof brandstof, float gewicht)
        {
            throw new NotImplementedException();
        }
    }
}
