using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Redux;
using WinstonPuckett.PipeExtensions;
using PolarCoordinates;

public class BlockStateTest
{
    [Test]
    public void ConstructorTest()
    {
        Assert.IsEmpty(
            new BlockState()
                .Pipe(BlockState.selectBlocks),
            "Constructor initializes empty list of blocks."
        );
    }

    [Test]
    public void NewBlockActionTest()
    {
        var block = new Block
        (
            new Point(10, 0),
            new List<Slot>(){
                new Slot(0, 0),
                new Slot(1, 0)
            }
        );

        Assert.AreEqual(
            block,
            new List<IAction>() {
                    new NewBlockAction(block)
                }
                .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
                .Pipe(BlockState.selectBlocks)
                .Pipe(Enumerable.First),
            "Block layout is set correctly."
        );

        // Assert.AreEqual(
        //     spawnPoint1,
        //     new List<IAction>() {
        //             new NewBlockAction(spawnPoint1, layout1)
        //         }
        //         .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
        //         .Pipe(BlockState.selectPosition),
        //     "Block position is set correctly."
        // );

        // var spawnPoint2 = new Point(8, 8);
        // var layout2 = new List<Slot>(){
        //     new Slot(0, 0),
        //     new Slot(0, 1),
        //     new Slot(1, 0),
        //     new Slot(1, 1)
        // };

        // Assert.AreEqual(
        //     layout2,
        //     new List<IAction>() {
        //             new NewBlockAction(spawnPoint1, layout1),
        //             new NewBlockAction(spawnPoint2, layout2)
        //         }
        //         .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
        //         .Pipe(BlockState.selectLayout),
        //     "New block layout is set correctly"
        // );

        // Assert.AreEqual(
        //     spawnPoint2,
        //     new List<IAction>() {
        //             new NewBlockAction(spawnPoint1, layout1),
        //             new NewBlockAction(spawnPoint2, layout2)
        //         }
        //         .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
        //         .Pipe(BlockState.selectPosition),
        //     "New block position is set correctly."
        // );
    }

    // [Test]
    // public void MoveBlocksActionTest()
    // {
    //     var spawnPoint = new Point(10, 0);
    //     var layout = new List<Slot>(){
    //         new Slot(0, 0),
    //         new Slot(1, 0)
    //     };
    //     Func<Point, float> selectX = (point) => point.X;

    //     Assert.AreEqual(
    //         new Point(9, 0)
    //             .Pipe(selectX),
    //         new List<IAction>() {
    //                 new NewBlockAction(spawnPoint, layout),
    //                 new MoveBlocksAction(new Point(1, 0))
    //             }
    //             .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
    //             .Pipe(BlockState.selectPosition)
    //             .Pipe(selectX),
    //         "Block is moved down by one slot."
    //     );

    //     Assert.AreEqual(
    //         new Point(9, 0)
    //             .Pipe(selectX),
    //         new List<IAction>() {
    //                 new MoveBlocksAction(new Point(1, 0))
    //             }
    //             .Pipe((actions) => Reduce(BlockState.Reducer, new BlockState(), actions))
    //             .Pipe(BlockState.selectPosition)
    //             .Pipe(selectX),
    //         "Block is moved down by one slot."
    //     );
    // }

    private static TState Reduce<TState>(
        Reducer<TState> reducer,
        TState initialState,
        IEnumerable<IAction> actions
    ) => actions.Aggregate(initialState, reducer.Invoke);
}
