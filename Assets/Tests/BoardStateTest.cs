using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Redux;

public class BoardStateTest
{
    [Test]
    public void RotationOffsetTest()
    {
        var actions = new List<IAction>();
        var initialState = 0;

        Assert.AreEqual(0, Reduce(BoardState.RotationOffsetRecuder, initialState, actions));

        actions.Add(new RotateLeftAction());

        Assert.AreEqual(1, Reduce(BoardState.RotationOffsetRecuder, initialState, actions));

        actions.Add(new RotateRightAction());

        Assert.AreEqual(0, Reduce(BoardState.RotationOffsetRecuder, initialState, actions));

        actions.Add(new RotateRightAction());

        Assert.AreEqual(-1, Reduce(BoardState.RotationOffsetRecuder, initialState, actions));
    }

    private static TState Reduce<TState>(
        Reducer<TState> reducer,
        TState initialState,
        IEnumerable<IAction> actions
    ) => actions.Aggregate(initialState, reducer.Invoke);
}
