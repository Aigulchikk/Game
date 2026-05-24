using GameProject.Entities;

namespace GameProject.Weapons
{
    public class Sword : IWeapon
    {
        public int GetDamage() => 10;
        public string GetDescription() => "Обычный меч";

        public void Attack(Entity target)
        {
            target.TakeDamage(GetDamage());
        }
    }
}