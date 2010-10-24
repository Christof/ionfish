using Core.Commands;
using Graphics.Cameras;
using Input;
using Graphics;

namespace Sandbox
{
    internal class FirstPersonCameraCommandBindingManager : CameraCommandBindingManager
    {
        private const string TURN_LEFT = "turn left";
        private const string TURN_RIGHT = "turn right";
        private const string TURN_UP = "turn up";
        private const string TURN_DOWN = "turn down";
        private const string ROLL_LEFT = "roll left";
        private const string ROLL_RIGHT = "roll right";
        
        public FirstPersonCameraCommandBindingManager(ICommandManager commandManager, InputCommandBinder inputCommandBinder, FirstPersonStand stand, Game game)
            : base(commandManager, inputCommandBinder, stand, game)
        {
            commandManager.Add(TURN_LEFT, () => stand.Yaw(-FrameTime));
            commandManager.Add(TURN_RIGHT, () => stand.Yaw(FrameTime));
            commandManager.Add(TURN_UP, () => stand.Pitch(-FrameTime));
            commandManager.Add(TURN_DOWN, () => stand.Pitch(FrameTime));
            commandManager.Add(ROLL_LEFT, () => stand.Roll(FrameTime));
            commandManager.Add(ROLL_RIGHT, () => stand.Roll(-FrameTime));

            inputCommandBinder.Bind(Button.J, TURN_LEFT);
            inputCommandBinder.Bind(Button.L, TURN_RIGHT);
            inputCommandBinder.Bind(Button.I, TURN_UP);
            inputCommandBinder.Bind(Button.K, TURN_DOWN);
            inputCommandBinder.Bind(Button.U, ROLL_LEFT);
            inputCommandBinder.Bind(Button.O, ROLL_RIGHT);
        }
    }
}