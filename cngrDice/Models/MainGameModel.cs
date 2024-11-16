using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using cngrDice.ViewModels;
using cngrDice.Commands;
using System.Collections.ObjectModel;

namespace cngrDice.Models
{
    class MainGameModel : BaseViewModel
    {
        Random rnd = new Random();
        Canvas canvas = new Canvas();
        public Canvas GameCanvas { get => canvas; private set => Set(ref canvas, value); }

        Dictionary<string, int[]> shifts = new Dictionary<string, int[]>() { { "W", [0, -1] }, { "S", [0, 1] }, { "A", [-1, 0] }, { "D", [1, 0] } };

        PlayField playField = new PlayField();
        public List<List<char>> Textures { get => playField.Textures; }

        List<List<int>> tiles;

        DicesModel diceModel = new DicesModel();
        Player player;
        List<Enemy> enemies = new List<Enemy>();
        int enemyMoveTime = 500;
        public int AP { get => player.AP; set { player.AP = value; Notify(); } }


        public RelayCommandParameters Move { get => new(movePlayer); }
        private void movePlayer(string key)
        {
            int shiftX = player.X + shifts[key][0];
            int shiftY = player.Y + shifts[key][1];
            int tile = tiles[shiftY][shiftX];

            player.Move(shiftX, shiftY, tile);
            Notify(nameof(AP));
        }


        ObservableCollection<string> diceImages = ["Images/Dices/Dice6.png", "Images/Dices/Dice6.png"];
        public ObservableCollection<string> DiceImages { get => diceImages; set => Set(ref diceImages, value); }
        public RelayCommand DropDices { get => new(dropDices); }
        private void dropDices()
        {
            if (PlayerTurn)
                return;

            int dice1 = rnd.Next(6);
            int dice2 = rnd.Next(6);

            DiceImages[0] = diceModel.Images[dice1];
            DiceImages[1] = diceModel.Images[dice2];

            AP = dice1 + dice2 + 2;

            PlayerTurn = true;
        }

        bool playerTurn;
        public bool PlayerTurn { get => playerTurn; set => Set(ref playerTurn, value); }
        public RelayCommand SwitchTurn { get => new(switchTurn); }

        private async void switchTurn()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Move(tiles, shifts, enemyMoveTime);
            }

            AP = 0;

            //await Task.Run(() =>
            //{
            //    Thread.Sleep(enemyMoveTime * 4);
            //    PlayerTurn = false;
            //});
            await Task.Delay(enemyMoveTime * 4);

            PlayerTurn = false;
        }
        // Доделать текстовый функционал
        string[] texts = new string[] { "1", "2", "3", "4" };
        int counter = 0;

        string storyText;

        public string StoryText { get => storyText; set => Set(ref storyText, value); }

        public RelayCommandParameters ChangeText { get => new(changeText); }

        void changeText(string key)
        {
            int add = int.Parse(key);

            int newValue = counter + add;

            if (newValue < 0 || newValue == texts.Count())
                return;

            counter = newValue;

            StoryText = texts[counter];
        }
        public MainGameModel()
        {
            tiles = playField.Tiles;

            foreach (var enemy in playField.Enemies)
            {
                Enemy newEnemy = new Enemy(enemy[0], enemy[1]);

                GameCanvas.Children.Add(newEnemy);
                enemies.Add(newEnemy);
                tiles[enemy[1]][enemy[0]] = 3;
            }

            player = new Player(playField.PlayerPosition[0], playField.PlayerPosition[1]);

            GameCanvas.Children.Add(player);

            StoryText = texts[0];

            AP = 0;
        }
    }
}
