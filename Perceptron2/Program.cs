using Perceptron2;
using static System.Net.Mime.MediaTypeNames;

// This program classifies Iris flowers into 3 classes: Iris Setosa, Iris Versicolour, or Iris Virginica
// The Iris dataset contains 150 samples and comes from https://archive.ics.uci.edu/ml/datasets/iris


// Training will cycle until the epoch count is reached
int epochCounter = 1, epochCount = 5;

// The below split is the 80/20 split that is common for training and testing
int trainingCount = 120; // Number of training irises (80%/20%)

// List of Iris objects
List<Iris> irisList = new List<Iris>();

// Loading in the iris dataset and using the data to create 150 instances of the iris object
using(StreamReader sr = new StreamReader("iris.data"))
{
    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        string[] values = line.Split(',');

        Iris iris = new Iris();
        iris.irisData[0] = double.Parse(values[0]);
        iris.irisData[1] = double.Parse(values[1]);
        iris.irisData[2] = double.Parse(values[2]);
        iris.irisData[3] = double.Parse(values[3]);
        iris.correctSpecies = values[4];

        irisList.Add(iris);
    }
}

Console.WriteLine(irisList[0].irisData[0]);
Console.WriteLine(irisList[0].irisData[1]);
Console.WriteLine(irisList[0].irisData[2]);
Console.WriteLine(irisList[0].irisData[3]);
Console.WriteLine(irisList[0].correctSpecies);
// Shuffling the elements in the list
Random rand = new Random();
irisList = irisList.OrderBy(x => rand.Next()).ToList();

// Initializing training and testing iris lists
List<Iris> trainingIris = new List<Iris>();
List<Iris> testingIris = new List<Iris>();

// Filling both iris lists
foreach (Iris iris in irisList)
{
    if (trainingCount > 0)
    {
        trainingIris.Add(iris);
        trainingCount--;
    }
    else
    {
        testingIris.Add(iris);
    }
}

// Initializing perceptron
Perceptron perceptron = new Perceptron(trainingIris);

// Running the training loop
// Training will cycle until the epochCount is reached
Console.WriteLine("---- Training Phase Beginning ----\n");
do
{
    Console.WriteLine("---- Beginning Epoch " + epochCounter + " ----");
    perceptron.TrainClassify();
    Console.WriteLine("Epoch " + epochCounter + " correct rate: " + perceptron.GetCorrectRate().ToString("P0"));
    Console.WriteLine();
    epochCounter++;
}
while (epochCount > epochCounter);

// Sending the testing irises to the perceptron
perceptron.SetIrisList(testingIris);

// Classifying test 
Console.WriteLine("---- Testing Phase Beginning ----\n");
perceptron.TestClassify();

