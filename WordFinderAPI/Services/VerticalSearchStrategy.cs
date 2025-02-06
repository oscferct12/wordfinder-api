public class VerticalSearchStrategy : IWordSearchStrategy
{
    public bool Search(string word, List<string> matrix)
    {
        int numRows = matrix.Count;
        int numCols = matrix[0].Length;

        for (int col = 0; col < numCols; col++)
        {
            string columnString = string.Concat(matrix.Select(row => row[col]));
            if (columnString.Contains(word))
                return true;
        }
        return false;
    }
}