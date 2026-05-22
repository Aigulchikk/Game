namespace GameProject.Weapons
{
    public class FireDamage : WeaponDecorator
    {
        public FireDamage(IWeapon weapon) : base(weapon) { }

        public override int GetDamage()
        {
            return _weapon.GetDamage() + 5;
        }

        public override string GetDescription()
        {
            return _weapon.GetDescription() + " + Огонь";
        }
    }
}