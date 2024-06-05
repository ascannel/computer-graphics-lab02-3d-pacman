using System.Drawing;


namespace PacMan
{
    internal class Map
    {
        public int[,] map;
        public int coins = 0;
        public int score = 0;
        public Map(string path) 
        {
            Bitmap bitmap = new Bitmap(path); 
            int width=bitmap.Width;
            int height=bitmap.Height;
            map = new int[width,height];
            for(int i=0;i< width; i++)
            {
                for(int j=0;j< height; j++) 
                {
                    Color clr = bitmap.GetPixel(i,j);
                    if (clr == Color.FromArgb(65, 75, 235))
                    {
                        map[i, j] = 1;
                    }
                    else if (clr == Color.FromArgb(0, 0, 0))
                    {
                        map[i, j] = 0;
                    }
                    else if (clr == Color.FromArgb(255, 184, 151))
                    {
                        map[i, j] = 2; coins++;
                    }
                }
            }
        }
    }
}
