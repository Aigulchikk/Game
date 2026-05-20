namespace GameProject.Entities
{
    public abstract class Enemy : Entity
    {
        protected Enemy(string name, int health) : base(name, health) { }
        public abstract void Attack();
    }
}