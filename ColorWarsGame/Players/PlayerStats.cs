using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars.Players
{
    class PlayerStats
    {
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Territory { get; set; }

        public PlayerStats()
        {
            this.Territory = 1;
        }
    }
}
