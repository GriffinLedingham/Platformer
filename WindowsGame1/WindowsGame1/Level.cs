using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    class Level
    {
        public static int Width = 21;
        public List<bool[]> Grid;

        public Level(string filename)
        {
            StreamReader reader = new StreamReader(filename);
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
                
            }

            reader.Close();
        }
    }
}
