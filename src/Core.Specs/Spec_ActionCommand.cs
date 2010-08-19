using Core.Commands;
using Machine.Specifications;

namespace Core.Specs
{
    public class Spec_ActionCommand
    {
        [Subject(typeof(ActionCommand))]
        public class execute
        {
            static ICommand command;
            static bool wasActionCalled;

            Establish context = () => command = new ActionCommand("Name", () => wasActionCalled = true);

            Because of = () => command.Execute();

            It should_call_the_action = () => wasActionCalled.ShouldBeTrue();
            It should_have_the_name_given_in_the_constructor = () => command.Name.ShouldEqual("Name");
        }
    }
}