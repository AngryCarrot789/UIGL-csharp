using System;

namespace UIGL.Utils {
    public struct Rect {
        public double x;
        public double y;
        public double w;
        public double h;

        public Rect(double x, double y, double w, double h) {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
    }
}