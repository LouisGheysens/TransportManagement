namespace BussinesLayer.Managers
{
    public class OngevalManager
    {
        private IOngevalRepository _repo;

        public OngevalManager(IOngevalRepository repo)
        {
            this._repo = repo;  
        }

        public bool BestaatOngeval(Ongeval ongeval)
        {
            try
            {
                return _repo.BestaatOngeval(ongeval);
            }catch(Exception ex)
            {
                throw new OngevalException("Ongeval: BestaatOngeval - gefaald", ex);
            }
        }

        public void VerwijderOngeval(Ongeval ongeval)
        {
            try
            {
                 _repo.VerwijderOngeval(ongeval);
            }
            catch (Exception ex)
            {
                throw new OngevalException("Ongeval: VerwijderOngeval - gefaald", ex);
            }
        }

        public Ongeval VoegOngevalToe(Ongeval ongeval)
        {
            try
            {
                return _repo.VoegOngevalToe(ongeval);
            }
            catch (Exception ex)
            {
                throw new OngevalException("Ongeval: VoegOngevalToe - gefaald", ex);
            }
        }

        public IReadOnlyCollection<Ongeval> ZoekOngevallen(bool? arbeidsongeval, string plaats, DateTime? datum, bool? pertetotal, ErnstGraad graad)
        {
            try
            {
                return _repo.ZoekOngevallen(arbeidsongeval, plaats, datum, pertetotal, graad);
            }
            catch (Exception ex)
            {
                throw new OngevalException("Ongeval: ZoekOngevallen - gefaald", ex);
            }
        }
    }
}
