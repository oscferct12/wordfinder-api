using WordFinderAPI.Services;

public class WordFinder : IWordFinderService
{
    private readonly List<string> _matrix;
    private readonly List<IWordSearchStrategy> _strategies;

    public WordFinder(IEnumerable<string> matrix)
    {
        MatrixValidator.Validate(matrix);
        _matrix = matrix.ToList();
        _strategies = new List<IWordSearchStrategy> { new HorizontalSearchStrategy(), new VerticalSearchStrategy() };
    }

    public IEnumerable<string> Find(IEnumerable<string> wordStream)
    {
        var wordCount = new Dictionary<string, int>();
        var uniqueWords = new HashSet<string>(wordStream); 

        foreach (var word in uniqueWords)
        {
            if (IsWordInMatrix(word))
            {
                wordCount[word] = wordStream.Count(w => w == word); 
            }
        }

        return wordCount.OrderByDescending(kv => kv.Value)
                         .Take(10)
                         .Select(kv => kv.Key);
    }

    private bool IsWordInMatrix(string word)
    {
        return _strategies.Any(strategy => strategy.Search(word, _matrix));
    }
}