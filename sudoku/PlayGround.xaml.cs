using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
//строка 161

namespace sudoku
{
    /// <summary>
    /// Логика взаимодействия для PlayGround.xaml
    /// </summary>
    /// 
    public partial class PlayGround : Window
    {
        int numberOfEmptyCells;
        static int attempt = 1;
        static Button currentButton = null;
        static Button previousButton = null;
        int[,] sudoku = new int[9, 9];
        int[,] puzzle = new int[9, 9];
        static int currentButtonRow;
        static int currentButtonCol;
        DispatcherTimer timer = new DispatcherTimer();
        int secondsElapsed;
        bool isPaused = false;
        Label timerLabel, scoreLabel;
        Grid playGroundGrid;
        Panel insertPanel;
        int harmode;
        int score;
        int secondsLastReply;
        bool hintUsed = false;

        public PlayGround(int[,] savedSudoku, int[,] savedPuzzle, int savedHardmode, int savedNumberOfEmptyCells, int savedSecondsElapsed, int savedScore)
        {
            InitializeComponent();
            InitializeGUI();

            harmode = savedHardmode;
            numberOfEmptyCells = savedNumberOfEmptyCells;
            secondsElapsed = savedSecondsElapsed;
            score = savedScore;
            scoreLabel.Content = score;

            if(savedSudoku != null && savedPuzzle != null)
            {
                Array.Copy(savedSudoku, sudoku, savedSudoku.Length);
                Array.Copy(savedPuzzle, puzzle, savedPuzzle.Length);

                CreatePlayGround(playGroundGrid, savedPuzzle);

                string arrayAsString = string.Join(",", savedSudoku.Cast<int>());
                string arrayAsString2 = string.Join(",", savedPuzzle.Cast<int>());
                MessageBox.Show(arrayAsString + "");
                MessageBox.Show(arrayAsString2 + "");

            }
            else
            {
                sudoku = GenerateSudoku();
                Array.Copy(sudoku, puzzle, sudoku.Length);

                RemoveNumbers(puzzle, numberOfEmptyCells);
                CreatePlayGround(playGroundGrid, puzzle);
            }

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;

            if(secondsElapsed < 3600)
            {
                timerLabel.Content = TimeSpan.FromSeconds(secondsElapsed).ToString(@"mm\:ss");
            }
            else
            {
                timerLabel.Content = TimeSpan.FromSeconds(secondsElapsed).ToString(@"hh\:mm\:ss");
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Parent is Border border && border.Parent is Grid grid)
                {
                    // Получаем позицию Border в Grid
                    currentButtonRow = Grid.GetRow(border);
                    currentButtonCol = Grid.GetColumn(border);

                    currentButton = button;
                    currentButton.Background = Brushes.LightBlue;

                    if (previousButton != null && previousButton.Background != Brushes.Red)
                    {
                        previousButton.Background = Brushes.White;

                    }
                    else if (previousButton != null && previousButton.Background == Brushes.Red)
                    {
                        previousButton.Background = Brushes.Red;
                    }

                    if (currentButton != null)
                    {
                        previousButton = currentButton;
                    }
                }
            }
        }

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (int.TryParse(button.Content.ToString(), out int num) && currentButton != null)
                {
                    TryPutNumInButton(num);
                }
            }

        }

        public void TryPutNumInButton(int num)
        {

            if (sudoku[currentButtonRow, currentButtonCol] == num)
            {
                puzzle[currentButtonRow, currentButtonCol] = num;
                currentButton.Content = num;
                currentButton.IsEnabled = false;
                currentButton.Background = Brushes.White;
                numberOfEmptyCells--;

                CalculateScore(1);

                if(numberOfEmptyCells == 0)
                {
                    SudokuComplited();
                }
            }
            else
            {
                currentButton.Content = num;
                currentButton.Background = Brushes.Red;

                CalculateScore(-1);
            }
            
        }

        private void CalculateScore(int condition)
        {
            secondsLastReply = secondsElapsed - secondsLastReply;
            int tempScore = 0;

            if (hintUsed)
            {
                scoreLabel.Content = score - 100;
                hintUsed = false;
                score = score - 100;
            }
            else
            {
                switch (condition)
                {
                    case 1:
                        if (secondsLastReply < 5)
                        {
                            tempScore = tempScore + (75 * secondsLastReply * harmode);
                        }
                        else if (secondsLastReply < 15 && secondsLastReply > 5)
                        {
                            tempScore = tempScore + (50 * (secondsLastReply / 2) * harmode);
                        }
                        else if (secondsLastReply < 30 && secondsLastReply > 15)
                        {
                            tempScore = tempScore + (25 * (secondsLastReply / 3) * harmode);
                        }
                        else
                        {
                            tempScore = tempScore + (50 * harmode);
                        }
                        break;
                    case -1:
                        if (secondsLastReply < 5)
                        {
                            tempScore = tempScore - (50 * harmode);
                        }
                        else if (secondsLastReply < 15 && secondsLastReply > 5)
                        {
                            tempScore = tempScore - (25 * (secondsLastReply / 3) * harmode);
                        }
                        else if (secondsLastReply < 30 && secondsLastReply > 15)
                        {
                            tempScore = tempScore - (50 * (secondsLastReply / 2) * harmode);
                        }
                        else
                        {
                            tempScore = tempScore - (75 * (int)(secondsLastReply / 1.5) * harmode);
                        }
                        break;
                }
                score += tempScore;
                scoreLabel.Content = score;
            }
        }


        public void SudokuComplited()
        {
            timer.Stop();
            timerLabel.Content = "Congratulations!!!";
            pauseButton.IsEnabled = false;
            exitButton.IsEnabled = false;


            Tools.AddInformation(secondsElapsed, score, harmode);

            FinalWindow finalWindow = new FinalWindow(secondsElapsed, score, harmode);
            bool? result = finalWindow.ShowDialog();

            if (result == true)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                Close();
            }
        }


        public void CreatePlayGround(Grid grid, int[,] puzzle)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Button button = new Button();
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Black;
                    button.BorderBrush = Brushes.Gray;
                    button.BorderThickness = new Thickness(1);
                    button.Height = 50;
                    button.Width = 50;
                    button.Click += Button_Click;


                    if (puzzle[row, col] != 0)
                    {
                        button.IsEnabled = false;
                        button.BorderBrush = Brushes.Gray;
                        button.BorderThickness = new Thickness(1);
                        button.Content = $"{puzzle[row, col]}";
                    }
                    else
                    {
                        button.Content = $"";
                    }

                    if (row % 3 == 2 && col % 3 == 2)
                    {
                        Border border = new Border();
                        border.BorderBrush = Brushes.Black;
                        border.BorderThickness = new Thickness(0, 0, 2, 2);
                        border.Child = button;

                        Grid.SetRow(border, row);
                        Grid.SetColumn(border, col);
                        grid.Children.Add(border);
                    }
                    else if (row % 3 == 2)
                    {
                        Border border = new Border();
                        border.BorderBrush = Brushes.Black;
                        border.BorderThickness = new Thickness(0, 0, 0, 2);
                        border.Child = button;

                        Grid.SetRow(border, row);
                        Grid.SetColumn(border, col);
                        grid.Children.Add(border);
                    }
                    else if (col % 3 == 2)
                    {
                        Border border = new Border();
                        border.BorderBrush = Brushes.Black;
                        border.BorderThickness = new Thickness(0, 0, 2, 0);
                        border.Child = button;

                        Grid.SetRow(border, row);
                        Grid.SetColumn(border, col);
                        grid.Children.Add(border);
                    }
                    else
                    {
                        Border border = new Border();

                        border.Child = button;

                        Grid.SetRow(border, row);
                        Grid.SetColumn(border, col);
                        grid.Children.Add(border);
                    }
                }
            }
        }

        static int[,] GenerateSudoku()
        {
            int[,] sudoku = new int[9, 9];

            // Заполнение диагональных блоков
            for (int block = 0; block < 9; block += 3)
            {
                FillBlock(sudoku, block);
            }

            // Заполнение оставшихся ячеек
            FillRemaining(sudoku);

            return sudoku;
        }

        static void FillBlock(int[,] sudoku, int block)
        {
            Random random = new Random();

            for (int num = 1; num <= 9; num++)
            {
                int row, col;

                do
                {
                    row = random.Next(block, block + 3);
                    col = random.Next(block, block + 3);
                } while (sudoku[row, col] != 0);

                sudoku[row, col] = num;
            }
        }

        static void FillRemaining(int[,] sudoku)
        {
            SolveSudoku(sudoku);
        }

        static bool SolveSudoku(int[,] sudoku)
        {
            int row, col;

            if (!FindUnassignedLocation(sudoku, out row, out col))
            {
                return true; // Sudoku решено
            }

            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(sudoku, row, col, num))
                {
                    Console.WriteLine("Попытка " + attempt + ": " + num);
                    attempt++;
                    sudoku[row, col] = num;

                    if (SolveSudoku(sudoku))
                    {
                        return true; // Если решение найдено
                    }

                    sudoku[row, col] = 0; // Если текущее решение не приводит к правильному ответу, отменяем и пытаемся другую цифру.
                }
            }

            return false; // Возврат false, если ни одно число не подходит

        }

        static bool FindUnassignedLocation(int[,] sudoku, out int row, out int col)
        {
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (sudoku[row, col] == 0)
                    {
                        return true;
                    }
                }
            }

            row = -1;
            col = -1;
            return false;
        }


        static bool UsedInRow(int[,] sudoku, int row, int num)
        {
            for (int col = 0; col < 9; col++)
                if (sudoku[row, col] == num)
                    return true;

            return false;
        }

        static bool UsedInCol(int[,] sudoku, int col, int num)
        {
            for (int row = 0; row < 9; row++)
                if (sudoku[row, col] == num)
                    return true;

            return false;
        }

        static bool UsedInBox(int[,] sudoku, int boxStartRow, int boxStartCol, int num)
        {
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    if (sudoku[row + boxStartRow, col + boxStartCol] == num)
                        return true;

            return false;
        }

        static bool IsSafe(int[,] sudoku, int row, int col, int num)
        {
            return !UsedInRow(sudoku, row, num) &&
                   !UsedInCol(sudoku, col, num) &&
                   !UsedInBox(sudoku, row - row % 3, col - col % 3, num);
        }

        static void RemoveNumbers(int[,] puzzle, int count)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                int row = random.Next(0, 9);
                int col = random.Next(0, 9);

                if (puzzle[row, col] != 0)
                {
                    puzzle[row, col] = 0;
                }
                else
                {
                    i--; // Повторная попытка выбора другой ячейки
                }
            }
        }

        private void Hint_Click(object sender, RoutedEventArgs e)
        {
            hintUsed = true;
            Random random = new Random();

            bool notZero = true;
            do
            {
                int row = random.Next(0, 9);
                int col = random.Next(0, 9);

                if (puzzle[row, col] == 0)
                {
                    currentButtonRow = row;
                    currentButtonCol = col;

                    Border border = (Border)playGroundGrid.Children[row * 9 + col];
                    Button button = (Button)border.Child;

                    currentButton = button;
                    currentButton.Background = Brushes.LightBlue;

                    if (previousButton != null && previousButton.Background != Brushes.Red)
                    {
                        previousButton.Background = Brushes.White;

                    }
                    else if (previousButton != null && previousButton.Background == Brushes.Red)
                    {
                        previousButton.Background = Brushes.Red;
                    }

                    if (currentButton != null)
                    {
                        previousButton = currentButton;
                    }
                    TryPutNumInButton(sudoku[row, col]);

                    
                    notZero = false;
                }
            } while (notZero);
        }

        private int[,] CheckForMistakes(int[,] array)
        {
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    Border border = (Border)playGroundGrid.Children[i * 9 + j];
                    Button button = (Button)border.Child;

                    if (button.Background == Brushes.Red)
                    {
                        array[i, j] = 0;
                    }
                }
            }

            return array;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Pause();
            Confirmation confirmation = new Confirmation();
            bool? result = confirmation.ShowDialog();

            if(result == true)
            {
                Tools.AddSave(harmode, secondsElapsed,score, sudoku, CheckForMistakes(puzzle));
                MainWindow main = new MainWindow();
                main.Show();
                Close();
            }
            else
            {
                Pause();
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            Pause();
        }

        private void Pause()
        {
            if (isPaused)
            {
                PaintPlayGround(Brushes.Black);

                isPaused = false;
                pauseButton.Content = "Pause";
                timer.Start();
            }
            else
            {
                timer.Stop();

                PaintPlayGround(Brushes.White);

                pauseButton.Content = "Unpause";
                isPaused = true;
            }
        }

        private void PaintPlayGround(Brush color)
        {
            if(color == Brushes.Black)
            {
                foreach (UIElement element in playGroundGrid.Children)
                {
                    if (element is Border)
                    {
                        Border border = (Border)element;

                        // Получаем первый (и единственный) дочерний элемент в Border
                        UIElement borderChild = border.Child;

                        if (borderChild is Button)
                        {
                            Button button = (Button)borderChild;
                            button.Foreground = Brushes.Black;
                            if(button.Content == "" || button.Background == Brushes.Red)
                            {
                                button.IsEnabled = true;
                            }
                            else
                            {
                                button.IsEnabled = false;
                            }
                        }
                    }
                }

                foreach (UIElement element in insertPanel.Children)
                {
                    if (element is Button)
                    {
                        Button button = (Button)element;
                        button.IsEnabled = true;
                    }
                }
            }
            else
            {
                foreach (UIElement element in playGroundGrid.Children)
                {
                    if (element is Border)
                    {
                        Border border = (Border)element;

                        // Получаем первый (и единственный) дочерний элемент в Border
                        UIElement borderChild = border.Child;

                        if (borderChild is Button)
                        {
                            Button button = (Button)borderChild;
                            button.Foreground = Brushes.White;
                            button.IsEnabled = false;
                        }
                    }
                }
                foreach (UIElement element in insertPanel.Children)
                {
                    if (element is Button)
                    {
                        Button button = (Button)element;
                        button.IsEnabled = false;
                    }
                }
            }
        }

        private void InitializeGUI()
        {
            playGroundGrid = (Grid)FindName("PlayGroundGrid");
            timerLabel = (Label)FindName("TimerLabel");
            insertPanel = (Panel)FindName("InsertPanel");
            scoreLabel = (Label)FindName("ScoreLabel");
        }
    }
}
