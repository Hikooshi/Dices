using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cngrDice.Models
{
    public class Enemy : Canvas
    {
        public int HP {get; set;}
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool EnemyTurn = false;
        string[] keys = ["W", "A", "S", "D"];
        Random rnd = new Random();

        public Enemy(int x, int y)
        {
            X = x;
            Y = y;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri("/Images/Entities/Enemy.png", UriKind.Relative));
            Canvas.SetLeft(image, 0);
            Canvas.SetTop(image, 0);

            Rectangle rect = new Rectangle();
            rect.Width = 5;
            rect.Height = 32;
            rect.Fill = Brushes.Red;
            Canvas.SetLeft(rect, 35);
            Canvas.SetTop(rect, 4);

            this.Children.Add(image);
            this.Children.Add(rect);

            Canvas.SetLeft(this, x * 40.0);
            Canvas.SetTop(this, y * 40.0);
        }

        public void Move(List<List<int>> tiles, Dictionary<string, int[]> shifts, int time)
        {
            this.EnemyTurn = true;

            int fullSteps = 4;

            DoubleAnimationUsingKeyFrames animationX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames animationY = new DoubleAnimationUsingKeyFrames();

            animationX.Completed += (s, e) => EnemyTurn = false;

            while (fullSteps > 0)
            {
                string key = keys[this.rnd.Next(4)];

                int newX = this.X + shifts[key][0];
                int newY = this.Y + shifts[key][1];

                if (tiles[newY][newX] == 0)
                {
                    animationX.KeyFrames.Add(new LinearDoubleKeyFrame(newX * 40.0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time))));
                    animationY.KeyFrames.Add(new LinearDoubleKeyFrame(newY * 40.0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time))));

                    time += 500;

                    this.X = newX;
                    this.Y = newY;

                    fullSteps--;
                }

                this.BeginAnimation(Canvas.LeftProperty, animationX);
                this.BeginAnimation(Canvas.TopProperty, animationY);
            }
        }

        private void AnimationX_Completed(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
