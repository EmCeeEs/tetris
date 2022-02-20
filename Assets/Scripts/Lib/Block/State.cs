using System;
using System.Collections.Generic;

using Redux;
using System.Linq;

public class Block
{
    private readonly List<Slot> Layout;
    private readonly Point Position;

    public Block(
        Point position,
        List<Slot> layout)
    {
        Position = position;
        Layout = layout;
    }

    public readonly static Func<Block, List<Slot>> selectLayout =
        (state) => state.Layout;

    public readonly static Func<Block, Point> selectPosition =
        (state) => state.Position;
}

public class BlockState : IState
{
    private readonly List<Block> Blocks;

    public BlockState(List<Block> blocks = null)
    {
        Blocks = blocks ?? new List<Block>();
    }

    public readonly static Reducer<BlockState> Reducer = (state, action) =>
        action switch
        {
            NewBlockAction _action => new BlockState(
                    state.Blocks.Append(_action.block).ToList()
                ),
            _ => state,
        };

    public readonly static Func<BlockState, List<Block>> selectBlocks = (state) => state.Blocks;
}

public class NewBlockAction : IAction
{
    public readonly Block block;

    public NewBlockAction(Block newBlock)
    {
        block = newBlock;
    }
}

namespace PolarCoordinates
{
    public class Point
    {
        public readonly float X;
        public readonly float Y;

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point a, Point b)
            => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b)
            => new Point(a.X - b.X, a.Y - b.Y);

        public override string ToString() => $"({X}, {Y})";
    }

}
