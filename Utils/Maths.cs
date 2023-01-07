using System;

namespace UIGL.Utils {
    public static class Maths {
        public const float PI_HALF       =  1.57079632679489f;
        public const float PI            =  3.14159265358979f;
        public const float PI_DOUBLE     =  6.28318530717958f;
        public const float PI_NEG_HALF   = -1.57079632679489f;
        public const float PI_NEG        = -3.14159265358979f;
        public const float PI_NEG_DOUBLE = -6.28318530717958f;

        public static double Clamp(double value, double min, double max) {
            return Math.Max(Math.Min(value, max), min);
        }

        public static double Clamp(int value, int min, int max) {
            return Math.Max(Math.Min(value, max), min);
        }
    }
}