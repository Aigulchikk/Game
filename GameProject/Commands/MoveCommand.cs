using GameProject.Entities;

namespace GameProject.Commands
{
    public class MoveCommand : ICommand
    {
        private Player _player;
        private int _dx;
        private int _dy;

        public MoveCommand(Player player, int dx, int dy) 
        { 
            _player = player; 
            _dx = dx; 
            _dy = dy; 
        }

        public void Execute()
        {
            _player.Move(_dx, _dy);
        }
    }
}