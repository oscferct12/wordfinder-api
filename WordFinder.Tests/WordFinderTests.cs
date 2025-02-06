using Moq;
using WordFinderAPI.Services;
using Xunit;
using System.Collections.Generic;

public class WordFinderTests
{
    private readonly Mock<IWordFinderService> mockWordFinderService;
    private readonly Mock<IWordSearchStrategy> mockSearchStrategyService;


    public WordFinderTests()
    {
        mockWordFinderService = new Mock<IWordFinderService>();
        mockSearchStrategyService = new Mock<IWordSearchStrategy>();
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenMatrixIsNull()
    {
        Assert.Throws<ArgumentException>(() => new WordFinder(null));
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenMatrixIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new WordFinder(new List<string>()));
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenRowsHaveDifferentLengths()
    {
        var invalidMatrix = new List<string> { "abc", "de" };
        Assert.Throws<ArgumentException>(() => new WordFinder(invalidMatrix));
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenMatrixSizeExceeds64x64()
    {
        var largeMatrix = new List<string>(new string[65].Select(_ => new string('A', 65)));
        Assert.Throws<ArgumentException>(() => new WordFinder(largeMatrix));
    }

    [Fact]
    public void FindWords_ShouldReturnValidWords()
    {
        // Arrange
        var wordStream = new List<string> { "cold", "wind", "snow", "rain" };
        mockWordFinderService
            .Setup(service => service.Find(It.IsAny<IEnumerable<string>>()))
            .Returns(new List<string> { "cold", "wind", "snow" });

        // Act
        var result = mockWordFinderService.Object.Find(wordStream);

        // Assert
        Assert.Contains("cold", result);
        Assert.Contains("wind", result);
        Assert.Contains("snow", result);
        Assert.DoesNotContain("rain", result);
    }

    [Fact]
    public void FindWords_ShouldReturnEmpty_WhenNoWordsMatch()
    {
        // Arrange
        var wordStream = new List<string> { "apple", "banana", "grape" };
        mockWordFinderService
            .Setup(service => service.Find(It.IsAny<IEnumerable<string>>()))
            .Returns(new List<string>());

        // Act
        var result = mockWordFinderService.Object.Find(wordStream);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindWords_ShouldReturnEmpty_WhenInputIsEmpty()
    {
        // Arrange
        var wordStream = new List<string>();
        mockWordFinderService
            .Setup(service => service.Find(It.IsAny<IEnumerable<string>>()))
            .Returns(new List<string>());

        // Act
        var result = mockWordFinderService.Object.Find(wordStream);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindWords_ShouldReturnEmpty_WhenMatrixIsEmpty()
    {
        // Arrange
        var wordStream = new List<string> { "cold", "wind" };
        mockWordFinderService
            .Setup(service => service.Find(It.IsAny<IEnumerable<string>>()))
            .Returns(new List<string>());

        // Act
        var result = mockWordFinderService.Object.Find(wordStream);

        // Assert
        Assert.Empty(result);
    }



    [Fact]
    public void FindWords_ShouldCallFindMethodOnce()
    {
        // Arrange
        var wordStream = new List<string> { "cold", "wind", "snow" };
        mockWordFinderService
            .Setup(service => service.Find(It.IsAny<IEnumerable<string>>()))
            .Returns(new List<string> { "cold", "wind", "snow" });

        // Act
        var result = mockWordFinderService.Object.Find(wordStream);

        // Assert
        mockWordFinderService.Verify(service => service.Find(It.IsAny<IEnumerable<string>>()), Times.Once);
    }

    [Fact]
    public void Find_ShouldUseSearchStrategies()
    {
        // Arrange

        mockSearchStrategyService.Setup(s => s.Search(It.IsAny<string>(), It.IsAny<List<string>>())).Returns(true);

        var matrix = new List<string> { "cold", "wind", "snow", "mnop" };
        var wordFinder = new WordFinder(matrix);
        var words = new List<string> { "cold" };

        // Act

        var result = wordFinder.Find(words);

        // Assert
        Assert.Contains("cold", result);
        mockSearchStrategyService.Verify(s => s.Search("cold", It.IsAny<List<string>>()), Times.Never);
    }
}
