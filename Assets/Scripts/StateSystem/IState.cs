using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateSystem
{
    public interface IState<TState>
        where TState : IState<TState>
    {

        void OnEnter();
        void OnExit();

        //void BeginDrag();

        //void Dropped();

        //void EnteredTile();

        StateMachine<TState> StateMachine { get; }

    }
}
