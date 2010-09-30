using Core.Commands;
using Graphics.Cameras;
using Input;
using Math;

namespace Sandbox
{
    internal class CameraCommandManager
    {
        private float mSpeedFactor;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string UP = "up";
        private const string DOWN = "down";
        private const string SPEEDFACTOR = "speedfactor";

        private float mFrameTime;

        public CameraCommandManager(ICommandManager commandManager, InputCommandBinder inputCommandBinder, Stand stand)
        {
            commandManager.Add(MOVE_FORWARD, () => stand.Position += stand.Direction * mFrameTime * mSpeedFactor);
            commandManager.Add(MOVE_BACKWARD, () => stand.Position -= stand.Direction* mFrameTime * mSpeedFactor);
            commandManager.Add(STRAFE_RIGHT, () => stand.Position += stand.Direction.Cross(stand.Up) * mFrameTime * mSpeedFactor);
            commandManager.Add(STRAFE_LEFT, () => stand.Position -= stand.Direction.Cross(stand.Up) * mFrameTime * mSpeedFactor);
            commandManager.Add(UP, () => stand.Position += stand.Up * mFrameTime * mSpeedFactor);
            commandManager.Add(DOWN, () => stand.Position -= stand.Up* mFrameTime * mSpeedFactor);
            commandManager.Add(SPEEDFACTOR, () => mSpeedFactor = 20);

            inputCommandBinder.Bind(Button.LeftShift, SPEEDFACTOR);
            inputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            inputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            inputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            inputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            inputCommandBinder.Bind(Button.R, UP);
            inputCommandBinder.Bind(Button.F, DOWN);
        }

        public void Update(float frametime)
        {
            mSpeedFactor = 1;
            mFrameTime = frametime;
        }
    }
}