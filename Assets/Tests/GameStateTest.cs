using System.Collections;
using System.Collections.Generic;
using NUnit.Framework; // Assert
using UnityEngine;
using UnityEngine.TestTools;

using System; // Action
using System.Linq;

using Game;

public class GameStateTest
{
    const int N_BLOCKS_PER_ROW = 12;
    const int N_ROWS = 5;

    [Test]
    public void RotationLeftTest()
    {
        Game.State state = new Game.State(N_ROWS, N_BLOCKS_PER_ROW);

        Assert.AreEqual(state.getRotationState(), 0);

        RepeatAction(N_BLOCKS_PER_ROW, state.rotateLeft);
        Assert.AreEqual(state.getRotationState(), 0,
            "Rotating left 12 times returns to starting postion.");
        
        state.rotateLeft();
        Assert.AreEqual(state.getRotationState(), 1);

        state.rotateLeft();
        Assert.AreEqual(state.getRotationState(), 2);
    }

    [Test]
    public void RotationRightTest()
    {
        Game.State state = new Game.State(N_ROWS, N_BLOCKS_PER_ROW);

        Assert.AreEqual(state.getRotationState(), 0);

        RepeatAction(N_BLOCKS_PER_ROW, state.rotateRight);
        Assert.AreEqual(state.getRotationState(), 0,
            "Rotating right 12 times returns to starting postion.");

        state.rotateRight();
        Assert.AreEqual(state.getRotationState(), N_BLOCKS_PER_ROW-1);

        state.rotateRight();
        Assert.AreEqual(state.getRotationState(), N_BLOCKS_PER_ROW-2);

    }

    [Test]
    public void ActivateBlockTest()
    {
        Game.State state = new Game.State(N_ROWS, N_BLOCKS_PER_ROW);

        state.activateBlock(0, 0);
        Assert.IsTrue(state.isBlockActive(0, 0));

        state.activateBlock(1, 1);
        Assert.IsTrue(state.isBlockActive(1, 1));

        state.deactivateBlock(1, 1);
        Assert.IsFalse(state.isBlockActive(1, 1));
    }

    [Test]
    public void IsRowCompleteTest()
    {
        Game.State state = new Game.State(10, 2);

        Assert.IsFalse(state.isRowComplete(0), "Row is initially empty.");
        
        state.activateBlock(0, 0);
        Assert.IsFalse(state.isRowComplete(0), "Row is still incomplete.");

        state.activateBlock(0, 1);
        Assert.IsTrue(state.isRowComplete(0), "Row is finally complete.");

        state.deactivateBlock(0, 1);
        Assert.IsFalse(state.isRowComplete(0), "Row is incomplete again.");
    }

    [Test]
    public void BlowUpRowTest()
    {
        Game.State state = new Game.State(10, 2);

        Assert.IsFalse(true);
    }

    private static void RepeatAction(int repeatCount, Action action)
    {
        for (int i = 0; i < repeatCount; i++)
            action();
    }
}

