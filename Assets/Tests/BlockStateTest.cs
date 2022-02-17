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
        Assert.IsFalse(
            new List<IAction>() { }
                .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
                .Pipe(BlockState.GetInvertX),
            "By default, block should not be inverted at X axis."
        );

        Assert.IsTrue(
            new List<IAction>() {
                    new InvertXAction()
                }
                .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
                .Pipe(BlockState.GetInvertX),
            "InvertXAction should set inversion flag for X axis."
        );

        Assert.IsFalse(
            new List<IAction>(){
                    new InvertXAction(),
                    new InvertXAction()
                }
                .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
                .Pipe(BlockState.GetInvertX),
            "Second InvertXAction should reset inversion flag for X axis."
        );
    }

    [Test]
    public void InvertYTest()
    {
        Assert.IsFalse(
            new List<IAction>() { }
                .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
                .Pipe(BlockState.GetInvertY),
            "By default, block should not be inverted at Y axis."
        );

        Assert.IsTrue(
            new List<IAction>() {
                    new InvertYAction()
                }
                .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
                .Pipe(BlockState.GetInvertY),
            "InvertYAction should set inversion flag for Y axis."
        );

        Assert.IsFalse(
            new List<IAction>(){
                    new InvertYAction(),
                    new InvertYAction()
                }
                .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
                .Pipe(BlockState.GetInvertY),
            "Second InvertYAction should reset inversion flag for Y axis."
        );
    }

    private static TState Reduce<TState>(
        Reducer<TState> reducer,
        TState initialState,
        IEnumerable<IAction> actions
    ) => actions.Aggregate(initialState, reducer.Invoke);
}
