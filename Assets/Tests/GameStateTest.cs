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
    const int WIDTH = 12;
    const int HEIGHT = 6;

    [Test]
    public void RotationLeftTest()
    {
        Game.State state = new Game.State(WIDTH, HEIGHT);

        Assert.AreEqual(state.getRotationState(), 0);

        RepeatAction(WIDTH, state.rotateLeft);
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
        Game.State state = new Game.State(WIDTH, HEIGHT);

        Assert.AreEqual(state.getRotationState(), 0);

        RepeatAction(WIDTH, state.rotateRight);
        Assert.AreEqual(state.getRotationState(), 0,
            "Rotating right 12 times returns to starting postion.");

        state.rotateRight();
        Assert.AreEqual(state.getRotationState(), WIDTH-1);

        state.rotateRight();
        Assert.AreEqual(state.getRotationState(), WIDTH-2);

    }

    [Test]
    public void ActivateBlockTest()
    {
        Game.State state = new Game.State(WIDTH, HEIGHT);

        state.activateBlock(0, 0);
        Assert.IsTrue(state.isBlockActive(0, 0));

        state.activateBlock(1, 1);
        Assert.IsTrue(state.isBlockActive(1, 1));

        state.deactivateBlock(1, 1);
        Assert.IsFalse(state.isBlockActive(1, 1));
    }

    private static void RepeatAction(int repeatCount, Action action)
    {
        for (int i = 0; i < repeatCount; i++)
            action();
    }
}

