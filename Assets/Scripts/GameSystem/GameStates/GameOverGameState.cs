using StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.GameStates 
{
    class GameOverGameState : GameStateBase
    {

        public GameOverGameState(StateMachine<GameStateBase> stateMachine) : base(stateMachine)
        {
            //_replayManager = replayManager;
        }

        public override void OnEnter()
        {
            //Backward();
        }
    }
}