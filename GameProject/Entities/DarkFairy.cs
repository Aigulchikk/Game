using System;
namespace GameProject.Entities
{
    public class DarkFairy : Enemy
    {
        public DarkFairy() : base("Чёрная фея", 80) { }

        public override void Attack()
        {
            Console.WriteLine("Фея окутывает тебя магическим туманом...");
        }
    }
}