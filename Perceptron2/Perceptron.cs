using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Perceptron2
{
    internal class Perceptron
    {
        private double correctRate;
        private double[] weights;
        private Neuron[] inputLayer;
        private Neuron outputLayer;
        private double learningRate = 0.1;
        private List<Iris> irisList;
        //private double 

        public Perceptron(List<Iris> irisList)
        {
            this.irisList = irisList;
            inputLayer = new Neuron[5];
            outputLayer = new Neuron();
            weights = new double[5];
            Random rand = new Random();

            // sets initial weights to a random value between -1 and 1
            for (int i = 0; i < 5; i++)
            {
                weights[i] = rand.NextDouble() * 2 - 1;
            }
        }
        // Used to enter test set after training
        public void SetIrisList(List<Iris> irisList)
        {
            this.irisList = irisList;
        }
        // Returns the correct classification rate
        public double GetCorrectRate()
        {
            return correctRate / irisList.Count;
        }
        // Used to train the perceptron be recaluclating weights
        public void TrainClassify()
        {
            correctRate = 0;

            foreach (Iris iris in irisList)
            {
                SetInputs(iris);
                double value = outputLayer.Sum(inputLayer, weights);
                Console.WriteLine(value);
                double[] output = Neuron.Squash(new double[] { value });
                //Console.WriteLine(output[0]);

                if(output[0] != iris.classes[iris.correctSpecies])
                {
                    // Calculating error
                    double diff = iris.classes[iris.correctSpecies] - output[0];

                    for (int i = 0; i < 5; i++)
                    {
                        // Calculating the error
                        double error = learningRate * diff * inputLayer[i].input;
                        // Adjusting the weight according to the error
                        weights[i] += error;
                    }
                }
                else
                {
                    correctRate++;
                }
                
            }
        }
        // Used to run the test set through the perceptron
        // When testing weights won't be adjusted
        public void TestClassify()
        {
            foreach (Iris iris in irisList)
            {
                SetInputs(iris);
                double value = outputLayer.Sum(inputLayer, weights);
                double[] output = Neuron.Squash(new double[] { value });

                Console.WriteLine("Image assigned label: " + iris.classes.FirstOrDefault(x => x.Value == output[0]).Key);
                Console.WriteLine("Image correct label: " + iris.correctSpecies);
                Console.WriteLine();
            }
        }
        // Used to length/width values into the input layer
        private void SetInputs(Iris iris)
        {
            for (int i = 0; i < 4; i++)
            {
                inputLayer[i] = new Neuron();
                inputLayer[i].input = iris.irisData[i];
            }

            inputLayer[4] = new Neuron();
            inputLayer[4].input = 1;
        }
    }
}
