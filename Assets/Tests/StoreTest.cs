using System.Collections.Generic;
using NUnit.Framework;

using Redux;

public class StoreTest
{
    private class DummyAction : IAction { };

    private struct ComplexState : IState
    {
        public int integerProperty;
        public bool booleanProperty;
        public List<string> listOfStringsProperty;
    }

    [Test]
    public void GetStateTest()
    {
        ComplexState initialState = new ComplexState()
        {
            integerProperty = 3,
            booleanProperty = true,
            listOfStringsProperty = new List<string>() { "hi", "there" },
        };

        var store = new Store<ComplexState>((state, action) => state, initialState);

        Assert.AreEqual(initialState, store.GetState());
    }

    [Test]
    public void DispatchTest()
    {
        int callCount = 0;
        ReducerDelegate<int> mockReducer = (state, action) =>
            {
                callCount++;
                return state;
            };

        var store = new Store<int>(mockReducer, 0);

        Assert.AreEqual(0, callCount);

        store.Dispatch(new DummyAction());

        Assert.AreEqual(1, callCount);

        store.Dispatch(new DummyAction());
        store.Dispatch(new DummyAction());

        Assert.AreEqual(3, callCount);
    }

    [Test]
    public void SubscribeTest()
    {
        int callCount = 0;
        OnDispatchDelegate<int> mockOnDispatch = (state) =>
            {
                callCount++;
            };

        var store = new Store<int>((state, action) => state, 0);

        Assert.AreEqual(0, callCount);

        store.Subscribe(mockOnDispatch);
        store.Dispatch(new DummyAction());

        Assert.AreEqual(1, callCount);

        store.Subscribe(mockOnDispatch);
        store.Dispatch(new DummyAction());

        Assert.AreEqual(3, callCount);
    }
}
