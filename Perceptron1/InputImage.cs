using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace Perceptron1
{
    internal class InputImage
    {
        public enum Label { bright = 1, dark = -1 };
        public Label correctLabel { get; } // The correct label of an image
        public Label assignedLabel { get; set; } // The assigned label of an image given by the perceptron
        public Image<Rgba32> image { get; } // The image 
        public Pixel[] pixels { get; } // An array containing the values (1 or -1) of the 4 pixels in the image

        public InputImage(string filePath, Label correctLabel)
        {
            image = Image.Load<Rgba32>(filePath); // Loading image from file
            this.correctLabel = correctLabel; // Assigning the correct label

            int width = image.Width;
            int height = image.Height;
            pixels = new Pixel[width * height];
            int counter = 0;

            // Adding the 4 pixel images to the pixel array
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Rgba32 pixel = image[x, y];
                    int grayscale = (int)(0.2989 * pixel.R + 0.5870 * pixel.G + 0.1140 * pixel.B); // Grayscale calculation
                    pixels[counter] = new Pixel();
                    pixels[counter].value = (grayscale >= 128) ? 1 : -1;
                    counter++;
                }
            }
        }
    }
}
