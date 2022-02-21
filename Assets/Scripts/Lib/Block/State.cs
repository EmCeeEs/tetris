using System;
using System.Linq;
using System.Collections.Generic;

using Redux;
using PolarCoordinates;

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
            AddBlockAction _action => new BlockState(
                    state.Blocks.Append(_action.block.Copy()).ToList()
                ),
            _ => state,
        };

    public readonly static Func<BlockState, List<Block>> selectBlocks =
        (state) => state.Blocks;
}

public class Block
{
    private readonly List<GridPoint> Layout;
    private readonly Point Position;

    public Block(
        Point position,
        List<GridPoint> layout)
    {
        Position = position;
        Layout = layout;
    }

    public readonly static Func<Block, List<GridPoint>> selectLayout =
        (state) => state.Layout;

    public readonly static Func<Block, Point> selectPosition =
        (state) => state.Position;

    public Block Copy() => new Block(
        Position,
        Layout.Select(slot => new GridPoint(slot.X, slot.Y)).ToList()
    );
}

namespace PolarCoordinates
{
    public readonly struct Point
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

        public override string ToString()
            => $"({X}, {Y})";
    }

    public readonly struct GridPoint
    {
        public readonly int X;
        public readonly int Y;

        public GridPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static GridPoint operator +(GridPoint a, GridPoint b)
            => new GridPoint(a.X + b.X, a.Y + b.Y);

        public static GridPoint operator -(GridPoint a, GridPoint b)
            => new GridPoint(a.X - b.X, a.Y - b.Y);

        public override string ToString()
            => $"({X}, {Y})";
    }
}
