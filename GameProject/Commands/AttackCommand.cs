using GameProject.Entities;
using GameProject.Core;
using System;
using System.Collections.Generic;

namespace GameProject.Commands
{
    public class AttackCommand : ICommand
    {
        private Player _player;
        private GameManager _gameManager; 
        private BattleFacade _battleFacade;
        private Action<string> _logAction;

        public AttackCommand(Player player, GameManager gameManager, BattleFacade battleFacade, Action<string> logAction)
        {
            _player = player;
            _gameManager = gameManager; 
            _battleFacade = battleFacade;
            _logAction = logAction;
        }

        public void Execute()
        {
            var enemies = _gameManager.Enemies; 
            
            Enemy? target = null;
            double minDistance = 3.0; 

            foreach (var enemy in enemies)
            {
                double dist = Math.Sqrt(Math.Pow(enemy.X - _player.X, 2) + Math.Pow(enemy.Y - _player.Y, 2));
                
                if (dist < minDistance)
                {
                    target = enemy;
                    minDistance = dist;
                }
            }

            if (target != null)
            {
                _player.Attack(target, _battleFacade, _logAction);
                _logAction($"Атака! {target.Name} (HP: {target.Health}) в зоне поражения.");
            }
            else
            {
                _logAction("Рядом нет врагов для атаки!");
            }
        }
    }
}