using alphaRayTracer.Intersectables;
using alphaRayTracer.Materials;
using System;
using System.Numerics;

namespace alphaRayTracer
{
    class Program
    {
        private readonly Random random = new Random();

        static void Main(string[] args)
        {
            var rayTracer = new Program();
            rayTracer.RenderScene();
        }

        private void RenderScene()
        {
            int imageWidth = 1920;
            int imageHeight = 1080;
            var image = new DirectBitmap(imageWidth, imageHeight);
            int samplesPerPixel = 100;
            var camera = new Camera((float)imageWidth / imageHeight);
            var world = GenerateSpheres();

            for (var y = 0; y < imageHeight; y++)
            {
                for (var x = 0; x < imageWidth; x++)
                {
                    var color = Vector3.Zero;
                    for (int i = 0; i < samplesPerPixel; i++)
                    {
                        float u = (float)(x + random.NextDouble()) / imageWidth;
                        float v = (float)(y + random.NextDouble()) / imageHeight;
                        Ray ray = camera.GetRay(u, v);

                        color += TraceRay(ray, world);
                    }
                    color /= samplesPerPixel;
                    color = CorrectGamma(color);

                    image.SetPixel(x, y, color);
                }
            }

            image.Bitmap.Save("Image.png");
        }

        private Vector3 TraceRay(Ray ray, IntersectableList world, int depth = 0)
        {
            if (world.Intersect(out Intersection intersection, ray))
            {
                if (depth > 50) return Vector3.Zero;

                if (intersection.Material.Scatter(ray, intersection, out Vector3 attenuation, out Ray scatteredRay))
                {
                    return attenuation * TraceRay(scatteredRay, world, depth + 1); // Element-wise multiplication
                }
            }

            return GetBackgroundColor(ray);
        }

        private Vector3 GetBackgroundColor(Ray ray)
        {
            Vector3 direction = ray.NormalizedDirection;
            float t = 0.5f * (direction.Y + 1f);
            return t * Vector3.One + (1f - t) * new Vector3(0.5f, 0.7f, 1);
        }

        private Vector3 CorrectGamma(Vector3 color)
        {
            color.X = (float)Math.Sqrt(color.X);
            color.Y = (float)Math.Sqrt(color.Y);
            color.Z = (float)Math.Sqrt(color.Z);
            return color;
        }

        private IntersectableList GenerateSpheres()
        {
            var list = new IntersectableList();
            list.Add(new Sphere(new Vector3(0, 0, 2), 0.5f, new Metal(new Vector3(0.831f, 0.686f, 0.216f))));
            list.Add(new Sphere(new Vector3(0.3f, -0.2f, 1.2f), 0.15f, new Metal(new Vector3(0.533f, 0.604f, 0.592f))));
            list.Add(new Sphere(new Vector3(-1.1f, 0, 2), 0.5f, new Lambertian(new Vector3(1f, 0.263f, 0.643f))));
            list.Add(new Sphere(new Vector3(0, 100.5f, 2), 100, new Lambertian(new Vector3(0.086f, 0.357f, 0.192f))));
            return list;
        }
    }
}
