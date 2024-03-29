﻿using System;
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
using System.Xml;
using static sudoku.ranks;

namespace sudoku
{
    /// <summary>
    /// Логика взаимодействия для MoreData.xaml
    /// </summary>
    public partial class MoreData : Window
    {
        public Label easyLevelCountLabel, middleLevelCountLabel, hardLevelCountLabel, bestTimeLabel, scoreLabel;
        public MoreData(PlayerDataItem item)
        {
            InitializeComponent();

            InitializeLabels();

            PrepareData(item);

        }

        private void InitializeLabels()
        {
            easyLevelCountLabel = (Label)FindName("EasyLevelCountLabel");
            middleLevelCountLabel = (Label)FindName("MiddleLevelCountLabel");
            hardLevelCountLabel = (Label)FindName("HardLevelCountLabel");
            bestTimeLabel = (Label)FindName("BestTimeLabel");
            scoreLabel = (Label)FindName("ScoreLabel");
        }


        private void PrepareData(PlayerDataItem item)
        {
            Player[] players = Tools.getAllPlayers();
            //MessageBox.Show("" + Tools.IsNew(players));

            if (players != null)
            {
                int currentPosition = Tools.FindCurrentPosition(players, item.nickName);


                if (currentPosition >= 0)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void FillLabels(Player player)
        {
            easyLevelCountLabel.Content = player.GetEasyLevel();
            middleLevelCountLabel.Content = player.GetMiddleLevel();
            hardLevelCountLabel.Content = player.GetHardLevel();
            scoreLabel.Content = player.GetScore();

            if (player.GetBestTime() < 3600)
            {
                bestTimeLabel.Content = TimeSpan.FromSeconds(player.GetBestTime()).ToString(@"mm\:ss");
            }
            else
            {
                bestTimeLabel.Content = TimeSpan.FromSeconds(player.GetBestTime()).ToString(@"hh\:mm\:ss");
            }
        }
    }
}
