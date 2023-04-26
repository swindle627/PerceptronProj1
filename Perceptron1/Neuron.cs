using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron1
{
    internal class Neuron
    {
        public double input = 0; 

        public double Sum(Neuron[] nodes, double[] weights)
        {
            double value = 0;

            for (int i = 0; i < nodes.Length; i++)
            {
                value = value + (nodes[i].input * weights[i]);
            }

            return value;
        }

        public static InputImage.Label Squash(double value, double threshold)
        {
            if(value >= threshold)
            {
                return InputImage.Label.bright;
            }
            else
            {
                return InputImage.Label.dark;
            }
        }
    }
}
