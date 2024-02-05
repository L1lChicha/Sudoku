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
    /// Логика взаимодействия для DownloadOrDelete.xaml
    /// </summary>
    public partial class DownloadOrDelete : Window
    {
        public DownloadOrDelete()
        {
            InitializeComponent();
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            PlayGround playGround = new PlayGround(Tools.GetArrayOfDigits(0, 80, Tools.choosenSave.Sudoku.Split(',')), Tools.GetArrayOfDigits(0, 80, Tools.choosenSave.Puzzle.Split(',')));
            playGround.Show();
            Close();
            /*int[,] data = Tools.GetArrayOfDigits(0, 80, Tools.choosenSave.Puzzle.Split(','));
            foreach (int i in data)
            {
                MessageBox.Show(i + ".");
            }*/
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Tools.DeleteSave();
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
