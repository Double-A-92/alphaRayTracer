using System;
using System.Drawing;
using System.Numerics;

namespace alphaRayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateTestImage();
        }

        static void CreateTestImage()
        {
            int imageWidth = 2000;
            int imageHeight = 1000;
            var image = new DirectBitmap(imageWidth, imageHeight);

            for (var x = 0; x < imageWidth; x++)
            {
                for (var y = 0; y < imageHeight; y++)
                {
                    var color = new Vector3((float)x / imageWidth, (float)y / imageHeight, 1f/3);
                    image.SetPixel(x, y, color);
                }
            }
            image.Bitmap.Save("Image.png");
        }
    }
}
