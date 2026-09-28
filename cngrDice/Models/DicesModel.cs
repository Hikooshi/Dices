using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class DicesModel
    {
        //private List<string> images = new List<string>();
        //public List<string> Images 
        //{
        //    get => images;
        //}

        //public DicesModel()
        //{
        //    for (int i = 1; i < 7; i++)
        //    {
        //        string path = $"/Images/Dices/dice{i}.png";
        //        Images.Add(path);
        //    }
        //}

        Canvas diceCanvas = new Canvas() { Width = 100, Height = 48 };
        Geometry pg = Geometry.Parse("M 2,0 L 4,8 2,16 0,8 Z");
        //Geometry pg1 = Geometry.Parse("M 2,0 L 4,8 2,16 0,8 Z");
        Path path = new Path();
        Path path1 = new Path();
        List<Path> diceTiles = new List<Path>();
        List<Path> diceTiles1 = new List<Path>();
        Rectangle diceCorner = new Rectangle() { Width = 48, Height = 48, Stroke = Brushes.Black, StrokeThickness = 4 };
        Rectangle diceCorner1 = new Rectangle() { Width = 48, Height = 48, Stroke = Brushes.Black, StrokeThickness = 4 };
        public DicesModel()
        {
            //path.Stroke = Brushes.Black;
            //path.StrokeThickness = 4;
            //path.Data = pg;
            //path.RenderTransformOrigin = new System.Windows.Point(0.5, 1);
            //path1.Stroke = Brushes.Black;
            //path1.StrokeThickness = 4;
            //path1.Data = pg;
            //path1.RenderTransformOrigin = new System.Windows.Point(0.5, 1);
            for (int i = 0; i < 6; i++)
            {
                Path path = new Path();
                path.Stroke = Brushes.Black;
                path.StrokeThickness = 1;
                path.Data = pg;
                path.RenderTransformOrigin = new System.Windows.Point(0.5, 1);
                diceTiles.Add(path);
            }
            for (int i = 0; i < 6; i++)
            {
                Path path1 = new Path();
                path1.Stroke = Brushes.Black;
                path1.StrokeThickness = 1;
                path1.Data = pg;
                path1.RenderTransformOrigin = new System.Windows.Point(0.5, 1);
                diceTiles1.Add(path1);
            }

            for (int i = 0; i < 6; i++)
            {
                Canvas.SetLeft(diceTiles[i], 22);
                Canvas.SetTop(diceTiles[i], 16);
                diceCanvas.Children.Add(diceTiles[i]);
            }
            for (int i = 0; i < 6; i++)
            {
                Canvas.SetLeft(diceTiles1[i], 74);
                Canvas.SetTop(diceTiles1[i], 16);
                diceCanvas.Children.Add(diceTiles1[i]);
            }

            Canvas.SetLeft(diceCorner, 0);
            Canvas.SetTop(diceCorner, 0);
            Canvas.SetLeft(diceCorner1, 52);
            Canvas.SetTop(diceCorner1, 0);

            diceCanvas.Children.Add(diceCorner);
            diceCanvas.Children.Add(diceCorner1);
        }

        public void DropDices(int dice1, int dice2)
        {
            if (dice1 == 1)
            {
                ResetTiles(diceTiles);
            }
            else
            {
                MoveDiceTiles(diceTiles, dice1);
            }

            if (dice2 == 1)
            {
                ResetTiles(diceTiles1);
            }
            else
            {
                MoveDiceTiles(diceTiles1, dice2);
            }

            //DoubleAnimation rotateDiceTile = new DoubleAnimation()
            //{
            //    From = 0,
            //    To = 180,
            //    Duration = TimeSpan.FromSeconds(1)
            //};

            //DoubleAnimation moveDiceTiles = new DoubleAnimation(16, 6, TimeSpan.FromSeconds(1));

            //for (int i = 0; i < 6; i++)
            //{
            //    diceTiles[i].BeginAnimation(Canvas.TopProperty, moveDiceTiles);
            //}

            //diceTiles[3].RenderTransform = new RotateTransform();
            //diceTiles[3].RenderTransform.BeginAnimation(RotateTransform.AngleProperty, rotateDiceTile);
            //rotateTransformDiceTile.BeginAnimation(RotateTransform.AngleProperty, rotateDiceTile);

        }

        void ResetTiles(List<Path> diceTiles)
        {
            for (int i = 0; i < 6; i++)
            {
                diceTiles[i].RenderTransform = new RotateTransform(0);
                //Canvas.SetTop(diceTiles[i], 16);
                diceTiles[i].BeginAnimation(Canvas.TopProperty, null);
            }
        }

        void MoveDiceTiles(List<Path> diceTiles, int value)
        {
            List<DoubleAnimation> rotateAnimations = new List<DoubleAnimation>();

            int degree = 360 / value;

            for (int i = 1; i < value; i++)
            {
                DoubleAnimation rotateDiceTile = new DoubleAnimation(0, degree * i, TimeSpan.FromMilliseconds(240));
                rotateAnimations.Add(rotateDiceTile);
            }

            DoubleAnimation moveDiceTiles = new DoubleAnimation(16, 6, TimeSpan.FromMilliseconds(240));

            for (int i = 0; i < 6; i++)
            {
                diceTiles[i].RenderTransform = new RotateTransform();
                diceTiles[i].BeginAnimation(Canvas.TopProperty, moveDiceTiles);
            }

            for (int i = 0; i < rotateAnimations.Count; i++)
            {
                diceTiles[i].RenderTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimations[i]);
            }
        }

        public Canvas DiceCanvas { get => diceCanvas; }

    }
}
