using System.Numerics;

namespace alphaRayTracer
{
    class Camera
    {
        private Vector3 origin = Vector3.Zero;
        private Vector3 upperLeftCorner;
        private Vector3 viewWidth, viewHeight;

        public Camera(float aspectRatio)
        {
            viewWidth = aspectRatio * Vector3.UnitX;
            viewHeight = Vector3.UnitY;
            upperLeftCorner = -0.5f * (viewWidth + viewHeight) + Vector3.UnitZ;
        }

        public Ray getRay(float u, float v)
        {
            return new Ray(origin, upperLeftCorner + u*viewWidth + v*viewHeight - origin);
        }
    }
}
