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
        public Label correctLabel { get; }
        public Label assignedLabel { get; set; }
        public Image<Rgba32> image { get; }
        public Pixel[] pixels { get; }

        public InputImage(string filePath, Label correctLabel)
        {
            image = Image.Load<Rgba32>(filePath);
            this.correctLabel = correctLabel;

            int width = image.Width;
            int height = image.Height;
            pixels = new Pixel[width * height];
            int counter = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Rgba32 pixel = image[x, y];
                    int grayscale = (int)(0.2989 * pixel.R + 0.5870 * pixel.G + 0.1140 * pixel.B);
                    pixels[counter] = new Pixel();
                    pixels[counter].value = (grayscale >= 128) ? 1 : -1;
                    counter++;
                }
            }
        }
    }
}
