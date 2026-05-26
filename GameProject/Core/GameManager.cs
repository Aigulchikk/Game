using System;
using System.Text;
using GameProject.Entities;
using GameProject.Factories;
using GameProject.Weapons;
using GameProject.Strategies;
using GameProject.UI;
using GameProject.Commands;
using System.IO;
using System.Text.Json;

namespace GameProject.Core
{
    public enum GameDifficulty { Easy, Medium, Hard }

    public class GameManager
    {
        private const int LogMessageYPosition = 11;
        private const int BuffCooldownSeconds = 60;
        private const int FireBuffDurationSeconds = 10;
        private const int AttackScorePoints = 20;

        private static GameManager? _instance;
        private bool isRunning = true;

        private bool _isPotionActive = false;
        private int _potionX;
        private int _potionY;

        private BattleFacade _battleFacade = new BattleFacade();

        private DateTime? _fireBuffEndTime = null; 
        private DateTime? _lastBuffActivationTime = null;
        private Player player = null!;
        private Map gameMap = null!;
        private EnemyFactory _enemyFactory = null!;

        private int _portalX = 70;
        private int _portalY = 8;
        private int _appleX = 10;
        private int _appleY = 10;
        private bool _isAppleActive = true;

        private List<Enemy> _enemies = new List<Enemy>();
        public List<Enemy> Enemies => _enemies;
        private int _enemiesKilled = 0;

        private IWeapon? _baseWeapon;

        private Random _random = new Random();

        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public GameDifficulty Difficulty { get; private set; }

        private InputManager _inputManager = new InputManager();

        private GameManager()
        {
            MapWidth = 75;
            MapHeight = 10;
            Difficulty = GameDifficulty.Easy;
            
            SelectFactory();
        }

        public static GameManager Instance => _instance ??= new GameManager();

        public void ActivateFireBuff()
        {
            if (_lastBuffActivationTime.HasValue && (DateTime.Now - _lastBuffActivationTime.Value).TotalSeconds < BuffCooldownSeconds)
            {
                double remaining = BuffCooldownSeconds - (DateTime.Now - _lastBuffActivationTime.Value).TotalSeconds;
                LogMessage($"[!] Бафф еще на перезарядке! Осталось: {remaining:F0} сек.");
                return;
            }

            if (!(player.Weapon is FireDamage)) 
            {
                _baseWeapon = player.Weapon;
                player.Weapon = new FireDamage(player.Weapon);
            }

            System.Diagnostics.Debug.WriteLine($"DEBUG: Активирую бафф для игрока: {player.GetHashCode()}");

            player.IsFireSwordActive = true;
            _lastBuffActivationTime = DateTime.Now;
            _fireBuffEndTime = DateTime.Now.AddSeconds(FireBuffDurationSeconds);
            
            LogMessage("[!] Бафф Огня активирован!");
        }

        private void SetupNextLevel()
        {

            player.X = 2;
            player.Y = 2;

            _portalX = MapWidth - 3; 
            _portalY = MapHeight - 3;

            _potionX = _random.Next(5, MapWidth - 5);
            _potionY = _random.Next(2, MapHeight - 2);
            _isPotionActive = true;

            _appleX = _random.Next(5, MapWidth - 5);
            _appleY = _random.Next(2, MapHeight - 2);
            _isAppleActive = true;

            _enemies.Clear();
            
            var fairy = new DarkFairy("Темная Фея", 50);
            fairy.X = _random.Next(20, MapWidth - 2);
            fairy.Y = _random.Next(2, MapHeight - 2);
            fairy.SetAttackStrategy(new RangedAttackStrategy());
            _enemies.Add(fairy);

            var bear = new Bear("Гризли", 80);
            bear.X = MapWidth - 5;
            bear.Y = MapHeight - 3;
            bear.SetAttackStrategy(new MeleeAttackStrategy());
            _enemies.Add(bear);

            LogMessage("Уровень обновлен: позиция стабилизирована!");
        }

        public void Run()
        {
        #if WINDOWS
            try 
            {
                Console.SetWindowSize(75, 15);
                Console.SetBufferSize(75, 15);
            }
            catch { }
        #endif

            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            InitializeGame();

            if (player == null)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine($"=== Game Started with difficulty: [{Difficulty}] ===");
            Console.WriteLine($"В лесу появились враги!");
            PrintInstructions();

            int _attackTimer = 0;

            while (isRunning)
            {
                _attackTimer++;

                for (int i = 0; i < _enemies.Count; i++)
                {
                    var e = _enemies[i];
                    if (e == null) continue;

                    e.DistanceToPlayer = Math.Sqrt(Math.Pow(e.X - player.X, 2) + Math.Pow(e.Y - player.Y, 2));

                    if (_attackTimer % 20 == 0)
                    {
                        e.PerformAttack(player);
                    }

                    if (e.ActiveProjectile != null)
                    {
                        if (e.ActiveProjectile.X < player.X) e.ActiveProjectile.X++;
                        else if (e.ActiveProjectile.X > player.X) e.ActiveProjectile.X--;

                        if (e.ActiveProjectile.Y < player.Y) e.ActiveProjectile.Y++;
                        else if (e.ActiveProjectile.Y > player.Y) e.ActiveProjectile.Y--;

                        if (e.ActiveProjectile.X == player.X && e.ActiveProjectile.Y == player.Y)
                        {
                            player.TakeDamage(2);
                            e.ActiveProjectile = null;
                        }
                        else if (e.ActiveProjectile.X <= 0 || e.ActiveProjectile.X >= MapWidth - 1 ||
                                e.ActiveProjectile.Y <= 0 || e.ActiveProjectile.Y >= MapHeight - 1)
                        {
                            e.ActiveProjectile = null;
                        }
                    }
                }

                UpdateEnemies();
                UpdateBuffs();
                HandleInput();

                if (CheckGameEnd())
                {
                    isRunning = false;
                    break;
                }

                if (player.X == _portalX && player.Y == _portalY)
                {
                    LogMessage("Ты перешел на следующий уровень!");
                    SetupNextLevel();
                    _attackTimer = 0;
                }

                if (_isPotionActive && player.X == _potionX && player.Y == _potionY)
                {
                    player.Health += 30;
                    _isPotionActive = false;
                    LogMessage("Ты выпил зелье! +30 HP!");
        #if WINDOWS
                    Console.Beep(800, 200);
        #endif
                }

                if (_isAppleActive && player.X == _appleX && player.Y == _appleY)
                {
                    player.Health += 10;
                    _isAppleActive = false;
                    LogMessage("Ты съел яблоко! +10 HP!");
                }

                Render();

                System.Threading.Thread.Sleep(50);
            }

            Console.Clear();
            Console.WriteLine("\nИгра завершена. Спасибо за игру!");
            Console.WriteLine("Нажми любую клавишу для выхода...");
            Console.ReadKey();
        }

        private void InitializeGame()
        {
            gameMap = new Map(MapWidth, MapHeight);
            player = new PlayerBuilder()
                .SetName("Рейнджер")
                .SetHealth(150)
                .SetLevel(1)
                .Build();

            _enemies.Clear();

            var fairy = new DarkFairy("Темная Фея", 50);
            fairy.X = 15;
            fairy.Y = 5;
            fairy.SetAttackStrategy(new RangedAttackStrategy());
            _enemies.Add(fairy);

            var bear = new Bear("Медведь", 100);
            bear.X = 10;
            bear.Y = 10;
            bear.SetAttackStrategy(new MeleeAttackStrategy());
            _enemies.Add(bear);

            _inputManager.BindCommand(ConsoleKey.Spacebar, new AttackCommand(player, this, _battleFacade, LogMessage));
            _inputManager.BindCommand(ConsoleKey.W, new MoveCommand(player, 0, -1));
            _inputManager.BindCommand(ConsoleKey.S, new MoveCommand(player, 0, 1));
            _inputManager.BindCommand(ConsoleKey.A, new MoveCommand(player, -1, 0));
            _inputManager.BindCommand(ConsoleKey.D, new MoveCommand(player, 1, 0));
            
            _inputManager.BindCommand(ConsoleKey.F, new DelegateCommand(ActivateFireBuff));
            _inputManager.BindCommand(ConsoleKey.F5, new DelegateCommand(SaveGame));
            _inputManager.BindCommand(ConsoleKey.F9, new DelegateCommand(LoadGame));

            player.X = 2;
            player.Y = 2;
            
            LogMessage("Команды привязаны!");
        }

        private void UpdateBuffs()
        {
            if (_fireBuffEndTime.HasValue && DateTime.Now > _fireBuffEndTime.Value)
            {
                if (_baseWeapon != null)
                {
                    player.Weapon = _baseWeapon;
                    _baseWeapon = null;
                }
                
                player.IsFireSwordActive = false;

                _fireBuffEndTime = null;
                LogMessage("\n--- Эффект огня иссяк ---");
            }
        }

        private void PrintInstructions()
        {
            Console.WriteLine("Управление: WASD — движение, SPACE — атака, ESC — выход");
            Console.WriteLine("------------------------------------------------------------");
        }

        private void UpdateEnemies()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                Enemy e = _enemies[i];

                if (e.Health <= 0)
                {
                    if (e.X >= 0 && e.Y >= 0)
                    {
                        Console.SetCursorPosition(e.X, e.Y);
                        Console.Write(" ");
                    }

                    if (e.ActiveProjectile != null && e.ActiveProjectile.X >= 0 && e.ActiveProjectile.Y >= 0)
                    {
                        Console.SetCursorPosition(e.ActiveProjectile.X, e.ActiveProjectile.Y);
                        Console.Write(" ");
                    }

                    if (e is DarkFairy)
                    {
                        SpawnPotion(e.X, e.Y);
                    }
                    
                    _enemies.RemoveAt(i);
                    
                    _enemiesKilled++; 
                    
                    LogMessage($"Враг {e.Name} побежден! (Всего: {_enemiesKilled})");
                }

                else
                {
                    e.PerformAttack(player);
                }
            }
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                System.Diagnostics.Debug.WriteLine($"DEBUG: Нажата клавиша {keyInfo.Key}");

                if (keyInfo.Key == ConsoleKey.Escape) 
                { 
                    isRunning = false; 
                    return; 
                }

                _inputManager.HandleInput(keyInfo.Key);
                Render();
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

        public void SpawnPotion(int x, int y)
        {
            _isPotionActive = true;
            _potionX = x;
            _potionY = y;
            LogMessage("Фея побеждена! Она оставила целебное зелье 🧪");
        }

        private void DrawMap()
        {

            for (int y = 0; y < MapHeight; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("@");
                
                Console.SetCursorPosition(MapWidth - 1, y);
                Console.Write("@");
            }

            Console.SetCursorPosition(0, 0);
            for (int x = 0; x < MapWidth; x++) Console.Write("@");
            
            Console.SetCursorPosition(0, MapHeight - 1);
            for (int x = 0; x < MapWidth; x++) Console.Write("@");
        }

        private void Render()
        {
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    if (x == 0 || x == MapWidth - 1 || y == 0 || y == MapHeight - 1)
                        sb.Append("@");
                    else
                        sb.Append(" ");
                }
                sb.AppendLine();
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(sb.ToString());

            DrawObject(_portalX, _portalY, "🖼");
            if (_isPotionActive)
            {
                DrawObject(_potionX, _potionY, "🧪");
            }

            if (_isAppleActive)
            {
                DrawObject(_appleX, _appleY, "🍎");
            }

            if (player != null)
            {
                if (player.IsFireSwordActive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    DrawObject(Math.Max(1, player.X - 1), player.Y, "🗡");
                    Console.ResetColor();
                }
                Console.ForegroundColor = player.IsFireSwordActive ? ConsoleColor.Yellow : ConsoleColor.White;
                DrawObject(player.X, player.Y, "🧝");
                Console.ResetColor();
            }

            foreach (var e in _enemies)
            {
                DrawObject(e.X, e.Y, e is DarkFairy ? "🧚" : "🐻");
                if (e.ActiveProjectile != null)
                    DrawObject(e.ActiveProjectile.X, e.ActiveProjectile.Y, "✨");
            }

            Console.SetCursorPosition(0, MapHeight);
            string buff = (player != null && player.IsFireSwordActive) ? " | 🗡 ОГОНЬ" : "";
            string ui = $"Игрок: {player?.Name ?? "???"} | HP: {player?.Health ?? 0} | Врагов: {_enemies.Count}{buff}";
            Console.Write(ui.PadRight(MapWidth));
        }

        private void DrawObject(int x, int y, string symbol)
        {
            if (x > 0 && x < MapWidth - 1 && y > 0 && y < MapHeight - 1)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
            }
        }
   
        private void LogMessage(string message)
        {
            Console.SetCursorPosition(0, LogMessageYPosition);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, LogMessageYPosition);
            Console.Write(message);
        }

        public void SaveGame()
        {
            var data = player.ToSaveData();
            
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("save.json", json);
            
            LogMessage("\n[!] Игра сохранена!");
        }

        public void LoadGame()
        {
            if (!File.Exists("save.json"))
            {
                LogMessage("\n[!] Файл сохранения не найден.");
                return;
            }

            string json = File.ReadAllText("save.json");
            var data = JsonSerializer.Deserialize<SaveData>(json);

            if (data != null)
            {
                player.LoadFromSaveData(data);
                
                LogMessage("\n[!] Игра загружена!");
            }
        }

        private bool CheckGameEnd()
        {
            if (player.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine("Вы умерли, игра окончена!");
                return true;
            }

            if (_enemiesKilled >= 20)
            {
                Console.Clear();
                Console.WriteLine("Вы победили всех врагов! Вы выиграли!");
                return true;
            }

            return false;
        }
    }
}