using Xunit;
using GameProject.Factories;
using GameProject.Entities;

namespace GameProject.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void BearFactory_Create_ShouldReturnNotNull()
        {
            var factory = new BearFactory(); 
            var enemy = factory.CreateEnemy(); 
            
            Assert.NotNull(enemy);
            Assert.IsAssignableFrom<Enemy>(enemy);
        }
    }
}