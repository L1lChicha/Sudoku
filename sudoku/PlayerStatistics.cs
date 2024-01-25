using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class PlayerStatistics
    {

        public string PlayerName { get; set; }
        public Dictionary<string, int> LevelCompletionCount { get; private set; }
        public Dictionary<string, double> AverageTimePerLevel { get; private set; }

        public PlayerStatistics(string playerName)
        {
            PlayerName = playerName;
            LevelCompletionCount = new Dictionary<string, int>();
            AverageTimePerLevel = new Dictionary<string, double>();
        }

        public void RecordLevelCompletion(string difficulty, double timeSpent)
        {
            if (LevelCompletionCount.ContainsKey(difficulty))
            {
                LevelCompletionCount[difficulty]++;
                AverageTimePerLevel[difficulty] = ((AverageTimePerLevel[difficulty] * (LevelCompletionCount[difficulty] - 1)) + timeSpent) / LevelCompletionCount[difficulty];
            }
            else
            {
                LevelCompletionCount[difficulty] = 1;
                AverageTimePerLevel[difficulty] = timeSpent;
            }
        }

        public int GetLevelCompletionCount(string difficulty)
        {
            return LevelCompletionCount.ContainsKey(difficulty) ? LevelCompletionCount[difficulty] : 0;
        }

        public double GetAverageTimePerLevel(string difficulty)
        {
            return AverageTimePerLevel.ContainsKey(difficulty) ? AverageTimePerLevel[difficulty] : 0.0;
        }

    }
}
