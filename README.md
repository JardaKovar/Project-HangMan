# Colorful Hangman Game in Unity

This is a colorful Hangman game built in Unity with C#. The game allows players to choose word categories and provides poetic hints.

## Features
- Choose from categories: Animals, Names, Cars, Fruits/Vegetables, People
- Colorful UI with vibrant colors
- Poetic hints for each word
- Hangman drawing with colored parts
- Win/lose conditions with restart option

## Setup Instructions

The scripts have been moved into the Unity project located at `Setup Guide In-Editor Tutorial/`.

1. Open the Unity project in `Setup Guide In-Editor Tutorial/`.
2. Open the existing scene (e.g., GetStarted_Scene) or create a new scene.
3. Set up the UI in the scene:
   - Add a Canvas to the scene.
   - Add TMP_Text for word display, hint display, and message text.
   - Add TMP_InputField for letter input.
   - Add Buttons for Guess, Hint, and Restart.
- Add 6 Image components for hangman parts (head, body, left arm, right arm, left leg, right leg).
- Add 3 additional Buttons for power-ups: Reset Hangman, Extra Limbs, Mercy.
4. Create empty GameObjects for WordManager, HintManager, and HangmanGame.
5. Attach the respective scripts from `Assets/Scripts/` to these GameObjects.
6. In the HangmanGame script, assign the UI elements in the Inspector.
7. Assign the WordManager and HintManager references.
8. Run the scene.

## Scripts Overview
- `WordManager.cs`: Manages word categories and random word selection.
- `HintManager.cs`: Provides poetic hints for words.
- `HangmanGame.cs`: Main game logic, UI handling, and colorful design.

## Customization
- Add more words and hints by editing the dictionaries in WordManager and HintManager.
- Adjust colors in the HangmanGame script's Inspector.
- Modify the hangman drawing by changing the Image components.

Enjoy the game!
