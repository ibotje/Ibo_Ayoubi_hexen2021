using BoardSystem;
using GameSystem.Views;
using StateSystem;

namespace GameSystem.GameStates
{
    abstract class GameStateBase : IState<GameStateBase>
    {
        public const string PlayingState = "playing";
        public const string StartState = "Starting";
        public const string GameOverState = "GameOver";

        public StateMachine<GameStateBase> StateMachine => _stateMachine;

        private StateMachine<GameStateBase> _stateMachine;

        protected GameStateBase(StateMachine<GameStateBase> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void BeginDrag(CardView cardView) { }

        public virtual void Dropped(Tile hoverTile) { }

        public virtual void EnteredTile(Tile hoverTile) { }

        public virtual void ExitedTile(Tile hoverTile) { }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void Ended() { }

        public virtual void Started() { }

        //public virtual void Select(Piece<Tile> piece) { }

        //public virtual void Select(Tile tile) { }
    }
}
