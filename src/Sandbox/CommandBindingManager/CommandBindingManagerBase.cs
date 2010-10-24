using Core.Commands;
using Graphics;
using Input;

namespace Sandbox
{
    public class CommandBindingManagerBase
    {
        private readonly InputCommandBinder mInputCommandBinder;

        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";

        protected CommandBindingManagerBase(ICommandManager commandManager, InputCommandBinder inputCommandBinder, Game game)
        {
            mInputCommandBinder = inputCommandBinder;

            commandManager.Add(ESCAPE, game.Exit);
            commandManager.Add(TAKE_SCREENSHOT, game.TakeScreenshot);

            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);
        }

        public void Update(float frametime)
        {
            FrameTime = frametime;
            mInputCommandBinder.Update();

            InnerUpdate();
        }

        protected virtual void InnerUpdate()
        {
            
        }

        protected float FrameTime { get; set; }
    }
}