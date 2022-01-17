using StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.GameStates 
{
    class StartGameState : GameStateBase
    {

        public StartGameState(StateMachine<GameStateBase> stateMachine) : base(stateMachine)
        {

        }
        public override void Started()
        {
            StateMachine.MoveTo(PlayingState);
        }

        public override void OnExit()
        {

        }


    }
}