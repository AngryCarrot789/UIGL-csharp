using System;

namespace UIGL.Utils {
    public static class Maths {
        public const float PI_HALF = 1.57079632679489f;
        public const float PI = 3.14159265358979f;
        public const float PI_DOUBLE = 6.28318530717958f;
        public const float PI_NEG_HALF = -1.57079632679489f;
        public const float PI_NEG = -3.14159265358979f;
        public const float PI_NEG_DOUBLE = -6.28318530717958f;

        public static double Clamp(double value, double min, double max) {
            return Math.Max(Math.Min(value, max), min);
        }

        public static double Clamp(int value, int min, int max) {
            return Math.Max(Math.Min(value, max), min);
        }

        public static int min(int a, int b) {
            return Math.Min(a, b);
        }

        public static int min(int a, int b, int c) {
            return Math.Min(a, Math.Min(b, c));
        }

        public static int min(int a, int b, int c, int d) {
            return Math.Min(a, Math.Min(b, Math.Min(c, d)));
        }

        public static int min(int a, int b, int c, int d, int e) {
            return Math.Min(a, Math.Min(b, Math.Min(c, Math.Min(d, e))));
        }

        public static int min(int a, int b, int c, int d, int e, int f) {
            return Math.Min(a, Math.Min(b, Math.Min(c, Math.Min(d, Math.Min(e, f)))));
        }

        public static int min(params int[] values) {
            if (values == null || values.Length == 0) {
                throw new ArgumentException("No arguments provided");
            }

            int min = values[0], val;
            for (int i = 1, len = values.Length; i < len; i++) {
                if ((val = values[i]) < min) {
                    min = val;
                }
            }

            return min;
        }

        public static int max(int a, int b) {
            return Math.Max(a, b);
        }

        public static int max(int a, int b, int c) {
            return Math.Max(a, Math.Max(b, c));
        }

        public static int max(int a, int b, int c, int d) {
            return Math.Max(a, Math.Max(b, Math.Max(c, d)));
        }

        public static int max(int a, int b, int c, int d, int e) {
            return Math.Max(a, Math.Max(b, Math.Max(c, Math.Max(d, e))));
        }

        public static int max(int a, int b, int c, int d, int e, int f) {
            return Math.Max(a, Math.Max(b, Math.Max(c, Math.Max(d, Math.Max(e, f)))));
        }

        public static int max(params int[] values) {
            if (values == null || values.Length == 0) {
                throw new ArgumentException("No arguments provided");
            }

            int max = values[0], val;
            for (int i = 1, len = values.Length; i < len; i++) {
                if ((val = values[i]) > max) {
                    max = val;
                }
            }

            return max;
        }

        // float

        public static float min(float a, float b) {
            return Math.Min(a, b);
        }

        public static float min(float a, float b, float c) {
            return Math.Min(a, Math.Min(b, c));
        }

        public static float min(float a, float b, float c, float d) {
            return Math.Min(a, Math.Min(b, Math.Min(c, d)));
        }

        public static float min(float a, float b, float c, float d, float e) {
            return Math.Min(a, Math.Min(b, Math.Min(c, Math.Min(d, e))));
        }

        public static float min(float a, float b, float c, float d, float e, float f) {
            return Math.Min(a, Math.Min(b, Math.Min(c, Math.Min(d, Math.Min(e, f)))));
        }

        public static float min(params float[] values) {
            if (values == null || values.Length == 0) {
                throw new ArgumentException("No arguments provided");
            }

            float min = values[0], val;
            for (int i = 1, len = values.Length; i < len; i++) {
                if ((val = values[i]) < min) {
                    min = val;
                }
            }

            return min;
        }

        public static float max(float a, float b) {
            return Math.Max(a, b);
        }

        public static float max(float a, float b, float c) {
            return Math.Max(a, Math.Max(b, c));
        }

        public static float max(float a, float b, float c, float d) {
            return Math.Max(a, Math.Max(b, Math.Max(c, d)));
        }

        public static float max(float a, float b, float c, float d, float e) {
            return Math.Max(a, Math.Max(b, Math.Max(c, Math.Max(d, e))));
        }

        public static float max(float a, float b, float c, float d, float e, float f) {
            return Math.Max(a, Math.Max(b, Math.Max(c, Math.Max(d, Math.Max(e, f)))));
        }

        public static float max(params float[] values) {
            if (values == null || values.Length == 0) {
                throw new ArgumentException("No arguments provided");
            }

            float max = values[0], val;
            for (int i = 1, len = values.Length; i < len; i++) {
                if ((val = values[i]) > max) {
                    max = val;
                }
            }

            return max;
        }

        public static double min(double a, double b) {
            return Math.Min(a, b);
        }

        public static double min(double a, double b, double c) {
            return Math.Min(a, Math.Min(b, c));
        }

        public static double min(double a, double b, double c, double d) {
            return Math.Min(a, Math.Min(b, Math.Min(c, d)));
        }

        public static double min(double a, double b, double c, double d, double e) {
            return Math.Min(a, Math.Min(b, Math.Min(c, Math.Min(d, e))));
        }

        public static double min(double a, double b, double c, double d, double e, double f) {
            return Math.Min(a, Math.Min(b, Math.Min(c, Math.Min(d, Math.Min(e, f)))));
        }

        public static double min(params double[] values) {
            if (values == null || values.Length == 0) {
                throw new ArgumentException("No arguments provided");
            }

            double min = values[0], val;
            for (int i = 1, len = values.Length; i < len; i++) {
                if ((val = values[i]) < min) {
                    min = val;
                }
            }

            return min;
        }

        public static double max(double a, double b) {
            return Math.Max(a, b);
        }

        public static double max(double a, double b, double c) {
            return Math.Max(a, Math.Max(b, c));
        }

        public static double max(double a, double b, double c, double d) {
            return Math.Max(a, Math.Max(b, Math.Max(c, d)));
        }

        public static double max(double a, double b, double c, double d, double e) {
            return Math.Max(a, Math.Max(b, Math.Max(c, Math.Max(d, e))));
        }

        public static double max(double a, double b, double c, double d, double e, double f) {
            return Math.Max(a, Math.Max(b, Math.Max(c, Math.Max(d, Math.Max(e, f)))));
        }

        public static double Max(params double[] values) {
            if (values == null || values.Length == 0) {
                throw new ArgumentException("No arguments provided");
            }

            double max = values[0];
            for (int i = 1, len = values.Length; i < len; i++) {
                double val = values[i];
                if (val > max) {
                    max = val;
                }
            }

            return max;
        }

        public static double Map(double value, double inMin, double inMax, double outMin, double outMax) {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
    }
}