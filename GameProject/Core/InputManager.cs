using System;
using System.Collections.Generic;
using GameProject.Commands;

namespace GameProject.Core
{
    public class InputManager
    {
        private Dictionary<ConsoleKey, ICommand> _commands = new Dictionary<ConsoleKey, ICommand>();

        public void BindCommand(ConsoleKey key, ICommand command)
        {
            _commands[key] = command;
        }

        public void HandleInput(ConsoleKey key)
        {
            if (_commands.ContainsKey(key))
            {
                _commands[key].Execute();
            }
        }
    }
}