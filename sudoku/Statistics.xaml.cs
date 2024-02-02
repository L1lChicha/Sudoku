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

        Label nameLabel, easyLevelCountLabel, middleLevelCountLabel, hardLevelCountLabel, bestTimeLabel, scoreLabel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            Close();
            window.Show();
        }

        public Statistics()
        {
            InitializeComponent();

            InitializeLabels();

            PrepareData();
        }

        private void InitializeLabels()
        {
            nameLabel = (Label)FindName("NameLabel");
            easyLevelCountLabel = (Label)FindName("EasyLevelCountLabel");
            middleLevelCountLabel = (Label)FindName("MiddleLevelCountLabel");
            hardLevelCountLabel = (Label)FindName("HardLevelCountLabel");
            bestTimeLabel = (Label)FindName("BestTimeLabel");
            scoreLabel = (Label)FindName("ScoreLabel");
        }

        private void PrepareData()
        {
            Player[] players = Tools.getAllPlayers();
            MessageBox.Show("" + Tools.IsNew(players));

            if (players != null && !Tools.IsNew(players))
            {
                int currentPosition = Tools.FindCurrentPosition(players, Tools.currentNickname);
                

                if(currentPosition >= 0)
                {
                    FillLabels(players[currentPosition]);
                }
                else
                {
                    FillLabels(new Player("No info", 0, 0, 0, 0, 0));
                }
            }
            else
            {
                FillLabels(new Player("No info", 0, 0, 0, 0, 0));
            }
        }

        private void FillLabels(Player player)
        {
            nameLabel.Content = player.GetNickname();
            easyLevelCountLabel.Content = player.GetEasyLevel();
            middleLevelCountLabel.Content = player.GetMiddleLevel();
            hardLevelCountLabel.Content = player.GetHardLevel();
            bestTimeLabel.Content = player.GetBestTime();
            scoreLabel.Content = player.GetScore();
        }
    }
}
