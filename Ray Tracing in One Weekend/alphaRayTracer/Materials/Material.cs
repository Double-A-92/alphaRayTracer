using System.Numerics;

namespace alphaRayTracer.Materials
{
    abstract class Material
    {
        public abstract bool Scatter(Ray ray, Intersection intersection, out Vector3 attenuation, out Ray scatteredRay); 
    }
}
