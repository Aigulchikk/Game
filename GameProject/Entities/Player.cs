namespace GameProject.Entities
{
    public class Player : Entity
    {
        public Player(string name, int health) : base(name, health) { }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0; 
        }

        public void Heal(int amount)
        {
            Health += amount;
        }
        
        public int Level { get; set; } = 1;
        public string Weapon { get; set; } = "None";
        public int Score { get; set; } = 0;
        public void Move(int dx, int dy) 
        { 
            X += dx; 
            Y += dy; 
        }
    }
}