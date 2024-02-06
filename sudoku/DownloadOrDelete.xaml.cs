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
        Saves savesWindow;

        public DownloadOrDelete(Saves save)
        {
            InitializeComponent();
            savesWindow = save;
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            PlayGround playGround = new PlayGround(Tools.GetArrayOfDigits(0, 80, Tools.choosenSave.Sudoku.Split(',')), Tools.GetArrayOfDigits(0, 80, Tools.choosenSave.Puzzle.Split(',')), Tools.choosenSave.Hardmode, Tools.CountZeros(Tools.GetArrayOfDigits(0, 80, Tools.choosenSave.Puzzle.Split(','))), Tools.choosenSave.Time, Tools.choosenSave.Score);
            savesWindow.Close();
            playGround.Show();
            Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Tools.DeleteSave();
            savesWindow.Close();

            if (Tools.CheckSaves())
            {
                Save[] currentSaves = Tools.GetCurrentSaves();


                Saves currentSavesListView = new Saves(currentSaves);
                currentSavesListView.Show();
            }
            else
            {
                Close();
                LevelSelect levelSelect = new LevelSelect();
                levelSelect.ShowDialog();
            }

            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
