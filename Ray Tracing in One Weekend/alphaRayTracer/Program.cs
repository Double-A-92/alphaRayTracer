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
            int imageWidth = 1920;
            int imageHeight = 1080;
            var image = new DirectBitmap(imageWidth, imageHeight);

            Vector3 origin = Vector3.Zero;
            Vector3 upperLeftCorner = new Vector3(-imageWidth / 2f, -imageHeight / 2f, -imageHeight);
            Vector3 horizontal = imageWidth * Vector3.UnitX;
            Vector3 vertical = imageHeight * Vector3.UnitY;


            for (var x = 0; x < imageWidth; x++)
            {
                for (var y = 0; y < imageHeight; y++)
                {
                    float u = (float)x / imageWidth;
                    float v = (float)y / imageHeight;

                    Ray ray = new Ray(origin, upperLeftCorner + u * horizontal + v * vertical - origin);
                    var color = GetBackgroundColor(ray);
                    image.SetPixel(x, y, color);
                }
            }

            image.Bitmap.Save("Image.png");
        }

        static Vector3 GetBackgroundColor(Ray ray)
        {
            Vector3 direction = ray.NormalizedDirection;
            float t = 0.5f * (direction.Y + 1f);
            return t * Vector3.One + (1f - t) * new Vector3(0.5f, 0.7f, 1);
        }
    }
}
