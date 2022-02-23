using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroLogic
{
    class Vector
    {
        public int lenth;
        private double[] array;
        public Vector(int lenth)
        {
            this.lenth = lenth;
            array = new double[lenth];
        }
        public Vector(params double[] array)
        {
            lenth = array.Length;
            this.array = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                this.array[i] = array[i];
            }
        }
        public double this[int index]
        {
            get { return array[index]; }
            set { array[index] = value; }
        }
        public override string ToString()
        {
            string line = "";
            for (int i = 0; i < array.Length; i++)
            {
                line += array[i].ToString() + " ";
            }
            return line;
        }
    }
}
