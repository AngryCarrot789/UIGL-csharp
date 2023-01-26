namespace UIGL.UI {
    public readonly struct Thickness {
        public double Left { get; }
        public double Top { get; }
        public double Right { get; }
        public double Bottom { get; }

        public Thickness(double all) {
            this.Left = this.Top = this.Right = this.Bottom = all;
        }

        public Thickness(double horizontal, double vertical) {
            this.Left = this.Right = horizontal;
            this.Top = this.Bottom = vertical;
        }

        public Thickness(double left, double top, double right, double bottom) {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }
    }
}