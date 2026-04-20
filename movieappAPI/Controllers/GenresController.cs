using Microsoft.AspNetCore.Mvc;
using MovieappAPI.Contexts;
using MovieappAPI.Models;

namespace MovieappAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly GenresContext _context;

        public GenresController(IConfiguration config)
        {
            string constr = config.GetConnectionString("DefaultConnection");
            _context = new GenresContext(constr);
        }

        // 1. GET list semua genre
        [HttpGet]
        public IActionResult GetGenres()
        {
            var genres = _context.ListGenres();
            return Ok(new { status = "success", data = genres });
        }

        // 2. GET detail genre by id
        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            var genre = _context.ListGenres().FirstOrDefault(g => g.Id == id);
            if (genre == null)
                return NotFound(new { status = "error", message = "Genre not found" });

            return Ok(new { status = "success", data = genre });
        }

        // 3. POST tambah genre baru
        [HttpPost]
        public IActionResult CreateGenre([FromBody] Genres genre)
        {
            var success = _context.InsertGenre(genre);
            if (!success)
                return StatusCode(500, new { status = "error", message = "Failed to insert genre" });

            return Ok(new { status = "success", message = "Genre created", data = genre });
        }

        // 4. PUT update genre
        [HttpPut]
        public IActionResult UpdateGenre(int id, [FromBody] Genres genre)
        {
            var success = _context.UpdateGenre(id, genre);
            if (!success)
                return NotFound(new { status = "error", message = "Genre not found" });

            return Ok(new { status = "success", message = "Genre updated", data = genre });
        }

        // 5. DELETE hapus genre
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var success = _context.DeleteGenre(id);
            if (!success)
                return NotFound(new { status = "error", message = "Genre not found" });

            return Ok(new { status = "success", message = "Genre deleted" });
        }
    }
}
