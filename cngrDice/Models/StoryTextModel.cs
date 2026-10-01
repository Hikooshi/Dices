using cngrDice.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cngrDice.Models
{
    internal class StoryTextModel
    {
        string[] texts = new string[] { "1", "2", "3", "4" };
        int counter = 0;

        string storyText = "";

        public string StoryText { get => storyText; private set { storyText = value; } }

        public StoryTextModel() {
            this.setText("start");
        }

        public void changeText(string key)
        {
            int add = int.Parse(key);

            int newValue = counter + add;

            if (newValue < 0 || newValue == texts.Count())
                return;

            counter = newValue;

            StoryText = texts[counter];
        }

        public void setText(string name)
        {
            var uri = new Uri($"pack://application:,,,/Texts/{name}.txt");
            var streamInfo = Application.GetResourceStream(uri);
            if (streamInfo == null)
            {
                texts = [];
                StoryText = "";
                return;
            }

            using (var reader = new StreamReader(streamInfo.Stream, Encoding.UTF8))
            {
                string fullText = reader.ReadToEnd();
                texts = fullText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                StoryText = texts[0];
            }
        }
    }
}
