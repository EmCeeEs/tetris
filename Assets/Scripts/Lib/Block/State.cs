using System;

using Redux;

namespace BlockLogic
{
    #region STATE
    public readonly struct State : IState
    {
        public readonly bool InvertX;
        public readonly bool InvertY;

        public State(bool invertX = false, bool invertY = false)
        {
            InvertX = invertX;
            InvertY = invertY;
        }
    }
    #endregion

    #region ACTIONS
    public readonly struct InvertXAction : IAction
    {
    };

    public readonly struct InvertYAction : IAction
    {
    };
    #endregion

    #region REDUCER
    public readonly struct Reducer
    {
        public static ReducerDelegate<State> Root = (state, action) =>
            action switch
            {
                InvertXAction _action => new State(!state.InvertX, state.InvertY),
                InvertYAction _action => new State(state.InvertX, !state.InvertY),
                _ => state,
            };
    }
    #endregion

    #region SELECTORS
    public readonly struct Selector
    {
        public static Func<State, bool> GetInvertX = (state) => state.InvertX;
        public static Func<State, bool> GetInvertY = (state) => state.InvertY;
    }
    #endregion
}
