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
                    AwayTeam = awayTeam
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

        private bool Filter(Match match, string teamName) => match.AwayTeam == teamName || match.AwayTeam == teamName;

    }
}
