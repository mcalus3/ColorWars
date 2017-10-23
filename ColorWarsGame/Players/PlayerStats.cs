namespace ColorWars.Players
{
    internal class PlayerStats
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
