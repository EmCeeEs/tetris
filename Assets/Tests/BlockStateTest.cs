using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Redux;
using WinstonPuckett.PipeExtensions;

public class BlockStateTest
{
    [Test]
    public void InvertXTest()
    {
        Func<IEnumerable<IAction>, BlockLogic.State> reduceWithNewBlockState = (actions)
           => Reduce(BlockLogic.Reducer.Root, new BlockLogic.State(), actions);

        Assert.IsFalse(
            new List<IAction>() { }
                .Pipe(reduceWithNewBlockState)
                .Pipe(BlockLogic.Selector.GetInvertX),
            "By default, block should not be inverted at X axis."
        );

        Assert.IsTrue(
            new List<IAction>() {
                    new BlockLogic.InvertXAction()
                }
                .Pipe(reduceWithNewBlockState)
                .Pipe(BlockLogic.Selector.GetInvertX),
            "InvertXAction should set inversion flag for X axis."
        );

        Assert.IsFalse(
            new List<IAction>(){
                    new BlockLogic.InvertXAction(),
                    new BlockLogic.InvertXAction()
                }
                .Pipe(reduceWithNewBlockState)
                .Pipe(BlockLogic.Selector.GetInvertX),
            "Second InvertXAction should reset inversion flag for X axis."
        );
    }

    [Test]
    public void InvertYTest()
    {
        Func<IEnumerable<IAction>, BlockLogic.State> reduceWithNewBlockState = (actions)
           => Reduce(BlockLogic.Reducer.Root, new BlockLogic.State(), actions);

        Assert.IsFalse(
            new List<IAction>() { }
                .Pipe(reduceWithNewBlockState)
                .Pipe(BlockLogic.Selector.GetInvertY),
            "By default, block should not be inverted at Y axis."
        );

        Assert.IsTrue(
            new List<IAction>() {
                    new BlockLogic.InvertYAction()
                }
                .Pipe(reduceWithNewBlockState)
                .Pipe(BlockLogic.Selector.GetInvertY),
            "InvertYAction should set inversion flag for Y axis."
        );

        Assert.IsFalse(
            new List<IAction>(){
                    new BlockLogic.InvertYAction(),
                    new BlockLogic.InvertYAction()
                }
                .Pipe(reduceWithNewBlockState)
                .Pipe(BlockLogic.Selector.GetInvertY),
            "Second InvertYAction should reset inversion flag for Y axis."
        );
    }

    private static TState Reduce<TState>(
        ReducerDelegate<TState> reducer,
        TState initialState,
        IEnumerable<IAction> actions
    ) => actions.Aggregate(initialState, reducer.Invoke);
}
