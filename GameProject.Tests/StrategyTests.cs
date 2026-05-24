using Xunit;
using GameProject.Entities;
using GameProject.Strategies;

namespace GameProject.Tests
{
    public class StrategyTests
    {
        [Fact]
        public void Enemy_PerformAttack_ShouldReducePlayerHealth()
        {
            // Arrange
            var player = new Player("Тест-герой", 100);
            var enemy = new Bear();
            
            enemy.SetAttackStrategy(new MeleeAttackStrategy()); 

            // Act
            enemy.PerformAttack(player);

            // Assert
            Assert.True(player.Health < 100, "Стратегия атаки не сработала: здоровье игрока не изменилось.");
        }
    }
}