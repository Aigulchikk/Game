using GameProject.Entities;
using System;

namespace GameProject.EnemyStates
{
    public class ChaseState : EnemyState
    {
        public override void Handle(Enemy enemy)
        {
            Console.WriteLine($"{enemy.Name} перешел в состояние преследования!");
        }

        public override void Update(Enemy enemy)
        {
            Console.WriteLine($"[!] {enemy.Name} яростно преследует игрока!");

            if (enemy.DistanceToPlayer > 8.0)
            {
                Console.WriteLine($"[!] {enemy.Name} потерял след и вернулся в патруль.");
                enemy.ChangeState(new PatrolState());
            }
            else if (enemy.DistanceToPlayer < 1.5)
            {
                Console.WriteLine($"[!] {enemy.Name} вплотную подошел к игроку!");
            }
        }
    }
}