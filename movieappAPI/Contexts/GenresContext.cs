
using MovieappAPI.Helpers;
using MovieappAPI.Models;
using Npgsql;

namespace MovieappAPI.Contexts
{
    public class GenresContext
    {
        private string __constr;
        private string __ErrorMsg;

        public GenresContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Genres> ListGenres()
        {
            List<Genres> genres = new List<Genres>();
            string query = @"SELECT id, name FROM genres;";
            SqlDBHelpers db = new SqlDBHelpers (this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genres.Add(new Genres
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; }
            finally { db.Close(); }

            return genres;
        }

        public bool InsertGenre(Genres genre)
        {
            string query = @"INSERT INTO genres (name, created_at, updated_at)
                             VALUES (@name, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
                             RETURNING id;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@name", genre.Name);
                    var newId = cmd.ExecuteScalar();
                    genre.Id = Convert.ToInt32(newId);
                    return true;
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
            finally { db.Close(); }
        }

        public bool UpdateGenre(int id, Genres genre)
        {
            string query = @"UPDATE genres SET name=@name, updated_at=CURRENT_TIMESTAMP WHERE id=@id;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@name", genre.Name);
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
            finally { db.Close(); }
        }

        public bool DeleteGenre(int id)
        {
            string query = @"DELETE FROM genres WHERE id=@id;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
            finally { db.Close(); }
        }
    }
}
