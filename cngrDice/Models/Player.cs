using cngrDice.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace cngrDice.Models
{
    public class Player : Image
    {
        public int HP { get; private set; } = 10;
        public int MP { get; private set; } = 10;
        public int AP { get; private set; } = 0;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int LastX { get; private set; }
        public int LastY { get; private set; }

        Canvas statsCanvas = new Canvas() { Width = 230, Height = 80 };
        public ObservableCollection<Rectangle> HPData { get; private set; }
        public ObservableCollection<Rectangle> MPData { get; private set; }
        public ObservableCollection<Rectangle> APData { get; private set; }

        public Player(int x, int y)
        {
            this.Source = new BitmapImage(new Uri("/Images/Entities/Player.png", UriKind.Relative));
            X = x;
            Y = y;
            Canvas.SetLeft(this, x * 40.0);
            Canvas.SetTop(this, y * 40.0);

            SetLastPosition(x, y);

            HPData = new ObservableCollection<Rectangle>();
            MPData = new ObservableCollection<Rectangle>();
            APData = new ObservableCollection<Rectangle>();
            double hpWidth = 192 / HP;
            double mpWidth = 192 / MP;
            for (int i = 0; i < HP; i++)
            {
                HPData.Add(new Rectangle { Width = hpWidth, Height = 10, Stroke = Brushes.DarkRed });
            }
            for (int i = 0; i < MP; i++)
            {
                MPData.Add(new Rectangle { Width = mpWidth, Height = 10, Stroke = Brushes.DarkBlue });
            }
            for (int i = 0; i < 12; i++)
            {
                APData.Add(new Rectangle { Width = 16, Height = 10, Stroke = Brushes.DarkGreen });
            }
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
            //DoubleAnimation ra = new DoubleAnimation(0, 40, TimeSpan.FromMilliseconds(100));
            //this.BeginAnimation(Canvas.RenderTransformProperty, ra);

            SetAPData(-1);
        }

        public void SetLastPosition(int x, int y)
        {
            LastX = x;
            LastY = y;
        }

        public void SetAPData(int ap)
        {
            if (ap == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    APData[i].Fill = Brushes.Transparent;
                }

                AP = 0;

                return;
            }

            if (ap > 0)
            {
                for (int i = 0; i < ap; i++)
                {
                    APData[i].Fill = Brushes.Green;
                }
            }
            else
            {
                int newAP = AP + ap;
                if (newAP < 0)
                {
                    newAP = 0;
                }

                for (int i = AP - 1; i > newAP - 1; i--)
                {
                    APData[i].Fill = Brushes.Transparent;
                }
            }
            ;

            AP += ap;
        }

        public Canvas StatsCanvas { get => statsCanvas; }
    }
}
