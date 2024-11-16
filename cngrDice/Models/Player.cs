using cngrDice.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cngrDice.Models
{
    public class Player: Image
    {
        public int HP { get; set; } = 100;
        public int MP { get; set; } = 100;
        public int AP { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int LastX { get; private set; }
        public int LastY { get; private set; }

        public Player(int x, int y)
        {
            this.Source = new BitmapImage(new Uri("/Images/Entities/Player.png", UriKind.Relative));
            X = x;
            Y = y;
            Canvas.SetLeft(this, x * 40.0);
            Canvas.SetTop(this, y * 40.0);

            AP = 0;

            SetLastPosition(x, y);
        }

        public void Move(int newX, int newY, int tile)
        {
            if (AP < 1 || tile > 0)
            {
                return;
            }

            X = newX;
            Y = newY;

            double x = Canvas.GetLeft(this);
            double y = Canvas.GetTop(this);

            DoubleAnimation daX = new DoubleAnimation(x, X * 40.0, TimeSpan.FromMilliseconds(100));
            DoubleAnimation daY = new DoubleAnimation(y, Y * 40.0, TimeSpan.FromMilliseconds(100));

            this.BeginAnimation(Canvas.LeftProperty, daX);
            this.BeginAnimation(Canvas.TopProperty, daY);

            AP--;
        }

        public void SetLastPosition(int x, int y)
        {
            LastX = x;
            LastY = y;
        }
    }
}
