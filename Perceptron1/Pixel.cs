﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron1
{
    // The pixel class holds the value for a pixel
    // Used for inputs into the input layer
    // if bright, value = 1
    // else, value = -1
    internal class Pixel
    {
        public double value { get; set; }
    }
}
