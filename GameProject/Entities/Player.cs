using System;
using GameProject.Weapons;
using GameProject.Entities;
using GameProject.Core;

namespace GameProject.Entities
{
    public class Player : Entity
    {
        public event Action<int>? OnHealthChanged;

        public Player(string name, int health) : base(name, health) 
        {
            Weapon = new Sword();
        }

        public void Attack(Enemy enemy, BattleFacade battleFacade, Action<string> logAction)
        {
            if (enemy.Health > 0)
            {
                battleFacade.ExecuteAttack(this, enemy, logAction);
                if (enemy.Health > 0) enemy.PerformAttack(this);
                else logAction($"\n[!] {enemy.Name} повержен!");
            }
            else 
            {
                logAction("\n[!] Здесь никого нет, ты бьешь воздух!");
            }
        }
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            if (Health < 0) Health = 0;

            OnHealthChanged?.Invoke(Health); 
        }

        public void Heal(int amount)
        {
            Health += amount;

            OnHealthChanged?.Invoke(Health); 
        }

        public void UseItem(object item)
        {

        }

        public IWeapon Weapon { get; set; }

        public int Level { get; set; } = 1;
        public int Score { get; set; } = 0;

        public void Move(int dx, int dy) 
        { 
            X += dx; 
            Y += dy;
            
            Console.SetCursorPosition(0, 16);
            Console.Write($"PLAYER DEBUG: Новые координаты: {X}, {Y}      ");
        }

        public SaveData ToSaveData() 
        {
            return new SaveData 
            {
                PlayerX = this.X,
                PlayerY = this.Y,
                Health = this.Health,
                Level = this.Level,
                Score = this.Score
            };
        }

        public void LoadFromSaveData(SaveData data) 
        {
            this.X = data.PlayerX;
            this.Y = data.PlayerY;
            this.Health = data.Health;
            this.Level = data.Level;
            this.Score = data.Score;
            
            OnHealthChanged?.Invoke(this.Health);
        }
    }
}