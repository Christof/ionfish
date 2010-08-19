using System;

namespace Core.Commands
{
    public class ActionCommand : ICommand
    {
        readonly Action mAction;

        public ActionCommand(string name, Action action)
        {
            mAction = action;
            Name = name;
        }

        public string Name { get; private set; }

        public void Execute()
        {
            mAction();
        }
    }
}