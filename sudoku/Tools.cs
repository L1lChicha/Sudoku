using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Tools
    {
        private static string filePath = "statistics.txt";

        public static string currentNickname = "";
        public static int numberOfPlayers;


        public static void AddInformation(int time, int score, int hardmode)
        {
            if (ExistenceCheck())
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

                using (StreamWriter writer = new StreamWriter(filePath))
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
            using (StreamWriter writer = new StreamWriter(filePath))
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
            using (StreamWriter writer = new StreamWriter(filePath))
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
                using (StreamReader reader = new StreamReader(filePath))
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

        private static bool ExistenceCheck()
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
    }
}
