namespace GameProject.Entities
{
    public class Player : Entity
    {
        public int Level { get; set; }
        public string Weapon { get; set; }
        public int Score { get; set; }

        public Player(string name, int health) : base(name, health) { }

        public void Move(int dx, int dy) { X += dx; Y += dy; }
    }
}