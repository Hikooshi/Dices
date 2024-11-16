using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace cngrDice.Models
{
    internal class DicesModel
    {
        private List<string> images = new List<string>();
        public List<string> Images 
        {
            get => images;
        }

        public DicesModel()
        {
            for (int i = 1; i < 7; i++)
            {
                string path = $"/Images/Dices/dice{i}.png";
                Images.Add(path);
            }
        }
    }
}
