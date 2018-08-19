using System;
using System.Numerics;

namespace alphaRayTracer.Materials
{
    class Lambertian : Material
    {
        private readonly Random random = new Random();
        private Vector3 albedo;

        public Lambertian(Vector3 albedo)
        {
            this.albedo = albedo;
        }

        public override bool Scatter(Ray ray, Intersection intersection, out Vector3 attenuation, out Ray scatteredRay)
        {
            var target = intersection.Position + intersection.Normal + GetRandomPointInUnitSphere();
            scatteredRay = new Ray(intersection.Position, target - intersection.Position);
            attenuation = albedo;
            return true;
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
    }
}
