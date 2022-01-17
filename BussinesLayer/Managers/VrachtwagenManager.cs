namespace BussinesLayer.Managers
{
    public class VrachtwagenManager
    {
        private IVrachtwagenRepository _repo;

        public VrachtwagenManager(IVrachtwagenRepository repo)
        {
            this._repo = repo;
        }


        public bool BestaatVrachtwagen(string chassisnummer)
        {
            try
            {
                return _repo.BestaatVrachtwagen(chassisnummer);
            }catch(Exception ex)
            {
                throw new VrachtwagenException("Vrachtwagenmanager: BestaatVrachtWagen - gefaald", ex);
            }
        }

        public void VerwijderVrachtwagen(string chassisnummer)
        {
            try
            {
                 _repo.VerwijderVrachtwagen(chassisnummer);
            }
            catch (Exception ex)
            {
                throw new VrachtwagenException("Vrachtwagenmanager: VerwijderVrachtwagen - gefaald", ex);
            }
        }

        public Vrachtwagen SelecteerVrachtwagen(Vrachtwagen vrachtwagen)
        {
            try
            {
                return _repo.SelecteerVrachtwagen(vrachtwagen);
            }
            catch (Exception ex)
            {
                throw new VrachtwagenException("Vrachtwagenmanager: SelecteerVrachtwagen - gefaald", ex);
            }
        }

        public Vrachtwagen UpdateVrachtwagen(Vrachtwagen vrachtwagen)
        {
            try
            {
                return _repo.UpdateVrachtwagen(vrachtwagen);
            }
            catch (Exception ex)
            {
                throw new VrachtwagenException("Vrachtwagenmanager: UpdateVrachtwagen - gefaald", ex);
            }
        }

        public Vrachtwagen VoegVrachtwagenToe(Vrachtwagen vrachtwagen)
        {
            try
            {
                return _repo.VoegVrachtwagenToe(vrachtwagen);
            }
            catch (Exception ex)
            {
                throw new VrachtwagenException("Vrachtwagenmanager: VoegVrachtwagenToe - gefaald", ex);
            }
        }

        public ICollection<Vrachtwagen> ZoekVrachtwagen(string chassisnummer, string merk, string model, Brandstof brandstof, float gewicht)
        {
            try
            {
                return _repo.ZoekVrachtwagen(chassisnummer, merk, model, brandstof, gewicht);
            }
            catch (Exception ex)
            {
                throw new VrachtwagenException("Vrachtwagenmanager: ZoekVrachtwagen - gefaald", ex);
            }
        }
    }
}
