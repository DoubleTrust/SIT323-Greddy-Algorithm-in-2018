using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrozzleApplication
{
    /// <summary>
    /// Contains each word's name, location and orientation data in the grid
    /// </summary>
    class WordInfo
    {

        public String word;
        public int startRow;
        public int startCol;
        public String direction;

        public WordInfo(String word, int startRow, int startCol, String direction)
        {
            this.word = word;
            this.startRow = startRow;
            this.startCol = startCol;
            this.direction = direction;
        }

        /// <summary>
        /// Store each word's name, locationa and orientation in a new wordlist
        /// </summary>
        /// <returns></returns>
        public static List<WordInfo> GetWordInfo(char[,] GridContent, int CrozzleRow, int CrozzleColumn)
        {
            List<WordInfo> wordList = null;

            // Horizental word info
            for (int r = 0; r < CrozzleRow; r++)
            {
                for (int c = 0; c < CrozzleColumn; c++)
                {
                    int letterCount = 0;
                    for (int i = 0; i + c < CrozzleColumn; i++)
                    {
                        if (GridContent[r, c + i] != ' ')
                        {
                            letterCount++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (letterCount > 1)
                    {
                        if (wordList == null)
                        {
                            wordList = new List<WordInfo>();
                        }
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < letterCount; i++)
                        {
                            sb.Append(GridContent[r, c + i]);
                        }
                        wordList.Add(new WordInfo(sb.ToString(), r, c, "HORIZENTAL"));
                    }
                    c += letterCount;
                }
            }

            // Vertical word info
            for (int c = 0; c < CrozzleColumn; c++)
            {
                for (int r = 0; r < CrozzleRow; r++)
                {
                    int letterCount = 0;
                    for (int i = 0; i + r < CrozzleRow; i++)
                    {
                        if (GridContent[r + i, c] != ' ')
                        {
                            letterCount++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (letterCount > 1)
                    {
                        if (wordList == null)
                        {
                            wordList = new List<WordInfo>();
                        }
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < letterCount; i++)
                        {
                            sb.Append(GridContent[r + i, c]);
                        }
                        wordList.Add(new WordInfo(sb.ToString(), r, c, "VERTICAL"));
                    }
                    r += letterCount;
                }
            }
            return wordList;
        }

        /// <summary>
        /// Get the base score of the word based on the intersection letter and word itself
        /// </summary>
        /// <param name="word"></param>
        /// <param name="intersection"></param>
        /// <returns></returns>
        public static int GetBaseScore(String word, char intersection, int maxGroup, Dictionary<char, int> IntersectingPointsPerLetter, Dictionary<char, int> NonIntersectingPointsPerLetter, int pointsPerWord)
        {

            int baseScore = 0;
            foreach (char letter in word)
            {
                if (maxGroup == 1)
                {
                    if (letter == intersection)
                        baseScore += IntersectingPointsPerLetter[letter];
                    else
                        baseScore += NonIntersectingPointsPerLetter[letter];
                }
                else
                    baseScore += IntersectingPointsPerLetter[letter];
            }
            if (maxGroup != 1)
                baseScore += pointsPerWord;

            return baseScore;
        }

        /// <summary>
        /// Calculate the score based on current grid
        /// </summary>
        public static int CalculateScore(int pointsPerWord, int CrozzleRow, int CrozzleColumn, char[,] GridContent, Dictionary<char, int> IntersectingPointsPerLetter, Dictionary<char, int> NonIntersectingPointsPerLetter)
        {
            int score = 0;
            score += (CountWordAmount(CrozzleRow, CrozzleColumn, GridContent) * pointsPerWord);
            for (int r = 0; r < CrozzleRow; r++)
            {
                for (int c = 0; c < CrozzleColumn; c++)
                {
                    if (GridContent[r, c] != ' ')
                    {
                        if (LetterInfo.IsInsertionLetter(r, c, CrozzleRow, CrozzleColumn, GridContent))
                        {
                            score += IntersectingPointsPerLetter[GridContent[r, c]];
                        }
                        else
                        {
                            score += NonIntersectingPointsPerLetter[GridContent[r, c]];
                        }
                    }
                }
            }
            return score;
        }

        /// <summary>
        /// Count the amount of word in the grid
        /// </summary>
        /// <returns></returns>
        public static int CountWordAmount(int CrozzleRow, int CrozzleColumn, char[,] GridContent)
        {
            int count = 0;

            // Count horizental words
            for (int r = 0; r < CrozzleRow; r++)
            {
                for (int c = 0; c < CrozzleColumn; c++)
                {
                    int letterCount = 0;
                    for (int i = 0; i + c < CrozzleColumn; i++)
                    {
                        if (GridContent[r, c + i] != ' ')
                        {
                            letterCount++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (letterCount > 1)
                    {
                        count++;
                    }
                    c += letterCount;
                }
            }

            // Count vertical words
            for (int c = 0; c < CrozzleColumn; c++)
            {
                for (int r = 0; r < CrozzleRow; r++)
                {
                    int letterCount = 0;
                    for (int i = 0; i + r < CrozzleRow; i++)
                    {
                        if (GridContent[r + i, c] != ' ')
                        {
                            letterCount++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (letterCount > 1)
                    {
                        count++;
                    }
                    r += letterCount;
                }
            }
            return count;
        }
    }
}
