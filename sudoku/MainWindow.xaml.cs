using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            
        }

        private void ranksButton_Click(object sender, RoutedEventArgs e)
        {
            ranks ranks = new ranks();
            ranks.Show();
            Close();
        }

        private void statisticsButton_Click(object sender, RoutedEventArgs e)
        {

            if (putName())
            {
                Tools.currentNickname = nameTextBox.Text;
                Statistics statistics = new Statistics();
                statistics.Show();
                Close();
            }

        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {

            if (putName())
            {
                Tools.currentNickname = nameTextBox.Text;

                if (Tools.CheckSaves())
                {
                    Save[] saves = Tools.GetCurrentSaves();

                    foreach (Save s in saves)
                    {
                        MessageBox.Show(s.Nickname + s.Time);
                    }

                    Saves currentSavesListView = new Saves(saves);
                    currentSavesListView.Show();
                }
                else
                {
                    LevelSelect levelSelect = new LevelSelect();
                    levelSelect.ShowDialog();
                    Close();
                }

                
            }
        }

        




        public bool putName()
        {
         
            
            if (nameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Put nickname in the field","", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }

        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }
    }
}