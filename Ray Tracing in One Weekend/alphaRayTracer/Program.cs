using System.Diagnostics;
using System.Numerics;

namespace alphaRayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderScene();
        }

        static void RenderScene()
        {
            int imageWidth = 1920;
            int imageHeight = 1080;
            var image = new DirectBitmap(imageWidth, imageHeight);

            Vector3 origin = Vector3.Zero;
            Vector3 upperLeftCorner = new Vector3(-imageWidth / 2f, -imageHeight / 2f, imageHeight);
            Vector3 horizontal = imageWidth * Vector3.UnitX;
            Vector3 vertical = imageHeight * Vector3.UnitY;


            for (var x = 0; x < imageWidth; x++)
            {
                for (var y = 0; y < imageHeight; y++)
                {
                    float u = (float)x / imageWidth;
                    float v = (float)y / imageHeight;

                    Ray ray = new Ray(origin, upperLeftCorner + u * horizontal + v * vertical - origin);
                    var color = TraceRay(ray);
                    image.SetPixel(x, y, color);
                }
            }

            image.Bitmap.Save("Image.png");
        }

        static Vector3 TraceRay(Ray ray)
        {
            if (HitsSphere(new Vector3(30, 100, 500), 50, ray))
                return Vector3.UnitX; //Red
            else
                return GetBackgroundColor(ray);
        }

        static bool HitsSphere(Vector3 center, float radius, Ray ray)
        {
            Vector3 oc = ray.Position - center;

            float a = Vector3.Dot(ray.Direction, ray.Direction);
            float b = 2f * Vector3.Dot(ray.Direction, oc);
            float c = Vector3.Dot(oc, oc) - radius * radius;

            float discriminant = b * b - 4 * a * c;
            return discriminant > 0;
        }

        static Vector3 GetBackgroundColor(Ray ray)
        {
            Vector3 direction = ray.NormalizedDirection;
            float t = 0.5f * (direction.Y + 1f);
            return t * Vector3.One + (1f - t) * new Vector3(0.5f, 0.7f, 1);
        }
    }
}
