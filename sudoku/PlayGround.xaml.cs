using System;
using System.Collections.Generic;
using System.Linq;
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

namespace sudoku
{
    /// <summary>
    /// Логика взаимодействия для PlayGround.xaml
    /// </summary>
    /// 
    public partial class PlayGround : Window
    {
        public static int attempt = 1;
        public static Button currentButton = null;
        public static Button previousButton = null;
        private int[,] sudoku = new int[9, 9];
        private int[,] puzzle = new int[9, 9];
        public static int currentButtonRow;
        public static int currentButtonCol;

        public PlayGround()
        {
            InitializeComponent();
            Grid playGroundGrid = FindName("playGroundGrid") as Grid;

            sudoku = GenerateSudoku();

            Console.WriteLine("Полное решенное судоку:");
            PrintSudoku(sudoku);

            Array.Copy(sudoku, puzzle, sudoku.Length);

            RemoveNumbers(puzzle, 20);


            CreatePlayGround(playGroundGrid, puzzle);

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

                    if (currentButton != null)
                    {
                        previousButton = currentButton;
                    }
                    if (previousButton != null && previousButton.Background != Brushes.Red)
                    {
                        previousButton.Background = Brushes.White;

                    }

                    currentButton = button;
                    currentButton.Background = Brushes.LightBlue;
                }
            }
        }

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (int.TryParse(button.Content.ToString(), out int num))
                {
                    TryPutNumInButton(currentButton, num);
                }
            }

        }

        public void TryPutNumInButton(Button button, int num)
        {
            if (sudoku[currentButtonRow, currentButtonCol] == num)
            {
                puzzle[currentButtonRow, currentButtonCol] = num;
                currentButton.Content = num;
                currentButton.IsEnabled = false;
                currentButton.Background = Brushes.White;
            }
            else
            {
                currentButton.Content = num;
                currentButton.Background = Brushes.Red;
                MessageBox.Show("Grid position:\n" + currentButtonRow + " " + currentButtonCol + "\nSudoku number:\n" + sudoku[currentButtonRow, currentButtonCol]);
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
                    int temp = puzzle[row, col];
                    puzzle[row, col] = 0;

                    // Проверка, что удаленная цифра оставляет единственное решение
                    /*if (!HasUniqueSolution(puzzle))
                    {
                        puzzle[row, col] = temp; // Восстановление цифры, если удаление нарушает единственность решения
                        i--; // Повторная попытка удаления
                    }*/
                }
                else
                {
                    i--; // Повторная попытка выбора другой ячейки
                }
            }

            Console.WriteLine("\nГотовое для решения судоку:");
            PrintSudoku(puzzle);
        }

        static void PrintSudoku(int[,] sudoku)
        {
            bool test1 = true;
            bool test2 = true;
            Console.WriteLine();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i == 3 && test1 == true)
                    {
                        Console.Write("\n");
                        test1 = false;
                    }
                    if (i == 6 && test2 == true)
                    {
                        Console.Write("\n");
                        test2 = false;
                    }

                    Console.Write(sudoku[i, j] + " ");

                    if (j == 2 || j == 5)
                    {
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
