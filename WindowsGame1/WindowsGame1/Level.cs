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
        public static int Width = 99;
        public static int Height = 13;
        public List<bool[]> Grid;

        public Level(string filename)
        {
            

            StreamReader reader = new StreamReader(filename);
            Grid = new List<bool[]>();

            //File Generated Levels--------------------------------------------------------------------------
            /*string line = string.Empty;
            line = reader.ReadLine();
            int levelLength = int.Parse(line);
            Width = levelLength;
            while ((line = reader.ReadLine()) != null)
            {
                bool[] row = new bool[levelLength];
                //row[0] = true;
                //row[levelLength-1] = true;
                for (int i = 1; i < levelLength-1; i++)
                {
                    row[i] = (line[i - 1] == '1' ? true : false);
                }

                //Grid.Insert(0,row);
                Grid.Add(row);

            }*/


            //Tall Vertical Levels---------------------------------------------------------------------------
            /*Random random = new Random();
            for (int i = 0; i < Height;i++ )//height
            {
                bool[] row = new bool[21];
                
                if (i != Height-1)
                {

                    for (int j = 1; j < 20; j++)
                    {
                        if (random.Next(0, 100) < 30)
                        {
                            row[j] = true;
                        }
                        else
                        {
                            row[j] = false;
                        }
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

                //Grid.Insert(0,row);
                Grid.Add(row);

            }*/

            //Long Horizontal Levels---------------------------------------------------------------------------
            Random random = new Random();
            for (int i = 0; i < Height;i++ )//height
            {
                bool[] row = new bool[100];
                
                if (i != Height-1)
                {

                    for (int j = 1; j < 99; j++)
                    {
                        if (random.Next(0, 100) < 20)
                        {
                            row[j] = true;
                        }
                        else
                        {
                            row[j] = false;
                        }
                    }
                }
                else
                {
                    for (int j = 1; j < 99; j++)
                    {
                        row[j] = true;
                    }
                }

                //row[0] = true;
                //row[18] = true;

                //Grid.Insert(0,row);
                Grid.Add(row);

            }



            reader.Close();
        }
    }
}
