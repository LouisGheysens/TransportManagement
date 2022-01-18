namespace BussinesLayer.Interfaces
{
    public interface IOngevalRepository
    {
        bool BestaatOngeval(Ongeval ongeval);
        Ongeval VoegOngevalToe(Ongeval ongeval);
        void VerwijderOngeval(Ongeval ongeval);
        IReadOnlyCollection<Ongeval> ZoekOngevallen(Boolean? arbeidsongeval, string plaats, DateTime? datum, Boolean? pertetotal, ErnstGraad graad);
    }
}
