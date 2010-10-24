using Core.Commands;
using Graphics.Cameras;
using Input;
using Graphics;

namespace Sandbox
{
    internal class OrbitingCameraCommandBindingManager : CommandBindingManagerBase
    {
        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string UP = "up";
        private const string DOWN = "down";

        public OrbitingCameraCommandBindingManager(ICommandManager commandManager, InputCommandBinder inputCommandBinder, OrbitingStand stand, Game game)
            : base(commandManager, inputCommandBinder, game)
        {
            commandManager.Add(MOVE_BACKWARD, () => stand.Radius += FrameTime);
            commandManager.Add(MOVE_FORWARD, () => stand.Radius -= FrameTime);
            commandManager.Add(STRAFE_RIGHT, () => stand.Azimuth -= FrameTime);
            commandManager.Add(STRAFE_LEFT, () => stand.Azimuth += FrameTime);
            commandManager.Add(UP, () => stand.Declination += FrameTime);
            commandManager.Add(DOWN, () => stand.Declination -= FrameTime);

            inputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            inputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            inputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            inputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            inputCommandBinder.Bind(Button.R, UP);
            inputCommandBinder.Bind(Button.F, DOWN);
        }
    }
}