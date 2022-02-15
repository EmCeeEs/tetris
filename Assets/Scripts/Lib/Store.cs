
namespace Redux
{
    public interface IAction { };
    public interface IState { };

    public delegate void OnDispatchDelegate<TState>(TState state);
    public delegate TState ReducerDelegate<TState>(TState state, IAction action);

    public interface IStore<TState>
    {
        public TState GetState();
        public void Dispatch(IAction action);
        public void Subscribe(OnDispatchDelegate<TState> callback);
    };

    public class Store<TState> : IStore<TState>
    {
        private TState state;

        private OnDispatchDelegate<TState> OnDispatch;
        private readonly ReducerDelegate<TState> Reducer;

        public Store(ReducerDelegate<TState> reducer, TState initialState)
        {
            Reducer = reducer;
            state = initialState;
        }

        public TState GetState() => state;

        public void Dispatch(IAction action)
        {
            state = Reducer(state, action);
            OnDispatch?.Invoke(state);
        }

        public void Subscribe(OnDispatchDelegate<TState> callback)
        {
            OnDispatch += callback;
        }
    }
}
