using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron2
{
    internal class Iris
    {
        // Setting up classes
        public Dictionary<string, double> classes = new Dictionary<string, double>()
        {
            { "Iris-setosa", 0 },
            { "Iris-versicolor", 1 },
            { "Iris-virginica", 2 }
        };

        public double[] irisData = new double[4];
        public string correctSpecies = "";
    }
}
