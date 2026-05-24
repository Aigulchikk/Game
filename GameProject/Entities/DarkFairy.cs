using GameProject.Entities;

namespace GameProject.Entities
{
    public class DarkFairy : Enemy
    {
        public DarkFairy(string name, int health) : base(name, health) 
        { 
        }

        public override void Attack()
        {
            Console.WriteLine("Темная Фея взмахивает крыльями!");
        }
    }
}