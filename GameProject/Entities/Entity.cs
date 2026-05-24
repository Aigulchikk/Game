namespace GameProject.Entities
{
    public abstract class Entity
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        protected Entity(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public virtual void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }
    }
}