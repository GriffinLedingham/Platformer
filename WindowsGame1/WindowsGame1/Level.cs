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
        public static int Width = 21;
        public List<bool[]> Grid;

        public Level(string filename)
        {
            /*StreamReader reader = new StreamReader(filename);
            Grid = new List<bool[]>();
            string line = string.Empty;

            while ((line = reader.ReadLine()) != null)
            {
                bool[] row = new bool[21];
                row[0] = true;
                row[20] = true;
                for (int i = 1; i < 20; i++)
                {
                    row[i] = (line[i-1] == '1' ? true : false);
                }

                //Grid.Insert(0,row);
                Grid.Add(row);
                
            }*/

            StreamReader reader = new StreamReader(filename);
            Grid = new List<bool[]>();
            string line = string.Empty;
            line = reader.ReadLine();
            int levelLength = int.Parse(line);
            Width = levelLength;
            while ((line = reader.ReadLine()) != null)
            {
                bool[] row = new bool[levelLength];
                row[0] = true;
                row[levelLength-1] = true;
                for (int i = 1; i < levelLength-1; i++)
                {
                    row[i] = (line[i - 1] == '1' ? true : false);
                }

                //Grid.Insert(0,row);
                Grid.Add(row);

            }

            /*Random random = new Random();
            for (int i = 0; i < 13;i++ )//height
            {
                bool[] row = new bool[21];
                
                if (i != 12)
                {

                    for (int j = 1; j < 20; j++)
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



            reader.Close();
        }
    }
}
