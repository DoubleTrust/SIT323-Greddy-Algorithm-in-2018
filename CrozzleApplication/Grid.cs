using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CrozzleApplication
{
    class Grid
    {
        private Stopwatch timer;
        private bool CrozzleValid;
        private char[,] GridContent;
        private int CrozzleRow;
        private int CrozzleColumn;
        private List<LetterInfo> letters;
        private List<String> subWordList;

        public int score = 0;
        private int pointsPerWord = 0;
        private static string horizontal = "HORIZENTAL";
        private static string vertical = "VERTICAL";
        private Dictionary<char, int> IntersectingPointsPerLetter = null;
        private Dictionary<char, int> NonIntersectingPointsPerLetter = null;

        public int Score { get { return score; } }
        public Configuration Configuration { get; set; }
        public CrozzleSequences CrozzleSequences { get; set; }
        private List<String[]> CrozzleRows { get; set; }
        private List<String[]> CrozzleColumns { get; set; }
        public int PointsPerWord { get { return pointsPerWord; } set { pointsPerWord = value; } }
        public char[,] GridCoordinate { get { return GridContent; } set { GridContent = value; } }

        /// <summary>
        /// Initialize the grid
        /// </summary>
        /// <param name="CrozzleRow"></param>
        /// <param name="CrozzleColumn"></param>
        /// <param name="IntersectingPointsPerLetter"></param>
        /// <param name="NonIntersectingPointsPerLetter"></param>
        /// <param name="pointsPerWord"></param>
        public Grid(int CrozzleRow, int CrozzleColumn, Dictionary<char, int> IntersectingPointsPerLetter, Dictionary<char, int> NonIntersectingPointsPerLetter, int pointsPerWord)
        {
            letters = new List<LetterInfo>();
            this.IntersectingPointsPerLetter = IntersectingPointsPerLetter;
            this.NonIntersectingPointsPerLetter = NonIntersectingPointsPerLetter;
            this.pointsPerWord = pointsPerWord;
            this.CrozzleRow = CrozzleRow;
            this.CrozzleColumn = CrozzleColumn;
            GridContent = new char[CrozzleRow, CrozzleColumn];
            for (int i = 0; i < CrozzleRow; i++)
                for (int j = 0; j < CrozzleColumn; j++)
                    GridContent[i, j] = ' ';
        }

        /// <summary>
        /// Get Crozzle file for validation
        /// </summary>
        /// <param name="aConfiguration"></param>
        public void GetConfiguration(Configuration aConfiguration)
        {
            Configuration = aConfiguration;
        }

        /// <summary>
        /// Check whether the grid is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            bool isEmpty = true;
            for (int i = 0; i < CrozzleRow; i++)
                for (int j = 0; j < CrozzleColumn; j++)
                    if (GridContent[i, j] != ' ')
                        isEmpty = false;
            return isEmpty;
        }

        /// <summary>
        /// Contains Greedy Algorithm which searches the best fit grid 
        /// </summary>
        /// <param name="wordlist"></param>
        /// <returns></returns>
        public List<String> GreedyAlgorithm(List<String> wordlist, int maxGroup, int minGroup,  out TimeSpan timeConsume)
        {
            // Set timer
            timer = new Stopwatch();
            timer.Start();

            Grid maxGrid = null;
            Grid grid = new Grid(CrozzleRow, CrozzleColumn, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, pointsPerWord);

            List<String> temp = new List<string>();
            

            // Get all posibilities of the first insertion word lication and orientations
            List<Grid> firstChilds = new List<Grid>();

            if (IsEmpty())
            {
                // Get the first inserted letter or letters under multiple group circumstances
                int currentScore = -1;
                String firstInsertedWord = null;
                String firstInsertedWord2 = null;
                char firstIntersectiveLetter = '\0';
                char firstIntersectiveLetter2 = '\0';
                foreach (char key in IntersectingPointsPerLetter.Keys)
                {
                    temp.Clear();
                    temp.AddRange(wordlist);

                    firstIntersectiveLetter = key;

                    currentScore = -1;
                    foreach (String word in wordlist)
                    {

                        int baseScore = WordInfo.GetBaseScore(word, firstIntersectiveLetter, maxGroup, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, pointsPerWord);
                        if (currentScore < baseScore)
                        {
                            currentScore = baseScore;
                            firstInsertedWord = word;
                        }
                    }
                    temp.Remove(firstInsertedWord);

                    if (maxGroup >= 2 && minGroup != 1)
                    {
                        int currentScore2 = -1;

                        foreach (char key1 in IntersectingPointsPerLetter.Keys)
                        {
                            if (currentScore2 < IntersectingPointsPerLetter[key1])
                            {
                                currentScore2 = IntersectingPointsPerLetter[key1];
                                firstIntersectiveLetter2 = key1;
                            }
                        }

                        currentScore2 = -1;
                        foreach (String word in temp)
                        {
                            int basescore = WordInfo.GetBaseScore(word, firstIntersectiveLetter2, maxGroup, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, pointsPerWord);
                            if (currentScore2 < basescore)
                            {
                                currentScore2 = basescore;
                                firstInsertedWord2 = word;
                            }
                        }
                        temp.Remove(firstInsertedWord2);
                        
                        
                    }

                    // All horizental insertions posibilities
                    for (int r = 0; r <= CrozzleRow-1; r++)
                    {
                        for (int c = 0; c <= CrozzleColumn - firstInsertedWord.Length; c++)
                        {
                            Grid tempGrid = new Grid(CrozzleRow, CrozzleColumn, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, PointsPerWord);
                            tempGrid.subWordList = new List<string>();
                            if (maxGroup > 1 && minGroup != 1)
                            {
                                if (r <= 1)
                                    if (c + firstInsertedWord.Length >= CrozzleColumn - firstInsertedWord2.Length - 1)
                                        continue;
                                for (int i = 0; i < firstInsertedWord2.Length; i++)
                                {
                                    if (CrozzleColumn - firstInsertedWord2.Length + i < 0)
                                        break;
                                    else
                                        tempGrid.GridCoordinate[0, CrozzleColumn - firstInsertedWord2.Length + i] = firstInsertedWord2[i];
                                }
                            }

                            for (int lp = 0; lp < firstInsertedWord.Length; lp++)
                            {
                                tempGrid.GridCoordinate[r, c + lp] = firstInsertedWord[lp];
                                tempGrid.SaveLetters();
                                score = WordInfo.CalculateScore(pointsPerWord, CrozzleRow, CrozzleColumn, GridContent, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter);
                            }
                            if (!tempGrid.IsEmpty())
                            {
                                tempGrid.subWordList.AddRange(temp);
                                firstChilds.Add(tempGrid);
                            }
                        }
                    }

                    // All vertical insertions posibilities
                    for (int c = 0; c < CrozzleColumn; c++)
                    {
                        for (int r = 0; r <= CrozzleRow - firstInsertedWord.Length; r++)
                        {
                            Grid tempGrid = new Grid(CrozzleRow, CrozzleColumn, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, PointsPerWord);
                            tempGrid.subWordList = new List<string>();
                            if (maxGroup > 1  && minGroup != 1)
                            {
                                if (r <= 2)
                                    if (c >= CrozzleColumn - firstInsertedWord2.Length - 1)
                                        continue;

                                for (int i = 0; i < firstInsertedWord2.Length; i++)
                                {
                                    if (CrozzleColumn - firstInsertedWord2.Length + i < 0)
                                        break;
                                    else
                                        tempGrid.GridCoordinate[0, CrozzleColumn - firstInsertedWord2.Length + i] = firstInsertedWord2[i];
                                }
                            }

                            for (int lp = 0; lp < firstInsertedWord.Length; lp++)
                            {
                                tempGrid.GridCoordinate[r + lp, c] = firstInsertedWord[lp];
                                tempGrid.SaveLetters();
                                score = WordInfo.CalculateScore(pointsPerWord, CrozzleRow, CrozzleColumn, GridContent, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter);
                            }
                            if (!tempGrid.IsEmpty())
                            {
                                tempGrid.subWordList.AddRange(temp);
                                firstChilds.Add(tempGrid);
                            }
                        }
                    }
                }
            }
            
            // Find the rest insertions, get the best grid 
            for (int i = 0; i < firstChilds.Count && timer.Elapsed.TotalMilliseconds < 400000; i++)
            {
                if (minGroup != 1)
                {
                    firstChilds[i].GetBestWordData(firstChilds[i].subWordList, maxGroup);
                }
                else
                    firstChilds[i].GetBestWordData(firstChilds[i].subWordList, 1);

                // Validate each generated crozzle
                CrozzleValid = Validate(firstChilds[i]);
                if (firstChilds[i].Score > grid.Score && CrozzleValid)
                {
                    grid = firstChilds[i];
                }
            }

            // Generate the newKeyValue to replace the origialKeyValue
            maxGrid = grid;
            List<String> GenerateKeyValueGroup = new List<string>();
            List<WordInfo> list = WordInfo.GetWordInfo(grid.GridContent, CrozzleRow, CrozzleColumn);
            String newLine = null;
            foreach (WordInfo word in list)
            {
                if (word.direction == horizontal)
                {
                    newLine = "ROW=" + (word.startRow + 1) + "," + word.word + "," + (word.startCol + 1);
                    GenerateKeyValueGroup.Add(newLine);
                }
            }
            foreach (WordInfo word in list)
            {
                if (word.direction == vertical)
                {
                    newLine = "COLUMN=" + (word.startCol + 1) + "," + word.word + "," + (word.startRow + 1);
                    GenerateKeyValueGroup.Add(newLine);
                }
            }

            // Stop the time after calculation
            timer.Stop();
            // If the time has passed the limit
            if (timer.Elapsed.TotalMilliseconds >= 400000)
            {
                timeConsume = TimeSpan.Zero;
            }
            else
            {
                timeConsume = timer.Elapsed;
            }

            return GenerateKeyValueGroup;
        }

        /// <summary>
        /// Find the best fit grid based on the first child
        /// </summary>
        /// <param name="wordList"></param>
        private void GetBestWordData(List<String> wordList, int maxGroup)
        {
            if (wordList == null || maxGroup == 0)
                return;

            List<LetterInfo> traversedLetters = new List<LetterInfo>();
            List<String> traversedWords = new List<string>();

            List<LetterInfo> temp = new List<LetterInfo>();

            // Test which letter would be the most suitable insertion letter in current word
            foreach (LetterInfo letter in letters)  
            {
                temp.Add(letter);
            }

            while (temp.Count > 0)  
            {
                List<LetterInfo> IntersectionLetterList = new List<LetterInfo>();

                // Find out what letter in suitable for insertion letter
                foreach (LetterInfo letter in temp)
                {
                   IntersectionLetterList.Add(letter);
                }

                if (IntersectionLetterList.Count == 0)
                {
                    break;
                }

                // Test each word from remaining words in wordlist
                List<String> remainWordList = new List<String>();
                foreach (String word in wordList)
                {
                    remainWordList.Add(word);
                }
                foreach (String word in traversedWords)
                {
                    remainWordList.Remove(word);
                }

                // Find out the following best insertion words
                while (IntersectionLetterList.Count > 0 && remainWordList.Count > 0)
                {
                    int baseScore = -1;
                    List<String> insertWords = new List<string>();
                    foreach (String word in remainWordList)
                    {
                        bool IsInsertionLetter = false;

                        for (int i = 0; i < IntersectionLetterList.Count; i++)
                        {
                            foreach (char letter in word)
                            {
                                if (letter == IntersectionLetterList[i].Letter)
                                {
                                    IsInsertionLetter = true;
                                }
                            }
                            if (IsInsertionLetter == true)
                            {
                                int actualScore = WordInfo.GetBaseScore(word, IntersectionLetterList[i].Letter, maxGroup, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, pointsPerWord);
                                if (baseScore < actualScore)
                                {
                                    insertWords.Clear();
                                    baseScore = actualScore;
                                    insertWords.Add(word);
                                }
                                else if (baseScore == actualScore)
                                {
                                    insertWords.Add(word);
                                }
                            }
                        }
                    }

                    if (insertWords.Count == 0)
                    {
                        break;
                    }

                    for (int i = 0; i < IntersectionLetterList.Count; i++)
                    {
                        for (int j = 0; j < insertWords.Count; j++)
                        {
                            if (InsertWords(IntersectionLetterList[i], insertWords[j]))
                            {
                                traversedWords.Add(insertWords[j]);
                                remainWordList.Remove(insertWords[j]);
                                insertWords.Remove(insertWords[j]);

                                temp.Clear();
                                foreach (LetterInfo letter in letters)
                                {
                                    temp.Add(letter);
                                }

                                traversedLetters.Add(IntersectionLetterList[i]);
                                IntersectionLetterList.Remove(IntersectionLetterList[i]);
                                i--;
                                j--;

                                break;
                            }
                        }
                    }
                    for (int i = 0; i < insertWords.Count; i++)
                    {
                        remainWordList.Remove(insertWords[i]);
                    }
                }

                for (int i = 0; i < IntersectionLetterList.Count; i++)
                {
                    traversedLetters.Add(IntersectionLetterList[i]);
                }
                foreach (LetterInfo letter in traversedLetters)
                {
                    if (temp.Contains(letter))
                        temp.Remove(letter);
                }
            }
        }

        /// <summary>
        /// Determin whether the insert word could insert into the letter in insert letter list, and process insertion if positive
        /// </summary>
        /// <param name="insertionLetter"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        private bool InsertWords(LetterInfo insertionLetter, String word)
        {
            if (IsEmpty())
                return false;

            List<int> locations = new List<int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == insertionLetter.Letter)
                {
                    locations.Add(i);
                }
            }

            if (locations.Count == 0)
                return false;

            String direction = vertical;
            int insertIndex = -1;
            int decrease = -1;
            int plus = -1;

            // Determine if it could be inserted horizontally
            foreach (int location in locations)
            {
                decrease = location;
                plus = word.Length - location;

                if (insertionLetter.Column - decrease < 0 || insertionLetter.Column + plus > CrozzleColumn)
                {
                    continue;
                }
                bool Continue = false;
                for (int gLocation = insertionLetter.Column - decrease; gLocation < insertionLetter.Column + plus; gLocation++)
                {
                    if (gLocation == insertionLetter.Column)
                        continue;

                    if (GridContent[insertionLetter.Row, gLocation] != ' ')
                    {
                        Continue = true;
                        break;
                    }

                    if (gLocation == insertionLetter.Column - 1 || gLocation == insertionLetter.Column + 1)
                    {
                        // The 4 direction of inserted letter only allows 1 letter
                        if (CheckSurrounding(insertionLetter.Row, gLocation) > 1)
                        {
                            Continue = true;
                            break;
                        }

                        LetterInfo testLetter = new LetterInfo(insertionLetter.Row, gLocation, word[gLocation - (insertionLetter.Column - decrease)]);

                        // Check surrounding specific circumstances (2 letters around)
                        if (LetterInfo.HasAdjacentLetters(testLetter, insertionLetter, CrozzleRow, CrozzleColumn, GridContent))
                        {
                            Continue = true;
                            break;
                        }
                    }
                    else
                    {
                        if (CheckSurrounding(insertionLetter.Row, gLocation) > 0)
                        {
                            Continue = true;
                            break;
                        }
                    }

                }
                if (Continue == true)
                {
                    continue;
                }
                else
                {
                    // The word could be inserted horizontaly 
                    direction = horizontal;
                    insertIndex = location;
                    break;
                }
            }

            if (direction == vertical)
            {
                foreach (int location in locations)
                {
                    decrease = location;
                    plus = word.Length - location;

                    if (insertionLetter.Row - decrease < 0 || insertionLetter.Row + plus > CrozzleRow)
                    {
                        continue;
                    }
                    bool Continue = false;
                    for (int gLocation = insertionLetter.Row - decrease; gLocation < insertionLetter.Row + plus; gLocation++)
                    {
                        if (gLocation == insertionLetter.Row)
                        {
                            continue;
                        }

                        if (GridContent[gLocation, insertionLetter.Column] != ' ')
                        {
                            Continue = true;
                            break;
                        }

                        if (gLocation == insertionLetter.Row - 1 || gLocation == insertionLetter.Row + 1)
                        {
                            // the same as vertical
                            if (CheckSurrounding(gLocation, insertionLetter.Column) > 1)
                            {
                                Continue = true;
                                break;
                            }

                            LetterInfo testLetter = new LetterInfo(gLocation, insertionLetter.Column, word[gLocation - (insertionLetter.Row - decrease)]);

                            if (LetterInfo.HasAdjacentLetters(testLetter, insertionLetter, CrozzleRow, CrozzleColumn, GridContent))
                            {
                                Continue = true;
                                break;
                            }
                        }
                        else
                        {
                            if (CheckSurrounding(gLocation, insertionLetter.Column) > 0)
                            {
                                Continue = true;
                                break;
                            }
                        }

                    }
                    if (Continue == true)
                    {
                        continue;
                    }
                    else
                    {
                        // The word can be inserted vertically
                        direction = vertical;
                        insertIndex = location;
                        break;
                    }
                }
            }
            // If thw word can't be inserted
            if (insertIndex == -1)
            {
                return false;
            }

            if (direction == horizontal)
            {
                for (int gLocation = insertionLetter.Column - decrease, i = 0; gLocation < insertionLetter.Column + plus; gLocation++, i++)
                {
                    GridContent[insertionLetter.Row, gLocation] = word[i];
                }
            }
            else if (direction == vertical)
            {
                for (int gLocation = insertionLetter.Row - decrease, i = 0; gLocation < insertionLetter.Row + plus; gLocation++, i++)
                {
                    GridContent[gLocation, insertionLetter.Column] = word[i];
                }
            }
            SaveLetters();
            score = WordInfo.CalculateScore(pointsPerWord, CrozzleRow, CrozzleColumn, GridContent, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter);
            return true;
        }

        /// <summary>
        /// Check whether the letter's 4 directions exist other letters
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private int CheckSurrounding(int r, int c)
        {
            int count = 0;
            if (c + 1 < CrozzleColumn && GridContent[r, c + 1] != ' ')
            {
                count++;
            }
            if (c - 1 >= 0 && GridContent[r, c - 1] != ' ')
            {
                count++;
            }
            if (r + 1 < CrozzleRow && GridContent[r + 1, c] != ' ')
            {
                count++;
            }
            if (r - 1 >= 0 && GridContent[r - 1, c] != ' ')
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// Store each inserted letter's name and location
        /// </summary>
        public void SaveLetters()
        {
            letters.Clear();
            for (int r = 0; r < CrozzleRow; r++)
            {
                for (int c = 0; c < CrozzleColumn; c++)
                {
                    if (GridContent[r, c] != ' ')
                    {
                        letters.Add(new LetterInfo(r, c, GridContent[r, c]));
                    }
                }
            }
        }

        /// <summary>
        /// Validate each generated crozzle
        /// </summary>
        /// <param name="testGrid"></param>
        public bool Validate(Grid testGrid)
        {
            CrozzleRows = new List<String[]>();
            CrozzleColumns = new List<String[]>();

            // Get all row data 
            for (int r = 0; r < CrozzleRow; r++)
            {
                String[] row = new String[CrozzleColumn];
                for (int c = 0; c < row.Length; c++)
                    row[c] = testGrid.GridCoordinate[r,c].ToString();
                CrozzleRows.Add(row);
            }

            // Get all column data 
            for (int c = 0; c < CrozzleColumn; c++)
            {
                String[] col = new String[CrozzleRow];
                for (int r = 0; r < col.Length; r++)
                    col[r] = testGrid.GridCoordinate[r, c].ToString();
                CrozzleColumns.Add(col);
            }

            CrozzleSequences = new CrozzleSequences(CrozzleRows, CrozzleColumns, Configuration);

            // Check that the number of rows is within limits.
            if (CrozzleRow < Configuration.MinimumNumberOfRows || CrozzleRow > Configuration.MaximumNumberOfRows)
                //CrozzleGridErrors.Add(String.Format(CrozzleFileErrors.RowCountError, Rows, Configuration.MinimumNumberOfRows, Configuration.MaximumNumberOfRows));
                return false;
            // Check that the number of columns is within limits.
            if (CrozzleColumn < Configuration.MinimumNumberOfColumns || CrozzleColumn > Configuration.MaximumNumberOfColumns)
                //CrozzleGridErrors.Add(String.Format(CrozzleFileErrors.ColumnCountError, Columns, Configuration.MinimumNumberOfColumns, Configuration.MaximumNumberOfColumns));
                return false;
            // Check that the number of vertical words that intersect a horizontal word is within limits.
            CrozzleSequences.CheckHorizontalIntersections(Configuration.MinimumIntersectionsInHorizontalWords, Configuration.MaximumIntersectionsInHorizontalWords);

            // Check that the number of horizontal words that intersect a vertical word is within limits.
            CrozzleSequences.CheckVerticalIntersections(Configuration.MinimumIntersectionsInVerticalWords, Configuration.MaximumIntersectionsInVerticalWords);

            // Check that the number of duplicate words is within limits.
            CrozzleSequences.CheckDuplicateWords(Configuration.MinimumNumberOfTheSameWord, Configuration.MaximumNumberOfTheSameWord);

            // Check that the number of groups of connected words is within the limit.
            CrozzleSequences.CheckConnectivity(Configuration.MinimumNumberOfGroups, Configuration.MaximumNumberOfGroups, CrozzleRows, CrozzleColumns);

            if (CrozzleSequences.ErrorsDetected)
                return false;
            else
                return true;
        }

    }
}
