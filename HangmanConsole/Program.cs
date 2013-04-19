using System;
using System.Collections.Generic;

namespace HangmanConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // The character used to represent a letter not yet guessed
            const char placeHolderChar = '_';

            // Used to determine if user input is a letter
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // The current word to guess
            string word = "HORSE";

            // The letters already guessed
            List<char> guessedLetters = new List<char>();

            // The current state of the guessed word
            List<char> guessedWord = new List<char>(new string(placeHolderChar, word.Length));

            // the number of lives remaining
            int lives = 5;

            // The color assigned to warning text
            ConsoleColor warningColor = ConsoleColor.Yellow;

            // The color assigned to error text
            ConsoleColor errorColor = ConsoleColor.Red;

            // The color assigned to exciting text
            ConsoleColor successColor = ConsoleColor.Green;

            Console.WriteLine("Welcome to Hangman!");

            // while the user still has lives
            while(lives > 0)
            {
                
                Console.WriteLine();
                Console.WriteLine("Lives: {0}", lives);
                Console.Write("Word: ");

                // output each letter in the guessed word
                foreach (char letter in guessedWord)
                {
                    Console.Write("{0} ", letter); 
                }

                // prompt user to guess a letter
                Console.WriteLine();
                Console.Write("Guess a letter: ");

                // get pressed key from user
                char input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);

                // after user presses a key, clear the screen to "refresh" new output
                Console.Clear();

                // if the key pressed is NOT a letter
                if (!alphabet.Contains(input.ToString()))
                {
                    Console.ForegroundColor = errorColor;
                    Console.WriteLine("Invalid input! Expected a letter");
                    Console.ResetColor();
                    continue;
                }

                // if user has already guessed the letter
                if (guessedLetters.Contains(input))
                {
                    Console.ForegroundColor = errorColor;
                    Console.WriteLine("Oops! You've already chosen \"{0}\"", input);
                    Console.ResetColor();
                    continue;
                }

                // keep track of the guessed letter
                guessedLetters.Add(input);

                // if the word contains the guessed letter
                if (word.Contains(input.ToString()))
                {
                    Console.ForegroundColor = successColor;
                    Console.WriteLine("Great Guess!");
                    Console.ResetColor();

                    // reveal all the letters of the word (guessed word) that match the guessed letter
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == input)
                        {
                            guessedWord[i] = input;
                        }
                    }

                    // if thw word has been guessed
                    if (!guessedWord.Contains(placeHolderChar))
                    {
                        Console.ForegroundColor = successColor;
                        Console.WriteLine("Congratulations! You've guessed the word!");
                        Console.ResetColor();
                        break;
                    }
                }
                // if the word does NOT contain the guessed letter
                else
                {
                    Console.ForegroundColor = warningColor;
                    Console.WriteLine("Sorry, \"{0}\" was not in the word", input);
                    Console.ResetColor();

                    // decrement the user's lives
                    lives = lives - 1;

                    // if the user has no more lives
                    if (lives <= 0)
                    {
                        Console.ForegroundColor = errorColor;
                        Console.WriteLine("You've run out of lives! You LOSE!");
                        Console.ResetColor();
                    }                
                }
            }

            Console.Write("The word was: ");
            Console.ForegroundColor = lives <= 0 ? errorColor : successColor;
            Console.WriteLine(word);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Thanks for playing :-)");
            Console.ReadKey();
        }
    }
}
