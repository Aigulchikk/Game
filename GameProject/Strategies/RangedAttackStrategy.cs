using GameProject.Entities;

namespace GameProject.Strategies
{
    public class RangedAttackStrategy : IAttackStrategy
    {
        public void Execute(Player player, Enemy enemy)
        {

            if (enemy.DistanceToPlayer < 10.0)
            {
                Console.WriteLine($"{enemy.Name} выпускает магическую стрелу!");
                player.TakeDamage(5);
            }
            else
            {
                Console.WriteLine($"{enemy.Name} не достает до игрока!");
            }
        }
    }
}