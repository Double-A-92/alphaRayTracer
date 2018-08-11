using System.Numerics;

namespace alphaRayTracer
{
    class Ray
    {
        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }

        public Ray(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
        }

        public Vector3 PointAt(float parameter)
        {
            return Position + parameter * Direction;
        }

        public Vector3 NormalizedDirection
        {
            get
            {
                return Vector3.Normalize(Direction);
            }
        }
    }
}
