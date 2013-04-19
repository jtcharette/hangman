using System;
using System.Collections.Generic;

namespace HangmanConsole
{
    public class Program
    {
        /// <summary>
        /// The character used to represent a letter not yet guessed
        /// </summary>
        private const char placeHolderChar = '_';

        /// <summary>
        /// The current word to guess
        /// </summary>
        private static string word = "HORSE";

        /// <summary>
        /// The letters already guessed
        /// </summary>
        private static List<char> guessedLetters = new List<char>();

        /// <summary>
        /// The current state of the guessed word
        /// </summary>
        private static List<char> guessedWord = new List<char>(new string(placeHolderChar, word.Length));

        /// <summary>
        /// the number of lives remaining
        /// </summary>
        private static int lives = 5;

        /// <summary>
        ///  Message types
        /// </summary>
        private enum MessageType
        {
            /// <summary>
            /// Value represents an info message
            /// </summary>
            Info,

            /// <summary>
            /// Value represents a warning message
            /// </summary>
            Warning,

            /// <summary>
            /// Value represents an error message
            /// </summary>
            Error,

            /// <summary>
            /// Value represents a successful message
            /// </summary>
            Success
        }

        public static void Main(string[] args)
        {
            DisplayMessage(MessageType.Info, "Welcome to Hangman!");

            // while the user still has lives
            while(UserHasLives())
            {
                DisplayStatus();

                // prompt user to guess a letter
                char input = PromptUserForLetter();

                // after user responds, clear the screen to "refresh" for new output
                Console.Clear();

                // if the key pressed is NOT a letter
                if (!IsLetter(input))
                {
                    DisplayMessage(MessageType.Error, "Invalid input! Expected a letter\n");
                    continue;
                }

                // if user has already guessed the letter
                if (LetterAlreadyGuessed(input))
                {
                    DisplayMessage(MessageType.Error, "Oops! You've already chosen \"{0}\"\n", input);
                    continue;
                }

                // keep track of the guessed letter
                guessedLetters.Add(input);

                // if the word contains the guessed letter
                if (WordHasLetter(input))
                {
                    DisplayMessage(MessageType.Success, "Great Guess!\n");

                    // reveal all the letters of the word (guessed word) that match the guessed letter
                    RevealLetterInWord(input);

                    // if the word has been guessed
                    if (WordGuessed())
                    {
                        DisplayMessage(MessageType.Success, "Congratulations! You've guessed the word!\n");
                        break;
                    }
                }
                // if the word does NOT contain the guessed letter
                else
                {
                    DisplayMessage(MessageType.Warning, "Sorry, \"{0}\" was not in the word\n", input);

                    // decrement the user's lives
                    lives = lives - 1;

                    // if the user has no more lives
                    if (!UserHasLives())
                    {
                        DisplayMessage(MessageType.Error, "You've run out of lives! You LOSE!\n");
                    }                
                }
            }

            DisplayMessage(MessageType.Info, "The word was: ");
            DisplayMessage(WordGuessed() ? MessageType.Success : MessageType.Error, word);
            DisplayMessage(MessageType.Info, "\nThanks for playing :-)");

            Console.ReadKey();
        }

        /// <summary>
        /// Returns true if the user still has lives remaining, otherwise false
        /// </summary>
        private static bool UserHasLives()
        {
            return lives > 0;
        }

        /// <summary>
        /// Displays current status of game (i.e. # of lives, and guessed word)
        /// </summary>
        private static void DisplayStatus()
        {
            DisplayMessage(MessageType.Info,  "\nLives: {0}\n", lives);
            DisplayMessage(MessageType.Info, "Word: ");

            // output each letter in the guessed word
            foreach (char letter in guessedWord)
            {
                DisplayMessage(MessageType.Info, "{0} ", letter);
            }
        }

        /// <summary>
        /// Prompts the user for a letter and waits for user to press a key
        /// </summary>
        /// <returns>Returns the character associated with the key pressed by the user</returns>
        private static char PromptUserForLetter()
        {
            DisplayMessage(MessageType.Info,  "\nGuess a letter: ");

            // get pressed key from user
            char input = Console.ReadKey().KeyChar;
            input = char.ToUpper(input);

            return input;
        }

        /// <summary>
        /// Determines if the given input is a letter
        /// </summary>
        /// <param name="input">The input to check</param>
        /// <returns>Returns true if the given input is a letter, otherwise false</returns>
        private static bool IsLetter(char input)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabet.Contains(input.ToString());
        }

        /// <summary>
        /// Determines if the given letter has already been guessed
        /// </summary>
        /// <param name="letter">The letter to check</param>
        /// <returns>Returns true if the given letter has already been guessed</returns>
        private static bool LetterAlreadyGuessed(char letter)
        {
            return guessedLetters.Contains(letter);
        }

        /// <summary>
        /// Determines if the word contains the given letter
        /// </summary>
        /// <param name="letter">The letter to check</param>
        /// <returns>Returns true if the word contains the givent letter</returns>
        private static bool WordHasLetter(char letter)
        {
            return word.Contains(letter.ToString());
        }

        /// <summary>
        /// Reveals the given letter for the guessed word
        /// </summary>
        /// <param name="letter">The letter to reveal in the guessed word</param>
        private static void RevealLetterInWord(char letter)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    guessedWord[i] = letter;
                }
            }
        }

        /// <summary>
        /// Determines if the word has been guessed
        /// </summary>
        /// <returns>Returns true if the word has been guessed</returns>
        private static bool WordGuessed()
        {
            return UserHasLives() && !guessedWord.Contains(placeHolderChar);
        }

        /// <summary>
        /// Displays a message
        /// </summary>
        /// <param name="messageType">The type of message (determines the output color)</param>
        /// <param name="message">The message format</param>
        /// <param name="args">The optional arguments for the message</param>
        private static void DisplayMessage(MessageType messageType, string message, params object[] args)
        {
            ConsoleColor previousForeColor = Console.ForegroundColor;
            ConsoleColor previousBackColor = Console.BackgroundColor;
            Console.ForegroundColor = GetConsoleForeColorFromMessageType(messageType);
            Console.BackgroundColor = GetConsoleBackColorFromMessageType(messageType);
            Console.Write(message, args);
            Console.ForegroundColor = previousForeColor;
            Console.BackgroundColor = previousBackColor;
        }

        /// <summary>
        /// Gets the console fore color for the given message type
        /// </summary>
        /// <param name="messageType">The message type to get the console fore color for</param>
        /// <returns>Returns the console color for the given message type</returns>
        private static ConsoleColor GetConsoleForeColorFromMessageType(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Info:
                    return ConsoleColor.Gray;
                case MessageType.Warning:
                    return ConsoleColor.Yellow;
                case MessageType.Error:
                    return ConsoleColor.Red;
                case MessageType.Success:
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.Gray;
            }
        }

        /// <summary>
        /// Gets the console fore color for the given message type
        /// </summary>
        /// <param name="messageType">The message type to get the console fore color for</param>
        /// <returns>Returns the console color for the given message type</returns>
        private static ConsoleColor GetConsoleBackColorFromMessageType(MessageType messageType)
        {
            return ConsoleColor.Black;
        }

    }
}
