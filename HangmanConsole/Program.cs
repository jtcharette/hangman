using System;
using System.Collections.Generic;

namespace HangmanConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const char placeHolderChar = '_';
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string word = "HORSE";
            List<char> guessedLetters = new List<char>();
            List<char> guessedWord = new List<char>(new string(placeHolderChar, word.Length));
            int lives = 5;
            ConsoleColor warningColor = ConsoleColor.Yellow;
            ConsoleColor errorColor = ConsoleColor.Red;
            ConsoleColor successColor = ConsoleColor.Green;

            Console.WriteLine("Welcome to Hangman!");

            while(lives > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Lives: {0}", lives);
                Console.Write("Word: ");

                foreach (char letter in guessedWord)
                {
                    Console.Write("{0} ", letter); 
                }

                Console.WriteLine();
                Console.Write("Guess a letter: ");

                char input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);

                Console.Clear();

                if (!alphabet.Contains(input.ToString()))
                {
                    Console.ForegroundColor = errorColor;
                    Console.WriteLine("Invalid input! Expected a letter");
                    Console.ResetColor();
                    continue;
                }

                if (guessedLetters.Contains(input))
                {
                    Console.ForegroundColor = errorColor;
                    Console.WriteLine("Oops! You've already chosen \"{0}\"", input);
                    Console.ResetColor();
                    continue;
                }

                guessedLetters.Add(input);

                if (word.Contains(input.ToString()))
                {
                    Console.ForegroundColor = successColor;
                    Console.WriteLine("Great Guess!");
                    Console.ResetColor();

                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == input)
                        {
                            guessedWord[i] = input;
                        }
                    }

                    bool isWordGuessed = true;
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] != guessedWord[i])
                        {
                            isWordGuessed = false;
                            break;
                        }
                    }

                    if (isWordGuessed)
                    {
                        Console.ForegroundColor = successColor;
                        Console.WriteLine("Congratulations! You've guessed the word!");
                        Console.ResetColor();
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = warningColor;
                    Console.WriteLine("Sorry, \"{0}\" was not in the word", input);
                    Console.ResetColor();

                    lives = lives - 1;

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
