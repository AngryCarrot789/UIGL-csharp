using System;
using OpenTK.Windowing.GraphicsLibraryFramework;
using UIGL.UI;

namespace UIGL.Application.Inputs {
    public class Keyboard {
        private int lastTick;

        private readonly InputAction[] keys;
        private readonly InputAction[] keysFrame;

        private readonly KeyModifiers[] mods;
        private readonly KeyModifiers[] modsFrame;

        public Keyboard() {
            const int maxKeysEnum = 348;

            this.keys = new InputAction[maxKeysEnum];
            this.keysFrame = new InputAction[maxKeysEnum];
        }

        public bool IsKeyDown(Keys key) {
            InputAction action = this.keys[(int) key];
            return action == InputAction.Press || action == InputAction.Repeat;
        }

        public bool IsKeyDown(Keys key, out bool isRepeat) {
            InputAction action = this.keys[(int) key];
            isRepeat = action == InputAction.Repeat;
            return action == InputAction.Press || isRepeat;
        }

        public bool IsKeyUp(Keys key) {
            return this.keys[(int) key] == InputAction.Release;
        }

        public void OnWindowInput(UIWindow window, Keys key, int scan, InputAction action, KeyModifiers modifiers) {
            if (App.Instance.Tick != this.lastTick) {
                Array.Fill(this.keysFrame, InputAction.Release);
                Array.Fill(this.modsFrame, (KeyModifiers) 0);
            }

            int keyIndex = (int) key;
            this.keys[keyIndex] = action;
            this.keysFrame[keyIndex] = action;

            this.mods[keyIndex] |= modifiers;
            this.modsFrame[keyIndex] |= modifiers;
        }
    }
}