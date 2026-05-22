namespace GameProject.Weapons
{
    public abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon _weapon;

        public WeaponDecorator(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public virtual int GetDamage() => _weapon.GetDamage();
        public virtual string GetDescription() => _weapon.GetDescription();
    }
}