using System;
using GameProject.Entities;

namespace GameProject.Core
{
    // Создаем enum
    public enum GameDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class GameManager
    {
        // Поле для хранения единственного экземпляра
        private static GameManager _instance;

        // Настройки
        private bool isRunning = true;
        private Player player;
        private Enemy enemy;
        private Map gameMap;

        // Настройки, доступные через Instance
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public GameDifficulty Difficulty { get; private set; }

        // Приватный конструктор!
        private GameManager()
        {
            MapWidth = 10;
            MapHeight = 10;
            Difficulty = GameDifficulty.Medium;
        }

        // 3. Singleton
        public static GameManager Instance
        {
            get
            {
                // Реализация Lazy Initialization
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        // Метод запуска игры
        public void Run()
        {
            gameMap = new Map(MapWidth, MapHeight);
            player = new Player("Рейнджер", 100);
            enemy = new Enemy("Злой Орк", 50, 15);

            player.X = 2;
            player.Y = 2;

            Console.Clear();
            Console.WriteLine($"=== Game Started with difficulty: [{Difficulty}] ===");
            PrintInstructions();

            while (isRunning)
            {
                HandleInput();
                Update();
                Render();

                System.Threading.Thread.Sleep(50); 
            }

            Console.WriteLine("\nИгра завершена. Спасибо за игру!");
        }

        private void PrintInstructions()
        {
            Console.WriteLine("Управление: СТРЕЛОЧКИ — движение персонажа");
            Console.WriteLine("            SPACE (Пробел) — атака врага");
            Console.WriteLine("            ESC — выход из игры");
            Console.WriteLine("----------------------------------------");
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                int newX = player.X;
                int newY = player.Y;

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    isRunning = false;
                    return;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)    newY--;
                else if (keyInfo.Key == ConsoleKey.DownArrow)  newY++;
                else if (keyInfo.Key == ConsoleKey.LeftArrow)  newX--;
                else if (keyInfo.Key == ConsoleKey.RightArrow) newX++;
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    if (enemy.Health > 0)
                    {
                        enemy.Health -= 10;
                        player.Score += 20;
                        Console.WriteLine($"[АТАКА] Вы ударили {enemy.Name}! HP врага: {enemy.Health}. Очки: {player.Score}");
                    }
                    return;
                }

                if (gameMap.IsInsideBounds(newX, newY))
                {
                    player.Move(newX - player.X, newY - player.Y);
                }
            }
        }

        private void Update() { }

        private void Render()
        {
            Console.SetCursorPosition(0, 7); 
            Console.WriteLine($"Текущая позиция игрока: X: {player.X}, Y: {player.Y} (Карта: {MapWidth}x{MapHeight})   ");
            Console.WriteLine($"Статус врага ({enemy.Name}): HP: {enemy.Health}  ");
        }
    }
}