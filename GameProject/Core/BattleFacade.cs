using GameProject.Entities;

namespace GameProject.Core
{
    public class BattleFacade
    {
        public void ExecuteAttack(Player player, Enemy enemy, Action<string> logAction)
        {
            if (enemy.Health <= 0) return;
            enemy.Attack();
            int damage = player.Weapon.GetDamage();
            enemy.Health -= damage;
            player.Score += 20;
            logAction($"Удар на {damage} урона!");
        }
    }
}