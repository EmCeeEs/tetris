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
    public void AddBlockTest()
    {
        Block blockFromState =
            new List<IAction>() {
                    new AddBlockAction(BLOCK_1)
                }
                .Pipe(Reduce(BlockState.Reducer, new BlockState()))
                .Pipe(BlockState.selectBlocks)
                .Pipe(Enumerable.First);

        Assert.AreEqual(
            Block.selectPosition(blockFromState),
            Block.selectPosition(BLOCK_1),
            "Block position is equal."
        );

        Assert.AreEqual(
            Block.selectLayout(blockFromState),
            Block.selectLayout(BLOCK_1),
            "Block layout is equal."
        );

        Assert.AreNotEqual(
            blockFromState,
            BLOCK_1,
            "Blocks are not equal (hold different references to layout)."
        );

        Assert.AreNotSame(
            blockFromState,
            BLOCK_1,
            "Blocks are not the same (have distinct references)."
        );
    }

    public void AddMultipleBlocksTest()
    {
        int numberOfBlocksInState =
            new List<IAction>() {
                    new AddBlockAction(BLOCK_1),
                    new AddBlockAction(BLOCK_2),
                    new AddBlockAction(BLOCK_3)
                }
                .Pipe(Reduce(BlockState.Reducer, new BlockState()))
                .Pipe(BlockState.selectBlocks)
                .Pipe(Enumerable.Count);

        Assert.AreEqual(
            3,
            numberOfBlocksInState,
            "State contains exactly three blocks."
        );
    }

    readonly Block BLOCK_1 = new Block(
        position: new Point(10, 0),
        layout: new List<GridPoint>() {
            new GridPoint(0, 0),
            new GridPoint(1, 0)
        }
    );

    readonly Block BLOCK_2 = new Block(
        position: new Point(10, 10),
        layout: new List<GridPoint>(){
            new GridPoint(0, 0),
            new GridPoint(0, 1),
            new GridPoint(1, 0),
            new GridPoint(1, 1)
        }
    );

    readonly Block BLOCK_3 = new Block(
        position: new Point(0, 0),
        layout: new List<GridPoint>(){
                new GridPoint(0, 0),
        }
    );

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

    private static Func<IEnumerable<IAction>, TState> Reduce<TState>(
        Reducer<TState> reducer,
        TState initialState
    ) => (
        actions
    ) => actions.Aggregate(initialState, reducer.Invoke);
}
