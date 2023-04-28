using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        //private double learningRate;
        private List<Person> people;

        public Perceptron(List<Person> people)
        {
            this.people = people;
            inputLayer = new Neuron[9]; // 8 inputs + 1 bias node
            outputLayer = new Neuron();
            weights = new double[9];
            Random rand = new Random();

            // sets initial weights to a random value between -1 and 1
            for (int i = 0; i < 9; i++)
            {
                weights[i] = rand.NextDouble() * 2 - 1;
            }
        }
        // Used to enter test set after training
        public void SetIrisList(List<Person> people)
        {
            this.people = people;
        }
        // Returns the correct classification rate
        public double GetCorrectRate()
        {
            return correctRate / people.Count;
        }
        // Used to train the perceptron be recaluclating weights
        public void TrainClassify(double learningRate)
        {
            correctRate = 0;

            foreach (Person p in people)
            {
                SetInputs(p);
                double value = outputLayer.Sum(inputLayer, weights);
                p.assignedLabel = Neuron.Squash(value, p);

                if (p.assignedLabel != p.correctLabel)
                {
                    // Calculating error
                    double diff = p.correctLabel - p.assignedLabel;

                    for (int i = 0; i < 9; i++)
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
            correctRate = 0;

            foreach (Person p in people)
            {
                SetInputs(p);
                double value = outputLayer.Sum(inputLayer, weights);
                p.assignedLabel = Neuron.Squash(value, p);

                if(p.assignedLabel == p.correctLabel)
                {
                    correctRate++;
                }

                Console.WriteLine("Image assigned label: " + p.assignedLabel);
                Console.WriteLine("Image correct label: " + p.correctLabel);
                Console.WriteLine();
            }
        }
        // Used to length/width values into the input layer
        private void SetInputs(Person p)
        {
            for (int i = 0; i < 8; i++)
            {
                inputLayer[i] = new Neuron();
                inputLayer[i].input = p.data[i];
            }

            inputLayer[8] = new Neuron();
            inputLayer[8].input = 1;
        }
    }
}
