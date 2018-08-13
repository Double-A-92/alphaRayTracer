namespace alphaRayTracer
{
    abstract class IntersectableObject
    {
        public abstract bool Intersect(out Intersection intersection, Ray ray, float tMin, float tMax);
    }
}
