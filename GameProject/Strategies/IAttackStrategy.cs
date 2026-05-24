using GameProject.Entities;

namespace GameProject.Strategies
{
    public interface IAttackStrategy
    {
        void Execute(Player player, Enemy enemy);
    }
}