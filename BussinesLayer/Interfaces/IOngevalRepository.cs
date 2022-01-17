namespace BussinesLayer.Interfaces
{
    public interface IOngevalRepository
    {
        bool BestaatOngeval(string plaats);
        Ongeval VoegOngevalToe(Ongeval ongeval, Chauffeur chaffeur, Vrachtwagen vrachtwagen);
        void VerwijderOngeval(string plaats);
        Ongeval UpdateOngeval(Ongeval ongeval);
        ICollection<Ongeval> ZoekOngevallen(Boolean? arbeidsongeval, Chauffeur chaffeur, Vrachtwagen vrachtwagen, string plaats, DateTime? datum, Boolean? pertetotal, ErnstGraad graad);
    }
}
