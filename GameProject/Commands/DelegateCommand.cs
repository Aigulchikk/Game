using System;

namespace GameProject.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action) 
        { 
            _action = action; 
        }

        public void Execute() => _action();
    }
}