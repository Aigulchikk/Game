using Xunit;
using GameProject.Entities;
using GameProject.EnemyStates;

namespace GameProject.Tests
{
    public class EnemyStateTests
    {
        private class TestEnemy : Enemy {
            public TestEnemy(string name, int health) : base(name, health) { }
            public override void Attack() { }
        }

        [Fact]
        public void Enemy_WhenPlayerTooFar_ShouldSwitchToPatrolState()
        {
            // Arrange
            var enemy = new TestEnemy("Тест-Враг", 100);
            enemy.ChangeState(new ChaseState());
            enemy.DistanceToPlayer = 10.0; // Игрок далеко

            // Act
            enemy.Update();

            // Assert
            Assert.IsType<PatrolState>(enemy.CurrentState);

        }
    }
}