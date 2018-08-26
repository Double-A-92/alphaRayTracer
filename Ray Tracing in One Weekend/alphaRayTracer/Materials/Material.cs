using System;
using System.Numerics;

namespace alphaRayTracer.Materials
{
    abstract class Material
    {
        private readonly Random random = new Random();

        public abstract bool Scatter(Ray ray, Intersection intersection, out Vector3 attenuation, out Ray scatteredRay);

        protected Vector3 GetRandomPointInUnitSphere()
        {
            Vector3 point;

            do
            {
                point = 2f * new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()) - Vector3.One;
            } while (point.LengthSquared() >= 1f);

            return point;
        }
    }


}
