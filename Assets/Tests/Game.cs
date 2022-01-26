// using System.Linq;

namespace Game {
    public class State {

        private bool[,] _blocks;
        private int _rotationState = 0;

        public State(int width, int height){
            _blocks = new bool[width, height];
        }

        private int width()
        {
            return _blocks.GetLength(0);
        }

        private int height()
        {
            return _blocks.GetLength(1);
        }

        public void rotateLeft()
        {
            _rotationState = mod(_rotationState+1, width());
        }
        
        public void rotateRight()
        {
            _rotationState = mod(_rotationState-1, width());
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

        // public bool[] getRow(int rowNumber)
        // {
        //     return _blocks[rowNumber,];
        // }

        // public bool isRowComplete(int rowNumber) {
        //     return getRow(rowNumber).All(x => x)
        // }

        // public void blowUpRow(int rowNumber) {
        //     _blocks.Skip(rowNumber-1)
        // }

        // helper function to implement modulo operation
        // https://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
        private static int mod(int k, int n)
        {
            return ((k %= n) < 0) ? k+n : k;
        }
    }
}

