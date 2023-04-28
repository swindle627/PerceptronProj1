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
        // Uses sigmoid function
        public static Person.Label Squash(double value, Person p)
        {
            double output = 1 / (1 + Math.Exp(-value));

            //Console.WriteLine(p.correctLabel + " / " + output);

            return (Person.Label)Math.Round(output);
        }
    }
}
