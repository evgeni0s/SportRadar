using SportRadar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportRadar.Repositories
{
    internal class MatchRepository(DataContext DataContext)
    {
        public void AddMatch(string homeTeam, string awayTeam)
        {
            var match = GetMatch(homeTeam, awayTeam);
            if (match == null)
            {
                var newMatch = new Match()
                {
                    HomeTeam = homeTeam,
                    AwayTeam = awayTeam,
                    StartTime = DateTime.Now,
                };
                DataContext.MatchesOnBoard.Add(newMatch);
            }
            else
            {
                throw new ArgumentException($"Match {homeTeam}:{awayTeam} already exists.");
            }
        }

        public Match? GetMatch(string homeTeam, string awayTeam)
        {
            return DataContext.MatchesOnBoard.FirstOrDefault(m => Filter(m, homeTeam) || Filter(m, awayTeam));
        }

        public Match? GetMatch(string teamName)
        {
            return DataContext.MatchesOnBoard.FirstOrDefault(m => Filter(m, teamName));
        }

        public void DeleteMatch(string homeTeam, string awayTeam)
        {
            var match = GetMatch(homeTeam, awayTeam);
            if (match == null)
            {
                throw new ArgumentException($"Match {homeTeam}:{awayTeam} does not exist.");
            }
            DataContext.MatchesOnBoard.Remove(match);
        }

        public void UpdateMatch(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore)
        {
            var match = GetMatch(homeTeam, awayTeam);
            if (match == null)
            {
                throw new ArgumentException($"Match {homeTeam}:{awayTeam} does not exist.");
            }
            match.HomeTeamScore = homeTeamScore;
            match.AwayTeamScore = awayTeamScore;
        }

        public List<Match> GetMatchesInProgressOrderedByScore()
        {
            return DataContext.MatchesOnBoard
                    .OrderByDescending(m => m.HomeTeamScore + m.AwayTeamScore)
                    .ThenByDescending(m => m.StartTime)
                    .ToList();
        }
        


        private bool Filter(Match match, string teamName) => match.HomeTeam == teamName || match.AwayTeam == teamName;

    }
}
