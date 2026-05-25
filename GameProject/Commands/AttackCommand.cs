using GameProject.Entities;
using System;
using GameProject.Core;

namespace GameProject.Commands
{
    public class AttackCommand : ICommand
    {
        private Player _player;
        private Enemy _enemy;
        private BattleFacade _battleFacade;
        private Action<string> _logAction;

        public AttackCommand(Player player, Enemy enemy, BattleFacade battleFacade, Action<string> logAction)
        {
            _player = player;
            _enemy = enemy;
            _battleFacade = battleFacade;
            _logAction = logAction;
        }

        public void Execute()
        {
            _player.Attack(_enemy, _battleFacade, _logAction);
        }
    }
}