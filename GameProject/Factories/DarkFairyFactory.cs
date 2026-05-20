using GameProject.Entities;
namespace GameProject.Factories
{
    public class DarkFairyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy() => new DarkFairy();
    }
}