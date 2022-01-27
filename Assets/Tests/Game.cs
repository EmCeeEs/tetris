using System.Linq;
using System; // Array

namespace Game {
    public class State {

        private int _rotationState = 0;
        private bool[,] _blocks;

        public State(int nRows, int nBlocksPerRow){
            _blocks = new bool[nRows, nBlocksPerRow];
        }

        private int nRows()
        {
            return _blocks.GetLength(0);
        }

        private int nBlocksPerRow()
        {
            return _blocks.GetLength(1);
        }

        public void rotateLeft()
        {
            _rotationState = mod(_rotationState+1, nBlocksPerRow());
        }
        
        public void rotateRight()
        {
            _rotationState = mod(_rotationState-1, nBlocksPerRow());
        }

        public int getRotationState() {
            return _rotationState;
        }

        public void activateBlock(int row, int col)
        {
            _blocks[row, col] = true;
        }

        public void deactivateBlock(int row, int col)
        {
            _blocks[row, col] = false;
        }

        public bool isBlockActive(int row, int col)
        {
            return _blocks[row, col];
        }

        public bool isRowComplete(int rowNumber)
        {
            return Enumerable.Range(0, nBlocksPerRow())
                .Select(x => _blocks[rowNumber, x])
                .All(x => x);
        }

        public void blowUpRow(int rowNumber)
        {
            _blocks = clearRowAndShiftArray(rowNumber);
        }

        private bool[,] clearRowAndShiftArray(int rowNumber)
        {
            bool[,] updatedBlocks = new bool[nRows(), nBlocksPerRow()];

            int nBlocksBeforeRow = nBlocksPerRow() * rowNumber;
            int nBlocksAfterRow = nBlocksPerRow() *(nRows()-rowNumber-1);

            int iCurrentRow = nBlocksPerRow() * rowNumber; // = nBlocksBeforeRow
            int iNextRow = nBlocksPerRow() * (rowNumber+1);

            // https://docs.microsoft.com/en-us/dotnet/api/system.array.copy
            Array.Copy(_blocks, 0, updatedBlocks, 0, nBlocksBeforeRow);
            Array.Copy(_blocks, iNextRow, updatedBlocks, iCurrentRow, nBlocksAfterRow);

            return updatedBlocks;
        }

        // helper function to implement modulo operation
        // https://stackoverflow.com/questions/1082917
        private static int mod(int k, int n)
        {
            return ((k %= n) < 0) ? k+n : k;
        }
    }
}

// https://stackoverflow.com/questions/27427527
public class CustomArray<T> {
    public T[] GetColumn(T[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public T[] GetRow(T[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
}