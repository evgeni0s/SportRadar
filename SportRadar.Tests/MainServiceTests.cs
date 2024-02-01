using SportRadar.Models;

namespace SportRadar.Tests
{
    public class MainServiceTests
    {
        [Test]
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
            TestDelegate act = () =>
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

        [Test]
        public void GetSummary_ValidData_NoException()
        {
            // Arrange
            var mainService = new MianService();
            var matchDataList = new List<(string HomeTeam, string AwayTeam, int HomeTeamScore, int AwayTeamScore)>
            {
                ("Mexico", "Canada", 0, 5),
                ("Spain", "Brazil", 10, 2),
                ("Germany", "France", 2, 2),
                ("Uruguay", "Italy", 6, 6),
                ("Argentina", "Australia", 3, 1)
            };

            foreach (var matchData in matchDataList)
            {
                mainService.StartMatch(matchData.HomeTeam, matchData.AwayTeam);
            }

            foreach (var matchData in matchDataList)
            {
                mainService.UpdateScore(matchData.HomeTeam, matchData.AwayTeam, matchData.HomeTeamScore, matchData.AwayTeamScore);
            }

            // Act
            var summary = mainService.GetSummary();

            // Assert
            Assert.AreEqual(5, summary.Count);

            Assert.AreEqual("1. Uruguay 6 - Italy 6", summary[0]);
            Assert.AreEqual("2. Spain 10 - Brazil 2", summary[1]);
            Assert.AreEqual("3. Mexico 0 - Canada 5", summary[2]);
            Assert.AreEqual("4. Argentina 3 - Australia 1", summary[3]);
            Assert.AreEqual("5. Germany 2 - France 2", summary[4]);
        }

        [Test]
        public void GetSummary_EmptyInput_NoException()
        {
            // Arrange
            var mainService = new MianService();

            // Act
            var summary = mainService.GetSummary();

            // Assert
            Assert.IsNotNull(summary);
            Assert.IsEmpty(summary);
        }
    }
}