using Npgsql;

namespace MovieappAPI.Helpers
{
    public class SqlDBHelpers
    {
        private string _constr;
        private NpgsqlConnection _conn;

        public SqlDBHelpers(string constr)
        {
            _constr = constr;
            _conn = new NpgsqlConnection(_constr);
            _conn.Open();
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            return new NpgsqlCommand(query, _conn);
        }

        public void Close()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
            }
        }
    }
}
