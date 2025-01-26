namespace WordFinderAPI.Models
{
    public class WordFinderRequest
    {
        public IEnumerable<string> Matrix { get; set; }
        public IEnumerable<string> WordStream { get; set; }
    }
}