using System.Linq;

namespace Redux
{
    public interface IAction { };
    public interface IState { };

    public delegate IAction Dispatch(IAction action);
    public delegate void OnDispatch();
    public delegate TState Reducer<TState>(TState state, IAction action);
    public delegate Dispatch Middleware<TState>(IStore<TState> store, Dispatch next);

    public interface IStore<TState>
    {
        public TState GetState();
        public IAction Dispatch(IAction action);
        public void Subscribe(OnDispatch callback);
    };

    public class Store<TState> : IStore<TState>
    {
        private readonly object syncLock = new object();
        private TState state;

        private OnDispatch onDispatch;
        private readonly Reducer<TState> reducer;
        private readonly Dispatch dispatch;

        public Store(
            Reducer<TState> stateReducer,
            TState initialState,
            params Middleware<TState>[] middlewares)
        {
            reducer = stateReducer;
            dispatch = ApplyMiddlewares<TState>(middlewares);
            state = initialState;
        }

        public TState GetState()
            => state;

        public IAction Dispatch(IAction action)
            => dispatch(action);

        public void Subscribe(OnDispatch callback)
            => onDispatch += callback;

        private Dispatch ApplyMiddlewares<TSTate>(params Middleware<TState>[] middlewares)
        {
            Dispatch next = BaseDispatch;

            foreach (var middleware in middlewares.Reverse())
                next = middleware(this, next);

            return next;
        }

        private IAction BaseDispatch(IAction action)
        {
            lock (syncLock)
                state = reducer(state, action);

            onDispatch?.Invoke();
            return action;
        }
    }
}
