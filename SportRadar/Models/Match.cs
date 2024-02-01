using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportRadar.Models
{
    public record Match()
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}
