using GameProject.Entities;

namespace GameProject.Strategies
{
    public class RangedAttackStrategy : IAttackStrategy
    {
        public void Execute(Player player, Enemy enemy)
        {
            Console.WriteLine($"{enemy.Name} выпускает стрелу!");
            player.TakeDamage(5);
        }
    }
}