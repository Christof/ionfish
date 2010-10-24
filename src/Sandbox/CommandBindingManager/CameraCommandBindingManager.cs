using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Input;

namespace Sandbox
{
    internal class CameraCommandBindingManager : CommandBindingManagerBase
    {
        private float mSpeedFactor;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string UP = "up";
        private const string DOWN = "down";
        private const string SPEEDFACTOR = "speedfactor";

        public CameraCommandBindingManager(ICommandManager commandManager, InputCommandBinder inputCommandBinder, Stand stand, Game game)
            : base(commandManager, inputCommandBinder, game)
        {
            commandManager.Add(MOVE_FORWARD, () => stand.Position += stand.Direction * FrameTime * mSpeedFactor);
            commandManager.Add(MOVE_BACKWARD, () => stand.Position -= stand.Direction* FrameTime * mSpeedFactor);
            commandManager.Add(STRAFE_RIGHT, () => stand.Position += stand.Direction.Cross(stand.Up) * FrameTime * mSpeedFactor);
            commandManager.Add(STRAFE_LEFT, () => stand.Position -= stand.Direction.Cross(stand.Up) * FrameTime * mSpeedFactor);
            commandManager.Add(UP, () => stand.Position += stand.Up * FrameTime * mSpeedFactor);
            commandManager.Add(DOWN, () => stand.Position -= stand.Up* FrameTime * mSpeedFactor);
            commandManager.Add(SPEEDFACTOR, () => mSpeedFactor = 20);

            inputCommandBinder.Bind(Button.LeftShift, SPEEDFACTOR);
            inputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            inputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            inputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            inputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            inputCommandBinder.Bind(Button.R, UP);
            inputCommandBinder.Bind(Button.F, DOWN);
        }

        protected override void  InnerUpdate()
        {
            mSpeedFactor = 1;
        }
    }
}