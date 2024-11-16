using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace cngrDice.Converters
{
    internal class CharToTextureConverter : IValueConverter
    {
        static string prefix = "/Images/Textures/";
        static string suffix = "40x40.png";

        Dictionary<char, string> fieldTextures = new Dictionary<char, string>()
        {
            ['B'] = prefix + "Brick" + suffix,
            [' '] = prefix + "Stone" + suffix,
            ['r'] = prefix + "Rock" + suffix,
            ['R'] = prefix + "Element" + suffix,
            ['S'] = prefix + "Terrain" + suffix,
            ['b'] = prefix + "Wood" + suffix,
            ['T'] = prefix + "Tree" + suffix,
            ['G'] = prefix + "Tile" + suffix
        };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (fieldTextures.TryGetValue((char)value, out string str))
                return str;
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
