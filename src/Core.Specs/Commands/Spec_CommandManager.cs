using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Core.Commands
{
    public class Spec_CommandManager
    {
        [Subject(typeof (CommandManager))]
        public class add_command
        {
            static CommandManager manager;
            static bool wasActionCalled;

            Establish context = () =>
            {
                manager = new CommandManager();
                manager.Add("test", () => wasActionCalled = true);
            };

            Because of = () => manager.Execute("test");

            It should_have_called_the_command = () => wasActionCalled.ShouldBeTrue();
        }

        [Subject(typeof (CommandManager))]
        public class call_not_existing_command
        {
            static Exception exception;

            Because of = () => exception = Catch.Exception(() => new CommandManager().Execute("not existing"));

            It should_throw_an_exceptoin = () => exception.ShouldBeOfType<KeyNotFoundException>();
        }

        [Subject(typeof (CommandManager))]
        public class add_two_times_with_same_name
        {
            static CommandManager manager;
            static Exception exception;

            Establish context = () =>
            {
                manager = new CommandManager();
                manager.Add("test", () => { });
            };

            Because of = () => exception = Catch.Exception(() => manager.Add("test", () => { }));

            It should_throw_an_exceptoin = () => exception.ShouldBeOfType<ArgumentException>();
        }
    }
}