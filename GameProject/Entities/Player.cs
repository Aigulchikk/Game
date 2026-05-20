namespace GameProject.Entities
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public string Weapon { get; set; }
        public int Score { get; set; }

        public Player(string name, int health) : base(name)
        {
            Name = name;
            Health = health;
        }

        public void Move(int dx, int dy) { X += dx; Y += dy; }
    }
}