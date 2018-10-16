using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrozzleApplication
{
    /// <summary>
    /// Contains each letter's name, location and name in the grid
    /// </summary>
    class LetterInfo
    {
        private int row;
        private int column;
        private char letter_name;

        public int Row { get {return row; } set { row = value; } }
        public int Column { get {return  column; } set { column = value; } }
        public char Letter { get {return letter_name; }set { letter_name = value; } }

        public LetterInfo(int row, int column, char letter)
        {
            this.row = row;
            this.column = column;
            this.letter_name = letter;
        }

        /// <summary>
        /// Determine whether this letter is an inserted word
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsInsertionLetter(int r, int c, int CrozzleRow, int CrozzleColumn, char[,] GridContent)
        {
            int hCounter = 0;
            int vCounter = 0;

            if (r - 1 >= 0 && GridContent[r - 1, c] != ' ')
            {
                vCounter++;
            }
            if (r + 1 < CrozzleRow && GridContent[r + 1, c] != ' ')
            {
                vCounter++;
            }
            if (c - 1 >= 0 && GridContent[r, c - 1] != ' ')
            {
                hCounter++;
            }
            if (c + 1 < CrozzleColumn && GridContent[r, c + 1] != ' ')
            {
                hCounter++;
            }

            if (hCounter >= 1 && vCounter >= 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check whether insert letter's surroundings have two other letters 
        /// </summary>
        /// <param name="testLetter"></param>
        /// <param name="insertionLetter"></param>
        /// <returns></returns>
        public static bool HasAdjacentLetters(LetterInfo testLetter, LetterInfo insertionLetter, int CrozzleRow, int CrozzleColumn, char[,] GridContent)
        {
            if (testLetter.Column == insertionLetter.Column - 1)
            {
                if (insertionLetter.Column + 1 < CrozzleColumn && GridContent[insertionLetter.Row, insertionLetter.Column + 1] != ' ')
                {
                    return true;
                }
            }
            else if (testLetter.Column == insertionLetter.Column + 1)
            {
                if (insertionLetter.Column - 1 >= 0 && GridContent[insertionLetter.Row, insertionLetter.Column - 1] != ' ')
                {
                    return true;
                }
            }
            else if (testLetter.Row == insertionLetter.Row - 1)
            {
                if (insertionLetter.Row + 1 < CrozzleRow && GridContent[insertionLetter.Row + 1, insertionLetter.Column] != ' ')
                {
                    return true;
                }
            }
            else if (testLetter.Row == insertionLetter.Row + 1)
            {
                if (insertionLetter.Row - 1 >= 0 && GridContent[insertionLetter.Row - 1, insertionLetter.Column] != ' ')
                {
                    return true;
                }
            }
            return false;
        }

    }
}
