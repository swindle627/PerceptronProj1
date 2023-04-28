using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron2
{
    internal class Person
    {
        public enum Label { Diabetes = 1, No_Diabetes = 0 };
        public Label correctLabel;
        public Label assignedLabel;
        public double[] data = new double[8];
    }
}
