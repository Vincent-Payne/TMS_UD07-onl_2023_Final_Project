using System;
using System.Collections;
using UnityEngine;

namespace Promenade.Scripts.GamePhysics.UI
{
    public class GameState : BaseUIState
    {
        public override void Enter()
        {
            base.Enter();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                _UIManager.SwitchTo(typeof(PauseState));
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}