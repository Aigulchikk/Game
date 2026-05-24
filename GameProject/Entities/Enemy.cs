using GameProject.Strategies;

namespace GameProject.Entities
{
    public abstract class Enemy : Entity
    {
        private IAttackStrategy _attackStrategy;

        protected Enemy(string name, int health) : base(name, health) { }

        public void SetAttackStrategy(IAttackStrategy strategy) 
        { 
            _attackStrategy = strategy; 
        }

        public void PerformAttack(Player player)
        {
            _attackStrategy?.Execute(player, this);
        }

        public abstract void Attack();
    }
}