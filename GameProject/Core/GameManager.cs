using System;
using GameProject.Entities;
using GameProject.Factories;

namespace GameProject.Core
{
    public enum GameDifficulty { Easy, Medium, Hard }

    public class GameManager
    {
        private static GameManager _instance;
        private bool isRunning = true;
        private Player player;
        
        private Enemy enemy; 
        
        private Map gameMap;

        // Поле для фабрики
        private EnemyFactory _enemyFactory;

        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public GameDifficulty Difficulty { get; private set; }

        private GameManager()
        {
            MapWidth = 10;
            MapHeight = 10;
            Difficulty = GameDifficulty.Medium;

            _enemyFactory = new BearFactory();
        }

        public static GameManager Instance => _instance ??= new GameManager();

        public void Run()
        {
            gameMap = new Map(MapWidth, MapHeight);
            player = new Player("Рейнджер", 100);
            
            enemy = _enemyFactory.CreateEnemy();

            player.X = 2;
            player.Y = 2;

            Console.Clear();
            Console.WriteLine($"=== Game Started with difficulty: [{Difficulty}] ===");
            Console.WriteLine($"В лесу появился: {enemy.Name}!");
            PrintInstructions();

            while (isRunning)
            {
                HandleInput();
                Update();
                Render();
                System.Threading.Thread.Sleep(50); 
            }
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                if (keyInfo.Key == ConsoleKey.Escape) { isRunning = false; return; }

                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    if (enemy.Health > 0)
                    {
                        enemy.Attack(); 
                        
                        enemy.Health -= 10;
                        player.Score += 20;
                        Console.WriteLine($"[АТАКА] Вы ударили {enemy.Name}! HP: {enemy.Health}. Очки: {player.Score}");
                    }
                }
            }
        }

        private void Render()
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine($"Статус врага ({enemy.Name}): HP: {enemy.Health}   ");
        }

        private void PrintInstructions() { /* ... */ }
        private void Update() { }
    }
}