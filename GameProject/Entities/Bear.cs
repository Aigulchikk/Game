using System;
using GameProject.Entities;

namespace GameProject.Entities
{
    public class Bear : Enemy
    {
        public Bear(string name, int health) : base(name, health) { }

        public override void Attack()
        {
            Console.WriteLine("Медведь грозно рычит и преграждает путь!");
        }
    }
}