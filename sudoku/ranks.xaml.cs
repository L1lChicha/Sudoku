using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ranks.xaml
    /// </summary>
    public partial class ranks : Window
    {
        public List<YourDataItem> YourDataItemList { get; set; }

        public ranks()
        {
            InitializeComponent();

            rankingList.SelectionChanged += ListView_SelectionChanged;

            rankingList.Items.Clear();
       
            YourDataItemList = new List<YourDataItem>
            {
                new YourDataItem { position = "1", name = "Mark", time= "3:21" },
                new YourDataItem { position = "2", name= "Ilya", time = "4:32" }
            };

            rankingList.ItemsSource = YourDataItemList;

             

        }


        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            YourDataItem selectedItem = (YourDataItem)rankingList.SelectedItem;

            if (selectedItem != null)
            {
                MoreData moreData= new MoreData(selectedItem);
                moreData.Left = 800; 
                moreData.Top =  100;

                moreData.Show();
                rankingList.SelectedItem = null;


            }
        }

        

        public class YourDataItem
        {
            public string position { get; set; }

            public string name { get; set; }
            public string time { get; set; }

        }

        

       
    }
}
