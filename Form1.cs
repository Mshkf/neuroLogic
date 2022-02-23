using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neuroLogic
{
    public partial class Form1 : Form
    {
        Network network = new Network(new int[] { 2, 3, 1 });

        Vector[] X =
        {
                new Vector(0,0),
                new Vector(0,1),
                new Vector(1,0),
                new Vector(1,1)
            };

        Vector[] Y =
{
                new Vector(0.0),
                new Vector(1.0),
                new Vector(1.0),
                new Vector(0.0)
            };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = $"0 ^ 0 = {0 ^ 0:F4}\n0 ^ 1 = {0 ^ 1:F4}" +
                $"\n1 ^ 0 = {1 ^ 0:F4}\n1 ^ 1 = {1 ^ 1:F4}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            network.Train(X, Y, 0.5, 1e-7, 100000);
            label1.Text = "Сеть тренирована";
            label1.ForeColor = Color.Green;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string message = "";
            for (int i = 0; i < 4; i++)
            {
                Vector output = network.Forward(X[i]);
                message += $"{X[i][0]} ^ {X[i][1]} = {output[0]:F4}\n";
            }
            richTextBox1.Text = message;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
