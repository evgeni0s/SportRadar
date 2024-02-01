﻿using SportRadar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportRadar
{
    public class MianService
    {
        private DataContext dataContext;
        private MatchRepository matchRepository;
        public MianService()
        {
            dataContext = new();
            matchRepository = new(dataContext);
        }

        public void StartMatch(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrEmpty(homeTeam))
            {
                throw new ArgumentNullException(nameof(homeTeam));
            }

            if (string.IsNullOrEmpty(awayTeam))
            {
                throw new ArgumentNullException(nameof(awayTeam));
            }
            if (homeTeam == awayTeam)
            {
                throw new ArgumentException($"Team name must be unique.");
            }
            matchRepository.AddMatch(homeTeam, awayTeam);
        }

        public int Score(string teamName)
        {
            if (string.IsNullOrEmpty(teamName))
            {
                throw new ArgumentNullException(nameof(teamName));
            }
            var match = matchRepository.GetMatch(teamName);
            if (match == null)
            {
                throw new KeyNotFoundException();
            }
            return match.HomeTeam == teamName ? match.HomeTeamScore : match.AwayTeamScore;
        }
    }
}
