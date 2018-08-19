using alphaRayTracer.Materials;
using System.Numerics;

namespace alphaRayTracer
{
    class Intersection
    {
        public float RayTParameter { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Normal { get; private set; }
        public Material Material { get; private set; }

        public Intersection(float t, Vector3 position, Vector3 normal, Material material)
        {
            RayTParameter = t;
            Position = position;
            Normal = normal;
            Material = material;
        }
    }
}
