using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Formats.Asn1.AsnWriter;

namespace sudoku
{
    class Tools
    {
        private static string statisticsFilePath = "statistics.txt";
        private static string savesFilePath = "saves.txt";

        public static string currentNickname = "";
        public static int numberOfPlayers;
        public static int numberOfSaves;


        public static void AddInformation(int time, int score, int hardmode)
        {
            if (ExistenceCheck(statisticsFilePath))
            {
                Player[] players = getAllPlayers();

                if (IsNew(players))
                {
                    AddNewPlayer(players, time, score, hardmode);
                }
                else
                {
                    ChangeData(players, time, score, hardmode);
                }
            }
            else
            {
                AddFirstPlayer(time, score, hardmode);
            }
        }

        private static void ChangeData(Player[] players, int time, int score, int hardmode)
        {
            int currentPosition = FindCurrentPosition(players, currentNickname);

            if (currentPosition != -1)
            {
                players[currentPosition].SetScore(score + players[currentPosition].GetScore());

                if (players[currentPosition].GetBestTime() > time)
                {
                    players[currentPosition].SetBestTime(time);
                }

                switch (hardmode)
                {
                    case 1:
                        players[currentPosition].SetEasyLevel(players[currentPosition].GetEasyLevel() + 1);
                        break;
                    case 2:
                        players[currentPosition].SetMiddleLevel(players[currentPosition].GetMiddleLevel() + 1);
                        break;
                    case 3:
                        players[currentPosition].SetHardLevel(players[currentPosition].GetHardLevel() + 1);
                        break;
                }

                using (StreamWriter writer = new StreamWriter(statisticsFilePath))
                {
                    writer.WriteLine(numberOfPlayers);

                    foreach (Player player in players)
                    {
                        writer.WriteLine($"{player.GetNickname()},{player.GetEasyLevel()},{player.GetMiddleLevel()},{player.GetHardLevel()},{player.GetBestTime()},{player.GetScore()}");
                    }
                }
            }
        }

        public static int FindCurrentPosition(Player[] players, string nickname) 
        { 
            for(int i = 0; i < players.Length; i++){
                if (players[i].GetNickname() == nickname)
                {
                    return i;
                }
            }
            return -1;
        }

        private static void AddNewPlayer(Player[] players,  int time, int score, int hardmode)
        {
            using (StreamWriter writer = new StreamWriter(statisticsFilePath))
            {
                writer.WriteLine(++numberOfPlayers);

                foreach (Player player in players)
                {
                    writer.WriteLine($"{player.GetNickname()},{player.GetEasyLevel()},{player.GetMiddleLevel()},{player.GetHardLevel()},{player.GetBestTime()},{player.GetScore()}");
                }

                switch (hardmode)
                {
                    case 1:
                        writer.WriteLine($"{currentNickname},1,0,0,{time},{score}");
                        break;
                    case 2:
                        writer.WriteLine($"{currentNickname},0,1,0,{time},{score}");
                        break;
                    case 3:
                        writer.WriteLine($"{currentNickname},0,0,1,{time},{score}");
                        break;
                } 
            }
        }

        private static void AddFirstPlayer(int time, int score, int hardmode)
        {
            numberOfPlayers = 0;
            using (StreamWriter writer = new StreamWriter(statisticsFilePath))
            {
                writer.WriteLine(++numberOfPlayers);

                switch (hardmode)
                {
                    case 1:
                        writer.WriteLine($"{currentNickname},1,0,0,{time},{score}");
                        break;
                    case 2:
                        writer.WriteLine($"{currentNickname},0,1,0,{time},{score}");
                        break;
                    case 3:
                        writer.WriteLine($"{currentNickname},0,0,1,{time},{score}");
                        break;
                }
            }
        }

        public static Player[] getAllPlayers()
        {
            try
            {
                string firstLine;
                using (StreamReader reader = new StreamReader(statisticsFilePath))
                {
                    firstLine = reader.ReadLine();

                    if (int.TryParse(firstLine, out numberOfPlayers))
                    {
                        Player[] players = new Player[numberOfPlayers];
                        for (int i = 0; i < numberOfPlayers; i++)
                        {
                            string line = reader.ReadLine();
                            string[] values = line.Split(',');

                            players[i] = new Player(values[0], int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), int.Parse(values[4]), int.Parse(values[5]));
                        }
                        return players;
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return null;
        }

        private static bool ExistenceCheck(string filePath)
        {
            return File.Exists(filePath);
        }

        public static bool IsNew(Player[] players)
        {
            foreach(Player player in players)
            {
                if(player.GetNickname() == currentNickname)
                {
                    return false;
                }
            }
            return true;
        }

        public static Player[] Sort(Player[] players)
        {
            int repetitions = players.Length;

            do
            {
                for(int i = 0; i < repetitions; i++)
                {
                    if(i + 1 < repetitions && players[i].GetScore() <= players[i + 1].GetScore())
                    {
                        int temp = players[i+1].GetScore();
                        players[i + 1].SetScore(players[i].GetScore());
                        players[i].SetScore(temp);
                    }
                }
                repetitions--;
            } while (repetitions > 0);

            return players;
        }

        public static void AddSave(int hardmode, int time, int[,] sudoku, int[,] puzzle)
        {
            string sudokuString = string.Join(",", Enumerable.Range(0, 9).SelectMany(i => Enumerable.Range(0, 9).Select(j => sudoku[i, j])));
            string puzzleString = string.Join(",", Enumerable.Range(0, 9).SelectMany(i => Enumerable.Range(0, 9).Select(j => puzzle[i, j])));

            if (ExistenceCheck(savesFilePath))
            {
                Save[] saves = GetAllSaves();

                if (IsNewSave(saves, hardmode))
                {
                    AddNewSave(saves, hardmode, time, sudokuString, puzzleString);
                }
                else
                {
                    ChangeSaveData(saves, hardmode, time, sudokuString, puzzleString);
                }
            }
            else
            {
                AddFirstSave(hardmode, time, sudokuString, puzzleString);
            }
        }

        public static Save[] GetAllSaves()
        {
            try
            {
                string firstLine;
                using (StreamReader reader = new StreamReader(savesFilePath))
                {
                    firstLine = reader.ReadLine();
                    MessageBox.Show(firstLine);

                    if (int.TryParse(firstLine, out numberOfSaves))
                    {
                        Save[] saves = new Save[numberOfSaves];
                        for (int i = 0; i < numberOfSaves; i++)
                        {
                            string line = reader.ReadLine();
                            string[] values = line.Split(',');

                            string savedSudoku = GetArray(3, 83, values);
                            string savedPuzzle = GetArray(84, 164, values);

                            saves[i] = new Save(values[0], int.Parse(values[1]), int.Parse(values[2]), savedSudoku, savedPuzzle);
                        }
                        return saves;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return null;
        }

        private static void AddNewSave(Save[] saves, int hardmode, int time, string sudoku, string puzzle)
        {
            using (StreamWriter writer = new StreamWriter(savesFilePath))
            {
                writer.WriteLine(++numberOfSaves);

                foreach (Save save in saves)
                {
                    writer.WriteLine($"{save.Nickname},{save.Hardmode},{save.Time},{save.Sudoku},{save.Puzzle}");
                }

                switch (hardmode)
                {
                    case 1:
                        writer.WriteLine($"{currentNickname},1,{time},{sudoku},{puzzle}");
                        break;
                    case 2:
                        writer.WriteLine($"{currentNickname},2,{time},{sudoku},{puzzle}");
                        break;
                    case 3:
                        writer.WriteLine($"{currentNickname},3,{time},{sudoku},{puzzle}");
                        break;
                }
            }
        }

        private static void ChangeSaveData(Save[] saves, int hardmode, int time, string sudoku, string puzzle)
        {
            int currentPosition = FindCurrentSavePosition(saves, currentNickname, hardmode);

            if (currentPosition != -1)
            {
                saves[currentPosition].Time = time;
                saves[currentPosition].Sudoku = sudoku;
                saves[currentPosition].Puzzle = puzzle;

                using (StreamWriter writer = new StreamWriter(savesFilePath))
                {
                    writer.WriteLine(numberOfSaves);

                    foreach (Save save in saves)
                    {
                        writer.WriteLine($"{save.Nickname},{save.Hardmode},{save.Time},{save.Sudoku},{save.Puzzle}");
                    }
                }
            }
        }

        public static int FindCurrentSavePosition(Save[] saves, string nickname, int hardmode)
        {
            for (int i = 0; i < saves.Length; i++)
            {
                if (saves[i].Nickname == nickname && saves[i].Hardmode == hardmode)
                {
                    return i;
                }
            }
            return -1;
        }

        private static void AddFirstSave(int hardmode, int time, string sudoku, string puzzle)
        {
            numberOfSaves = 0;
            using (StreamWriter writer = new StreamWriter(savesFilePath))
            {
                writer.WriteLine(++numberOfSaves);

                switch (hardmode)
                {
                    case 1:
                        writer.WriteLine($"{currentNickname},1,{time},{sudoku},{puzzle}");
                        break;
                    case 2:
                        writer.WriteLine($"{currentNickname},2,{time},{sudoku},{puzzle}");
                        break;
                    case 3:
                        writer.WriteLine($"{currentNickname},3,{time},{sudoku},{puzzle}");
                        break;
                }
            }
        }

        private static string GetArray(int startPostion, int endPosition, string[] values)
        {
            int[,] savedArray = new int[9, 9];

            int i = 0, j = 0;
            while(startPostion < endPosition + 1) {
                savedArray[i,j] = int.Parse(values[startPostion]);
                startPostion++;
                j++;
                
                if(j == 9)
                {
                    j = 0;
                    i++;
                }
            }

            return string.Join(",", Enumerable.Range(0, 9).SelectMany(i => Enumerable.Range(0, 9).Select(j => savedArray[i, j])));
        }

        public static bool IsNewSave(Save[] saves, int hardmode)
        {
            foreach (Save save in saves)
            {
                if (save.Nickname == currentNickname && save.Hardmode == hardmode)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckSaves()
        {
            Save[] saves = GetAllSaves();

            foreach (Save save in saves)
            {
                if(save.Nickname == currentNickname)
                {
                    return true;
                }
            }

            return false;
        }

        public static Save[] GetCurrentSaves()
        {
            Save[] allSaves = GetAllSaves();

            if (allSaves == null)
            {
                return null;
            }

            return allSaves.Where(save => save.Nickname == currentNickname).ToArray();
        }

        private static int FindNumberOfSavesOfCurrentPlayer()
        {
            int number = 0;

            foreach (Save save in GetAllSaves()) { 
                if(save.Nickname == currentNickname)
                {
                    number++;
                }
            }

            return number;
        }
    }
}
