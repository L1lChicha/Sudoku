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
    /// Логика взаимодействия для LevelSelect.xaml
    /// </summary>
    public partial class LevelSelect : Window
    {
        public LevelSelect()
        {
            InitializeComponent();


            if(easyRadioButton.IsChecked == true)
            {

                PlayGround playGround = new PlayGround();
                playGround.Show();
            }
            else if(middleRadioButton.IsChecked == true) 
            {

                PlayGround playGround = new PlayGround();
                playGround.Show();

            }
            else if(hardRadioButton.IsChecked == true)
            {

                PlayGround playGround = new PlayGround();
                playGround.Show();
            }



        }
    }
}
