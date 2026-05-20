using System;

namespace GameProject.Entities
{
    public class Player : Entity
    {
        public int Score { get; set; }

        public Player(string name, int health) : base(name, health)
        {
            Score = 0;
        }

        public void Move()
        {

        }
    }
}