using StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.GameStates 
{
    class StartGameState : GameStateBase
    {
        private GameObject _startMenu;
        private GameObject _startButton;
        private GameObject _deck;
        public StartGameState(StateMachine<GameStateBase> stateMachine, GameObject StartMenu, GameObject StartButton, GameObject Deck) : base(stateMachine)
        {
            _startMenu = StartMenu;
            _startButton = StartButton;
            _deck = Deck;
        }
        public override void Started()
        {
            StateMachine.MoveTo(PlayingState);
        }

        public override void OnEnter()
        {
            _deck.SetActive(false);
        }

        public override void OnExit()
        {
            _startButton.SetActive(false);
            _startMenu.SetActive(false);
            _deck.SetActive(true);
        }


    }
}