using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroLogic
{
    class Matrix
    {
        public int lines;
        public int columns;
        private double[,] array;
        public Matrix(int lines, int columns, Random rnd)
        {
            this.lines = lines;
            this.columns = columns;
            array = new double[lines, columns];
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] = rnd.NextDouble()-0.5;
                }
            }
        }
        public double this[int i, int j]
        {
            get { return array[i, j]; }
            set { array[i, j] = value; }
        }
    }
}
