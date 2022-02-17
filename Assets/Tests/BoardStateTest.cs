using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Redux;

public class BoardLogicTest
{
    [Test]
    public void RotateTest()
    {
        var actions = new List<IAction>();
        var initialState = 0;

        Assert.AreEqual(0, Reduce(BoardLogic.Reducer.RotationOffset, initialState, actions));

        actions.Add(new BoardLogic.RotateLeftAction());

        Assert.AreEqual(1, Reduce(BoardLogic.Reducer.RotationOffset, initialState, actions));

        actions.Add(new BoardLogic.RotateRightAction());

        Assert.AreEqual(0, Reduce(BoardLogic.Reducer.RotationOffset, initialState, actions));

        actions.Add(new BoardLogic.RotateRightAction());

        Assert.AreEqual(-1, Reduce(BoardLogic.Reducer.RotationOffset, initialState, actions));
    }

    private static TState Reduce<TState>(
        Reducer<TState> reducer,
        TState initialState,
        IEnumerable<IAction> actions
    ) => actions.Aggregate(initialState, reducer.Invoke);
}
