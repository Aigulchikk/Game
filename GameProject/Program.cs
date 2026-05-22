using System;
using GameProject.Core;
using GameProject.Weapons;

namespace GameProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Тест Декоратора ---");
            IWeapon mySword = new Sword();
            Console.WriteLine($"{mySword.GetDescription()} : {mySword.GetDamage()} урона");

            mySword = new FireDamage(mySword);
            Console.WriteLine($"{mySword.GetDescription()} : {mySword.GetDamage()} урона");
            
            mySword = new FireDamage(mySword);
            Console.WriteLine($"{mySword.GetDescription()} : {mySword.GetDamage()} урона");
            
            Console.WriteLine("------------------------\n");

            GameManager.Instance.Run();
        }
    }
}