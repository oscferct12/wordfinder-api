using Microsoft.AspNetCore.Mvc;
using WordFinderAPI.Services;

namespace WordFinderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordFinderController : ControllerBase
    {
        [HttpPost("find")]
        public IActionResult FindWords([FromBody] WordFinderRequest request)
        {
            var wordFinder = new WordFinder(request.Matrix);
            var result = wordFinder.Find(request.WordStream);

            return Ok(result.Take(10)); // Top 10 most repeated
        }
    }

    public class WordFinderRequest
    {
        public IEnumerable<string> Matrix { get; set; }
        public IEnumerable<string> WordStream { get; set; }
    }
}