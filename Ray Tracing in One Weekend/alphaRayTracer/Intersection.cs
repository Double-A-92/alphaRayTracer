using System.Numerics;

namespace alphaRayTracer
{
    class Intersection
    {
        public float RayTParameter { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Normal { get; private set; }

        public Intersection(float t, Vector3 position, Vector3 normal)
        {
            RayTParameter = t;
            Position = position;
            Normal = normal;
        }
    }
}
