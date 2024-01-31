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
        public List<PlayerDataItem> PlayerDataItemList { get; set; }

        public ranks()
        {
            InitializeComponent();

            rankingList.SelectionChanged += ListView_SelectionChanged;

            rankingList.Items.Clear();

            Player[] players = Tools.Sort(Tools.getAllPlayers());

            PlayerDataItemList = new List<PlayerDataItem>();

            int positionInList = 1;
            foreach(Player player in players)
            {
                //MessageBox.Show(player.GetNickname());
                PlayerDataItemList.Add(new PlayerDataItem { 
                    position = positionInList.ToString(),
                    nickName = player.GetNickname().ToString(),
                    score = player.GetScore().ToString(),
                });
                positionInList++;
            }

            rankingList.ItemsSource = PlayerDataItemList;

        }


        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            PlayerDataItem selectedItem = (PlayerDataItem)rankingList.SelectedItem;

            if (selectedItem != null)
            {
                MoreData moreData= new MoreData(selectedItem);
                moreData.Left = 800; 
                moreData.Top =  100;

                moreData.Show();
                rankingList.SelectedItem = null;
            }
        }

        public class PlayerDataItem
        {
            public string position { get; set; }
            public string nickName { get; set; }
            public string score { get; set; }

        }
    }
}
