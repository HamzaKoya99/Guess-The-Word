using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WordFamilies
{
    static class Display
    {
        public static void PrintWordState(string word)
        {
            Console.WriteLine("\nWord: " + word);
        }

        public static void PrintGuesses(List<char> letters, int guesses)
        {
            Console.WriteLine("You have {0} guesses left.", guesses);
            Console.Write("Your Used letters are: ");
            foreach (char letter in letters)
            {
                Console.Write(letter + " ");
            }
        }

        public static void PromptDifficultyLevel()
        {
            Console.WriteLine("Welcome to Guess the Word Game!!");
            Console.WriteLine();
            Console.WriteLine("Choose an Easy or Hard level...");
            Console.WriteLine();
            Console.WriteLine("1 :- EASY");
            Console.WriteLine("2 :- HARD");
            Console.WriteLine();
            Console.Write("Enter E for easy or H for hard: ");

        }

        public static void PrintDifficultyLevel(string difficultyLevel)
        {
            Console.WriteLine();
            Console.WriteLine("Difficulty Level Chosen: " + difficultyLevel);
        }

        public static void PromptGuess()
        {
            Console.Write("Enter a guess letter: ");
        }

        public static void PrintGameWon(string word)
        {
            Console.WriteLine("Congratulations!! You Have Guessed the Word and You Won!! The word was: " + word);
            Console.WriteLine();
        }

        public static void PrintGameLost(string word)
        {
            Console.WriteLine("You have lost! Better Luck Next Time! The word was: " + word);
            Console.WriteLine();
        }

        public static void PrintCorrectGuess(char guess, int occurences)
        {
            Console.WriteLine("Yes, the word contains {0} {1} time(s).\n", guess, occurences);
        }

        public static void PrintWrongGuess(char guess)
        {
            Console.WriteLine("Sorry, the word does not contain any {0}'s.\n", guess);
        }

        public static void PrintInvalidInput()
        {
            Console.WriteLine("Invalid Input. Please Input A Single Letter Only!! Please Try again...");
        }

        public static void PrintPlayAgain()
        {
            Console.WriteLine("Hope you Enjoyed the Game!! ");
            Console.WriteLine();
            Console.WriteLine("Do Want To Play again?");
            Console.WriteLine("Enter 'y' to play again or any other input to quit the game.");
        }
    }
}