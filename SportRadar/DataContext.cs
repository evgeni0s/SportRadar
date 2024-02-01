using SportRadar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportRadar
{
    internal class DataContext
    {
        public DataContext()
        {
            MatchesOnBoard = new();
        }
        public List<Models.Match> MatchesOnBoard { get; }
    }
}
