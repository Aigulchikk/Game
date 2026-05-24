using GameProject.Entities;

namespace GameProject.Strategies
{
    public class MeleeAttackStrategy : IAttackStrategy
    {
        public void Execute(Player player, Enemy enemy)
        {
            Console.WriteLine($"{enemy.Name} наносит удар ближнего боя!");
            player.TakeDamage(10); 
        }
    }
}