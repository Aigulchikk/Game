using GameProject.Entities;
using GameProject.Core;

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
            int newX = _player.X + _dx;
            int newY = _player.Y + _dy;

            if (newX > 0 && newX < GameManager.Instance.MapWidth - 1 &&
                newY > 0 && newY < GameManager.Instance.MapHeight - 1)
            {
                _player.X = newX;
                _player.Y = newY;
            }
        }
    }
}