using Xunit;
using GameProject.Entities;

namespace GameProject.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Player_Creation_ShouldSetPropertiesCorrectly()
        {
            string expectedName = "Рейнджер";
            int expectedHealth = 150;

            var player = new Player(expectedName, expectedHealth);

            Assert.Equal(expectedName, player.Name);
            Assert.Equal(expectedHealth, player.Health);
        }
    }
}