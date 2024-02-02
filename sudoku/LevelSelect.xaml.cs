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

namespace sudoku
{
    /// <summary>
    /// Логика взаимодействия для LevelSelect.xaml
    /// </summary>
    public partial class LevelSelect : Window
    {


        public LevelSelect()
        {
            InitializeComponent();


          
        }


        private void easyLevelButton_Click(object sender, RoutedEventArgs e)
        {
            PlayGround playGround = new PlayGround();
            playGround.Show();
            Close();
        }

        private void middleLevelButton_Click(object sender, RoutedEventArgs e)
        {
            PlayGround playGround = new PlayGround();
            playGround.Show();
            Close();
        }

        private void hardLevelButton_Click(object sender, RoutedEventArgs e)
        {
            PlayGround playGround = new PlayGround();
            playGround.Show();

            Close();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();   
        }


    }
}
