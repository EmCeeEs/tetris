using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PolarGridTest
{
    [Test]
    public void GetScaleTest()
    {
        PolarGrid grid = new PolarGrid();

        Assert.AreEqual(
            1,
            grid.GetScale(new Slot(0, 0)),
            "Scale of (0, 0) is 1."
        );
        Assert.AreEqual(
            1.2F,
            grid.GetScale(new Slot(1, 0)),
            "Scale of (1, 0) is 1.2."
        );
        Assert.AreEqual(
            1.44F,
            grid.GetScale(new Slot(2, 0)),
            "Scale of (2, 0) is 1.44."
        );
        Assert.AreEqual(
            1.44F,
            grid.GetScale(new Slot(2, -1)),
            "Scale of (2, -1) is 1.44."
        );
        Assert.AreEqual(
            1.44F,
            grid.GetScale(new Slot(2, 7)),
            "Scale of (2, 7) is 1.44."
        );
        Assert.AreEqual(
            6.1917F,
            grid.GetScale(new Slot(10, 0)),
            0.0001F,
            "Scale of (0, 0) is 6.1917."
        );
    }

    [Test]
    public void GetRotationTest()
    {
        PolarGrid grid = new PolarGrid();

        Assert.AreEqual(
            0,
            grid.GetRotation(new Slot(0, 0)),
            "Rotation of (0, 0) is 0."
        );
        Assert.AreEqual(
            30,
            grid.GetRotation(new Slot(0, 1)),
            "Rotation of (0, 1) is 30 degrees."
        );
        Assert.AreEqual(
            330,
            grid.GetRotation(new Slot(0, -1)),
            "Rotation of (0, 1) is 330 degrees."
        );
        Assert.AreEqual(
            0,
            grid.GetRotation(new Slot(7, 0)),
            "Rotation of (7, 0) is 0."
        );
        Assert.AreEqual(
            30,
            grid.GetRotation(new Slot(7, 1)),
            "Rotation of (7, 1) is 30 degrees."
        );
        Assert.AreEqual(
            330,
            grid.GetRotation(new Slot(7, -1)),
            "Rotation of (7, -1) is 330 degrees."
        );
    }

    [Test]
    public void Move2SlotTest()
    {
        GameObject go = new GameObject();
        PolarGrid grid = new PolarGrid();

    }
}

public class MyGridTest
{
    [Test]
    public void SlotTest()
    {
        MyGrid grid = new MyGrid(12, 12);
        MySlot slot5x5 = new MySlot(5, 5);
        GameObject go = new GameObject();

        Assert.IsTrue(
            grid.IsEmpty(slot5x5),
            "Slot (5, 5) is empty."
        );

        grid.SetSlot(slot5x5, go);

        Assert.IsFalse(
            grid.IsEmpty(slot5x5),
            "Slot (5, 5) is not empty."
        );
        Assert.AreSame(
            go,
            grid.GetSlot(slot5x5),
            "Slot (5, 5) holds correct game object."
        );

        grid.UnsetSlot(slot5x5);

        Assert.IsTrue(
            grid.IsEmpty(slot5x5),
            "Slot (5, 5) is empty again."
        );
    }
}