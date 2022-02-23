using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroLogic
{
    class Network
    {
        struct LayerT
        {
            public Vector x;
            public Vector z;
            public Vector df;
        }

        private int numberOfLayers;
        private Matrix[] weights;
        private LayerT[] L;
        private Vector[] deltas;

        public Network(int[] razmer)
        {
            numberOfLayers = razmer.Length - 1;
            weights = new Matrix[numberOfLayers];
            L = new LayerT[numberOfLayers];
            deltas = new Vector[numberOfLayers];

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < numberOfLayers; i++)
            {
                weights[i] = new Matrix(razmer[i + 1], razmer[i], rnd);

                L[i].x = new Vector(razmer[i]);
                L[i].z = new Vector(razmer[i + 1]);
                L[i].df = new Vector(razmer[i + 1]);

                deltas[i] = new Vector(razmer[i + 1]);
            }
        }

        public Vector Forward(Vector input)
        {
            for (int i = 0; i < numberOfLayers; i++)
            {
                if (i == 0)
                    for (int j = 0; j < input.lenth; j++)
                        L[i].x[j] = input[j];
                else
                    for (int j = 0; j < L[i - 1].z.lenth; j++)
                        L[i].x[j] = L[i - 1].z[j];

                for (int j = 0; j < weights[i].lines; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < weights[i].columns; k++)
                        sum += weights[i][j, k] * L[i].x[k];
                    L[i].z[j] = 1 / (1 + Math.Exp(-sum));
                    L[i].df[j] = L[i].z[j] * (1 - L[i].z[j]);
                }
            }
            return L[numberOfLayers - 1].z;
        }

        void Backward(Vector output, ref double error)
        {
            int last = numberOfLayers - 1;
            error = 0;
            for (int i = 0; i < output.lenth; i++)
            {
                deltas[last][i] = (L[last].z[i] - output[i]) * L[last].df[i];
                error += (L[last].z[i] - output[i]) * (L[last].z[i] - output[i]) / 2;
            }

            for (int k = last; k > 0; k--)
                for (int i = 0; i < weights[k].columns; i++)
                {
                    deltas[k-1][i] = 0;
                    for (int j = 0; j < weights[k].lines; j++)
                        deltas[k-1][i] += weights[k][j, i] * deltas[k][j];
                    deltas[k-1][i] *= L[k-1].df[i];
                }
        }
        void UpdateWeights(double speed)
        {
            for (int k = 0; k < numberOfLayers; k++)
                for (int i = 0; i < weights[k].lines; i++)
                    for (int j = 0; j < weights[k].columns; j++)
                        weights[k][i, j] -= speed * deltas[k][i] * L[k].x[j];
        }
        public void Train(Vector[] X, Vector[] Y, double speed, double epsilon, int generationLimit)
        {
            int generation = 1;
            double error;
            do
            {
                error = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    Forward(X[i]);
                    Backward(Y[i], ref error);
                    UpdateWeights(speed);
                }
                generation++;
            } while (generation <= generationLimit && error > epsilon);
        }
    }
}
