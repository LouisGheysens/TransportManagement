namespace BussinesLayer.Managers
{
    public class ChauffeurManager
    {
        private IChaffeurRepository _repo;
        
        public ChauffeurManager(IChaffeurRepository repo)
        {
            this._repo = repo;
        }

        public bool BestaatChauffeur(int personeelsnummer)
        {
            try
            {
                return _repo.BestaatChauffeur(personeelsnummer);
            }catch(Exception ex)
            {
                throw new ChauffeurException("Chaffeur: BestaatChauffeur - gefaald", ex);
            }
        }

        public void DeleteChaffeur(int personeelsnummer)
        {
            try
            {
                 _repo.DeleteChaffeur(personeelsnummer);
            }
            catch (Exception ex)
            {
                throw new ChauffeurException("Chaffeur: DeleteChaffeur - gefaald", ex);
            }
        }

        public Chauffeur SelecteerChauffeur(int personeelsnummer)
        {
            try
            {
                return _repo.SelecteerChauffeur(personeelsnummer);
            }
            catch (Exception ex)
            {
                throw new ChauffeurException("Chaffeur: SelecteerChauffeur - gefaald", ex);
            }
        }

        public Chauffeur UpdateChaffeur(Chauffeur chauffeur)
        {
            try
            {
                return _repo.UpdateChaffeur(chauffeur);
            }
            catch (Exception ex)
            {
                throw new ChauffeurException("Chaffeur: UpdateChaffeur - gefaald", ex);
            }
        }

        public Chauffeur VoegChauffeurToe(Chauffeur chauffeur)
        {
            try
            {
                return _repo.VoegChauffeurToe(chauffeur);
            }
            catch (Exception ex)
            {
                throw new ChauffeurException("Chaffeur: VoegChauffeurToe - gefaald", ex);
            }
        }

        public ICollection<Chauffeur> ZoekChauffeurs(int? personeelsnummer, string naam, DateTime? geboortedatum, bool? internationaal)
        {
            try
            {
                return _repo.ZoekChauffeurs(personeelsnummer, naam, geboortedatum, internationaal);
            }
            catch (Exception ex)
            {
                throw new ChauffeurException("Chaffeur: ZoekChauffeurs - gefaald", ex);
            }
        }
    }
}
