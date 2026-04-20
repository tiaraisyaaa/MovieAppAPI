using MovieappAPI.Helpers;
using MovieappAPI.Models;
using Npgsql;

namespace MovieappAPI.Contexts
{
    public class ReviewsContext
    {
        private string __constr;
        private string __ErrorMsg;

        public ReviewsContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Reviews> ListReviews()
        {
            List<Reviews> reviews = new List<Reviews>();
            string query = @"SELECT id, movie_id, username, comment, rating FROM reviews;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new Reviews
                        {
                            Id = reader.GetInt32(0),
                            MovieId = reader.GetInt32(1),
                            Username = reader.GetString(2),
                            Comment = reader.GetString(3),
                            Rating = reader.GetDecimal(4)
                        });
                    }
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; }
            finally { db.Close(); }

            return reviews;
        }

        public bool InsertReview(Reviews review)
        {
            string query = @"INSERT INTO reviews (movie_id, username, comment, rating, created_at, updated_at)
                             VALUES (@movie_id, @username, @comment, @rating, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
                             RETURNING id;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@movie_id", review.MovieId);
                    cmd.Parameters.AddWithValue("@username", review.Username);
                    cmd.Parameters.AddWithValue("@comment", review.Comment);
                    cmd.Parameters.AddWithValue("@rating", review.Rating);

                    var newId = cmd.ExecuteScalar();
                    review.Id = Convert.ToInt32(newId);
                    return true;
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
            finally { db.Close(); }
        }

        public bool UpdateReview(int id, Reviews review)
        {
            string query = @"UPDATE reviews SET comment=@comment, rating=@rating, updated_at=CURRENT_TIMESTAMP WHERE id=@id;";
            SqlDBHelpers db = new SqlDBHelpers(this.__constr);

            try
            {
                using (NpgsqlCommand cmd = db.GetNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@comment", review.Comment);
                    cmd.Parameters.AddWithValue("@rating", review.Rating);
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
            finally { db.Close(); }
        }

        public bool DeleteReview(int id)
        {
            string query = @"DELETE FROM reviews WHERE id=@id;";
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
