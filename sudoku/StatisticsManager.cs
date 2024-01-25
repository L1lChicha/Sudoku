using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sudoku
{
    public class StatisticsManager
    {


        private Dictionary<string, int> levelStatistics;

        public StatisticsManager()
        {
            levelStatistics = new Dictionary<string, int>();
            InitializeStatistics();
        }


        private void InitializeStatistics()
        {
            levelStatistics.Add("Easy", 0);
            levelStatistics.Add("Middle", 0);
            levelStatistics.Add("Hard", 0);
        }


        public void IncreaseLevelCount(string level)
        {
            if (levelStatistics.ContainsKey(level))
            {
                levelStatistics[level]++;
            }
            else
            {
                MessageBox.Show($"Уровень {level} не найден в статистике.");
            }
        }

        public int GetLevelCount(string level)
        {
            if (levelStatistics.ContainsKey(level))
            {
                return levelStatistics[level];
            }
            else
            {
                MessageBox.Show($"Уровень {level} не найден в статистике.");
                return 0;
            }

        }
    }
}
