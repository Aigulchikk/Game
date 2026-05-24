using GameProject.Entities;

namespace GameProject.Weapons
{
    public interface IWeapon
    {
        int GetDamage();
        string GetDescription();
        void Attack(GameProject.Entities.Entity target);
    }
}