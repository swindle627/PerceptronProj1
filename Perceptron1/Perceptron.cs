using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron1
{
    internal class Perceptron
    {
        private double correctRate;
        private List<InputImage> images;
        private double[] weights;
        private Neuron[] inputLayer;
        private Neuron outputLayer;
        private double learningRate = 0.1;

        public Perceptron(List<InputImage> images)
        {
            this.images = images;
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
        // Used to enter test images after training
        public void SetImages(List<InputImage> images)
        {
            this.images = images;
        }
        // Returns the correct classification rate
        public double GetCorrectRate()
        {
            return correctRate / images.Count;
        }
        // Used to train the perceptron be recaluclating weights
        public void TrainClassify()
        {
            correctRate = 0;

            foreach(InputImage image in images)
            {
                SetInputs(image);
                double value = outputLayer.Sum(inputLayer, weights);
                image.assignedLabel = Neuron.Squash(value, 0);

                Console.WriteLine("Image assigned label: " + image.assignedLabel.ToString());
                Console.WriteLine("Image correct label: " + image.correctLabel.ToString());

                // If the image is classified incorrectly the weights will be readjusted
                if (image.assignedLabel != image.correctLabel)
                {
                    Console.WriteLine();
                    double diff = image.correctLabel - image.assignedLabel;

                    for (int i = 0; i < 5; i++)
                    {
                        // Calculating the error
                        double error = learningRate * diff * inputLayer[i].input;

                        Console.WriteLine("Original weight " + i + ": " + weights[i].ToString("F2"));
                        // Adjusting the weight according to the error
                        weights[i] += error;
                        Console.WriteLine("New weight " + i + ": " + weights[i].ToString("F2"));
                    }
                }
                else
                {
                    correctRate += 1;
                }

                Console.WriteLine();
            }
        }
        // Used to run test images through the perceptron
        // When testing weights won't be adjusted
        public void TestClassify()
        {
            foreach (InputImage image in images)
            {
                SetInputs(image);
                double value = outputLayer.Sum(inputLayer, weights);
                image.assignedLabel = Neuron.Squash(value, 0);

                Console.WriteLine("Image assigned label: " + image.assignedLabel.ToString());
                Console.WriteLine("Image correct label: " + image.correctLabel.ToString());
                Console.WriteLine();
            }
        }
        // Used to enter pixel values into the input layer neurons and sets up the bias neuron
        private void SetInputs(InputImage image)
        {
            for (int i = 0; i < 4; i++)
            {
                inputLayer[i] = new Neuron();
                inputLayer[i].input = image.pixels[i].value;
            }

            inputLayer[4] = new Neuron();
            inputLayer[4].input = 1;
        }
    }
}
