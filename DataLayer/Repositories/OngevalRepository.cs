namespace DataLayer.Repositories
{
    public class OngevalRepository : IOngevalRepository
    {
        public bool BestaatOngeval(string plaats)
        {
            throw new NotImplementedException();
        }

        public Ongeval UpdateOngeval(Ongeval ongeval)
        {
            throw new NotImplementedException();
        }

        public void VerwijderOngeval(string plaats)
        {
            throw new NotImplementedException();
        }

        public Ongeval VoegOngevalToe(Ongeval ongeval, Chauffeur chaffeur, Vrachtwagen vrachtwagen)
        {
            throw new NotImplementedException();
        }

        public ICollection<Ongeval> ZoekOngevallen(bool? arbeidsongeval, Chauffeur chaffeur, Vrachtwagen vrachtwagen, string plaats, DateTime? datum, bool? pertetotal, ErnstGraad graad)
        {
            throw new NotImplementedException();
        }
    }
}
