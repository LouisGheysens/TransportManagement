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

        public void DeleteChaffeur(Chauffeur chauffeur)
        {
            if (!BestaatChauffeur(chauffeur.PersoneelsNummer))
            {
                throw new ChauffeurRepositoryException("ChauffeurRepository: DeleteChaffeur - Chauffeur bestaat niet!");
            }
            string queryChaffeur="DELETE FROM Chauuffeur WHERE personeelsnummer=@personeelsnummer";
            string queryVrachtwagen="UPDATE Vrachtwagen SET Chauffeur = NULL WHERE chassisnummer=@chassisnummer";
            string queryMedia = "DELETE FROM ChauffeurAfbeeldingen WHERE Chauffeur = @personeelsnummer";
            SqlConnection connection = ConnectionClass.GetConnection();
            using SqlCommand cmdChauffeur = new(queryChaffeur, connection);
            using SqlCommand cmdVrachtwagen = new(queryVrachtwagen, connection);
            using SqlCommand cmdMedia = new(queryMedia, connection);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                #region Chauffeur
                cmdChauffeur.Transaction = transaction;
                cmdChauffeur.Parameters.AddWithValue("@personeelsnummer", chauffeur.PersoneelsNummer);
                cmdChauffeur.ExecuteNonQuery();
                #endregion

                #region Vrachtwagen
                if(chauffeur.Vrachtwagen != null)
                {
                    cmdVrachtwagen.Transaction = transaction;
                    cmdVrachtwagen.Parameters.AddWithValue("@chassisnummer", chauffeur.Vrachtwagen.ChassisNummer);
                }
                #endregion

                #region MediaChauffeur
                cmdMedia.Transaction = transaction;
                cmdMedia.Parameters.AddWithValue("@personeelsnummer", chauffeur.PersoneelsNummer);
                cmdMedia.ExecuteNonQuery();
                #endregion
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new ChauffeurRepositoryException("ChauffeurRepository: VerwijderChauffeur - gefaald", ex);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
        }

        public Chauffeur SelecteerChauffeur(string personeelsnummer)
        {
            string query = "SELECT c.* FROM Chauffeur c " +
                "LEFT JOIN Vrachtwagen v ON c.Vrachtwagenchassisnummer = v.chassisnummer "+
                "WHERE c.personeelsnummer = @personeelsnummer";
            Chauffeur chauffeur = null;
            Vrachtwagen vrachtwagen = null;
            SqlConnection conn = ConnectionClass.GetConnection();
            using(SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@personeelsnummer", personeelsnummer);
                conn.Open();
                try
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            #region Chauffeur
                            if(chauffeur == null && !reader.IsDBNull(reader.GetOrdinal("personeelsnummer")))
                            {
                                chauffeur = new((string)reader["personeelsnummer"],
                                    (string)reader["voornaam"],
                                    (string)reader["achternaam"],
                                    (DateTime)reader["geboortedatum"],
                                    (Boolean)reader["internationaal"]);
                            }
                            #endregion

                            #region Vrachtwagen
                            if(vrachtwagen == null && !reader.IsDBNull(reader.GetOrdinal("chassisnummer")))
                            {
                                Brandstof brandstof = (Brandstof)Enum.Parse(typeof(Brandstof), (string)reader["brandstof"]);
                                vrachtwagen = new(((string)reader["chassisnummer"]),
                                    (string)reader["merk"],
                                    (string)reader["model"],
                                    (float)reader["gewicht"],
                                    (Boolean)reader["aanhangwagen"],
                                    brandstof
                                    );
                            };

                            #endregion
                        }
                        reader.Close();
                        if(vrachtwagen != null) { chauffeur.UpdateVrachtwagen(vrachtwagen); }
                        return chauffeur;
                    }
                }catch(Exception ex)
                {
                    throw new ChauffeurException("ChaffeurRepository: SelecteerChauffeur - gefaald", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Chauffeur UpdateChaffeur(Chauffeur chauffeur)
        {
            SqlConnection conn = ConnectionClass.GetConnection();
            Chauffeur huidigeChauffeur = this.SelecteerChauffeur(chauffeur.PersoneelsNummer);
            if (huidigeChauffeur.Equals(chauffeur)) throw new ChauffeurRepositoryException("ChauffeurRepository: UpdateChaffeur - Er werden geen verschillen gevonden!");
            string sql = "UPDATE Chauffeur SET Voornaam = @Voornaam, " +
                "Achternaam = @Achternaam, Geboortedatum = @Geboortedatum, Internationaal = @Internationaal, " +
                "Competitie = @Competitie, " +
                "Vrachtwagenchassisnummer = @Vrachtwagenchassisnummer WHERE personeelsnummer = @personeelsnummer";
            using SqlCommand command = new(sql, conn);
            try
            {
                conn.Open();
                command.Parameters.AddWithValue("@personeelsnummer", chauffeur.PersoneelsNummer);
                command.Parameters.AddWithValue("@voornaam", chauffeur.Voornaam);
                command.Parameters.AddWithValue("@achternaam", chauffeur.Achternaam);
                command.Parameters.AddWithValue("@geboortedatum", chauffeur.Geboortedatum);
                command.Parameters.AddWithValue("@internationaal", chauffeur.Internationaal);
                command.Parameters.AddWithValue("@vrachtwagenchassisnummer", chauffeur.Vrachtwagen.ChassisNummer);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ChauffeurRepositoryException("ChauffeurRepository: UpdateChaffeur - gefaald", ex);
            }
            finally
            {
                conn.Close();
            }
            return chauffeur;
        }

        public Chauffeur VoegChauffeurToe(Chauffeur chauffeur)
        {
            SqlConnection conn = ConnectionClass.GetConnection();
            if (!BestaatChauffeur(chauffeur.PersoneelsNummer));
            string sql = "INSERT INTO Chauffeur(personeelsnummer, voornaam, achternaam, geboortedatum, internationaal, vrachtwagenchassisnummer) VALUES(@personeelsnummer, @voornaam, " +
                "@achternaam, @geboortedatum, @internationaal, @vrachtwagenchassisnummer)";
            using SqlCommand command = new(sql, conn);
            try
            {
                conn.Open();
                command.Parameters.AddWithValue("@personeelsnummer", chauffeur.PersoneelsNummer);
                command.Parameters.AddWithValue("@voornaam", chauffeur.Voornaam);
                command.Parameters.AddWithValue("@achternaam", chauffeur.Achternaam);
                command.Parameters.AddWithValue("@geboortedatum", chauffeur.Geboortedatum);
                command.Parameters.AddWithValue("@internationaal", chauffeur.Internationaal);
                command.Parameters.AddWithValue("@vrachtwagenchassisnummer", chauffeur.Vrachtwagen.ChassisNummer);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ChauffeurRepositoryException("ChauffeurRepository: VoegChauffeurToe - gefaald", ex);
            }
            finally
            {
                conn.Close();
            }
            return chauffeur;
        }

        public IReadOnlyCollection<Chauffeur> ZoekChauffeurs(string personeelsnummer, string naam, DateTime? geboortedatum, bool? internationaal)
        {
            List<Chauffeur> chauffeurs = new List<Chauffeur>();
            bool isWhere = true;
            bool isAnd = false;

            string query = "SELECT * FROM Chauffeur";

            if (personeelsnummer != null)
            {
                if (isWhere)
                {
                    query += " WHERE ";
                    isWhere = false;
                }
                if (isAnd)
                {
                    query += " AND ";
                }
                else
                {
                    isAnd = true;
                }
                query += "personeelsnummer = @personeelsnummer";
            }
            if (!string.IsNullOrWhiteSpace(naam))
            {
                if (isWhere)
                {
                    query += " WHERE ";
                    isWhere = false;
                }
                if (isAnd)
                {
                    query += " AND ";
                }
                else
                {
                    isAnd = true;
                }
                query += "naam = @naam";
            }
            if (geboortedatum != null)
            {
                if (isWhere)
                {
                    query += " WHERE ";
                    isWhere = false;
                }
                if (isAnd)
                {
                    query += " AND ";
                }
                else
                {
                    isAnd = true;
                }
                query += "geboortedatum = @geboortedatum";
            }
            if (internationaal != null)
            {
                if (isWhere)
                {
                    query += " WHERE ";
                    isWhere = false;
                }
                if (isAnd)
                {
                    query += " AND ";
                }
                else
                {
                    isAnd = true;
                }
                query += "internationaal = @internationaal";
            }
            SqlConnection conn = ConnectionClass.GetConnection();
            using SqlCommand cmd = new(query, conn);
            try
            {
                conn.Open();
                if (personeelsnummer != null)
                {
                    cmd.Parameters.AddWithValue("@personeelsnummer", personeelsnummer);
                }
                if (!string.IsNullOrWhiteSpace(naam))
                {
                    cmd.Parameters.AddWithValue("@naam", naam);
                }
                if (geboortedatum != null)
                {
                    cmd.Parameters.AddWithValue("@geboortedatum", geboortedatum);
                }
                if (internationaal != null)
                {
                    cmd.Parameters.AddWithValue("@internationaal", internationaal);
                }
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   Chauffeur chauffeur = new((string)reader["personeelsnummer"],
                              (string)reader["voornaam"],
                              (string)reader["achternaam"],
                              (DateTime)reader["geboortedatum"],
                              (Boolean)reader["internationaal"]);

                    chauffeurs.Add(chauffeur);
                }
                reader.Close();
                return chauffeurs;
            }
            catch (Exception ex)
            {
                throw new ChauffeurRepositoryException("ChauffeurRepository: ZoekChauffeurs - gefaald", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
