namespace DataLayer
{
    public class ConnectionClass
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-3CJB43N\SQLEXPRESS;Initial Catalog=Transport;Integrated Security=True");
                return sqlConnection;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}