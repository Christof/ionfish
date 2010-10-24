using Core.Commands;
using Graphics.Cameras;
using Input;

namespace Sandbox
{
    internal class OrbitingCameraCommandManager
    {
        private readonly InputCommandBinder mInputCommandBinder;
        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string UP = "up";
        private const string DOWN = "down";

        private float mFrametime;

        public OrbitingCameraCommandManager(ICommandManager commandManager, InputCommandBinder inputCommandBinder, OrbitingStand stand)
        {
            mInputCommandBinder = inputCommandBinder;
            commandManager.Add(MOVE_BACKWARD, () => stand.Radius += mFrametime);
            commandManager.Add(MOVE_FORWARD, () => stand.Radius -= mFrametime);
            commandManager.Add(STRAFE_RIGHT, () => stand.Azimuth -= mFrametime);
            commandManager.Add(STRAFE_LEFT, () => stand.Azimuth += mFrametime);
            commandManager.Add(UP, () => stand.Declination += mFrametime);
            commandManager.Add(DOWN, () => stand.Declination -= mFrametime);

            inputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            inputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            inputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            inputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            inputCommandBinder.Bind(Button.R, UP);
            inputCommandBinder.Bind(Button.F, DOWN);   }

        public void Update(float frametime)
        {
            mInputCommandBinder.Update();
            mFrametime = frametime;
        }
    }
}