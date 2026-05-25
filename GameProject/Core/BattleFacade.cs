using GameProject.Entities;

namespace GameProject.Core
{
    public class BattleFacade
    {
        public void ExecuteAttack(Player player, Enemy enemy, Action<string> logAction)
        {
            if (enemy.Health <= 0) return;

            int damage = player.Weapon.GetDamage();
            enemy.Health -= damage;
            player.Score += 20;
            logAction($"Удар на {damage} урона!");

            if (enemy.Health <= 0)
            {
                GameManager.Instance.SpawnPotion(enemy.X, enemy.Y);
                
                enemy.X = -1;
                enemy.Y = -1;
            }
        }
    }
}