using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class Save
    {
        private string nickname;
        private int hardmode;
        private int time;
        private int score;
        private string sudoku;
        private string puzzle;

        public string Nickname { get => nickname; }

        public int Hardmode
        {
            get => hardmode;
            set => hardmode = value;
        }

        public int Time
        {
            get => time;
            set => time = value;
        }

        public int Score
        {
            get => score;
            set => score = value;
        }

        public string Sudoku
        {
            get => sudoku;
            set => sudoku = value;
        }

        public string Puzzle
        {
            get => puzzle;
            set => puzzle = value;
        }

        public Save(string nickname, int hardmode, int time, int score, string sudoku, string puzzle)
        {
            this.nickname = nickname;
            this.hardmode = hardmode;
            this.time = time;
            this.score = score;
            this.sudoku = sudoku;
            this.puzzle = puzzle;
        }
    }
}
