using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Redux;
using WinstonPuckett.PipeExtensions;

public class BoardStateTest
{
    public void ConstructorTest()
    {
        Assert.AreEqual(
            null,
            new BoardState()
                .Pipe(state => state.Slots),
            "Slots are constructed as null."
        );
    }

    [Test]
    public void RotationOffsetTest()
    {
        Func<List<IAction>, int> reduceRotationOffset = (actions)
            => TestUtils.Reduce(BoardState.RotationOffsetRecuder, 0, actions);

        Assert.AreEqual(
            0,
            new List<IAction>() { }
                .Pipe(reduceRotationOffset),
            "Default rotation offset is 0."
        );

        Assert.AreEqual(
            1,
            new List<IAction>() {
                    new RotateLeftAction(),
                }
                .Pipe(reduceRotationOffset),
            "Offset is 1 after RotateLeftAction."
        );

        Assert.AreEqual(
            0,
            new List<IAction>() {
                    new RotateLeftAction(),
                    new RotateRightAction(),
                }
                .Pipe(reduceRotationOffset),
            "Offset is 0 after RotateLeftAction and RotateRightAction."
        );

        Assert.AreEqual(
            -1,
            new List<IAction>() {
                    new RotateLeftAction(),
                    new RotateRightAction(),
                    new RotateRightAction(),
                }
                .Pipe(reduceRotationOffset),
            "Offset is -1 after RotateLeftAction and 2xRotateRightAction."
        );
    }

    [Test]
    public void FlagSlotsTest()
    {
        Func<List<IAction>, bool[,]> reduceSlots = (actions)
            => TestUtils.Reduce(BoardState.SlotsReducer, GridUtils.CreateSlots(), actions);

        Assert.AreEqual(
            GridUtils.CreateSlots(),
            new List<IAction>() { }
                .Pipe(reduceSlots)
        );

        Assert.IsFalse(
            new List<IAction>() {
                new FlagSlotsAction(
                    new List<Slot>() {})}
                .Pipe(reduceSlots)
                .Pipe((state) => state[7, 9])
        );

        Assert.IsTrue(
            new List<IAction>() {
                new FlagSlotsAction(
                    new List<Slot>() {
                        new Slot(7, 9)
                    }
                )
            }
                .Pipe(reduceSlots)
                .Pipe((state) => state[7, 9])
        );
    }

    [Test]
    public void ResetTest()
    {
        Assert.AreEqual(
            new BoardState(),
            new List<IAction>() {
                new FlagSlotsAction(
                        new List<Slot>() {
                            new Slot(0, 0)
                        }
                    ),
                    new ResetAction()
                }
                .Pipe((actions) => TestUtils.Reduce(BoardState.Reducer, new BoardState(), actions))
        );
    }
}

public readonly struct TestUtils
{
    // NOTE: static methods cannot be marked readonly
    // But a lambda expression using Func cannot be generic...
    public static TState Reduce<TState>(
        Reducer<TState> reducer,
        TState initialState,
        IEnumerable<IAction> actions
    ) => actions.Aggregate(initialState, reducer.Invoke);
}
