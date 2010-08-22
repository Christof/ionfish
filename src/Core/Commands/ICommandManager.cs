using System;

namespace Core.Commands
{
    public interface ICommandManager
    {
        void Add(string name, Action action);
        void Execute(string name);
    }
}