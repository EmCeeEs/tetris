
namespace Redux
{
    public interface IAction { };
    public interface IState { };

    public delegate void OnDispatchCallback<TState>(TState state);
    public delegate TState Reducer<TState>(TState state, IAction action);

    public interface IStore<TState>
    {
        public TState GetState();
        public void Dispatch(IAction action);
        public void Subscribe(OnDispatchCallback<TState> callback);
    };

    public class Store<TState> : IStore<TState>
    {
        private TState state;

        private OnDispatchCallback<TState> OnDispatch;
        private readonly Reducer<TState> Reducer;

        public Store(Reducer<TState> reducer, TState initialState)
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

        public void Subscribe(OnDispatchCallback<TState> callback)
        {
            OnDispatch += callback;
        }
    }
}
