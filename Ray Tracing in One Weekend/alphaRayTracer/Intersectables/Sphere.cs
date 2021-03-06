﻿using alphaRayTracer.Materials;
using System;
using System.Numerics;

namespace alphaRayTracer
{
    class Sphere : IntersectableObject
    {
        private readonly Vector3 centerPosition;
        private readonly float radius;
        private readonly Material material;

        public Sphere(Vector3 centerPosition, float radius, Material material)
        {
            this.centerPosition = centerPosition;
            this.radius = radius;
            this.material = material;
        }

        public override bool Intersect(out Intersection intersection, Ray ray, float tMin = 0.001f, float tMax = float.MaxValue)
        {
            Vector3 oc = ray.Position - centerPosition;

            var a = Vector3.Dot(ray.Direction, ray.Direction);
            var b = Vector3.Dot(oc, ray.Direction); // 2f * Vector3.Dot(oc, ray.Direction);
            var c = Vector3.Dot(oc, oc) - radius * radius;

            var discriminant = b * b - a * c; // b * b - 4 * a * c;
            if (discriminant > 0)
            {
                float t1 = (-b - (float)Math.Sqrt(discriminant)) / a; // (-b - (float)Math.Sqrt(discriminant)) / (2f * a);
                if (t1 < tMax && t1 > tMin)
                {
                    intersection = PrepareIntersectionData(ray, t1);
                    return true;
                }

                float t2 = (-b + (float)Math.Sqrt(discriminant)) / a; // (-b + (float)Math.Sqrt(discriminant)) / (2f * a);
                if (t2 < tMax && t2 > tMin)
                {
                    intersection = PrepareIntersectionData(ray, t2);
                    return true;
                }
            }

            intersection = null;
            return false;
        }

        private Intersection PrepareIntersectionData(Ray ray, float t)
        {
            var intersectionPosition = ray.PointAt((float)t);
            return new Intersection(t, intersectionPosition, (intersectionPosition - centerPosition) / radius, material);
        }
    }
}
