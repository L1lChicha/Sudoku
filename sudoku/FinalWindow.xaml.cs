using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sudoku
{
    /// <summary>
    /// Логика взаимодействия для FinalWindow.xaml
    /// </summary>
    public partial class FinalWindow : Window
    {
        Label timeLabel, scoreLabel, difficultyLabel;

        string difficulty;
        public FinalWindow(int time, int score, int hardmode)
        {
            InitializeComponent();

            switch (hardmode)
            {
                case 1:
                    difficulty = "Easy";
                    break;
                case 2:
                    difficulty = "Middle";
                    break;
                case 3:
                    difficulty = "Hard";
                    break;
            }

            timeLabel = (Label)FindName("TimeLabel");
            scoreLabel = (Label)FindName("ScoreLabel");
            difficultyLabel = (Label)FindName("DifficultyLabel");

            if (time < 3600)
            {
                timeLabel.Content = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
            }
            else
            {
                timeLabel.Content = TimeSpan.FromSeconds(time).ToString(@"hh\:mm\:ss");
            }

            scoreLabel.Content = score;
            difficultyLabel.Content = difficulty;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
