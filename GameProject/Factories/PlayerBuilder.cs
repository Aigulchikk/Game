using GameProject.Entities;
using GameProject.Weapons;

namespace GameProject.Factories
{
    public class PlayerBuilder
    {
        private Player _player = new Player("Неизвестный", 100);

        public PlayerBuilder SetName(string name)
        {
            _player.Name = name;
            return this;
        }

        public PlayerBuilder SetHealth(int health)
        {
            _player.Health = health;
            return this;
        }

        public PlayerBuilder SetLevel(int level)
        {
            _player.Level = level;
            return this;
        }

        public PlayerBuilder SetWeapon(IWeapon weapon)
        {
            _player.Weapon = weapon;
            return this;
        }

        public Player Build() => _player;
    }
}