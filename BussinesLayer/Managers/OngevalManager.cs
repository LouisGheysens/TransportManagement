namespace BussinesLayer.Managers
{
    public class OngevalManager
    {
        private IOngevalRepository _repo;

        public OngevalManager(IOngevalRepository repo)
        {
            this._repo = repo;  
        }

        public bool BestaatOngeval(string plaats)
        {
            try
            {
                return _repo.BestaatOngeval(plaats);
            }catch(Exception ex)
            {
                throw new OngevalException("Ongeval: BestaatOngeval - gefaald", ex);
            }
        }

        public Ongeval UpdateOngeval(Ongeval ongeval)
        {
            try
            {
                return _repo.UpdateOngeval(ongeval);
            }
            catch (Exception ex)
            {
                throw new OngevalException("Ongeval: UpdateOngeval - gefaald", ex);
            }
        }

        public void VerwijderOngeval(string plaats)
        {
            try
            {
                 _repo.VerwijderOngeval(plaats);
            }
            catch (Exception ex)
            {
                throw new OngevalException("Ongeval: VerwijderOngeval - gefaald", ex);
            }
        }

        public Ongeval VoegOngevalToe(Ongeval ongeval, Chauffeur chaffeur, Vrachtwagen vrachtwagen)
        {
            try
            {
                return _repo.VoegOngevalToe(ongeval, chaffeur, vrachtwagen);
            }
            catch (Exception ex)
            {
                throw new OngevalException("Ongeval: VoegOngevalToe - gefaald", ex);
            }
        }

        public ICollection<Ongeval> ZoekOngevallen(bool? arbeidsongeval, Chauffeur chaffeur, Vrachtwagen vrachtwagen, string plaats, DateTime? datum, bool? pertetotal, ErnstGraad graad)
        {
            try
            {
                return _repo.ZoekOngevallen(arbeidsongeval, chaffeur, vrachtwagen, plaats, datum, pertetotal, graad);
            }
            catch (Exception ex)
            {
                throw new OngevalException("Ongeval: ZoekOngevallen - gefaald", ex);
            }
        }
    }
}
