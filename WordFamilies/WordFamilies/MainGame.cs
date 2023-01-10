using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WordFamilies
{
    class MainGame
    {
        // holds all of the possible words, found in disctionary
        List<string> WordList { get; set; }

        // holds all of the guessed letters
        List<char> GuessedLetters { get; set; }

        // holds easy or hard difficulty level
        string DifficultyLevel { get; set; }

        // holds the number of letters of the word being guessed
        int WordLength { get; set; }

        // holds the number of guesses the user has left
        int GuessesLeft { get; set; }

        // holds the state of the current word
        string CurrentWord { get; set; }

        // holds the last guess entered by the user
        char LastGuess { get; set; } 

        // hold the state of game
        bool GameOver { get; set; }


        public void Initialise()
        {
            GameOver = false;
            WordList = File.ReadAllLines("dictionary.txt").ToList();
            GuessedLetters = new List<char>();

            // Randomises the word length between 4 to 12 characters long
            WordLength = new Random().Next(4, 12);

            // Player gets 2x times the guesses to the length of the word 
            GuessesLeft = 2 * WordLength;

            // Asks for Difficulty level
            DifficultyLevel = PromptDifficultyLevel(); 
            Display.PrintDifficultyLevel(DifficultyLevel);

            // Displays unguessed letters to dashes
            for (int i = 0; i < WordLength; i++)
            {
                CurrentWord += "-"; 
            }

            List<string> correctLengthWords = new List<string>();

            foreach (string word in WordList) 
            {
                if (word.Length == WordLength)
                {
                    correctLengthWords.Add(word);
                }
            }
    
            WordList = correctLengthWords;
        }

        private string PromptDifficultyLevel()
        {
            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                Display.PromptDifficultyLevel();
                input = Console.ReadLine().Trim();

                if (input == "E" || input == "e" || input == "H" || input == "h")
                {
                    validInput = true;
                }
                else
                {
                    Display.PrintInvalidInput();
                }
            }

            if (input == "E")
            {
                return "Easy";
            }
            if (input == "e")
            {
                return "Easy";
            }
            if (input == "H")
            {
                return "Hard";
            }
            else
            {
                return "Hard";
            }
        }

        public void PromptGuess()
        {
            bool validGuess = false;
            char guess = ' ';

            while (!validGuess)
            {
                Display.PromptGuess();
                try
                {
                    guess = Console.ReadLine().ToLower()[0]; 
                }
                catch
                {

                }

                if (GuessedLetters.Contains(guess) || guess < 97 || guess > 122) 
                {
                    Display.PrintInvalidInput();
                }
                else
                {
                    validGuess = true;
                }
            }

            LastGuess = guess;
            GuessedLetters.Add(guess);
        }

        
        /// Method to give each word with a code of 1's and 0's.
        /// Instantiates a dictionary of the word families and the number of elements in each.
        /// Calls GetLargestFamily, passing the dictionary as an argument.
        public Dictionary<string, int> SortFamilies()
        {
            Dictionary<string, int> families = new Dictionary<string, int>();

            foreach (string word in WordList)
            {
                StringBuilder familyCode = new StringBuilder(WordLength); 

                foreach (char c in word)
                {
                    if (c == LastGuess)
                    {
                        familyCode.Append('1'); 
                    }
                    else
                    {
                        familyCode.Append('0'); 
                    }
                }

                if (families.ContainsKey(familyCode.ToString()))
                {
                    families[familyCode.ToString()]++;
                }
                else
                {
                    families.Add(familyCode.ToString(), 1); 
                }
            }

            return families;
        }

        // Sorts a dictionary of word families, selecting the family with the largest number of elements.
        // Removes all words from the word list which are not in the selected family.
     
        /// <param name="familyDictionary"></param>
        public void SortLargestFamily(Dictionary<string, int> familyDictionary)
        {
            string code = ""; 
            int familyCount = 0; 

            
            foreach (string key in familyDictionary.Keys)
            {
                if (familyDictionary[key] > familyCount)
                {
                    code = key;
                    familyCount = familyDictionary[key];
                }

            }

            RemoveWordsFromWordList(code);
        }


        // Method used when the player chooses Difficulty Level: Hard
        // A MiniMax algorithm is used here that looks ahead and does not reveal any letters.
  
        /// <param name="familyDictionary"></param>
        public void SortMinMaxAlgorithm(Dictionary<string, int> familyDictionary)
        {
            string code = "";
            int weight = 0;

            foreach (string key in familyDictionary.Keys)
            {
                int wordWeight = 0;

                foreach (char c in key)
                {
                    if (c == '0')
                    {
                        wordWeight += 10;
                    }

                    else if (c == '1')
                    {
                        wordWeight += 1;
                    }
                }

                if (wordWeight > weight)
                {
                    code = key;
                    weight = wordWeight;
                }

            }

            RemoveWordsFromWordList(code);
        }

        public void RemoveWordsFromWordList(string code)
        {
            foreach (string word in WordList.ToList())
            {
                for (int i = 0; i < WordLength; i++)
                {
                    if (word[i] == LastGuess && code[i] == '0')
                    {
                        WordList.Remove(word);
                    }

                    else if (word[i] == LastGuess && code[i] == '1')
                    {
                        continue;
                    }

                    else if (word[i] != LastGuess && code[i] == '0')
                    {
                        continue;
                    }

                    else if (word[i] != LastGuess && code[i] == '1')
                    {
                        WordList.Remove(word);
                    }
                }
            }
        }

        public void CheckGameStatus()
        {
            if (GuessesLeft < 1)
            {
                if (WordList.Count > 1)
                {
                    Display.PrintGameLost(WordList[0]);
                    GameOver = true;
                }

                else
                {
                    if (CurrentWord.Contains('-'))
                    {
                        Display.PrintGameLost(WordList[0]);
                        GameOver = true;
                    }

                    else
                    {
                        Display.PrintGameWon(WordList[0]);
                    }
                }
            }

            else
            {
                if (!CurrentWord.Contains('-'))
                {
                    Display.PrintGameWon(WordList[0]);
                    GameOver = true;
                }
            }
        }

        public void MainLoop()
        {

            while (!GameOver)
            {
                Display.PrintGuesses(GuessedLetters, GuessesLeft);
                Display.PrintWordState(CurrentWord);
                PromptGuess();
                Dictionary<string, int> sortedFamilies = SortFamilies();

                if 
                    (DifficultyLevel == "Easy")
                    SortLargestFamily(sortedFamilies);
                
                else
                    SortMinMaxAlgorithm(sortedFamilies); 
                    UpdateWord();
                    UpdateGuesses();
                    CheckGameStatus();
            }
        }

        public bool PlayAgain()
        {
            Display.PrintPlayAgain();
            string input = Console.ReadLine().ToLower().Trim(); 

            return input == "y";
        }

        public void UpdateWord()
        {
            StringBuilder newWordState = new StringBuilder(CurrentWord, WordLength);

            for (int i = 0; i < WordLength; i++)
            {
                if (GuessedLetters.Contains(WordList[0][i]))
                {
                    newWordState[i] = WordList[0][i];
                }

                else
                {
                    newWordState[i] = '-';
                }
            }

            CurrentWord = newWordState.ToString();
        }

        public void UpdateGuesses()
        {
            if (!CurrentWord.Contains(LastGuess))
            {
                GuessesLeft--;
                Display.PrintWrongGuess(LastGuess);
            }

            else 
            {
                int count = 0; 
                foreach (char c in CurrentWord)
                {
                    if (c == LastGuess)
                    {
                        count++;
                    }
                }

                Display.PrintCorrectGuess(LastGuess, count);
            }
        }
    }
}
