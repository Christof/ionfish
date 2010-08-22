using System;
using System.Collections.Generic;

namespace Core.Commands
{
    public class CommandManager : ICommandManager
    {
        private readonly Dictionary<string, ActionCommand> mActionList = new Dictionary<string, ActionCommand>();

        public void Add(string name, Action action)
        {
            mActionList.Add(name, new ActionCommand(name, action));
        }

        public void Execute(string name)
        {
            mActionList[name].Execute();
        }
    }
}