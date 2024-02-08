using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private const int BoardSize = 9;
        private Button[] buttons;
        private bool isPlayerTurn;
        private bool gameEnded;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[BoardSize];
            isPlayerTurn = true;
            gameEnded = false;

            for (int i = 0; i < BoardSize; i++)
            {
                Button button = new Button();
                button.Tag = i;
                button.Click += Button_Click;
                buttons[i] = button;
                grid.Children.Add(button);
            }

            ResetGame();
        }

        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Content = string.Empty;
                button.IsEnabled = true;
            }

            gameEnded = false;
            isPlayerTurn = !isPlayerTurn;
            UpdateTurnLabel();

            if (!isPlayerTurn)
            {
                MakeRobotMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
                return;

            Button button = (Button)sender;

            if (!string.IsNullOrEmpty(button.Content.ToString()))
                return;

            button.Content = "X";
            button.IsEnabled = false;

            if (CheckForWin("X"))
            {
                gameEnded = true;
                ShowResult("Победа! Выиграли крестики!");
                return;
            }

            if (IsBoardFull())
            {
                gameEnded = true;
                ShowResult("Ничья!");
                return;
            }

            isPlayerTurn = false;
            UpdateTurnLabel();

            MakeRobotMove();
        }

        private void MakeRobotMove()
        {
            Random random = new Random();
            int index = -1;

            do
            {
                index = random.Next(0, BoardSize);
            }
            while (!string.IsNullOrEmpty(buttons[index].Content.ToString()));

            buttons[index].Content = "O";
            buttons[index].IsEnabled = false;

            if (CheckForWin("O"))
            {
                gameEnded = true;
                ShowResult("Победа! Выиграли нолики!");
                return;
            }

            if (IsBoardFull())
            {
                gameEnded = true;
                ShowResult("Ничья!");
                return;
            }

            isPlayerTurn = true;
            UpdateTurnLabel();
        }

        private bool CheckForWin(string symbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i].Content.ToString() == symbol && buttons[i + 3].Content.ToString() == symbol && buttons[i + 6].Content.ToString() == symbol)
                    return true;

                if (buttons[i * 3].Content.ToString() == symbol && buttons[i * 3 + 1].Content.ToString() == symbol && buttons[i * 3 + 2].Content.ToString() == symbol)
                    return true;
            }

            if (buttons[0].Content.ToString() == symbol && buttons[4].Content.ToString() == symbol && buttons[8].Content.ToString() == symbol)
                return true;

            if (buttons[2].Content.ToString() == symbol && buttons[4].Content.ToString() == symbol && buttons[6].Content.ToString() == symbol)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (Button button in buttons)
            {
                if (string.IsNullOrEmpty(button.Content.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        private void UpdateTurnLabel()
        {
            turnLabel.Content = isPlayerTurn ? "Ход игрока: X" : "Ход робота: O";
        }

        private void ShowResult(string message)
        {
            MessageBox.Show(message);
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }
    }
}