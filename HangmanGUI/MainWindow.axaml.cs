using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanGUI
{
    public partial class MainWindow : Window
    {
        private WordManager wordManager = new WordManager();
        private HintManager hintManager = new HintManager();

        private ComboBox? categoryComboBox;
        private Button? startButton;
        private TextBlock? wordLabel;
        private TextBlock? hintLabel;
        private TextBlock? messageLabel;
        private Canvas? hangmanCanvas;
        private WrapPanel? letterButtonsPanel;
        private Button? hintButton;
        private Button? restartButton;
        private Button? mercyButton;
        private Button? extraLimbsButton;

        private string? currentWord;
        private string? displayWord;
        private int wrongGuesses = 0;
        private int maxWrongGuesses = 6;
        private List<char> guessedLetters = new List<char>();
        private bool hintUsed = false;
        private WordManager.Category selectedCategory;
        private bool mercyUsed = false;
        private bool extraLimbsUsed = false;

        public MainWindow()
        {
            InitializeComponent();
            SetupUI();
            ShowCategorySelection();
        }

        private void SetupUI()
        {
            // Get references from XAML
            categoryComboBox = this.FindControl<ComboBox>("CategoryComboBox");
            startButton = this.FindControl<Button>("StartButton");
            wordLabel = this.FindControl<TextBlock>("WordLabel");
            hintLabel = this.FindControl<TextBlock>("HintLabel");
            messageLabel = this.FindControl<TextBlock>("MessageLabel");
            hangmanCanvas = this.FindControl<Canvas>("HangmanCanvas");
            letterButtonsPanel = this.FindControl<WrapPanel>("LetterButtonsPanel");
            hintButton = this.FindControl<Button>("HintButton");
            restartButton = this.FindControl<Button>("RestartButton");
            mercyButton = this.FindControl<Button>("MercyButton");
            extraLimbsButton = this.FindControl<Button>("ExtraLimbsButton");

            // Populate category combo box
            if (categoryComboBox != null)
                categoryComboBox.ItemsSource = Enum.GetNames(typeof(WordManager.Category));

            // Create letter buttons
            if (letterButtonsPanel != null)
            {
                for (int i = 0; i < 26; i++)
                {
                    Button btn = new Button
                    {
                        Content = ((char)('A' + i)).ToString(),
                        Width = 50,
                        Height = 50,
                        Margin = new Thickness(5),
                        FontSize = 20,
                        FontWeight = FontWeight.Bold
                    };
                    btn.Click += LetterButton_Click;
                    letterButtonsPanel.Children.Add(btn);
                }
            }
        }

        private void ShowCategorySelection()
        {
            if (categoryComboBox != null) categoryComboBox.IsVisible = true;
            if (startButton != null) startButton.IsVisible = true;
            if (wordLabel != null) wordLabel.IsVisible = false;
            if (hintLabel != null) hintLabel.IsVisible = false;
            if (messageLabel != null) messageLabel.Text = "Choose a category and start the game!";
            if (hangmanCanvas != null) hangmanCanvas.IsVisible = false;
            if (letterButtonsPanel != null) letterButtonsPanel.IsVisible = false;
            if (hintButton != null) hintButton.IsVisible = false;
            if (restartButton != null) restartButton.IsVisible = false;
            if (mercyButton != null) mercyButton.IsVisible = false;
            if (extraLimbsButton != null) extraLimbsButton.IsVisible = false;
        }

        private void StartButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (categoryComboBox?.SelectedIndex == -1) return;
            if (categoryComboBox == null) return;
            
            selectedCategory = (WordManager.Category)categoryComboBox.SelectedIndex;
            currentWord = wordManager.GetRandomWord(selectedCategory);
            displayWord = new string('_', currentWord.Length);
            wrongGuesses = 0;
            maxWrongGuesses = 6; // Reset to default
            guessedLetters.Clear();
            hintUsed = false;
            mercyUsed = false;
            extraLimbsUsed = false;

            UpdateWordDisplay();
            if (hintLabel != null) hintLabel.Text = "";
            if (messageLabel != null) messageLabel.Text = "Guess a letter!";

            if (categoryComboBox != null) categoryComboBox.IsVisible = false;
            if (startButton != null) startButton.IsVisible = false;
            if (wordLabel != null) wordLabel.IsVisible = true;
            if (hintLabel != null) hintLabel.IsVisible = true;
            if (hangmanCanvas != null) hangmanCanvas.IsVisible = true;
            if (letterButtonsPanel != null)
            {
                letterButtonsPanel.IsVisible = true;
                foreach (var child in letterButtonsPanel.Children)
                {
                    if (child is Button btn)
                    {
                        btn.IsEnabled = true;
                        btn.IsVisible = true;
                    }
                }
            }
            if (hintButton != null)
            {
                hintButton.IsVisible = true;
                hintButton.IsEnabled = true;
            }
            if (restartButton != null) restartButton.IsVisible = false;
            if (mercyButton != null)
            {
                mercyButton.IsVisible = true;
                mercyButton.IsEnabled = true;
                mercyButton.Opacity = 1.0;
            }
            if (extraLimbsButton != null)
            {
                extraLimbsButton.IsVisible = true;
                extraLimbsButton.IsEnabled = true;
                extraLimbsButton.Opacity = 1.0;
            }

            DrawHangman();
        }

        private void LetterButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (btn.Content?.ToString() == null || currentWord == null || displayWord == null) return;
            
            char letter = btn.Content.ToString()!.ToLower()[0];
            if (guessedLetters.Contains(letter)) return;
            guessedLetters.Add(letter);
            btn.IsEnabled = false;

            if (currentWord.Contains(letter))
            {
                char[] displayChars = displayWord.ToCharArray();
                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (currentWord[i] == letter)
                    {
                        displayChars[i] = letter;
                    }
                }
                displayWord = new string(displayChars);
                UpdateWordDisplay();
                if (messageLabel != null)
                {
                    messageLabel.Text = "Good guess!";
                    messageLabel.Foreground = Brushes.Green;
                }

                if (displayWord == currentWord)
                {
                    if (messageLabel != null)
                        messageLabel.Text = "You win! The word was: " + currentWord;
                    EndGame();
                }
            }
            else
            {
                wrongGuesses++;
                DrawHangman();
                if (messageLabel != null)
                {
                    messageLabel.Text = "Wrong guess! " + (maxWrongGuesses - wrongGuesses) + " guesses left.";
                    messageLabel.Foreground = Brushes.Red;
                }

                if (wrongGuesses >= maxWrongGuesses)
                {
                    if (messageLabel != null)
                        messageLabel.Text = "You lose! The word was: " + currentWord;
                    EndGame();
                }
            }
        }

        private void HintButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (messageLabel == null || hintLabel == null || hintButton == null) return;
            
            // Can only use hint when at least 50% of guesses are used
            double dangerThreshold = maxWrongGuesses * 0.5;
            
            if (wrongGuesses < dangerThreshold)
            {
                messageLabel.Text = $"Hint available only when you have {(int)dangerThreshold} or more wrong guesses!";
                messageLabel.Foreground = Brushes.Orange;
                return;
            }
            
            if (!hintUsed && currentWord != null)
            {
                hintLabel.Text = hintManager.GetHint(currentWord);
                hintUsed = true;
                hintButton.IsEnabled = false;
                messageLabel.Text = "Hint revealed!";
                messageLabel.Foreground = Brushes.Blue;
            }
            else if (hintUsed)
            {
                messageLabel.Text = "Hint already used!";
                messageLabel.Foreground = Brushes.Red;
            }
        }

        private void RestartButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ShowCategorySelection();
        }

        private void MercyButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (messageLabel == null || mercyButton == null) return;
            
            // Can only use mercy when at least 60% of guesses are used (ass is on fire!)
            double dangerThreshold = maxWrongGuesses * 0.6;
            
            if (mercyUsed)
            {
                messageLabel.Text = "Mercy already used!";
                messageLabel.Foreground = Brushes.Red;
                return;
            }
            
            if (wrongGuesses < dangerThreshold)
            {
                messageLabel.Text = $"Mercy available only when you have {(int)Math.Ceiling(dangerThreshold)} or more wrong guesses! Your ass must be on fire!";
                messageLabel.Foreground = Brushes.Orange;
                return;
            }
            
            wrongGuesses -= 2;
            if (wrongGuesses < 0) wrongGuesses = 0;
            mercyUsed = true;
            mercyButton.IsEnabled = false;
            mercyButton.Opacity = 0.5;
            DrawHangman();
            messageLabel.Text = "Mercy granted! 2 wrong guesses removed. You're saved!";
            messageLabel.Foreground = Brushes.Green;
        }

        private void ExtraLimbsButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (messageLabel == null || extraLimbsButton == null) return;
            
            // Can only use extra limbs when at least 70% of guesses are used (critical danger!)
            double dangerThreshold = maxWrongGuesses * 0.7;
            
            if (extraLimbsUsed)
            {
                messageLabel.Text = "Extra limbs already used!";
                messageLabel.Foreground = Brushes.Red;
                return;
            }
            
            if (wrongGuesses < dangerThreshold)
            {
                messageLabel.Text = $"Extra limbs available only when you have {(int)Math.Ceiling(dangerThreshold)} or more wrong guesses! Critical danger only!";
                messageLabel.Foreground = Brushes.Orange;
                return;
            }
            
            maxWrongGuesses += 4;
            extraLimbsUsed = true;
            extraLimbsButton.IsEnabled = false;
            extraLimbsButton.Opacity = 0.5;
            messageLabel.Text = "Extra limbs added! You now have " + maxWrongGuesses + " total guesses. Last chance!";
            messageLabel.Foreground = Brushes.Green;
        }

        private void UpdateWordDisplay()
        {
            if (wordLabel != null && displayWord != null)
                wordLabel.Text = string.Join(" ", displayWord.ToCharArray());
        }

        private void EndGame()
        {
            if (letterButtonsPanel != null)
            {
                foreach (var child in letterButtonsPanel.Children)
                {
                    if (child is Button btn) btn.IsEnabled = false;
                }
            }
            if (hintButton != null) hintButton.IsEnabled = false;
            if (mercyButton != null) mercyButton.IsEnabled = false;
            if (extraLimbsButton != null) extraLimbsButton.IsEnabled = false;
            if (restartButton != null) restartButton.IsVisible = true;
        }

        private void DrawHangman()
        {
            if (hangmanCanvas == null) return;
            
            hangmanCanvas.Children.Clear();

            // Draw gallows (scaled up for larger canvas)
            hangmanCanvas.Children.Add(new Line { StartPoint = new Point(80, 350), EndPoint = new Point(320, 350), Stroke = Brushes.Black, StrokeThickness = 5 });
            hangmanCanvas.Children.Add(new Line { StartPoint = new Point(200, 350), EndPoint = new Point(200, 80), Stroke = Brushes.Black, StrokeThickness = 5 });
            hangmanCanvas.Children.Add(new Line { StartPoint = new Point(200, 80), EndPoint = new Point(280, 80), Stroke = Brushes.Black, StrokeThickness = 5 });
            hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 80), EndPoint = new Point(280, 120), Stroke = Brushes.Black, StrokeThickness = 4 });

            // Calculate effective wrong guesses for display
            int displayWrongGuesses = wrongGuesses;
            
            // Draw parts based on wrongGuesses (scaled up)
            if (displayWrongGuesses >= 1) // head
            {
                var headColor = mercyUsed ? Brushes.LightGreen : Brushes.Orange;
                hangmanCanvas.Children.Add(new Ellipse { Width = 50, Height = 50, Fill = headColor, Stroke = Brushes.Black, StrokeThickness = 3, Margin = new Thickness(255, 120, 0, 0) });
            }
            if (displayWrongGuesses >= 2) // body
            {
                var bodyColor = mercyUsed ? Brushes.Green : Brushes.Black;
                hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 170), EndPoint = new Point(280, 260), Stroke = bodyColor, StrokeThickness = 4 });
            }
            if (displayWrongGuesses >= 3) // left arm
            {
                hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 190), EndPoint = new Point(250, 220), Stroke = Brushes.Black, StrokeThickness = 4 });
            }
            if (displayWrongGuesses >= 4) // right arm
            {
                hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 190), EndPoint = new Point(310, 220), Stroke = Brushes.Black, StrokeThickness = 4 });
            }
            if (displayWrongGuesses >= 5) // left leg
            {
                hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 260), EndPoint = new Point(250, 310), Stroke = Brushes.Black, StrokeThickness = 4 });
            }
            if (displayWrongGuesses >= 6) // right leg
            {
                hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 260), EndPoint = new Point(310, 310), Stroke = Brushes.Black, StrokeThickness = 4 });
            }
            
            // Draw extra limbs if power-up was used (shown in blue)
            if (extraLimbsUsed)
            {
                // Extra left arm (upper)
                if (displayWrongGuesses >= 7)
                {
                    hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 180), EndPoint = new Point(245, 200), Stroke = Brushes.Blue, StrokeThickness = 4 });
                }
                // Extra right arm (upper)
                if (displayWrongGuesses >= 8)
                {
                    hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 180), EndPoint = new Point(315, 200), Stroke = Brushes.Blue, StrokeThickness = 4 });
                }
                // Extra left leg (middle)
                if (displayWrongGuesses >= 9)
                {
                    hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 260), EndPoint = new Point(245, 295), Stroke = Brushes.Blue, StrokeThickness = 4 });
                }
                // Extra right leg (middle)
                if (displayWrongGuesses >= 10)
                {
                    hangmanCanvas.Children.Add(new Line { StartPoint = new Point(280, 260), EndPoint = new Point(315, 295), Stroke = Brushes.Blue, StrokeThickness = 4 });
                }
            }
        }
    }
}
