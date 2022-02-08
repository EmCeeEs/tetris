using System.Collections;
using System.Collections.Generic;
using NUnit.Framework; // Assert
using UnityEngine;
using UnityEngine.TestTools;

using System; // Action
using System.Linq;

public class GameLogicTest
{
    [Test]
    public void RotationLeftTest()
    {
        State state = new State(5, 12);

        Assert.AreEqual(state.GetRotationState(), 0);

        RepeatAction(12, state.RotateLeft);
        Assert.AreEqual(state.GetRotationState(), 0,
            "Rotating left 12 times returns to starting postion.");

        state.RotateLeft();
        Assert.AreEqual(state.GetRotationState(), 1);

        state.RotateLeft();
        Assert.AreEqual(state.GetRotationState(), 2);
    }

    [Test]
    public void RotationRightTest()
    {
        State state = new State(5, 12);

        Assert.AreEqual(state.GetRotationState(), 0);

        RepeatAction(12, state.RotateRight);
        Assert.AreEqual(state.GetRotationState(), 0,
            "Rotating right 12 times returns to starting postion.");

        state.RotateRight();
        Assert.AreEqual(state.GetRotationState(), 12 - 1);

        state.RotateRight();
        Assert.AreEqual(state.GetRotationState(), 12 - 2);

    }

    [Test]
    public void ActivateBlockTest()
    {
        State state = new State(5, 12);

        state.ActivateBlock(0, 0);
        Assert.IsTrue(state.IsBlockActive(0, 0));

        state.ActivateBlock(1, 1);
        Assert.IsTrue(state.IsBlockActive(1, 1));

        state.DeactivateBlock(1, 1);
        Assert.IsFalse(state.IsBlockActive(1, 1));
    }

    [Test]
    public void IsRowCompleteTest()
    {
        State state = new State(10, 2);

        Assert.IsFalse(state.IsRowComplete(0), "Row is initially empty.");

        state.ActivateBlock(0, 0);
        Assert.IsFalse(state.IsRowComplete(0), "Row is still incomplete.");

        state.ActivateBlock(0, 1);
        Assert.IsTrue(state.IsRowComplete(0), "Row is finally complete.");

        state.DeactivateBlock(0, 1);
        Assert.IsFalse(state.IsRowComplete(0), "Row is incomplete again.");
    }

    [Test]
    public void BlowUpRowTest()
    {
        State state = new State(7, 3);

        state.ActivateBlock(0, 0);
        state.ActivateBlock(0, 1);
        state.ActivateBlock(0, 2);
        state.ActivateBlock(1, 0);
        state.ActivateBlock(2, 0);

        state.ActivateBlock(6, 2);

        Assert.IsTrue(state.IsRowComplete(0));
        state.BlowUpRow(0);

        Assert.IsFalse(state.IsRowComplete(0));
        Assert.IsTrue(state.IsBlockActive(0, 0));
        Assert.IsFalse(state.IsBlockActive(0, 1));
        Assert.IsFalse(state.IsBlockActive(0, 2));
        Assert.IsTrue(state.IsBlockActive(1, 0));
        Assert.IsFalse(state.IsBlockActive(2, 0));

        Assert.IsFalse(state.IsBlockActive(6, 2));
        Assert.IsTrue(state.IsBlockActive(5, 2));

    }

    private static void RepeatAction(int repeatCount, Action action)
    {
        for (int i = 0; i < repeatCount; i++)
            action();
    }
}

