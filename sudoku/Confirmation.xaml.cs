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
    /// Логика взаимодействия для Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        public Confirmation()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
