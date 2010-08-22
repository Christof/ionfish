using System;
using System.Collections.Generic;
using Core.Commands;

namespace Input
{
    public class InputCommandBinder
    {
        private readonly Dictionary<Button, string> mBindings = new Dictionary<Button, string>();
        private readonly ICommandManager mCommandManager;
        private readonly IKeyboard mKeyboard;

        public InputCommandBinder(ICommandManager commandManager, IKeyboard keyboard)
        {
            mCommandManager = commandManager;
            mKeyboard = keyboard;
        }

        public void Bind(Button button, string name)
        {
            mBindings.Add(button, name);
        }

        public void Update()
        {
            foreach (var binding in mBindings)
            {
                if(mKeyboard.IsPressed(binding.Key))
                {
                    mCommandManager.Execute(binding.Value);
                }
            }
        }
    }
}