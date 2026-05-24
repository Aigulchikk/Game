using GameProject.Entities;

namespace GameProject.EnemyStates
{
    public abstract class EnemyState
    {
        public abstract void Handle(Enemy enemy);
        public abstract void Update(Enemy enemy);
    }
}