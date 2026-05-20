namespace GameProject.Entities
{
    public abstract class Enemy : Entity
    {
        public Enemy(string name, int health) : base(name, health) { }

        public abstract void Attack();
    }
}