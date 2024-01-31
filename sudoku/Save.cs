using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    internal class Save
    {
        private string nickname;
        private int hardmode;
        private int time;
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

        public Save(string nickname, int hardmode, int time, string sudoku, string puzzle)
        {
            this.nickname = nickname;
            this.hardmode = hardmode;
            this.time = time;
            this.sudoku = sudoku;
            this.puzzle = puzzle;
        }
    }
}
