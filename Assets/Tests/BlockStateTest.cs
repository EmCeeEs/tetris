using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Redux;

public class BlockStateTest
{
    private static TState Reduce<TState>(
        ReducerDelegate<TState> reducer,
        TState initialState,
        IEnumerable<IAction> actions
    ) => actions.Aggregate(initialState, reducer.Invoke);

    [Test]
    public void InvertXTest()
    {
        BlockLogic.State blockState;
        Func<BlockLogic.State, bool> IsInvertedX = (state) => state.InvertX;

        blockState = Reduce(
            BlockLogic.Reducer.Root,
            new BlockLogic.State(),
            new List<IAction>()
        );

        Assert.IsFalse(IsInvertedX(blockState));

        blockState = Reduce(
            BlockLogic.Reducer.Root,
            new BlockLogic.State(),
            new List<IAction>() {
                new BlockLogic.InvertXAction()
            }
        );

        Assert.IsTrue(IsInvertedX(blockState));

        blockState = Reduce(
            BlockLogic.Reducer.Root,
            new BlockLogic.State(),
            new List<IAction>() {
                new BlockLogic.InvertXAction(),
                new BlockLogic.InvertXAction()
            }
        );

        Assert.IsFalse(IsInvertedX(blockState));
    }
}
