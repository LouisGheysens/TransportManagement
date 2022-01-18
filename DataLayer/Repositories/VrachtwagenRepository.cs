namespace DataLayer.Repositories
{
    public class VrachtwagenRepository : IVrachtwagenRepository
    {
        public bool BestaatVrachtwagen(string chassisnummer)
        {

            SqlConnection conn = ConnectionClass.GetConnection();
            string query = "SELECT COUNT(*) FROM Vrachtwagen WHERE chassisnummer=@chassisnummer";
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = query;
                conn.Open();
                try
                {
                    command.Parameters.AddWithValue("@chassisnummer", chassisnummer);
                    int exists = (int)command.ExecuteScalar();
                    if (exists > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new ChauffeurRepositoryException("ChaufeurRepository: BestaatChauffeur - gefaald", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Vrachtwagen SelecteerVrachtwagen(string chassisnummer)
        {
            string query = "SELECT v.* FROM Vrachtwagen v " +
                "LEFT JOIN Chauffeur c ON v.chassisnummer=c.Vrachtwagenchassisnummer " +
                "WHERE v.chassisnummer=@chassisnummer";
            Chauffeur chauffeur = null;
            Vrachtwagen vrachtwagen = null;
            SqlConnection conn = ConnectionClass.GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@chassisnummer", chassisnummer);
                conn.Open();
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            #region Vrachtwagen
                            if (vrachtwagen == null && !reader.IsDBNull(reader.GetOrdinal("chassisnummer")))
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
                            #region Chauffeur
                            if (chauffeur == null && !reader.IsDBNull(reader.GetOrdinal("personeelsnummer")))
                            {
                                chauffeur = new((string)reader["personeelsnummer"],
                                    (string)reader["voornaam"],
                                    (string)reader["achternaam"],
                                    (DateTime)reader["geboortedatum"],
                                    (Boolean)reader["internationaal"]);
                            }
                            #endregion
                        }
                        reader.Close();
                        if (chauffeur != null) { vrachtwagen.UpdateChauffeur(chauffeur); }
                        return vrachtwagen;
                    }
                }
                catch (Exception ex)
                {
                    throw new ChauffeurException("ChaffeurRepository: SelecteerChauffeur - gefaald", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Vrachtwagen UpdateVrachtwagen(Vrachtwagen vrachtwagen)
        {
            SqlConnection conn = ConnectionClass.GetConnection();
            Vrachtwagen huidigeVrachtwagen = this.SelecteerVrachtwagen(vrachtwagen.ChassisNummer);
            if (huidigeVrachtwagen.Equals(vrachtwagen)) throw new ChauffeurRepositoryException("VrachtwagenRepository: UpdateVrachtwagen - Er werden geen verschillen gevonden!");
            string sql = "UPDATE Vrachtwagen SET Merk = @Merk, " +
                "Model = @Model, Gewicht = @Gewicht, Aanhangwagen = @Aanhangwagen, " +
                "Brandstof = @Brandstof, " +
                "Chauffeur = @Chauffeur WHERE ChassisNummer = @ChassisNummer";
            using SqlCommand command = new(sql, conn);
            try
            {
                conn.Open();
                command.Parameters.AddWithValue("@Merk", vrachtwagen.Merk);
                command.Parameters.AddWithValue("@Model", vrachtwagen.Model);
                command.Parameters.AddWithValue("@Gewicht", vrachtwagen.Gewicht);
                command.Parameters.AddWithValue("@Aanhangwagen", vrachtwagen.HeeftAanhangWagen);
                command.Parameters.AddWithValue("@Brandstof", vrachtwagen.Brandstof);
                command.Parameters.AddWithValue("@Chauffeur", vrachtwagen.Chauffeur.PersoneelsNummer);
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
            return vrachtwagen;
        }

        public void VerwijderVrachtwagen(Vrachtwagen vrachtwagen)
        {
            if (!BestaatVrachtwagen(vrachtwagen.ChassisNummer))
            {
                throw new ChauffeurRepositoryException("ChauffeurRepository: DeleteChaffeur - Chauffeur bestaat niet!");
            }
            string queryVrachtwagen = "DELETE FROM Vrachtwagen WHERE chassisnummer=@chassisnummer";
            string queryChaffeur = "UPDATE Chaffeur SET Vrachtwagenchassisnummer = NULL WHERE Vrachtwagenchassisnummer=@Vrachtwagenchassisnummer";
            SqlConnection connection = ConnectionClass.GetConnection();
            using SqlCommand cmdVrachtwagen = new(queryVrachtwagen, connection);
            using SqlCommand cmdChauffeur = new(queryChaffeur, connection);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                #region Chauffeur
                cmdVrachtwagen.Transaction = transaction;
                cmdVrachtwagen.Parameters.AddWithValue("@chassisnummer", vrachtwagen.ChassisNummer);
                cmdChauffeur.ExecuteNonQuery();
                #endregion

                #region Vrachtwagen
                if (vrachtwagen.Chauffeur != null)
                {
                    cmdChauffeur.Transaction = transaction;
                    cmdChauffeur.Parameters.AddWithValue("@Vrachtwagenchassisnummer", vrachtwagen.Chauffeur.Vrachtwagen);
                }
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

        public Vrachtwagen VoegVrachtwagenToe(Vrachtwagen vrachtwagen)
        {
            SqlConnection conn = ConnectionClass.GetConnection();
            if (!BestaatVrachtwagen(vrachtwagen.ChassisNummer));
            string sql = "INSERT INTO Chauffeur(Merk, Model, Gewicht, Aanhangwagen, Brandstof, Chauffeur) VALUES(@Merk, @Model, " +
                "@Gewicht, @Aanhangwagen, @Brandstof, @Chauffeur)";
            using SqlCommand command = new(sql, conn);
            try
            {
                conn.Open();
                command.Parameters.AddWithValue("@Merk", vrachtwagen.Merk);
                command.Parameters.AddWithValue("@Model", vrachtwagen.Model);
                command.Parameters.AddWithValue("@Gewicht", vrachtwagen.Gewicht);
                command.Parameters.AddWithValue("@Aanhangwagen", vrachtwagen.HeeftAanhangWagen);
                command.Parameters.AddWithValue("@Brandstof", vrachtwagen.Brandstof);
                command.Parameters.AddWithValue("@Chauffeur", vrachtwagen.Chauffeur.PersoneelsNummer);
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
            return vrachtwagen;
        }

        public IReadOnlyCollection<Vrachtwagen> ZoekVrachtwagen(string chassisnummer, string merk, string model, Brandstof brandstof, float gewicht)
        {
            List<Vrachtwagen> vrachtwagens = new List<Vrachtwagen>();
            bool isWhere = true;
            bool isAnd = false;

            string query = "SELECT * FROM Vrachtwagen";

            if (chassisnummer != null)
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
                query += "chassisnummer = @chassisnummer";
            }
            if (!string.IsNullOrWhiteSpace(merk))
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
                query += "merk = @merk";
            }
            if (!string.IsNullOrWhiteSpace(model))
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
                query += "model = @model";
            }
            if (string.IsNullOrWhiteSpace(brandstof.ToString()))
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
                query += "brandstof = @brandstof";
            }
            if (gewicht != 0)
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
                query += "gewicht = @gewicht";
            }
            SqlConnection conn = ConnectionClass.GetConnection();
            using SqlCommand cmd = new(query, conn);
            try
            {
                conn.Open();
                if (chassisnummer != null)
                {
                    cmd.Parameters.AddWithValue("@chassisnummer", chassisnummer);
                }
                if (!string.IsNullOrWhiteSpace(merk))
                {
                    cmd.Parameters.AddWithValue("@merk", merk);
                }
                if (!string.IsNullOrWhiteSpace(model))
                {
                    cmd.Parameters.AddWithValue("@model", model);
                }
                if (!string.IsNullOrWhiteSpace(brandstof.ToString()))
                {
                    cmd.Parameters.AddWithValue("@brandstof", brandstof);
                }
                if (gewicht != 0)
                {
                    cmd.Parameters.AddWithValue("@gewicht", gewicht);
                }
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Brandstof brandstofEnum = (Brandstof)Enum.Parse(typeof(Brandstof), (string)reader["brandstof"]);

                    Vrachtwagen vrachtwagen = new Vrachtwagen(((string)reader["chassisnummer"]),
                                            (string)reader["merk"],
                                            (string)reader["model"],
                                            (float)reader["gewicht"],
                                            (Boolean)reader["aanhangwagen"],
                                            brandstofEnum);

                    vrachtwagens.Add(vrachtwagen);
                }
                reader.Close();
                return vrachtwagens;
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
