using Microsoft.AspNetCore.Mvc;
using WordFinderAPI.Models;
using WordFinderAPI.Services;

namespace WordFinderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordFinderController : ControllerBase
    {

        private readonly IWordFinderService _wordFinderService;

        public WordFinderController(IWordFinderService wordFinderService)
        {
            _wordFinderService = wordFinderService;
        }

        [HttpPost("find")]
        public IActionResult FindWords([FromBody] WordFinderRequest request)
        {
            var result = _wordFinderService.Find(request.Matrix);

            return Ok(result);
        }
    }
}
