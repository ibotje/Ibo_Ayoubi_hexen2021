using StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.GameStates 
{
    class GameOverGameState : GameStateBase
    {
        private GameObject _startMenu;
        private GameObject _gameOverText;
        public GameOverGameState(StateMachine<GameStateBase> stateMachine, GameObject StartMenu, GameObject GameOverText) : base(stateMachine)
        {
            _startMenu = StartMenu;
            _gameOverText = GameOverText;
        }

        public override void OnEnter()
        {
            _startMenu.SetActive(true);
            _gameOverText.SetActive(true);
        }
    }
}