namespace WordFinderAPI.Services
{
    public class WordFinderService : IWordFinderService
    {
        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var wordFinder = new WordFinder(wordStream);
            var foundWords = wordFinder.Find(wordStream);

            return foundWords;
        }
    }
}
