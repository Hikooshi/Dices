using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace cngrDice.Models
{
    internal class PlayField
    {
        string map = @"
BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB
BP  T       SRS                B
B  rT       SRS    r      r    B
B   T     TSSRS        T       B
B   T      SRRS    r     e     B
B          SRS             r   B
B           R    T   r  T      B
B           R b                B
B    r      RRbRR              B
B             b RR   T         B
B        T       RR      e     B
B            r   GRRGGG T      B
B       r        GGRRGG        B
B                GGGRRG  T     B
B                GGGGRR        B
BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB
";
        List<List<int>> tiles = new List<List<int>>();
        List<int[]> enemies = new List<int[]>();
        List<List<char>> textures = new List<List<char>>();
        int[] playerPosition = new int[2];
        char[] passable = [' ', 'b', 'S', 'G', 'e', 'P'];

        public PlayField()
        {
            string[] mapStrings = map.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 16; i++)
            {
                List<char> img = new List<char>();
                List<int> tile = new List<int>();

                for (int j = 0; j < 32; j++)
                {
                    char str = mapStrings[i][j];
                    int nmb;

                    if (passable.Contains(str))
                        nmb = 0;
                    else
                        nmb = 1;

                    if (str == 'e')
                    {
                        enemies.Add(new int[2] { j, i });
                        str = ' ';
                    }
                    else if (str == 'P')
                    {
                        playerPosition[0] = j;
                        playerPosition[1] = i;
                        str = ' ';
                    }

                    img.Add(str);
                    tile.Add(nmb);
                }

                textures.Add(img);
                tiles.Add(tile);
            }
        }

        public List<List<char>> Textures { get => textures; }
        public List<List<int>> Tiles { get => tiles; }
        public List<int[]> Enemies { get => enemies; }
        public int[] PlayerPosition { get => playerPosition; }

        public static (int, int) GetTouchPosition(Point point)
        {
            int x = (int)Math.Floor(point.X / 40.0);
            int y = (int)Math.Floor(point.Y / 40.0);

            return (x, y);
        }
    }
}
