using System;
using GameProject.Entities;

namespace GameProject.Strategies
{
    public class RangedAttackStrategy : IAttackStrategy
    {
        private DateTime _lastAttackTime = DateTime.MinValue;
        private readonly TimeSpan _cooldown = TimeSpan.FromSeconds(3);

        public void Execute(Player player, Enemy enemy)
        {
            if (enemy.DistanceToPlayer < 10.0)
            {
                if (enemy.ActiveProjectile == null && (DateTime.Now - _lastAttackTime) > _cooldown)
                {
                    Console.WriteLine($"{enemy.Name} выпускает магическую пыльцу!");
                    enemy.LaunchProjectile();
                    player.TakeDamage(5);
                    
                    _lastAttackTime = DateTime.Now;
                }
            }
        }
    }
}