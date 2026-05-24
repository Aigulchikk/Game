using System;
using GameProject.Entities;

namespace GameProject.UI
{
    public class ConsoleHUD
    {
        private Player _player;

        public ConsoleHUD(Player player)
        {
            _player = player;
            _player.OnHealthChanged += UpdateUI;
            
            UpdateUI(_player.Health);
        }

        private void UpdateUI(int currentHealth)
        {
            int healthBarLength = 10;
            
            int clampedHealth = Math.Clamp(currentHealth, 0, 100);
            
            int filled = clampedHealth / 10; 
            
            string bar = new string('|', filled) + new string('.', healthBarLength - filled);
            
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"[HUD] HP: [{bar}] {currentHealth}/100    ");
        }

        public void Dispose()
        {
            _player.OnHealthChanged -= UpdateUI;
        }
    }
}