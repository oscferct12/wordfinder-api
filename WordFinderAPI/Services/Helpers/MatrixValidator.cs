public static class MatrixValidator
{
    public static void Validate(IEnumerable<string> matrix)
    {
        if (matrix == null || !matrix.Any())
            throw new ArgumentException(ErrorMessages.NullOrEmptyMatrix);

        var rowLengths = matrix.Select(row => row.Length).Distinct().ToList();
        if (rowLengths.Count > 1)
            throw new ArgumentException(ErrorMessages.InconsistentRowLengths);

        int numRows = matrix.Count();
        int numCols = rowLengths.First();
        if (numRows > 64 || numCols > 64)
            throw new ArgumentException(ErrorMessages.MatrixSizeExceeded);
    }
}