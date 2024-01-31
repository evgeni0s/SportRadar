using SportRadar.Models;

namespace SportRadar.Tests
{
    public class MainServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase()]
        public void Test1()
        {
            // Arrange
            var mainService = new ScoreBoard();
            var homeTeam = "Mexico";
            var awayTeam = "Canada";

            // Act
            mainService.StartMatch(homeTeam, awayTeam);

            // Assert
            Assert.AreEqual(0, mainService.Score(homeTeam));
            Assert.AreEqual(0, mainService.Score(awayTeam));
        }
}