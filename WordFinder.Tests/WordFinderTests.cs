using WordFinderAPI.Services;
using Xunit;

public class WordFinderTests
{
    [Fact]
    public void FindWords_Test()
    {
        // Arrange
        var matrix = new List<string>
        {
            "cold",
            "wind",
            "snow",
            "snow",
            "wind",
            "wind",
            "wind",
            "cold"
        };

        var wordStream = new List<string>
        {
            "cold", "wind", "chill", "snow", "cold", "rain"
        };

        IWordFinderService wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.Contains("cold", result);
        Assert.Contains("wind", result);
        Assert.Contains("snow", result);
        Assert.DoesNotContain("rain", result);
    }
}
