using SlimDX.DirectInput;

namespace Input
{
    public class Keyboard
    {
        private readonly SlimDX.DirectInput.Keyboard mKeyboard;
        private KeyboardState mState;

        public Keyboard()
        {
            var directInput = new DirectInput();
            mKeyboard = new SlimDX.DirectInput.Keyboard(directInput);
            mKeyboard.Acquire();
            mState = new KeyboardState();
        }

        public void Update()
        {
            mKeyboard.GetCurrentState(ref mState);
        }

        public bool IsPressed(Button button)
        {
            return mState.IsPressed((Key) button);
        }
    }
}