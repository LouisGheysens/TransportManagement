namespace DataLayer.Repositories
{
    public class ChauffeurRepository : IChaffeurRepository
    {
        public bool BestaatChauffeur(string personeelsnummer)
        {
            SqlConnection conn = ConnectionClass.GetConnection();
            string query = "SELECT COUNT(*) FROM Chauffeur WHERE Personeelsnummer=@personeelsnummer";
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = query;
                conn.Open();
                try
                {
                    command.Parameters.AddWithValue("@personeelsnummer", personeelsnummer);
                    int exists = (int)command.ExecuteScalar();
                    if(exists > 0) return true;
                    return false;
                }catch(Exception ex)
                {
                    throw new ChauffeurRepositoryException("ChaufeurRepository: BestaatChauffeur - gefaald", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteChaffeur(int personeelsnummer)
        {
            throw new NotImplementedException();
        }

        public Chauffeur SelecteerChauffeur(int personeelsnummer)
        {
            throw new NotImplementedException();
        }

        public Chauffeur UpdateChaffeur(Chauffeur chauffeur)
        {
            throw new NotImplementedException();
        }

        public Chauffeur VoegChauffeurToe(Chauffeur chauffeur)
        {
            throw new NotImplementedException();
        }

        public ICollection<Chauffeur> ZoekChauffeurs(int? personeelsnummer, string naam, DateTime? geboortedatum, bool? internationaal)
        {
            throw new NotImplementedException();
        }
    }
}
