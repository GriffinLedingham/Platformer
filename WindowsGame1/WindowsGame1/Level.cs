using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    public class Level
    {
        public List<bool[]> Grid;

        //Game noticably slows on Retina MacBook @ 33,000 - 34,000 Level Dimensions (2600x13)
        //Phones will not handle a level of this size...
        //Levels should never get this large, but optimization wouldn't be a bad idea
        //
        //Create an active_tile array every time player moves. For each tile, if tile Pos.X and Pos.Y is inside (Player.X/Y +/- (windowSize/2) + arbitrary safety amount) add to active_tile array. 
        //Only check against active tile array when doing all hit detections.
        //Easy to implement.
        public static int Width = 80;
        public static int Height = 13;
        //Vertical camera panning gets wonky past 13 height....wut
        //Something to do with the check between player position and windowHeight/2

        //Leveltype 0: Pre-Defined Input File
        //Leveltype 1: Tall Vertical Level w/ Pre Defined Side Bounds. Only Height variable is used in this case.
        //LevelType 2: Scrolling Horizontal Level
        public static int levelType = 2;

        public Level(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            Grid = new List<bool[]>();
            if (levelType == 0 || levelType > 2 || levelType < 0)
            {
                //File Generated Levels--------------------------------------------------------------------------
                string line = string.Empty;
                line = reader.ReadLine();
                int levelLength = int.Parse(line);
                Width = levelLength;
                while ((line = reader.ReadLine()) != null)
                {
                    bool[] row = new bool[levelLength];
                    for (int i = 1; i < levelLength - 1; i++)
                    {
                        row[i] = (line[i - 1] == '1' ? true : false);
                    }
                    Grid.Add(row);
                }
            }
            else if (levelType == 1)
            {
                //Tall Vertical Levels With Fixed Horizontal Bounds-----------------------------------------------
                Random random = new Random();
                Width = 20;
                for (int i = 0; i < Height; i++)//height
                {
                    bool[] row = new bool[21];
                    if (i != Height - 1 && i != Height - 2)
                    {
                        for (int j = 1; j < 20; j++)
                        {
                            if (j > 0 && i > 0)
                            {
                                if (row[j - 1] || Grid[i - 1][j] || Grid[i - 1][j - 1] || Grid[i - 1][j + 1])
                                {
                                    row[j] = (random.Next(0, 100) < 30 ? true : false);
                                }
                                else if (!row[j - 1] && !row[j - 2] && !Grid[i - 1][j - 1] && !Grid[i - 1][j - 2] && !Grid[i - 1][j] && !Grid[i - 1][j + 1] && !Grid[i - 1][j + 2])
                                {
                                    row[j] = (random.Next(0, 100) < 60 ? true : false);
                                }
                                else
                                {
                                    row[j] = (random.Next(0, 100) < 15 ? true : false);
                                }
                            }
                            else
                            {
                                row[j] = (random.Next(0, 100) < 15 ? true : false);
                            }

                            //Added basic tile clustering. Old random method commented out.
                            //row[j] = (random.Next(0, 100) < 20 ? true : false);
                        }
                    }
                    else if (i == Height - 2)
                    {
                        for (int j = 1; j < 20; j++)
                        {
                            row[j] = (random.Next(0, 100) < 20 ? true : false);
                        }
                    }
                    else
                    {
                        for (int j = 1; j < 20; j++)
                        {
                            row[j] = true;
                        }
                    }
                    row[0] = true;
                    row[18] = true;
                    Grid.Add(row);
                }
            }
            else if (levelType == 2)
            {
                //Long Horizontal Levels---------------------------------------------------------------------------
                Random random = new Random();
                for (int i = 0; i < Height; i++)//height
                {
                    bool[] row = new bool[Width + 1];
                    if (i != Height - 1)
                    {
                        for (int j = 1; j < Width; j++)
                        {
                            row[j] = (random.Next(0, 100) < 20 ? true : false);
                        }
                    }
                    else
                    {
                        for (int j = 1; j < Width; j++)
                        {
                            row[j] = true;
                        }
                    }
                    Grid.Add(row);
                }
            }

            reader.Close();
        }
    }
}
