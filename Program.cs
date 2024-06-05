namespace PacMan
{
    public static class Program
    {
        public static void Main()
        {
            using (Game game = new Game(900, 900))
            {
                game.Run();
            }
        }
    }
}
