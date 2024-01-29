using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Player
    {
        private string nickname;
        private int easyLevel;
        private int middleLevel;
        private int hardLevel;
        private int bestTime;
        private int score;

        public string GetNickname()
        {
            return nickname;
        }

        public int GetEasyLevel()
        {
            return easyLevel;
        }

        public void SetEasyLevel(int value)
        {
            easyLevel = value;
        }

        public int GetMiddleLevel()
        {
            return middleLevel;
        }

        public void SetMiddleLevel(int value)
        {
            middleLevel = value;
        }

        public int GetHardLevel()
        {
            return hardLevel;
        }

        public void SetHardLevel(int value)
        {
            hardLevel = value;
        }

        public int GetBestTime()
        {
            return bestTime;
        }

        public void SetBestTime(int value)
        {
            bestTime = value;
        }

        public int GetScore()
        {
            return score;
        }

        public void SetScore(int value)
        {
            score = value;
        }

        public Player(string nickname)
        {
            this.nickname = nickname;
            easyLevel = 0;
            middleLevel = 0;
            hardLevel = 0;
            bestTime = 0;
            score = 0;
        }

        public Player(string nickname, int easyLevel, int middleLevel, int hardLevel, int averageTime, int score) { 
            this.nickname = nickname;
            this.easyLevel = easyLevel;
            this.middleLevel = middleLevel;
            this.hardLevel = hardLevel;
            this.bestTime = averageTime;
            this.score = score;

        }
    }
}
