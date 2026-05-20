using System;

namespace GameProject.Entities
{
    public class Enemy : Entity
    {
        public int Damage { get; set; }

        public Enemy(string name, int health, int damage) : base(name, health)
        {
            Damage = damage;
        }

        public void Attack()
        {

        }
    }
}