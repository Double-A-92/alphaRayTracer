using alphaRayTracer.Intersectables;
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
            int imageWidth = 1280;
            int imageHeight = 720;
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

        private Vector3 TraceRay(Ray ray, IntersectableList world)
        {
            if (world.Intersect(out Intersection intersection, ray))
            {
                var target = intersection.Position + intersection.Normal + GetRandomPointInUnitSphere();
                return 0.5f * TraceRay(new Ray(intersection.Position, target - intersection.Position), world); // Diffuse

                //var normalVector = intersection.Normal;
                //return 0.5f * new Vector3(normalVector.X + 1, -normalVector.Y + 1, -normalVector.Z + 1); ; // Normal shade
            }
            else
            {
                return GetBackgroundColor(ray);
            }
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

        private Vector3 GetRandomPointInUnitSphere()
        {
            Vector3 point;

            do
            {
                point = 2 * new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()) - Vector3.One;
            } while (point.LengthSquared() >= 1f);

            return point;
        }

        private IntersectableList GenerateSpheres()
        {
            var list = new IntersectableList();
            list.Add(new Sphere(new Vector3(0, 0, 2), 0.5f));
            list.Add(new Sphere(new Vector3(0.3f, -0.2f, 1.2f), 0.15f));
            list.Add(new Sphere(new Vector3(0, 100.5f, 2), 100));
            return list;
        }
    }
}
