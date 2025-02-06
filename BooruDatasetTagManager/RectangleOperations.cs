using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public static class RectangleOperations
    {
        public static MoondreamRect FindLargestRectangle(MoondreamRect initial, List<MoondreamRect> subtractors)
        {
            List<MoondreamRect> currentCandidates = FindAllRectangles(initial, subtractors);

            if (currentCandidates.Count == 0)
                return null;

            MoondreamRect maxRect = currentCandidates[0];
            float maxArea = maxRect.Area;

            foreach (var rect in currentCandidates)
            {
                if (rect.Area > maxArea)
                {
                    maxArea = rect.Area;
                    maxRect = rect;
                }
            }

            return maxRect;
        }

        public static List<MoondreamRect> FindAllRectangles(MoondreamRect initial, List<MoondreamRect> subtractors)
        {
            List<MoondreamRect> currentCandidates = new List<MoondreamRect> { initial };

            foreach (var sub in subtractors)
            {
                List<MoondreamRect> newCandidates = new List<MoondreamRect>();

                foreach (var candidate in currentCandidates)
                {
                    if (!Intersects(candidate, sub))
                    {
                        newCandidates.Add(candidate);
                    }
                    else
                    {
                        var splitParts = Split(candidate, sub);
                        newCandidates.AddRange(splitParts);
                    }
                }

                currentCandidates = newCandidates;
            }

            if (currentCandidates.Count == 0)
                return new List<MoondreamRect>();

            return currentCandidates;
        }

        public static bool Intersects(MoondreamRect a, MoondreamRect b)
        {
            return !(a.x_max <= b.x_min || a.x_min >= b.x_max || a.y_max <= b.y_min || a.y_min >= b.y_max);
        }

        public static List<MoondreamRect> Split(MoondreamRect a, MoondreamRect b)
        {
            List<MoondreamRect> parts = new List<MoondreamRect>();

            if (!Intersects(a, b))
            {
                parts.Add(a);
                return parts;
            }

            float intersectX1 = Math.Max(a.x_min, b.x_min);
            float intersectY1 = Math.Max(a.y_min, b.y_min);
            float intersectX2 = Math.Min(a.x_max, b.x_max);
            float intersectY2 = Math.Min(a.y_max, b.y_max);

            // Left part
            if (a.x_min < intersectX1)
            {
                parts.Add(new MoondreamRect(a.x_min, a.y_min, intersectX1, a.y_max));
            }

            // Right part
            if (a.x_max > intersectX2)
            {
                parts.Add(new MoondreamRect(intersectX2, a.y_min, a.x_max, a.y_max));
            }

            // Top part
            if (a.y_min < intersectY1)
            {
                parts.Add(new MoondreamRect(a.x_min, a.y_min, a.x_max, intersectY1));

            }

            // Buttom part
            if (a.y_max > intersectY2)
            {
                parts.Add(new MoondreamRect(a.x_min, intersectY2, a.x_max, a.y_max));
            }

            return parts;
        }

        public static MoondreamRect Join(MoondreamRect a, MoondreamRect b)
        {
            return new MoondreamRect(Math.Min(a.x_min, b.x_min), Math.Min(a.y_min, b.y_min), Math.Max(a.x_max, b.x_max), Math.Max(a.y_max, b.y_max));
        }
    }
}
