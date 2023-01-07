using System;
using OpenTK.Mathematics;
using UIGL.Utils;

namespace UIGL.UI {
    public class UIElement {
        private Vector2d measured_size;

        private HorizontalAlignment alignmentH;
        private VerticalAlignment alignmentV;

        private Vector2d min_size;
        private Vector2d max_size;
        private Vector2d size;

        public Vector2d TargetSize => new Vector2d(Math.Clamp(this.size.X, this.min_size.X, this.max_size.X), Math.Clamp(this.size.Y, this.min_size.Y, this.max_size.Y));

        public UIElement() {

        }

        protected virtual void Render() {

        }

        public static void Render(UIElement element) {
            element.Render();
        }
    }
}