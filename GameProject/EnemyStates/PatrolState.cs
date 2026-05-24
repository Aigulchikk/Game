using GameProject.Entities;
using System;

namespace GameProject.EnemyStates
{
    public class PatrolState : EnemyState
    {
        public override void Handle(Enemy enemy)
        {
            Console.WriteLine($"{enemy.Name} патрулирует территорию.");
        }

        public override void Update(Enemy enemy)
        {
            if (enemy.DistanceToPlayer < 5) 
            {
                enemy.ChangeState(new ChaseState());
            }
        }
    }
}