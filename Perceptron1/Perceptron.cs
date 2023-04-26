using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron1
{
    internal class Perceptron
    {
        private InputImage image;
        private double[] weights;
        private Neuron[] inputLayer;
        private Neuron outputLayer;

        public Perceptron(InputImage image)
        {
            this.image = image;

            inputLayer = new Neuron[4];
            outputLayer = new Neuron();
            weights = new double[4];
            Random rand = new Random();

            for (int i = 0; i < 4; i++)
            {
                inputLayer[i] = new Neuron();
                inputLayer[i].input = image.pixels[i].value;
                weights[i] = rand.NextDouble() * 2 - 1;
            }
        }

        public bool ClassifyImage()
        {
            double value = outputLayer.Sum(inputLayer, weights);
            image.assignedLabel = Neuron.Squash(value, 0);

            if(image.assignedLabel == image.correctLabel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
