using System;
using GameProject.Entities;

namespace GameProject.Entities
{
    public class Bear : Enemy
    {
        public Bear() : base("Медведь-страж", 100) { }

        public override void Attack()
        {
            Console.WriteLine("Медведь грозно рычит и преграждает путь!");
        }
    }
}