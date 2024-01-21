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
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics()
        {
            PlayerStatistics playerStatistics = new PlayerStatistics("player1");
            InitializeComponent();

            playerStatistics.RecordLevelCompletion("Easy", 176);
            playerStatistics.RecordLevelCompletion("Middle", 1875);
            playerStatistics.RecordLevelCompletion("Easy",43564);
            playerStatistics.RecordLevelCompletion("Hard",6546);
            playerStatistics.RecordLevelCompletion("Middle",543);

            int easy = playerStatistics.GetLevelCompletionCount("Easy");
            int middle = playerStatistics.GetLevelCompletionCount("Middle");
            int hard = playerStatistics.GetLevelCompletionCount("Hard");

            easyLevelCountLabel.Content = easy;
            middleLevelCountLabel.Content = middle;
            hardLevelCountLabel.Content = hard;


        }
    }
}
