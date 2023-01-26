namespace UIGL.Application.Dispatch {
    public class Wrapper<T> {
        public T Value { get; set; }

        public Wrapper() {

        }

        public Wrapper(in T value) {
            this.Value = value;
        }
    }
}