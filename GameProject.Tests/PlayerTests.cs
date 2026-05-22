using Xunit;
using GameProject.Entities;

namespace GameProject.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void TakeDamage_ShouldReduceHealth()
        {
            // Arrange
            var player = new Player("Герой", 100);

            // Act
            player.TakeDamage(30);

            // Assert
            Assert.Equal(70, player.Health);
        }

        [Fact]
        public void TakeDamage_MoreThanHealth_ShouldBeZero()
        {
            // Arrange
            var player = new Player("Герой", 50);

            // Act
            player.TakeDamage(100);

            // Assert
            Assert.Equal(0, player.Health);
        }

        [Fact]
        public void Heal_ShouldIncreaseHealth()
        {
            // Arrange
            var player = new Player("Герой", 50);

            // Act
            player.Heal(20);

            // Assert
            Assert.Equal(70, player.Health);
        }
    }
}