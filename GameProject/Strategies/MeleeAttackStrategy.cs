using System;
using GameProject.Entities;

namespace GameProject.Strategies
{
    public class MeleeAttackStrategy : IAttackStrategy 
    {
        private DateTime _lastAttackTime = DateTime.MinValue;
        private readonly TimeSpan _attackCooldown = TimeSpan.FromSeconds(2);
        
        // Добавляем счетчик для замедления
        private int _moveCounter = 0; 

        public void Execute(Player player, Enemy enemy)
        {
            double dist = Math.Sqrt(Math.Pow(enemy.X - player.X, 2) + Math.Pow(enemy.Y - player.Y, 2));

            if (dist <= 1.5)
            {
                if ((DateTime.Now - _lastAttackTime) > _attackCooldown)
                {
                    player.TakeDamage(10);
                    Console.WriteLine($"{enemy.Name} ударил вас лапой!");
                    _lastAttackTime = DateTime.Now;
                }
            }
            else
            {
                _moveCounter++;
                if (_moveCounter >= 10)
                {
                    if (enemy.X < player.X) enemy.X++;
                    else if (enemy.X > player.X) enemy.X--;

                    if (enemy.Y < player.Y) enemy.Y++;
                    else if (enemy.Y > player.Y) enemy.Y--;

                    _moveCounter = 0;
                }
            }
        }
    }
}