using System;

namespace GameProject.Entities
{
    public class Entity
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public Entity(string name, int health)
        {
            Name = name;
            Health = health;
        }
    }
}