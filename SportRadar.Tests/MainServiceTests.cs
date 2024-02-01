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
        public void StartMatch_NoException()
        {
            // Arrange

            var mainService = new MianService();
            var homeTeam = "Mexico";
            var awayTeam = "Canada";

            // Act
            mainService.StartMatch(homeTeam, awayTeam);

            // Assert
            Assert.AreEqual(0, mainService.Score(homeTeam));
            Assert.AreEqual(0, mainService.Score(awayTeam));
        }

        [Test]
        [TestCase("Mexico", null)]
        [TestCase(null, "Canada")]
        [TestCase(null, null)]
        public void StartMatch_NullTeam_ArgumentNullExceptionThrown(string homeTeam, string awayTeam)
        {
            // Arrange
            var mainService = new MianService();

            // Act
            TestDelegate act = () => mainService.StartMatch(homeTeam, awayTeam);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        [TestCase("Canada", "Canada")]
        public void StartMatch_DublicateName_ArgumentNullExceptionThrown(string homeTeam, string awayTeam)
        {
            // Arrange
            var mainService = new MianService();

            // Act
            TestDelegate act = () => mainService.StartMatch(homeTeam, awayTeam);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }


        [Test]
        public void StartMultipleMatches_ValidData_NoExceptionThrown()
        {
            // Arrange
            var mainService = new MianService();
            var matchDataList = new List<(string HomeTeam, string AwayTeam)>
            {
                ("Mexico", "Canada"),
                ("Spain", "Brazil"),
                ("Germany", "France"),
                ("Uruguay", "Italy"),
                ("Argentina", "Australia")
            };

            // Act
            TestDelegate act = () =>
            {
                foreach (var matchData in matchDataList)
                {
                    mainService.StartMatch(matchData.HomeTeam, matchData.AwayTeam);
                }
            };

            // Assert
            Assert.DoesNotThrow(act);
        }


        [Test]
        public void StartMultipleMatches_DublicateTeams_ArgumenExceptionThrown()
        {
            // Arrange
            var mainService = new MianService();
            var matchDataList = new List<(string HomeTeam, string AwayTeam)>
            {
                ("Mexico", "Canada"),
                ("Spain", "Brazil"),
                ("Mexico", "Canada"),
            };

            // Act
            TestDelegate act = () =>
            {
                foreach (var matchData in matchDataList)
                {
                    mainService.StartMatch(matchData.HomeTeam, matchData.AwayTeam);
                }
            };

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Test]
        public void StopMatch_NoException()
        {
            // Arrange
            var mainService = new MianService();
            var homeTeam = "Mexico";
            var awayTeam = "Canada";

            // Act
            mainService.StartMatch(homeTeam, awayTeam);
            TestDelegate act = () =>
            {
                mainService.StopMatch(homeTeam, awayTeam);
            };

            // Assert
            Assert.DoesNotThrow(act);
        }

        [Test]
        public void StopMatch_WithoutStarting_ExceptionThrown()
        {
            // Arrange
            var mainService = new MianService();
            var homeTeam = "Mexico";
            var awayTeam = "Canada";

            // Act
            TestDelegate act = () =>
            {
                mainService.StopMatch(homeTeam, awayTeam);
            };

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Test]
        public void StartAndStop_Multiple_ValidData_NoExceptionThrown()
        {
            // Arrange
            var mainService = new MianService();
            var matchDataList = new List<(string HomeTeam, string AwayTeam)>
            {
                ("Mexico", "Canada"),
                ("Spain", "Brazil"),
                ("Germany", "France"),
                ("Uruguay", "Italy"),
                ("Argentina", "Australia")
            };

            // Act
            TestDelegate startDelegate = () =>
            {
                foreach (var matchData in matchDataList)
                {
                    mainService.StartMatch(matchData.HomeTeam, matchData.AwayTeam);
                }
            };

            TestDelegate stopDelegate = () =>
            {
                foreach (var matchData in matchDataList)
                {
                    mainService.StopMatch(matchData.HomeTeam, matchData.AwayTeam);
                }
            };

            // Assert
            Assert.DoesNotThrow(startDelegate);
            Assert.DoesNotThrow(stopDelegate);
        }


        [Test]
        public void UpdateScore_ValidData_NoExceptionThrown()
        {
            // Arrange
            var mainService = new MianService();
            var homeTeam = "Mexico";
            var awayTeam = "Canada";

            // Act

            mainService.StartMatch(homeTeam, awayTeam);
            mainService.UpdateScore(homeTeam, awayTeam, 7, 5);

            // Assert
            Assert.AreEqual(7, mainService.Score(homeTeam));
            Assert.AreEqual(5, mainService.Score(awayTeam));
        }

        [Test]
        public void UpdateScore_WithoutStarting_ExceptionThrown()
        {
            // Arrange
            var mainService = new MianService();
            var homeTeam = "Mexico";
            var awayTeam = "Canada";

            // Act
            TestDelegate stopDelegate = () =>
            {
                mainService.UpdateScore(homeTeam, awayTeam, 7, 5);
            };

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Test]
        public void UpdateScore_StoppedMatch_ExceptionThrown()
        {
            // Arrange
            var mainService = new MianService();
            var homeTeam = "Mexico";
            var awayTeam = "Canada";

            // Act
            TestDelegate act = () =>
            {
                mainService.StartMatch(homeTeam, awayTeam);
                mainService.StopMatch(homeTeam, awayTeam);
                mainService.UpdateScore(homeTeam, awayTeam, 7, 5);
            };

            // Assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}