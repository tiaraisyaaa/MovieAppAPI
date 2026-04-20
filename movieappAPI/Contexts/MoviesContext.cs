using MovieappAPI.Helpers;
using MovieappAPI.Models;
using Npgsql;

namespace MovieappAPI.Contexts
{
    public class MoviesContext
    {
        private string __constr;
        private string __ErrorMsg;

        public MoviesContext(string pConstr)
        {
            __constr = pConstr;
        }

        // GET list semua film
        public List<Movies> ListMovies()
        {
            List<Movies> movies = new List<Movies>();
            string query = @"SELECT id, title, genre_id, release_year FROM movies;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movies
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            GenreId = reader.GetInt32(2),
                            ReleaseYear = reader.GetInt32(3)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            finally
            {
                db.Close();
            }

            return movies;
        }

        // GET detail film by id
        public Movies? GetMovieById(int id)
        {
            Movies? movie = null;
            string query = @"SELECT id, title, genre_id, release_year FROM movies WHERE id=@id;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            movie = new Movies
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                GenreId = reader.GetInt32(2),
                                ReleaseYear = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; }
            finally { db.Close(); }

            return movie;
        }

        // POST tambah film baru
        public bool InsertMovie(Movies movie)
        {
            string query = @"INSERT INTO movies (title, genre_id, release_year, created_at, updated_at)
                             VALUES (@title, @genre_id, @release_year, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
                             RETURNING id;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@title", movie.Title);
                    cmd.Parameters.AddWithValue("@genre_id", movie.GenreId);
                    cmd.Parameters.AddWithValue("@release_year", movie.ReleaseYear);

                    var newId = cmd.ExecuteScalar();
                    movie.Id = Convert.ToInt32(newId);
                    return true;
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
            finally { db.Close(); }
        }

        // PUT update film
        public bool UpdateMovie(int id, Movies movie)
        {
            string query = @"UPDATE movies SET title=@title, genre_id=@genre_id, release_year=@release_year, updated_at=CURRENT_TIMESTAMP WHERE id=@id;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@title", movie.Title);
                    cmd.Parameters.AddWithValue("@genre_id", movie.GenreId);
                    cmd.Parameters.AddWithValue("@release_year", movie.ReleaseYear);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
            finally { db.Close(); }
        }

        // DELETE hapus film
        public bool DeleteMovie(int id)
        {
            string query = @"DELETE FROM movies WHERE id=@id;";
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
