using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PracticalWork1
{
    public partial class MainWindow : Window
    {
        private bool playerCross = true;
        private bool gameEnded = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Button[] buttons = { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9 };

            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
                button.Content = "";
                button.Background = Brushes.Transparent; // Сброс фона кнопок
                button.Click += Button_Click;
            }

            StartButton.Content = "Start";
            WinnerText.Text = ""; // Очищаем сообщение о победителе
            gameEnded = false;

            if (!playerCross)
            {
                AI();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (gameEnded || !string.IsNullOrEmpty(button.Content.ToString()))
            {
                return;
            }

            button.Content = playerCross ? "X" : "O";

            if (CheckForVictory())
            {
                gameEnded = true;
                string winner = playerCross ? "Крестик (X)" : "Нолик (O)";
                ShowWinner(winner);
            }
            else if (CheckForTie())
            {
                gameEnded = true;
                ShowWinner("Ничья");
            }
            else
            {
                AI();
                if (CheckForVictory())
                {
                    gameEnded = true;
                    string winner = playerCross ? "Крестик (X)" : "Нолик (O)";
                    ShowWinner(winner);
                }
                else if (CheckForTie())
                {
                    gameEnded = true;
                    ShowWinner("Ничья");
                }
            }
        }

        private void AI()
        {
            Button[] buttons = { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9 };

            int emptyCount = 0;
            foreach (Button button in buttons)
            {
                if (string.IsNullOrEmpty(button.Content.ToString()))
                {
                    emptyCount++;
                }
            }

            if (emptyCount > 0)
            {
                Random random = new Random();
                int choice;
                do
                {
                    choice = random.Next(buttons.Length);
                }
                while (!string.IsNullOrEmpty(buttons[choice].Content.ToString()));

                buttons[choice].Content = playerCross ? "O" : "X";
            }
        }

        private bool CheckForVictory()
        {
            Button[] buttons = { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9 };

            string playerSymbol = playerCross ? "X" : "O";

            for (int i = 0; i < 3; i++)
            {
                if (buttons[i * 3].Content.ToString() == playerSymbol &&
                    buttons[i * 3 + 1].Content.ToString() == playerSymbol &&
                    buttons[i * 3 + 2].Content.ToString() == playerSymbol)
                {
                    HighlightWinningCombination(buttons[i * 3], buttons[i * 3 + 1], buttons[i * 3 + 2]);
                    return true;
                }

                if (buttons[i].Content.ToString() == playerSymbol &&
                    buttons[i + 3].Content.ToString() == playerSymbol &&
                    buttons[i + 6].Content.ToString() == playerSymbol)
                {
                    HighlightWinningCombination(buttons[i], buttons[i + 3], buttons[i + 6]);
                    return true;
                }
            }

            if (buttons[0].Content.ToString() == playerSymbol &&
                buttons[4].Content.ToString() == playerSymbol &&
                buttons[8].Content.ToString() == playerSymbol)
            {
                HighlightWinningCombination(buttons[0], buttons[4], buttons[8]);
                return true;
            }

            if (buttons[2].Content.ToString() == playerSymbol &&
                buttons[4].Content.ToString() == playerSymbol &&
                buttons[6].Content.ToString() == playerSymbol)
            {
                HighlightWinningCombination(buttons[2], buttons[4], buttons[6]);
                return true;
            }

            return false;
        }


        private bool CheckForTie()
        {
            Button[] buttons = { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9 };

            foreach (Button button in buttons)
            {
                if (string.IsNullOrEmpty(button.Content.ToString()))
                {
                    return false; // Есть пустая клетка, игра не окончена
                }
            }

            return true; // Все клетки заполнены, ничья
        }

        private void HighlightWinningCombination(Button button1, Button button2, Button button3)
        {
            button1.Background = Brushes.Green;
            button2.Background = Brushes.Green;
            button3.Background = Brushes.Green;
        }

        private void ShowWinner(string winner)
        {
            WinnerText.Text = $"Победитель: {winner}";
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}
