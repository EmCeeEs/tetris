using System;
using System.Collections.Generic;

using Redux;

namespace BoardLogic
{
    #region STATE
    public readonly struct State : IState
    {
        public readonly int RotationOffset;
        public readonly bool[,] Slots;

        public State(
            int? rotationOffset = null,
            bool[,] slots = null
        )
        {
            RotationOffset = rotationOffset ?? 0;
            Slots = slots ?? Utils.CreateSlots();
        }
    }
    #endregion

    #region ACTIONS
    public readonly struct ResetAction : IAction
    {
    }

    public readonly struct RotateLeftAction : IAction
    {
    }

    public readonly struct RotateRightAction : IAction
    {
    }

    public readonly struct FlagSlotsAction : IAction
    {
        public readonly List<Slot> Slots;

        public FlagSlotsAction(List<Slot> slots)
        {
            Slots = slots;
        }
    }

    public readonly struct DeleteRowAction : IAction
    {
        public readonly int RowNumber;

        public DeleteRowAction(int rowNumber)
        {
            RowNumber = rowNumber;
        }
    }
    #endregion

    #region REDUCER
    public readonly struct Reducer
    {
        public static ReducerDelegate<State> Root =
            (state, action) => action switch
            {
                ResetAction _action => new State(),
                _ => new State(
                    RotationOffset(state.RotationOffset, action),
                    Slots(state.Slots, action)
                ),
            };

        public static ReducerDelegate<int> RotationOffset =
            (state, action) =>
                action switch
                {
                    RotateLeftAction _action => state + 1,
                    RotateRightAction _action => state - 1,
                    _ => state,
                };

        public static ReducerDelegate<bool[,]> Slots =
            (state, action) =>
                action switch
                {
                    FlagSlotsAction _action => Utils.FlagSlots(state, _action.Slots),
                    DeleteRowAction _action => Utils.DeleteRow(state, _action.RowNumber),
                    _ => state,
                };
    }
    #endregion

    # region UTILS
    public readonly struct Utils
    {
        private const int xDim = 12;
        private const int yDim = 12;

        public readonly static Func<bool[,]> CreateSlots =
            () => new bool[xDim, yDim];

        public readonly static Func<bool[,], List<Slot>, bool[,]> FlagSlots = (state, slots) =>
        {
            bool[,] copy = CreateSlots();

            for (var i = 0; i < xDim; i++)
            {
                for (var j = 0; j < yDim; j++)
                {
                    copy[i, j] = state[i, j];
                }
            }

            slots.ForEach(slot => copy[slot.X, slot.Y] = true);
            return copy;
        };

        public readonly static Func<bool[,], int, bool[,]> DeleteRow = (state, rowNumber) =>
        {
            bool[,] copy = CreateSlots();

            for (var i = 0; i < rowNumber; i++)
            {
                for (var j = 0; j < yDim; j++)
                {
                    copy[i, j] = state[i, j];
                }
            }

            for (var i = rowNumber + 1; i < xDim; i++)
            {
                for (var j = 0; j < yDim; j++)
                {
                    copy[i - 1, j] = state[i, j];
                }
            }

            return copy;
        };

    }
    # endregion
}
