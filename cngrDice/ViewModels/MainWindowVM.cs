using cngrDice.Commands;
using cngrDice.Converters;
using cngrDice.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cngrDice.ViewModels
{
    class MainWindowVM : BaseViewModel
    {
        PlayField field = new PlayField();

        MainGameModel mainGameModel = new MainGameModel();

        public Canvas GameCanvas { get => mainGameModel.GameCanvas; }

        public List<List<char>> Field { get => mainGameModel.Textures; }
        public string StoryText { get => mainGameModel.StoryText; }

        public RelayCommandParameters ChangeText { get => mainGameModel.ChangeText; }

        public MainWindowVM()
        {
            mainGameModel.PropertyChanged += (s, e) => Notify(e.PropertyName);
        }

        public RelayCommandParameters Move { get => mainGameModel.Move; }

        #region Dices ---------------------------------
        public ObservableCollection<string> DiceImages { get => mainGameModel.DiceImages; }
        public RelayCommand DropDicesCommand { get => mainGameModel.DropDices; }
        #endregion ------------------------------------------

        public bool PlayerTurn { get => mainGameModel.PlayerTurn; }
        public RelayCommand SwitchTurn { get => mainGameModel.SwitchTurn; }

        double hpWIdth = 180;
        public double HPWidth
        {
            get => hpWIdth;
            private set => Set(ref hpWIdth, value);
        }

        double mpWidth = 180;
        public double MPWidth
        {
            get => mpWidth;
            private set => Set(ref mpWidth, value);
        }

        public double AP { get => mainGameModel.AP * 15.0; }
    }
}
