﻿using alphaRayTracer.Intersectables;
using System;
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

            var world = generateRandomSpheres();

            for (var x = 0; x < imageWidth; x++)
            {
                for (var y = 0; y < imageHeight; y++)
                {
                    // TODO: Understand why this "scaling" exists. Seems unnecessary.
                    float u = (float)x / imageWidth;
                    float v = (float)y / imageHeight;

                    Ray ray = new Ray(origin, upperLeftCorner + u * horizontal + v * vertical - origin);
                    var color = TraceRay(ray, world);
                    image.SetPixel(x, y, color);
                }
            }

            image.Bitmap.Save("Image.png");
        }

        static Vector3 TraceRay(Ray ray, IntersectableList world)
        {
            if (world.Intersect(out Intersection sphereIntersection, ray))
            {
                var normalVector = sphereIntersection.Normal;
                return 0.5f * new Vector3(normalVector.X + 1, -normalVector.Y + 1, -normalVector.Z + 1); ; // Normal shade
            }
            else
            {
                return GetBackgroundColor(ray);
            }
        }

        static Vector3 GetBackgroundColor(Ray ray)
        {
            Vector3 direction = ray.NormalizedDirection;
            float t = 0.5f * (direction.Y + 1f);
            return t * Vector3.One + (1f - t) * new Vector3(0.5f, 0.7f, 1);
        }

        static IntersectableList generateRandomSpheres()
        {
            var list = new IntersectableList();

            int imageWidth = 1920;
            int imageHeight = 1080;

            var random = new Random();
            for (int i = 0; i < 20; i++)
            {
                var x = random.Next(-imageWidth / 2, imageWidth / 2);
                var y = random.Next(-imageHeight / 2, imageHeight / 2);
                var r = random.Next(50, 200);
                var z = random.Next(imageHeight, 2 * imageHeight);
                list.Add(new Sphere(new Vector3(x, y, z), r));
            }

            return list;
        }
    }
}
