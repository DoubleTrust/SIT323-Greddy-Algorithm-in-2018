using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrozzleApplication;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class Assessment2_FunctionTest
    {
        [TestMethod]
        public void Test_CalculateScore()
        {
            // Arrange
            int pointsPerWord = 10;
            int CrozzleRow = 4;
            int CrozzleColumn = 5;
            Dictionary<char, int> IntersectingPointsPerLetter = new Dictionary<char, int>()
            {
                {'A',1 }, {'B',2 },{'C',3 },{'D',4 },{'E',5 },{'F',6 },
                {'G',7 }, {'H',8 },{'I',9 },{'J',10 },{'K',11 },{'L',12 },
                {'M',13 },{'N',14 },{'O',15 },{'P',16 },{'Q',17 },{'R',18 },
                {'S',19 },{'T',20 },{'U',21 },{'V',22 },{'W',23 },{'X',24 },
                { 'Y',25 },{'Z',26 }
            };
            Dictionary<char, int> NonIntersectingPointsPerLetter = new Dictionary<char, int>()
            {
                {'A',1 }, {'B',2 },{'C',3 },{'D',4 },{'E',5 },{'F',6 },
                {'G',7 }, {'H',8 },{'I',9 },{'J',10 },{'K',11 },{'L',12 },
                {'M',13 },{'N',14 },{'O',15 },{'P',16 },{'Q',17 },{'R',18 },
                {'S',19 },{'T',20 },{'U',21 },{'V',22 },{'W',23 },{'X',24 },
                { 'Y',25 },{'Z',26 }
            };

            char[,] GridContent1 = new char[,]
            {
                {'A','N','N','A',' '},
                {' ','I',' ',' ',' '},
                {' ','C',' ',' ',' '},
                {' ','E','C','O',' '},
            };

            char[,] GridContent2 = new char[,]
            {
                {'B','E','T','A',' '},
                {' ',' ','I',' ',' '},
                {' ',' ','C','A','T'},
                {' ',' ','K',' ',' '},
            };

            char[,] GridContent3 = new char[,]
            {
                {'A','G','E','N','T'},
                {' ',' ','A',' ',' '},
                {' ',' ','S',' ',' '},
                {'D','E','T','L','A'},
            };
            char[,] GridContent4 = new char[,]
            {
                {'E','A','R','T','H'},
                {' ',' ',' ',' ','A'},
                {' ',' ','A','L','L'},
                {' ',' ','T',' ','O'},
            };

            int Test1Result, Test2Result, Test3Result, Test4Result = 0;

            // Act
            Test1Result = WordInfo.CalculateScore(pointsPerWord, CrozzleRow, CrozzleColumn, GridContent1, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter);
            Test2Result = WordInfo.CalculateScore(pointsPerWord, CrozzleRow, CrozzleColumn, GridContent2, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter);
            Test3Result = WordInfo.CalculateScore(pointsPerWord, CrozzleRow, CrozzleColumn, GridContent3, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter);
            Test4Result = WordInfo.CalculateScore(pointsPerWord, CrozzleRow, CrozzleColumn, GridContent4, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter);

            // Assert
            Assert.AreEqual(95, Test1Result, "error");
            Assert.AreEqual(102, Test2Result, "error");
            Assert.AreEqual(139, Test3Result, "error");
            Assert.AreEqual(153, Test4Result, "error");

        }

        [TestMethod]
        public void Test_CountWordAmount()
        {

            // Arrange
            int CrozzleRow = 4;
            int CrozzleColumn = 5;

            char[,] GridContent1 = new char[,]
            {
                {'A','N','N','A',' '},
                {' ','I',' ',' ',' '},
                {' ','C',' ',' ',' '},
                {' ','E','C','O',' '},
            };

            char[,] GridContent2 = new char[,]
            {
                {'B','E','T','A',' '},
                {'A',' ','I',' ',' '},
                {'K',' ','C','A','T'},
                {'E',' ','K',' ',' '},
            };

            char[,] GridContent3 = new char[,]
            {
                {'A','G','E','N','T'},
                {' ',' ','A',' ',' '},
                {' ',' ','S',' ',' '},
                {'D','E','T','L','A'},
            };
            char[,] GridContent4 = new char[,]
            {
                {'E','A','R','T','H'},
                {'A',' ',' ',' ','A'},
                {'T',' ','A','L','L'},
                {' ',' ','T',' ','O'},
            };

            int Test1Result, Test2Result, Test3Result, Test4Result = 0;

            // Act
            Test1Result = WordInfo.CountWordAmount(CrozzleRow, CrozzleColumn, GridContent1);
            Test2Result = WordInfo.CountWordAmount(CrozzleRow, CrozzleColumn, GridContent2);
            Test3Result = WordInfo.CountWordAmount(CrozzleRow, CrozzleColumn, GridContent3);
            Test4Result = WordInfo.CountWordAmount(CrozzleRow, CrozzleColumn, GridContent4);

            // Assert
            Assert.AreEqual(3, Test1Result, "error");
            Assert.AreEqual(4, Test2Result, "error");
            Assert.AreEqual(3, Test3Result, "error");
            Assert.AreEqual(5, Test4Result, "error");

        }

        [TestMethod]
        public void Test_GetWordInfo()
        {
            // Arrange
            int CrozzleRow = 4;
            int CrozzleColumn = 5;

            char[,] GridContent1 = new char[,]
            {
                {'A','N','N','A',' '},
                {' ','I',' ',' ', ' '},
                {' ','C',' ',' ', ' '},
                {' ','E','C','O', ' '},
            };

            char[,] GridContent2 = new char[,]
            {
                {'B','E','T','A',' '},
                {' ',' ','I',' ',' '},
                {' ',' ','C','A','T'},
                {' ',' ','K',' ',' '},
            };

            char[,] GridContent3 = new char[,]
            {
                {'A','G','E','N','T'},
                {' ',' ','A',' ',' '},
                {' ',' ','S',' ',' '},
                {'D','E','T','L','A'},
            };

            char[,] GridContent4 = new char[,]
            {
                {'E','A','R','T','H'},
                {' ',' ',' ',' ','A'},
                {' ',' ','A','L','L'},
                {' ',' ','T',' ','O'},
            };

            List<WordInfo> Test1Result, Test2Result, Test3Result, Test4Result = null;

            // Act
            Test1Result = WordInfo.GetWordInfo(GridContent1, CrozzleRow, CrozzleColumn);
            Test2Result = WordInfo.GetWordInfo(GridContent2, CrozzleRow, CrozzleColumn);
            Test3Result = WordInfo.GetWordInfo(GridContent3, CrozzleRow, CrozzleColumn);
            Test4Result = WordInfo.GetWordInfo(GridContent4, CrozzleRow, CrozzleColumn);

            // Assert
            Assert.AreEqual("ANNA", Test1Result[0].word, "error");
            Assert.AreEqual(2,Test2Result[1].startRow, "error");
            Assert.AreEqual(2, Test3Result[2].startCol, "error");
            Assert.AreEqual("VERTICAL", Test4Result[3].direction, "error");
        }
      
        [TestMethod]
        //Note: In order to test greedy alogorithm comprehensively, this test method contains 12 diverse unit tests
        public void Test_GreedyAlgorithm()
        {
            // Arrange
            bool TestResult1 = true, TestResult2 = true, TestResult3 = true,  TestResult4 = true,  TestResult5 = true, TestResult6 = true, TestResult7 = true, TestResult8 = true, TestResult9 = true, TestResult10 = true, TestResult11 = true, TestResult12 = true;
            TimeSpan timeConsume;
            Dictionary<char, int> IntersectingPointsPerLetter = new Dictionary<char, int>()
            {
                {'A',1 }, {'B',2 },{'C',3 },{'D',4 },{'E',5 },{'F',6 },
                {'G',7 }, {'H',8 },{'I',9 },{'J',10 },{'K',11 },{'L',12 },
                {'M',13 },{'N',14 },{'O',15 },{'P',16 },{'Q',17 },{'R',18 },
                {'S',19 },{'T',20 },{'U',21 },{'V',22 },{'W',23 },{'X',24 },
                { 'Y',25 },{'Z',26 }
            };
            Dictionary<char, int> NonIntersectingPointsPerLetter = new Dictionary<char, int>()
            {
                {'A',0 }, {'B',0 },{'C',0 },{'D',0 },{'E',0 },{'F',0 },
                {'G',0 }, {'H',0 },{'I',0 },{'J',0 },{'K',0 },{'L',0 },
                {'M',0 },{'N',0 },{'O',0 },{'P',0 },{'Q',0 },{'R',0 },
                {'S',0 },{'T',0 },{'U',0 },{'V',0 },{'W',0 },{'X',0 },
                { 'Y',0 },{'Z',0 }
            };

            int CrozzleRow1 = 4,    CrozzleRow2 = 5,    CrozzleRow3 = 5;
            int CrozzleColumn1 = 4, CrozzleColumn2 = 5, CrozzleColumn3 = 4;

            // Two configuration files indicate two different group limit
            Configuration aConfiguration1= null, aConfiguration2 = null;
            String configurationFileName1 = "Configuration\\Configuration1.txt";
            Configuration.TryParse(configurationFileName1, out aConfiguration1);
            String configurationFileName2 = "Configuration\\Configuration2.txt";
            Configuration.TryParse(configurationFileName2, out aConfiguration2);

            List<String> GenerateKeyValueGroup1 = new List<string>();
            List<String> wordlist1_1 = new List<string> { "BABY","BIRD", "BED", "DB", "DO", "BC" };
            List<String> GenerateKeyValueGroup2 = new List<string>();
            List<String> wordlist1_2 = new List<string> { "ROAD","BABY", "BIRD", "BED", "DO", "BC"};
            List<String> GenerateKeyValueGroup3 = new List<string>();
            List<String> wordlist1_3 = new List<string> { "BABY", "BIRD", "BED", "DB", "DO", "BC", "ROAD", "YEAH", "OK" };
            List<String> GenerateKeyValueGroup4 = new List<string>();
            List<String> wordlist1_4 = new List<string> { "BABY", "BIRD", "BED", "DB", "DO", "BC", "ROAD", "YEAH", "OK", "RONA"};
            List<String> GenerateKeyValueGroup5 = new List<string>();
            List<String> wordlist2_1 = new List<string> { "FACE", "ANGLE", "EARTH", "GIRD" };
            List<String> GenerateKeyValueGroup6 = new List<string>();
            List<String> wordlist2_2 = new List<string> { "FACE", "ANGLE", "EARTH", "GIRD", "LET", "IE" };
            List<String> GenerateKeyValueGroup7 = new List<string>();
            List<String> wordlist2_3 = new List<string> { "FACE", "ANGLE", "EARTH", "GIRD", "LET", "IE" };
            List<String> GenerateKeyValueGroup8 = new List<string>();
            List<String> wordlist2_4 = new List<string> { "FACE", "ANGLE", "EARTH", "GIRD", "LET", "IE", "BAT" };
            List<String> GenerateKeyValueGroup9 = new List<string>();
            List<String> wordlist3_1 = new List<string> { "ZONE", "ZEBRA", "BEND", "END" };
            List<String> GenerateKeyValueGroup10 = new List<string>();
            List<String> wordlist3_2 = new List<string> { "ZONE", "ZEBRA", "BEND", "AND", "NONE", "END"};
            List<String> GenerateKeyValueGroup11 = new List<string>();
            List<String> wordlist3_3 = new List<string> { "ZONE", "ZEBRA", "BEND", "AND", "NONE", "END", "ON" };
            List<String> GenerateKeyValueGroup12 = new List<string>();
            List<String> wordlist3_4 = new List<string> { "ZONE", "ZEBRA", "BEND", "AND", "NONE", "END", "ON" };

            List<String> ExpectedGenerateKeyValueGroup1 = new List<string>
            {
                "ROW=1,BIRD,1",
                "ROW=3,BED,1",
                "ROW=4,BC,3",
                "COLUMN=1,BABY,1",
                "COLUMN=3,DB,3",
                "COLUMN=4,DO,1",
            };

            List<String> ExpectedGenerateKeyValueGroup2 = new List<string>
            {
                "ROW=1,BABY,1",
                "ROW=3,ROAD,1",
                "COLUMN=1,BIRD,1",
                "COLUMN=4,DO,3",
            };

            List<String> ExpectedGenerateKeyValueGroup3 = new List<string>
            {
                "ROW=1,BED,1",
                "ROW=2,OK,3",
                "ROW=4,YEAH,1",
                "COLUMN=1,BABY,1",
                "COLUMN=3,DO,1",
            };

            List<String> ExpectedGenerateKeyValueGroup4 = new List<string>
            {
                "ROW=1,BED,1",
                "ROW=2,OK,3",
                "ROW=4,YEAH,1",
                "COLUMN=1,BABY,1",
                "COLUMN=3,DO,1",
            };

            List<String> ExpectedGenerateKeyValueGroup5 = new List<string>
            {
                "ROW=1,FACE,1",
                "ROW=3,GIRD,2",
                "COLUMN=2,ANGLE,1",
                "COLUMN=4,EARTH,1",
            };

            List<String> ExpectedGenerateKeyValueGroup6 = new List<string>
            {
                "ROW=1,FACE,1",
                "ROW=3,GIRD,2",
                "ROW=4,LET,2",
                "COLUMN=2,ANGLE,1",
                "COLUMN=3,IE,3",
                "COLUMN=4,EARTH,1",
            };

            List<String> ExpectedGenerateKeyValueGroup7 = new List<string>
            {
                "ROW=1,ANGLE,1",
                "ROW=5,EARTH,1",
                "COLUMN=1,IE,4",
                "COLUMN=4,LET,1",
            };

            List<String> ExpectedGenerateKeyValueGroup8 = new List<string>
            {
                "ROW=1,ANGLE,1", 
                "ROW=3,BAT,2",
                "ROW=5,EARTH,1",
                "COLUMN=1,IE,4",
                "COLUMN=4,LET,1",

            };

            List<String> ExpectedGenerateKeyValueGroup9 = new List<string>
            {
                "ROW=1,ZONE,1",
                "COLUMN=1,ZEBRA,1",
                "COLUMN=4,END,1",
            };

            List<String> ExpectedGenerateKeyValueGroup10 = new List<string>
            {
                "ROW=1,ZONE,1",
                "ROW=3,BEND,q",
                "COLUMN=1,ZEBRA,1",
                "COLUMN=3,NONE,1",

            };

            List<String> ExpectedGenerateKeyValueGroup11 = new List<string>
            {
                "ROW=1,ZONE,1",
                "ROW=2,ON,3",
                "ROW=3,BEND,1",
                "COLUMN=1,ZEBRA,1",
                "COLUMN=3,NONE,1",
                "COLUMN=4,END,1",
            };

            List<String> ExpectedGenerateKeyValueGroup12 = new List<string>
            {
                "ROW=1,ZONE,1",
                "ROW=4,END,3",
                "COLUMN=1,ZEBRA,1",
                "COLUMN=4,AND,3",
            };

            // Act
            Grid grid1 = new Grid(CrozzleRow1, CrozzleColumn1, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid1.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup1 = grid1.GreedyAlgorithm(wordlist1_1, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid2 = new Grid(CrozzleRow1, CrozzleColumn1, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid2.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup2 = grid2.GreedyAlgorithm(wordlist1_2, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid3 = new Grid(CrozzleRow1, CrozzleColumn1, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid3.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup3 = grid3.GreedyAlgorithm(wordlist1_3, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid4 = new Grid(CrozzleRow1, CrozzleColumn1, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid4.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup4 = grid4.GreedyAlgorithm(wordlist1_4, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid5 = new Grid(CrozzleRow2, CrozzleColumn2, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid5.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup5 = grid5.GreedyAlgorithm(wordlist2_1, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid6 = new Grid(CrozzleRow2, CrozzleColumn2, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid6.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup6 = grid6.GreedyAlgorithm(wordlist2_2, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid7 = new Grid(CrozzleRow2, CrozzleColumn2, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid7.GetConfiguration(aConfiguration2);
            GenerateKeyValueGroup7 = grid7.GreedyAlgorithm(wordlist2_3, aConfiguration2.MaximumNumberOfGroups, aConfiguration2.MinimumNumberOfGroups, out timeConsume);

            Grid grid8 = new Grid(CrozzleRow2, CrozzleColumn2, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid8.GetConfiguration(aConfiguration2);
            GenerateKeyValueGroup8 = grid8.GreedyAlgorithm(wordlist2_4, aConfiguration2.MaximumNumberOfGroups, aConfiguration2.MinimumNumberOfGroups, out timeConsume);

            Grid grid9 = new Grid(CrozzleRow3, CrozzleColumn3, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid9.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup9 = grid9.GreedyAlgorithm(wordlist3_1, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid10 = new Grid(CrozzleRow3, CrozzleColumn3, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid10.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup10 = grid10.GreedyAlgorithm(wordlist3_2, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid11 = new Grid(CrozzleRow3, CrozzleColumn3, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid11.GetConfiguration(aConfiguration1);
            GenerateKeyValueGroup11 = grid11.GreedyAlgorithm(wordlist3_3, aConfiguration1.MaximumNumberOfGroups, aConfiguration1.MinimumNumberOfGroups, out timeConsume);

            Grid grid12 = new Grid(CrozzleRow2, CrozzleColumn2, IntersectingPointsPerLetter, NonIntersectingPointsPerLetter, aConfiguration1.PointsPerWord);
            grid12.GetConfiguration(aConfiguration2);
            GenerateKeyValueGroup12 = grid12.GreedyAlgorithm(wordlist3_4, aConfiguration2.MaximumNumberOfGroups, aConfiguration2.MinimumNumberOfGroups, out timeConsume);

            // Assert
            if (ExpectedGenerateKeyValueGroup1.Count != GenerateKeyValueGroup1.Count)
                TestResult1 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup1.Count; i++)
                    if (ExpectedGenerateKeyValueGroup1[i] != GenerateKeyValueGroup1[i])
                        TestResult1 = false;
            }
            Assert.AreEqual(true, TestResult1, "TestResult1 error");

            if (ExpectedGenerateKeyValueGroup2.Count != GenerateKeyValueGroup2.Count)
                TestResult2 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup2.Count; i++)
                    if (ExpectedGenerateKeyValueGroup2[i] != GenerateKeyValueGroup2[i])
                        TestResult2 = false;
            }
            Assert.AreEqual(true, TestResult2, "TestResult1 error");

            if (ExpectedGenerateKeyValueGroup3.Count != GenerateKeyValueGroup3.Count)
                TestResult3 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup3.Count; i++)
                    if (ExpectedGenerateKeyValueGroup3[i] != GenerateKeyValueGroup3[i])
                        TestResult3 = false;
            }
            Assert.AreEqual(true, TestResult3, "TestResult3 error");

            if (ExpectedGenerateKeyValueGroup4.Count != GenerateKeyValueGroup4.Count)
                TestResult4 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup4.Count; i++)
                    if (ExpectedGenerateKeyValueGroup4[i] != GenerateKeyValueGroup4[i])
                        TestResult4 = false;
            }
            Assert.AreEqual(true, TestResult4, "TestResult4 error");

            if (ExpectedGenerateKeyValueGroup5.Count != GenerateKeyValueGroup5.Count)
                TestResult5 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup5.Count; i++)
                    if (ExpectedGenerateKeyValueGroup5[i] != GenerateKeyValueGroup5[i])
                        TestResult5 = false;
            }

            // This unit test is failed becasue a better solution had been found by programmers
            Assert.AreEqual(false, TestResult5, "TestResult5 error");

            if (ExpectedGenerateKeyValueGroup6.Count != GenerateKeyValueGroup6.Count)
                TestResult6 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup6.Count; i++)
                    if (ExpectedGenerateKeyValueGroup6[i] != GenerateKeyValueGroup6[i])
                        TestResult6 = false;
            }

            // This unit test is failed becasue a better solution had been found by programmers
            Assert.AreEqual(false, TestResult6, "TestResult6 error");

            if (ExpectedGenerateKeyValueGroup7.Count != GenerateKeyValueGroup7.Count)
                TestResult7 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup7.Count; i++)
                    if (ExpectedGenerateKeyValueGroup7[i] != GenerateKeyValueGroup7[i])
                        TestResult7 = false;
            }
            Assert.AreEqual(true, TestResult7, "TestResult7 error");

            if (ExpectedGenerateKeyValueGroup8.Count != GenerateKeyValueGroup8.Count)
                TestResult8 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup8.Count; i++)
                    if (ExpectedGenerateKeyValueGroup8[i] != GenerateKeyValueGroup8[i])
                        TestResult8 = false;
            }
            Assert.AreEqual(true, TestResult8, "TestResult8 error");

            if (ExpectedGenerateKeyValueGroup9.Count != GenerateKeyValueGroup9.Count)
                TestResult9 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup9.Count; i++)
                    if (ExpectedGenerateKeyValueGroup9[i] != GenerateKeyValueGroup9[i])
                        TestResult9 = false;
            }
            Assert.AreEqual(true, TestResult9, "TestResult9 error");

            if (ExpectedGenerateKeyValueGroup10.Count != GenerateKeyValueGroup10.Count)
                TestResult10 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup10.Count; i++)
                    if (ExpectedGenerateKeyValueGroup10[i] != GenerateKeyValueGroup10[i])
                        TestResult10 = false;
            }

            // This unit test is failed becasue a better solution had been found by programmers
            Assert.AreEqual(false, TestResult10, "TestResult10 error");

            if (ExpectedGenerateKeyValueGroup11.Count != GenerateKeyValueGroup11.Count)
                TestResult11 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup11.Count; i++)
                    if (ExpectedGenerateKeyValueGroup11[i] != GenerateKeyValueGroup11[i])
                        TestResult11 = false;
            }

            // This unit test is failed becasue a better solution had been found by programmers
            Assert.AreEqual(false, TestResult11, "TestResult11 error");

            if (ExpectedGenerateKeyValueGroup12.Count != GenerateKeyValueGroup12.Count)
                TestResult12 = false;
            else
            {
                for (int i = 0; i < GenerateKeyValueGroup12.Count; i++)
                    if (ExpectedGenerateKeyValueGroup12[i] != GenerateKeyValueGroup12[i])
                        TestResult12 = false;
            }

            // This unit test is failed becasue a better solution had been found by programmers
            Assert.AreEqual(false, TestResult12, "TestResult12 error");
        }
    }
}
