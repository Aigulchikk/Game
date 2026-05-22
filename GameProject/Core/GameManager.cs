using System;
using GameProject.Entities;
using GameProject.Factories;
using GameProject.Weapons;

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
        private EnemyFactory _enemyFactory;

        private DateTime? _fireBuffEndTime = null; 
        private DateTime? _lastBuffActivationTime = null;
        private const int FireBuffDurationSeconds = 10;

        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public GameDifficulty Difficulty { get; private set; }

        private GameManager()
        {
            MapWidth = 10;
            MapHeight = 10;
            Difficulty = GameDifficulty.Easy;
            
            SelectFactory();
        }

        public static GameManager Instance => _instance ??= new GameManager();

        public void ActivateFireBuff()
        {
            if (_lastBuffActivationTime.HasValue && (DateTime.Now - _lastBuffActivationTime.Value).TotalSeconds < 60)
            {
                double remaining = 60 - (DateTime.Now - _lastBuffActivationTime.Value).TotalSeconds;
                LogMessage($"\n [!] Бафф еще на перезарядке! Осталось: {remaining:F0} сек.");
                return;
            }
            _lastBuffActivationTime = DateTime.Now;
            _fireBuffEndTime = DateTime.Now.AddSeconds(10);

            player.Weapon = new FireDamage(player.Weapon); 
            LogMessage("\n[!] Бафф Огня активирован!");
        }

        public void Run()
        {
            gameMap = new Map(MapWidth, MapHeight);
            player = new PlayerBuilder()
            .SetName("Рейнджер")
            .SetHealth(150)
            .SetLevel(1)
            .Build();

            enemy = _enemyFactory.CreateEnemy();

            player.X = 2;
            player.Y = 2;

            Console.Clear();
            Console.WriteLine($"=== Game Started with difficulty: [{Difficulty}] ===");
            Console.WriteLine($"В лесу появился: {enemy.Name}!");
            PrintInstructions();

            while (isRunning)
            {
                UpdateBuffs();
                HandleInput();
                Render();
                System.Threading.Thread.Sleep(50); 
            }

            Console.WriteLine("\n Игра завершена. Спасибо за игру!");
        }

        private void UpdateBuffs()
        {
            if (_fireBuffEndTime.HasValue && DateTime.Now > _fireBuffEndTime.Value)
            {
                // Сбрасываем оружие на базовое
                player.Weapon = new Sword(); 
                _fireBuffEndTime = null;
                LogMessage("\n--- Эффект огня иссяк ---");
            }
        }

        private void PrintInstructions()
        {
            Console.WriteLine("Управление: СТРЕЛОЧКИ — движение, SPACE — атака, ESC — выход");
            Console.WriteLine("------------------------------------------------------------");
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape) { isRunning = false; return; }
            int newX = player.X;
            int newY = player.Y;

            if (keyInfo.Key == ConsoleKey.UpArrow)    newY--;
            else if (keyInfo.Key == ConsoleKey.DownArrow)  newY++;
            else if (keyInfo.Key == ConsoleKey.LeftArrow)  newX--;
            else if (keyInfo.Key == ConsoleKey.RightArrow) newX++;
        
            else if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                if (enemy.Health > 0)
                {
                    enemy.Attack();
                
                    int damage = player.Weapon.GetDamage(); 
                    enemy.Health -= damage;
                    player.Score += 20;
                    LogMessage($"Удар на {damage} урона!");
                }
            }
            else if (keyInfo.Key == ConsoleKey.F)
            {
                ActivateFireBuff();
            }

            if (gameMap.IsInsideBounds(newX, newY) && (newX != player.X || newY != player.Y))
            {
                player.Move(newX - player.X, newY - player.Y);
            }
            }
        }

        private void SelectFactory()
        {
            switch (Difficulty)
            {
                case GameDifficulty.Easy:
                    _enemyFactory = new BearFactory();
                    break;
                case GameDifficulty.Hard:
                    _enemyFactory = new DarkFairyFactory();
                    break;
                default:
                    _enemyFactory = new BearFactory();
                    break;
            }
        }

        private void Render()
        {
            Console.SetCursorPosition(0, 5); 
            string buffStatus = _fireBuffEndTime.HasValue ? "[ОГОНЬ АКТИВЕН]" : "[Обычный]";
            Console.WriteLine($"Оружие: {player.Weapon.GetDescription()} {buffStatus}");
            
            Console.WriteLine($"Позиция игрока: X: {player.X}, Y: {player.Y} (Карта: {MapWidth}x{MapHeight})      ");
            Console.WriteLine($"Статус врага ({enemy.Name}): HP: {enemy.Health}   ");
            Console.WriteLine($"Твои очки: {player.Score}   ");
        }
        
        private void LogMessage(string message)
        {
            Console.SetCursorPosition(0, 12);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, 12);
            Console.Write(message);
        }
    }
}