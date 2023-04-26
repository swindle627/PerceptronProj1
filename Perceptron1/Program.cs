
using Perceptron1;

InputImage bbdd = new InputImage("Images\\bbdd.png", InputImage.Label.bright);

foreach(Pixel p in bbdd.pixels)
{
    Console.WriteLine(p.value);
}