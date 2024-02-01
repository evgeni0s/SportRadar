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
                    IsStarted = true
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


        private bool Filter(Match match, string teamName) => match.HomeTeam == teamName || match.AwayTeam == teamName;

    }
}
