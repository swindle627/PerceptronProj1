using Perceptron2;
using static System.Net.Mime.MediaTypeNames;

// This program classifies people based into 2 classes based on whether they have diabetes or not
// Dataset from: https://www.kaggle.com/datasets/uciml/pima-indians-diabetes-database
// Classes are: Diabetes and No_Diabetes


// Training will cycle until the correctRate is greater than or equal to the goalCorrectRate for 100 consecutive cycles
double goalCorrectRate = 0.77;
int epochCounter = 1, consecCorrect = 100;

// The below split is the 80/20 split that is common for training and testing
int diabeteTrainingCount = 215; // 80% of diabetes classified
int noDiabeteTrainingCount = 400; // 80% of no diabetes classified

// List of person objects
List<Person> people = new List<Person>();

// Loading in the iris dataset and using the data to create 150 instances of the person object
using (StreamReader sr = new StreamReader("Dataset.csv"))
{
    // Initialize variables to store min and max values of each attribute
    double[] minVals = new double[8] { double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue };
    double[] maxVals = new double[8] { double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue, double.MinValue };


    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        string[] values = line.Split(',');

        Person person = new Person();
        person.data[0] = double.Parse(values[0]);
        person.data[1] = double.Parse(values[1]);
        person.data[2] = double.Parse(values[2]);
        person.data[3] = double.Parse(values[3]);
        person.data[4] = double.Parse(values[4]);
        person.data[5] = double.Parse(values[5]);
        person.data[6] = double.Parse(values[6]);
        person.data[7] = double.Parse(values[7]);
        person.correctLabel = (Person.Label)int.Parse(values[8]);

        // Update min and max values
        for (int i = 0; i < person.data.Length; i++)
        {
            if (person.data[i] < minVals[i])
            {
                minVals[i] = person.data[i];
            }
            if (person.data[i] > maxVals[i])
            {
                maxVals[i] = person.data[i];
            }
        }

        people.Add(person);
    }

    // Normalizing the data for increased accuracy in classification
    foreach (Person person in people)
    {
        for (int i = 0; i < person.data.Length; i++)
        {
            person.data[i] = (person.data[i] - minVals[i]) / (maxVals[i] - minVals[i]);
        }
    }
}

// Shuffling the elements in the list
Random rand = new Random();
people = people.OrderBy(x => rand.Next()).ToList();

// Initializing training and testing sets
List<Person> trainingSet = new List<Person>();
List<Person> testingSet = new List<Person>();

// Filling training and testing lists
foreach (Person p in people)
{
    if(p.correctLabel == Person.Label.Diabetes && diabeteTrainingCount > 0)
    {
        trainingSet.Add(p);
        diabeteTrainingCount--;
    }
    else if(p.correctLabel == Person.Label.No_Diabetes && noDiabeteTrainingCount > 0)
    {
        trainingSet.Add(p);
        noDiabeteTrainingCount--;
    }
    else
    {
        testingSet.Add(p);
    }
}

// Initializing perceptron
Perceptron perceptron = new Perceptron(trainingSet);

// The initial value of the perceptron's learning rate
double learningRate = 0.1;

// Running the training loop
// Training will cycle until the epochCount is reached
Console.WriteLine("---- Training Phase Beginning ----\n");
do
{
    perceptron.TrainClassify(learningRate);
    double correctRate = perceptron.GetCorrectRate();
    Console.WriteLine("Epoch " + epochCounter + " correct rate: " + correctRate.ToString("P0"));
    epochCounter++;

    // Decreasing learningRate as accuracy gets higher
    if(correctRate >= 0.77)
    {
        learningRate = 0.00001;
    }
    else if (correctRate >= 0.75)
    {
        learningRate = 0.0001;
    }
    else if (correctRate >= 0.73)
    {
        learningRate = 0.001;
    }
    else if(correctRate >= 0.7)
    {
        learningRate = 0.01;
    }
    else
    {
        learningRate = 0.1;
    }

    if(correctRate >= goalCorrectRate)
    {
        consecCorrect--;
    }
    else
    {
        consecCorrect = 100;
    }
}
while (goalCorrectRate > perceptron.GetCorrectRate() || consecCorrect > 0);

// Sending the testing set to the perceptron
perceptron.SetIrisList(testingSet);

// Classifying testing set
Console.WriteLine();
Console.WriteLine("---- Testing Phase Beginning ----\n");
perceptron.TestClassify();
Console.WriteLine("Testing correct rate: " + perceptron.GetCorrectRate().ToString("P0"));