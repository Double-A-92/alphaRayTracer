using System.Numerics;

namespace alphaRayTracer.Materials
{
    class Metal : Material
    {
        private Vector3 albedo;

        public Metal(Vector3 albedo)
        {
            this.albedo = albedo;
        }

        public override bool Scatter(Ray ray, Intersection intersection, out Vector3 attenuation, out Ray scatteredRay)
        {
            scatteredRay = Reflect(ray, intersection);
            attenuation = albedo;
            var rayOrthogonalToNormal = Vector3.Dot(scatteredRay.Direction, intersection.Normal) < 0; // TODO: Why?
            return !rayOrthogonalToNormal;
        }

        private Ray Reflect(Ray ray, Intersection intersection)
        {
            var rayDirection = ray.NormalizedDirection;
            var normal = intersection.Normal;
            var reflectedDirection = rayDirection - 2 * Vector3.Dot(rayDirection, normal) * normal;
            return new Ray(intersection.Position, reflectedDirection);
        }
    }
}
