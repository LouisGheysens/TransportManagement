namespace DataLayer.Repositories
{
    public class OngevalRepository : IOngevalRepository
    {
        public bool BestaatOngeval(Ongeval ongeval)
        {
            SqlConnection conn = ConnectionClass.GetConnection();
            string query = "SELECT COUNT(1) FROM Ongeval WHERE Id = @Id";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    bool result = false;
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int));
                    cmd.Parameters["@Id"].Value = ongeval.Id;

                    Console.WriteLine();
                    result = (int)cmd.ExecuteScalar() == 1 ? true : false;
                    return result;
                }
                catch (Exception ex)
                {
                    throw new OngevalRepositoryException("OngevalRepository: BetsaatOngeval - gefaald!", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VerwijderOngeval(Ongeval ongeval)
        {

            if (!BestaatOngeval(ongeval))
            {
                throw new  OngevalRepositoryException("OngevalRepository: VerwijderOngeval - Ongeval bestaat niet!");
            }
            string queryOngeval = "DELETE FROM Ongeval WHERE Id=@Id";
            SqlConnection connection = ConnectionClass.GetConnection();
            using SqlCommand cmdOngeval = new(queryOngeval, connection);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                #region Onngeval
                cmdOngeval.Transaction = transaction;
                cmdOngeval.Parameters.AddWithValue("@Id", ongeval.Id);
                #endregion
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new OngevalRepositoryException("OngevalRepository: VerwijderOngeval - gefaald", ex);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
        }

        public Ongeval VoegOngevalToe(Ongeval ongeval)
        {
            SqlConnection conn = ConnectionClass.GetConnection();
            if (!BestaatOngeval(ongeval)) ;
            string sql = "INSERT INTO Ongeval(Vrachtwagen, Chauffeur, Arbeidsongeval, Plaats, Datum, PerteTotal, Graad) VALUES(@Vrachtwagen, @Chauffeur, " +
                "@Arbeidsongeval, @Plaats, @Datum, @PerteTotal, @Graad)";
            using SqlCommand command = new(sql, conn);
            try
            {
                conn.Open();
                command.Parameters.AddWithValue("@Vrachtwagen", ongeval.Vrachtwagen.ChassisNummer);
                command.Parameters.AddWithValue("@Chauffeur", ongeval.Chaffeur.PersoneelsNummer);
                command.Parameters.AddWithValue("@Arbeidsongeval", ongeval.Arbeidsongeval);
                command.Parameters.AddWithValue("@Plaats", ongeval.Plaats);
                command.Parameters.AddWithValue("@Datum", ongeval.Datum);
                command.Parameters.AddWithValue("@PerteTotal", ongeval.PerteTotal);
                command.Parameters.AddWithValue("@Graad", ongeval.Graad);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new OngevalRepositoryException("ChauffeurRepository: VoegChauffeurToe - gefaald", ex);
            }
            finally
            {
                conn.Close();
            }
            return ongeval;
        }

        public IReadOnlyCollection<Ongeval> ZoekOngevallen(bool? arbeidsongeval, string plaats, DateTime? datum, bool? pertetotal, ErnstGraad graad)
        {
            List<Ongeval> ongevallen = new List<Ongeval>();
            bool isWhere = true;
            bool isAnd = false;

            string query = "SELECT * FROM Vrachtwagen";

            if (arbeidsongeval != null)
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
                query += "arbeidsongeval = @arbeidsongeval";
            }
            if (!string.IsNullOrWhiteSpace(plaats))
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
                query += "plaats = @plaats";
            }
            if (datum != null)
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
                query += "datum = @datum";
            }
            if (pertetotal != null)
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
                query += "pertetotal = @pertetotal";
            }
            if (!string.IsNullOrWhiteSpace(graad.ToString()))
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
                query += "graad = @graad";
            }
            SqlConnection conn = ConnectionClass.GetConnection();
            using SqlCommand cmd = new(query, conn);
            try
            {
                conn.Open();
                if (arbeidsongeval != null)
                {
                    cmd.Parameters.AddWithValue("@arbeidsongeval", arbeidsongeval);
                }
                if (!string.IsNullOrWhiteSpace(plaats))
                {
                    cmd.Parameters.AddWithValue("@plaats", plaats);
                }
                if (datum != null)
                {
                    cmd.Parameters.AddWithValue("@datum", datum);
                }
                if (pertetotal != null)
                {
                    cmd.Parameters.AddWithValue("@pertetotal", pertetotal);
                }
                if (!string.IsNullOrWhiteSpace(graad.ToString()))
                {
                    cmd.Parameters.AddWithValue("@graad", graad);
                }
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ErnstGraad eersteGraad = (ErnstGraad)Enum.Parse(typeof(ErnstGraad), (string)reader["Graad"]);

                    Ongeval ongeval = new Ongeval((bool)reader["arbeidsongeval"],
                        (Vrachtwagen)reader["vrachtwagen"],
                        (Chauffeur)reader["chauffeur"],
                        (string)reader["plaats"],
                        (DateTime)reader["datum"],
                        (bool)reader["pertetotal"],
                        graad);

                    ongevallen.Add(ongeval);
                }
                reader.Close();
                return ongevallen;
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
