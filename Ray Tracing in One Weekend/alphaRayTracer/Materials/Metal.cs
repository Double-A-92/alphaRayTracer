using System;
using System.Numerics;

namespace alphaRayTracer.Materials
{
    class Metal : Material
    {
        private Vector3 albedo;
        private readonly float roughness;

        public Metal(Vector3 albedo, float roughness = 0)
        {
            this.albedo = albedo;
            this.roughness = Math.Max(Math.Min(roughness, 1), 0);
        }

        public override bool Scatter(Ray ray, Intersection intersection, out Vector3 attenuation, out Ray scatteredRay)
        {
            scatteredRay = Reflect(ray, intersection);
            attenuation = albedo;
            var rayAbsorbed = Vector3.Dot(scatteredRay.Direction, intersection.Normal) <= 0; // "reflected" into material
            return !rayAbsorbed;
        }

        private Ray Reflect(Ray ray, Intersection intersection)
        {
            var rayDirection = ray.NormalizedDirection;
            var normal = intersection.Normal;
            var reflectedDirection = rayDirection - 2 * Vector3.Dot(rayDirection, normal) * normal;
            return new Ray(intersection.Position, reflectedDirection + GetRoughnessDeviation(reflectedDirection, normal));
        }

        private Vector3 GetRoughnessDeviation(Vector3 reflectedDirection, Vector3 normal)
        {
            var radius = roughness;

            var minimalHeightAboveSurface = Vector3.Dot(reflectedDirection, normal);
            if (minimalHeightAboveSurface < 1)
            {
                radius = minimalHeightAboveSurface * roughness;
            }

            return radius * GetRandomPointInUnitSphere();
        }
    }
}
