using GameProject.Entities;

namespace GameProject.Weapons
{
    public class FireDamage : WeaponDecorator
    {
        public FireDamage(IWeapon weapon) : base(weapon) { }

        public override int GetDamage() => base.GetDamage() + 5;
        public override string GetDescription() => base.GetDescription() + " (в огне)";
    }
}