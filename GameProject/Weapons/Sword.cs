namespace GameProject.Weapons
{
    public class Sword : IWeapon
    {
        public int GetDamage() => 10;
        
        public string GetDescription() => "Простой меч";
    }
}