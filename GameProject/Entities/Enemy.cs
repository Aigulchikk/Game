using GameProject.Strategies;
using GameProject.EnemyStates;

namespace GameProject.Entities
{
    public abstract class Enemy : Entity
    {
        private EnemyState _currentState;
        private IAttackStrategy? _attackStrategy;

        protected Enemy(string name, int health) : base(name, health) 
        { 
            _currentState = new PatrolState(); 
        }

        public void ChangeState(EnemyState newState)
        {
            _currentState = newState;
        }

        public void Update()
        {
            _currentState?.Update(this);
        }

        public void SetAttackStrategy(IAttackStrategy strategy) 
        { 
            _attackStrategy = strategy; 
        }

        public void PerformAttack(Player player)
        {
            _attackStrategy?.Execute(player, this);
        }

        public abstract void Attack();

        public double DistanceToPlayer { get; set; }

        public EnemyState CurrentState => _currentState;
    }
}