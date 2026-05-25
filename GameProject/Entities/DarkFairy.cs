using GameProject.Entities;
using GameProject.Strategies;

namespace GameProject.Entities
{
    public class DarkFairy : Enemy
    {
        public DarkFairy(string name, int health) : base(name, health) 
        {
            SetAttackStrategy(new RangedAttackStrategy());
        }

        public override void Attack()
        {
            Console.WriteLine("Темная Фея взмахивает крыльями!");
        }
    }
}