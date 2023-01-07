using System;
using System.Runtime.CompilerServices;

namespace UIGL.Utils {
    public static class FMath {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float a) => (float) Math.Sin(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sinh(float a) => (float) Math.Sinh(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float a) => (float) Math.Sqrt(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float a) => (float) Math.Tan(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tanh(float a) => (float) Math.Tanh(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, float y) => (float) Math.Pow(x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float a) => (float) Math.Acos(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Asin(float a) => (float) Math.Asin(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float a) => (float) Math.Atan(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float a, float b) => (float) Math.Atan2(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float a) => (float) Math.Cos(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cosh(float a) => (float) Math.Cosh(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Square(float a) => a * a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float a) => (a <= 0.0F) ? 0.0F - a : a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(float a, int b) {
            return (float) Math.Round(a, b);
        }

        public static float CopySign(float x, float y) {
            int xbits = FloatBitsToI32(x);
            int ybits = FloatBitsToI32(y);
            return (xbits ^ ybits) < 0 ? I32ToFloatBits(xbits ^ int.MinValue) : x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int FloatBitsToI32(float value) {
            return *(int*) &value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe float I32ToFloatBits(int value) {
            return *(float*) &value;
        }
    }
}