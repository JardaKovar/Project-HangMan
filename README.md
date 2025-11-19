# Cross-Platform Hangman Game in Avalonia UI

This is a cross-platform Hangman game built with Avalonia UI and C#. The game features a fullscreen interface with danger-based power-ups and visual effects.

## Features
- **Cross-Platform**: Runs on Windows, macOS, and Linux
- **Fullscreen Interface**: Maximized window with centered content
- **Category Selection**: Choose from Animals, Fruits, and Countries
- **Poetic Hints**: Get creative hints for each word
- **Danger-Based Power-Ups**:
  - **Hint**: Available when you've made ≥50% wrong guesses
  - **Mercy**: Available at ≥60% wrong guesses (removes 2 wrong guesses, one-time use)
  - **Extra Limbs**: Available at ≥70% wrong guesses (adds 4 extra attempts, one-time use)
- **Visual Effects**: 
  - Mercy turns the hangman green
  - Extra Limbs adds blue limbs to the drawing
- **Interactive Hangman Drawing**: Visual feedback with colored parts
- **Win/Lose Conditions**: Complete game flow with restart option

## Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Avalonia UI 11.0.10 (automatically installed via NuGet)

## Setup Instructions

1. **Clone the repository**:
   ```bash
   git clone https://github.com/JardaKovar/Project-HangMan.git
   cd Project-HangMan
   ```

2. **Navigate to the project directory**:
   ```bash
   cd HangmanGUI
   ```

3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

4. **Build the project**:
   ```bash
   dotnet build
   ```

5. **Run the application**:
   ```bash
   dotnet run
   ```

## Project Structure

```
HangmanGUI/
├── App.axaml              # Application definition
├── App.axaml.cs           # Application code-behind
├── MainWindow.axaml       # Main window XAML layout
├── MainWindow.axaml.cs    # Main window logic and game mechanics
├── WordManager.cs         # Word categories and selection
├── HintManager.cs         # Hint generation system
├── Program.cs             # Application entry point
└── HangmanGUI.csproj      # Project configuration
```

## Scripts Overview

- **MainWindow.axaml.cs**: Main game logic, UI handling, and visual effects
- **WordManager.cs**: Manages word categories (Animals, Fruits, Countries) and random word selection
- **HintManager.cs**: Provides poetic hints for words
- **App.axaml.cs**: Avalonia application initialization

## How to Play

1. **Start the Game**: Select a category from the dropdown and click "Start Game"
2. **Guess Letters**: Click on letter buttons to guess
3. **Use Power-Ups** (when available):
   - **Hint**: Reveals a poetic clue about the word
   - **Mercy**: Removes 2 wrong guesses (available at 60% danger, one-time use)
   - **Extra Limbs**: Adds 4 extra attempts (available at 70% danger, one-time use)
4. **Win or Lose**: Complete the word to win, or run out of guesses to lose
5. **Restart**: Click "Restart" to play again

## Customization

- **Add More Words**: Edit the dictionaries in `WordManager.cs`
- **Add More Hints**: Edit the hint dictionaries in `HintManager.cs`
- **Adjust Colors**: Modify the brush colors in `MainWindow.axaml.cs`
- **Change Layout**: Edit `MainWindow.axaml` to adjust the UI layout

## Dependencies

- **Avalonia**: 11.0.10
- **Avalonia.Desktop**: 11.0.10
- **Avalonia.Themes.Fluent**: 11.0.10

## Platform-Specific Notes

### Windows
- Runs natively with .NET 8.0
- No additional setup required

### macOS
- Requires .NET 8.0 SDK for macOS
- May need to grant permissions for the application

### Linux
- Requires .NET 8.0 SDK for Linux
- May need to install additional dependencies depending on your distribution

## Contributing

Feel free to fork this repository and submit pull requests for improvements or bug fixes.

## License

This project is open source and available for educational purposes.

Enjoy the game!
