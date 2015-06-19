using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypingTest
{
    public partial class TypingTest : Form
    {
        string input = string.Empty;
        string word = string.Empty;
        int score = 0;
        string[] words = File.ReadAllLines(@"words.txt");
        Random random = new Random();
        int gameTime = 60;
        List<string> quickWords = new List<string>();

        public TypingTest()
        {
            InitializeComponent();
            wordLabel.Text = string.Empty;
            wordLabel.TextAlign = ContentAlignment.MiddleCenter;
            gameLabel.Text = string.Empty;
            gameLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeLabel.Text = "Time:\r\n" + gameTime.ToString();
            timeLabel.Refresh();
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            inputTextBox.TextAlign = HorizontalAlignment.Center;
            scoreLabel.Width = gameLabel.Width = inputTextBox.Width;
            int randomWord = random.Next(1, words.Length - 1);
            word = words[randomWord];
            wordLabel.TextAlign = ContentAlignment.MiddleCenter;
            wordLabel.Text = word;
            wordLabel.Refresh();
            quickWords = generateWords();
            timer.Start();
        }

        private void roundTimer_Tick(object sender, EventArgs e)
        {
            gameTime--;
            timeLabel.Text = "Time:\r\n" + gameTime.ToString();
            timeLabel.Refresh();

            if (gameTime == 0)
            {
                gameLabel.Text = "Game over!";
                scoreLabel.Text = "Score: " + score + " WPM";
                inputTextBox.Enabled = false;
                timer.Stop();
                roundTimer.Stop();
                return;
            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {

            scoreLabel.Text = "Score: " + score.ToString();
            scoreLabel.Refresh();

            input = inputTextBox.Text;

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            else
            {
                if (!roundTimer.Enabled)
                {
                    roundTimer.Enabled = true;
                }

                char[] inputChars = input.ToCharArray();

                foreach (char character in inputChars)
                {
                    if (character == ' ')
                    {


                        if (input.Trim() == word)
                        {
                            score++;
                        }

                        inputTextBox.Clear();
                        int randomWord = random.Next(1, words.Length - 1);
                        word = words[randomWord];
                        wordLabel.Text = word;
                        wordLabel.Refresh();

                    }
                }

            }
        }

        private List<string> generateWords()
        {
            List<string> wordsToDisplay = new List<string>();

            int randomWord = random.Next(1, words.Length - 1);

            wordsToDisplay.Add(words[randomWord]);
            wordsToDisplay.Add(words[randomWord]);
            wordsToDisplay.Add(words[randomWord]);
            wordsToDisplay.Add(words[randomWord]);
            wordsToDisplay.Add(words[randomWord]);

            return wordsToDisplay;
        }


    }
}
