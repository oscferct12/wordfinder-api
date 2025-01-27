using System.Text;

namespace WordFinderAPI.Services
{
    public class WordFinder : IWordFinderService
    {
        private readonly List<string> _matrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null || !matrix.Any())
                throw new ArgumentException("The matrix cannot be null or empty.");

            var rowLengths = matrix.Select(row => row.Length).Distinct().ToList();
            if (rowLengths.Count > 1)
                throw new ArgumentException("All rows in the matrix must have the same number of characters.");

            _matrix = matrix.ToList();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var wordCount = new Dictionary<string, int>();

            foreach (var word in wordStream)
            {
                if (IsWordInMatrix(word))
                {
                    if (wordCount.ContainsKey(word))
                        wordCount[word]++;
                    else
                        wordCount[word] = 1;
                }
            }
            return wordCount.OrderByDescending(kv => kv.Value)
                             .Take(10)
                             .Select(kv => kv.Key);
        }

        private bool IsWordInMatrix(string word)
        {
            return SearchWordInMatrix(word);
        }

        private bool SearchWordInMatrix(string word)
        {
            return HorizontalSearch(word) || VerticalSearch(word);
        }

        private bool HorizontalSearch(string word)
        {
            foreach (var row in _matrix)
            {
                if (row.Contains(word))
                    return true;
            }
            return false;
        }

        private bool VerticalSearch(string word)
        {
            int numRows = _matrix.Count;
            int numCols = _matrix[0].Length;

            for (int col = 0; col < numCols; col++)
            {
                StringBuilder sb = new StringBuilder();
                for (int row = 0; row < numRows; row++)
                {
                    sb.Append(_matrix[row][col]);
                }

                if (sb.ToString().Contains(word))
                    return true;
            }
            return false;
        }
    }
}
