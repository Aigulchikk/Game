using GameProject.Entities;
using System;

namespace GameProject.Strategies
{
    public class FireSwordStrategy : IAttackStrategy
    {
        public void Execute(Player player, Enemy enemy)
        {
            Console.WriteLine($"[!] {enemy.Name} атакует огненным мечом! Игрок получил урон.");
            
            player.Health -= 20; 
        }
    }
}