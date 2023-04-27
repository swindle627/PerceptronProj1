
using Perceptron1;

// Training will cycle until the epochCount is reached
int epochCounter = 1, epochCount = 5;

// The below split is close to the 80/20 split that is common for training and testing
int trainingCount = 12; // Number of training images (75%/25%)

// Adding sample images to image list
// For the sake of good training results i went with half bright images and half dark images
List<InputImage> images = new List<InputImage>()
{
    new InputImage("Images\\bbbb.png", InputImage.Label.bright), // 1
    new InputImage("Images\\bbbd.png", InputImage.Label.bright), // 2
    new InputImage("Images\\bbdd.png", InputImage.Label.bright), // 3
    new InputImage("Images\\bdbd.png", InputImage.Label.bright), // 4
    new InputImage("Images\\bddb.png", InputImage.Label.bright), // 5
    new InputImage("Images\\bddd.png", InputImage.Label.dark), // 6
    new InputImage("Images\\dbbb.png", InputImage.Label.bright), // 7
    new InputImage("Images\\dbdd.png", InputImage.Label.dark), // 8
    new InputImage("Images\\ddbb.png", InputImage.Label.bright), // 9
    new InputImage("Images\\ddbd.png", InputImage.Label.dark), // 10
    new InputImage("Images\\ddbd2.png", InputImage.Label.dark), // 11
    new InputImage("Images\\dddb.png", InputImage.Label.dark), // 12
    new InputImage("Images\\dddb2.png", InputImage.Label.dark), // 13
    new InputImage("Images\\dddd.png", InputImage.Label.dark), // 14
    new InputImage("Images\\dddd2.png", InputImage.Label.dark), // 15
    new InputImage("Images\\dbdb.png", InputImage.Label.bright), // 16
};

// Shuffling the elements in the list
Random rand = new Random();
images = images.OrderBy(x => rand.Next()).ToList();

// Initializing training and testing image lists
List<InputImage> trainingImages = new List<InputImage>();
List<InputImage> testingImages = new List<InputImage>();

// Filling both image lists
foreach(InputImage image in images)
{
    if(trainingCount > 0)
    {
        trainingImages.Add(image);
        trainingCount--;
    }
    else
    {
        testingImages.Add(image);
    }
}

// Initializing perceptron
Perceptron perceptron = new Perceptron(trainingImages);

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
while(epochCount > epochCounter);

// Sending the testing images to the perceptron
perceptron.SetImages(testingImages);

// Classifying test images
Console.WriteLine("---- Testing Phase Beginning ----\n");
perceptron.TestClassify();
