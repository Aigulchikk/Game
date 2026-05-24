using GameProject.Entities;

namespace GameProject.Weapons
{
    public abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon _weapon;
        public WeaponDecorator(IWeapon weapon) { _weapon = weapon; }

        public virtual int GetDamage() => _weapon.GetDamage();
        public virtual string GetDescription() => _weapon.GetDescription();
        public virtual void Attack(Entity target) => _weapon.Attack(target);
    }
}