using System.Collections.Generic;

namespace alphaRayTracer.Intersectables
{
    class IntersectableList : IntersectableObject
    {
        private readonly List<IntersectableObject> intersectables = new List<IntersectableObject>();

        public override bool Intersect(out Intersection intersection, Ray ray, float tMin = 0, float tMax = float.MaxValue)
        {
            bool anythingHit = false;
            intersection = null;

            float tClosestSoFar = tMax;
            foreach(var intersectable in intersectables)
            {
                if (intersectable.Intersect(out Intersection tempIntersection, ray, tMin, tClosestSoFar))
                {
                    anythingHit = true;
                    tClosestSoFar = tempIntersection.RayTParameter;
                    intersection = tempIntersection;
                }
            }

            return anythingHit;
        }

        public void Add(IntersectableObject intersectableObject)
        {
            intersectables.Add(intersectableObject);
        }
    }
}
