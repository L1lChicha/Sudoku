using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static sudoku.ranks;

namespace sudoku
{
    /// <summary>
    /// Логика взаимодействия для Saves.xaml
    /// </summary>
    public partial class Saves : Window
    {
        private Save[] allCurrentSaves;
        public ListView listView;
        private int positionInList = 1;
        public Saves(Save[] currentSaves)
        {
            InitializeComponent();

            allCurrentSaves = new Save[currentSaves.Length];
            Array.Copy(currentSaves, allCurrentSaves, currentSaves.Length);

            listView = (ListView)FindName("ListViewSaves");
            listView.SelectionChanged += ListView_SelectionChanged;
            listView.Items.Clear();

            ObservableCollection<CurrentPersonSaves> currentPersonSaves = new ObservableCollection<CurrentPersonSaves> ();

            positionInList = 1;
            foreach(Save save in currentSaves)
            {
                currentPersonSaves.Add(new CurrentPersonSaves {position = positionInList, hardmode = save.Hardmode, time = save.Time, score = save.Score});
                positionInList++;
            }

            listView.ItemsSource = currentPersonSaves;
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CurrentPersonSaves selectedSave = (CurrentPersonSaves)listView.SelectedItem;


            if (selectedSave != null)
            {
                Tools.choosenSave = allCurrentSaves[selectedSave.position - 1];
                DownloadOrDelete choose = new DownloadOrDelete(this);

                listView.SelectedItem = null;
                choose.ShowDialog();
            }
        }


        public class CurrentPersonSaves
        {
            public int position {  get; set; }
            public int hardmode { get; set; }
            public int time { get; set; }

            public int score { get; set; }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void newSudokuButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
