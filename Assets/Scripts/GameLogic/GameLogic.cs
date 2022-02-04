using System.Linq;
using System; // Array

using UnityEngine; // Debug

namespace GameLogic {
    public class State {

        private int _rotationState = 0;
        private bool[,] _blocks;

        public State(int nRows, int nBlocksPerRow){
            _blocks = new bool[nRows, nBlocksPerRow];
        }

        public int nRows()
        {
            return _blocks.GetLength(0);
        }

        public int nBlocksPerRow()
        {
            return _blocks.GetLength(1);
        }

        public void rotateLeft()
        {
            _rotationState = Utils.mod(_rotationState+1, nBlocksPerRow());
        }
        
        public void rotateRight()
        {
            _rotationState = Utils.mod(_rotationState-1, nBlocksPerRow());
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
    }

    public class Utils {
        // helper function implementing modulo operation
        // https://stackoverflow.com/questions/1082917
        public static int mod(int k, int n)
        {
            return ((k %= n) < 0) ? k+n : k;
        }
        
        // visually pleasing log function for block state
        public void logBlockState(State state)
        {
            string strMatrix = "";
            for (var i = 0; i<state.nRows(); i++)
            {
                for (var j = 0; j < state.nBlocksPerRow(); j++)
                {
                    strMatrix += state.isBlockActive(i, j);
                    strMatrix += " ";
                }
                strMatrix += "\n";
            }
            Debug.Log(strMatrix);
        }
    }
}
