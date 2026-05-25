using GameProject.Entities;
namespace GameProject.Factories
{
    public class BearFactory : EnemyFactory
    {
        public override Enemy CreateEnemy() => new Bear("Гризли", 80);
    }
}