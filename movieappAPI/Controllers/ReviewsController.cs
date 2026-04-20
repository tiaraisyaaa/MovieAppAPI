using Microsoft.AspNetCore.Mvc;
using MovieappAPI.Contexts;
using MovieappAPI.Models;

namespace MovieappAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewsContext _context;

        public ReviewsController(IConfiguration config)
        {
            string constr = config.GetConnectionString("DefaultConnection");
            _context = new ReviewsContext(constr);
        }

        // 1. GET list semua review
        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _context.ListReviews();
            return Ok(new { status = "success", data = reviews });
        }

        // 2. GET detail review by id
        [HttpGet("{id}")]
        public IActionResult GetReviewById(int id)
        {
            var review = _context.ListReviews().FirstOrDefault(r => r.Id == id);
            if (review == null)
                return NotFound(new { status = "error", message = "Review not found" });

            return Ok(new { status = "success", data = review });
        }

        // 3. POST tambah review baru
        [HttpPost]
        public IActionResult CreateReview([FromBody] Reviews review)
        {
            var success = _context.InsertReview(review);
            if (!success)
                return StatusCode(500, new { status = "error", message = "Failed to insert review" });

            return Ok(new { status = "success", message = "Review created", data = review });
        }

        // 4. PUT update review
        [HttpPut]
        public IActionResult UpdateReview(int id, [FromBody] Reviews review)
        {
            var success = _context.UpdateReview(id, review);
            if (!success)
                return NotFound(new { status = "error", message = "Review not found" });

            return Ok(new { status = "success", message = "Review updated", data = review });
        }

        // 5. DELETE hapus review
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var success = _context.DeleteReview(id);
            if (!success)
                return NotFound(new { status = "error", message = "Review not found" });

            return Ok(new { status = "success", message = "Review deleted" });
        }
    }
}
