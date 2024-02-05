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

namespace sudoku
{
    /// <summary>
    /// Логика взаимодействия для Saves.xaml
    /// </summary>
    public partial class Saves : Window
    {

        public ListView listView;
        public Saves(Save[] currentSaves)
        {
            InitializeComponent();

            listView = (ListView)FindName("ListViewSaves");
            listView.Items.Clear();

            ObservableCollection<CurrentPersonSaves> currentPersonSaves = new ObservableCollection<CurrentPersonSaves> ();

            foreach(Save save in currentSaves)
            {
                currentPersonSaves.Add(new CurrentPersonSaves {hardmode = save.Hardmode, time = save.Time});
            }

            listView.ItemsSource = currentPersonSaves;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (sender is Button button && button.Tag is listView data)
            {
                int rowIndex = myListView.Items.IndexOf(data);
                MessageBox.Show($"Button clicked in row {rowIndex}");
            }*/
        }

        public class CurrentPersonSaves
        {
            public int hardmode { get; set; }
            public int time { get; set; }
        }
    }
}
