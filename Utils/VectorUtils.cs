using System;
using OpenTK.Mathematics;

namespace UIGL.Utils {
    public static class VectorUtils {
        public static double Clamp(double value, double min, double max) {
            return Math.Max(Math.Min(value, max), min);
        }

        public static Vector2d Clamp(this Vector2d vector, Vector2d min, Vector2d max) {
            return new Vector2d(Clamp(vector.X, min.X, max.X), Clamp(vector.Y, min.Y, max.Y));
        }
    }
}