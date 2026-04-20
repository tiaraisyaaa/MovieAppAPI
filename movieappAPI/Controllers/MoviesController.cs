using Microsoft.AspNetCore.Mvc;
using MovieappAPI.Contexts;
using MovieappAPI.Models;

namespace MovieappAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesContext _context;

        public MoviesController(IConfiguration config)
        {
            string constr = config.GetConnectionString("DefaultConnection");
            _context = new MoviesContext(constr);
        }

        // 1. GET list semua film
        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = _context.ListMovies();
            return Ok(new { status = "success", data = movies });
        }

        // 2. GET detail film by id
        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _context.GetMovieById(id);
            if (movie == null)
                return NotFound(new { status = "error", message = "Movie not found" });

            return Ok(new { status = "success", data = movie });
        }

        // 3. POST tambah film baru
        [HttpPost]
        public IActionResult CreateMovie([FromBody] Movies movie)
        {
            var success = _context.InsertMovie(movie);
            if (!success)
                return StatusCode(500, new { status = "error", message = "Failed to insert movie" });

            return Ok(new { status = "success", message = "Movie created", data = movie });
        }

        // 4. PUT update film
        [HttpPut]
        public IActionResult UpdateMovie([FromBody] Movies movie)
        {
            var success = _context.UpdateMovie(movie.Id, movie);
            if (!success)
                return NotFound(new { status = "error", message = "Movie not found" });

            return Ok(new { status = "success", message = "Movie updated", data = movie });
        }


        // 5. DELETE hapus film
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var success = _context.DeleteMovie(id);
            if (!success)
                return NotFound(new { status = "error", message = "Movie not found" });

            return Ok(new { status = "success", message = "Movie deleted" });
        }
    }
}
