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

        [Fact]
        public void Player_SaveLoad_ShouldMaintainDataIntegrity()
        {
            // Arrange
            // Используем конструктор, так как PlayerBuilder пока не нужен для теста
            var player = new Player("Тестовый Игрок", 100);
            player.Level = 5;
            player.X = 10;
            player.Y = 20;
            player.Score = 500;

            // Act
            var saveData = player.ToSaveData();
            
            var newPlayer = new Player("Новый Игрок", 0);
            newPlayer.LoadFromSaveData(saveData);

            // Assert
            Assert.Equal(player.X, newPlayer.X);
            Assert.Equal(player.Health, newPlayer.Health);
            Assert.Equal(player.Score, newPlayer.Score);
            Assert.Equal(player.Level, newPlayer.Level);
        }
    }
}