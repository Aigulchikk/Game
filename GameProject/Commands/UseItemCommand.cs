using GameProject.Entities;

namespace GameProject.Commands
{
    public class UseItemCommand : ICommand
    {
        private Player _player;

        public UseItemCommand(Player player) 
        { 
            _player = player; 
        }

        public void Execute()
        {
            _player.UseItem(null!);
        }
    }
}