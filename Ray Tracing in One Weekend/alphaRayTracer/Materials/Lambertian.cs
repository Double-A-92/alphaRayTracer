using System;
using System.Numerics;

namespace alphaRayTracer.Materials
{
    class Lambertian : Material
    {
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
    }
}
