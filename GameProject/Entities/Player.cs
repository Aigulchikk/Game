using System;

namespace GameProject.Entities
{
    public class Player : Entity
    {
        public int Score { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }

        public Player(string name, int health) : base(name, health)
        {
            Score = 0;
            X = 0;
            Y = 0;
        }

        public void Move(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
    }
}