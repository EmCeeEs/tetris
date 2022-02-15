using System.Collections.Generic;
using NUnit.Framework;

using System.Linq;

public class ReducerTest
{
    private static TState Reduce<TState>(
        ReducerDelegate<TState> reducer,
        TState initialState,
        IEnumerable<IAction> actions
    )
        => actions.Aggregate(initialState, reducer.Invoke);

    [Test]
    public void RotateTest()
    {
        var actions = new List<IAction>();
        var initialState = 0;

        Assert.AreEqual(0, Reduce(Reducer.rotate, initialState, actions));

        actions.Add(new RotateAction() { NumberOfRotations = 1 });

        Assert.AreEqual(1, Reduce(Reducer.rotate, initialState, actions));

        actions.Add(new RotateAction() { NumberOfRotations = -2 });

        Assert.AreEqual(-1, Reduce(Reducer.rotate, initialState, actions));

    }

    [Test]
    public void InvertTest()
    {
        var actions = new List<IAction>();
        var initialState = true;

        Assert.AreEqual(true, Reduce(Reducer.invert, initialState, actions));

        actions.Add(new InvertAction());

        Assert.AreEqual(false, Reduce(Reducer.invert, initialState, actions));

        actions.Add(new InvertAction());

        Assert.AreEqual(true, Reduce(Reducer.invert, initialState, actions));
    }
}
