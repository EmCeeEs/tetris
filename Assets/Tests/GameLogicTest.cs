using System.Collections;
using System.Collections.Generic;
using NUnit.Framework; // Assert
using UnityEngine;
using UnityEngine.TestTools;

using System; // Action
using System.Linq;

using GameLogic;

public class GameLogicTest
{
    [Test]
    public void RotationLeftTest()
    {
        GameLogic.State state = new GameLogic.State(5, 12);

        Assert.AreEqual(state.getRotationState(), 0);

        RepeatAction(12, state.rotateLeft);
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
        GameLogic.State state = new GameLogic.State(5, 12);

        Assert.AreEqual(state.getRotationState(), 0);

        RepeatAction(12, state.rotateRight);
        Assert.AreEqual(state.getRotationState(), 0,
            "Rotating right 12 times returns to starting postion.");

        state.rotateRight();
        Assert.AreEqual(state.getRotationState(), 12-1);

        state.rotateRight();
        Assert.AreEqual(state.getRotationState(), 12-2);

    }

    [Test]
    public void ActivateBlockTest()
    {
        GameLogic.State state = new GameLogic.State(5, 12);

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
        GameLogic.State state = new GameLogic.State(10, 2);

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
        GameLogic.State state = new GameLogic.State(7, 3);

        state.activateBlock(0, 0);
        state.activateBlock(0, 1);
        state.activateBlock(0, 2);
        state.activateBlock(1, 0);
        state.activateBlock(2, 0);

        state.activateBlock(6, 2);

        Assert.IsTrue(state.isRowComplete(0));
        state.blowUpRow(0);

        Assert.IsFalse(state.isRowComplete(0));
        Assert.IsTrue(state.isBlockActive(0, 0));
        Assert.IsFalse(state.isBlockActive(0, 1));
        Assert.IsFalse(state.isBlockActive(0, 2));
        Assert.IsTrue(state.isBlockActive(1, 0));
        Assert.IsFalse(state.isBlockActive(2, 0));

        Assert.IsFalse(state.isBlockActive(6, 2));
        Assert.IsTrue(state.isBlockActive(5, 2));

    }

    private static void RepeatAction(int repeatCount, Action action)
    {
        for (int i = 0; i < repeatCount; i++)
            action();
    }
}

