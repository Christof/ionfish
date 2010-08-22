using Core.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Input
{
    public class Spec_InputCommandBinder
    {
        [Subject(typeof (InputCommandBinder))]
        public class execute_command_for_pressed_key
        {
            static InputCommandBinder inputCommandBinder;
            static Mock<IKeyboard> keyboard;
            static Mock<ICommandManager> commandManager;

            Establish context = () =>
            {
                keyboard = new Mock<IKeyboard>();
                commandManager = new Mock<ICommandManager>();

                inputCommandBinder = new InputCommandBinder(commandManager.Object, keyboard.Object);
                inputCommandBinder.Bind(Button.W, "move");
                inputCommandBinder.Bind(Button.Escape, "close");

                keyboard.Setup(x => x.IsPressed(Button.W)).Returns(true);
            };

            Because of = () => inputCommandBinder.Update();

            It should_call_execute_for_the_binded_command_when_it_is_pressed = () => 
                commandManager.Verify(x => x.Execute("move"));

            It should_not_call_execute_for_the_binded_command_when_it_is_not_pressed = () => 
                commandManager.Verify(x => x.Execute("close"), Times.Never());
        }
    }
}