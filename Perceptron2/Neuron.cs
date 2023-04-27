using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron2
{
    internal class Neuron
    {
        public double input = 0;

        // Calculates the weighted sum of inputs
        public double Sum(Neuron[] nodes, double[] weights)
        {
            double value = 0;

            for (int i = 0; i < nodes.Length; i++)
            {
                value = value + (nodes[i].input * weights[i]);
            }

            return value;
        }

        // Squashing function
        // Uses softmax function
        public static double[] Squash(double[] values)
        {
            double[] output = new double[values.Length];
            double sum = 0;

            for (int i = 0; i < values.Length; i++)
            {
                output[i] = Math.Exp(values[i]);
                sum = sum + output[i];
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = output[i] / sum;
            }

            return output;
        }
    }
}
