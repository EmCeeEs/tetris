using System;
using NUnit.Framework;

public class GameManagerTest
{
    [Test]
    public void RotateTest()
    {
        Store store = new Store();
        Func<int> getRotation = () => store.GetState().Rotation;

        Assert.AreEqual(0, getRotation());

        store.Dispatch(new RotateAction() { NumberOfRotations = 1 });

        Assert.AreEqual(1, getRotation());

        store.Dispatch(new RotateAction() { NumberOfRotations = -2 });

        Assert.AreEqual(-1, getRotation());
    }

    [Test]
    public void InvertTest()
    {
        var store = new Store();
        Func<bool> getInversion = () => store.GetState().Inversion;

        Assert.IsFalse(getInversion());

        store.Dispatch(new InvertAction());

        Assert.IsTrue(getInversion());

        store.Dispatch(new InvertAction());

        Assert.IsFalse(getInversion());
    }
}
