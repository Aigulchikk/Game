using System;
using GameProject.Entities;

namespace GameProject.Core
{
    public class Game
    {
        private bool isRunning = true;
        
        // Поля для игрока и врага
        private Player player;
        private Enemy enemy;

        public void Run()
        {
            player = new Player("Рейнджер", 100);
            enemy = new Enemy("Злой Орк", 50, 15);

            Console.Clear();
            Console.WriteLine("=== ДОБРО ПОЖАЛОВАТЬ В PORTAL JUMPER ===");
            Console.WriteLine($"Игрок {player.Name} (HP: {player.Health}) готов к бою!");
            Console.WriteLine($"Впереди показался {enemy.Name} (HP: {enemy.Health}, Наносит урон: {enemy.Damage})!");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Нажмите SPACE (Пробел), чтобы атаковать врага.");
            Console.WriteLine("Нажмите ESC для выхода из игры.");
            Console.WriteLine("----------------------------------------");

            while (isRunning)
            {
                HandleInput();
                Update();
                Render();

                System.Threading.Thread.Sleep(100); 
            }

            Console.WriteLine("\nИгра завершена. Спасибо за игру!");
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    isRunning = false;
                }
                // Реагируем на Пробел — симулируем атаку игрока!
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    if (enemy.Health > 0)
                    {
                        enemy.Health -= 10; // Отнимаем здоровье у орка
                        player.Score += 20; // Начисляем очки игроку
                        Console.WriteLine($"[АТАКА] Вы ударили {enemy.Name}! Его HP стало: {enemy.Health}. Ваши очки: {player.Score}");
                    }
                    else
                    {
                        Console.WriteLine($"[ПОБЕДА] {enemy.Name} уже повержен! Портал открыт.");
                    }
                }
            }
        }

        private void Update()
        {
        }

        private void Render()
        {
        }
    }
}