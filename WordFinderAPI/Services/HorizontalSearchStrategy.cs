public class HorizontalSearchStrategy : IWordSearchStrategy
{
    public bool Search(string word, List<string> matrix)
    {
        return matrix.Any(row => row.Contains(word));
    }
}