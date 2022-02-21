using System;
using UnityEngine;
using UnityEngine.Assertions;

using WinstonPuckett.PipeExtensions;
using PolarCoordinates;

public class PolarGrid
{
    private readonly float Scale;
    public readonly int Periodicity;

    public PolarGrid(int periodicity = 12, float scale = 1.2F)
    {
        if (periodicity <= 0)
            throw new ArgumentException("Periodicity must be greater 0");

        if (scale <= 0)
            throw new ArgumentException("Scale must be greater 0");

        Scale = scale;
        Periodicity = periodicity;
    }

    public float RotationAngle(int offset)
        => offset * 360 / Periodicity;

    public float GetScale(GridPoint slot)
        => Mathf.Pow(Scale, slot.X);

    public float GetRotation(GridPoint slot)
        => Utils.Mod(slot.Y, Periodicity).Pipe(RotationAngle);

    // public Point GetCoordinates(Slot slot)
    //     => new Point(GetScale(slot), GetRotation(slot));

    public GridPoint LowerSlot(ref GameObject go)
    {
        Point coords = Point.GetCoordinates(ref go);

        int scaleExponent = -1; // -1 is base
        while (Mathf.Pow(Scale, scaleExponent + 1) < coords.Scale)
            scaleExponent++;

        int rotationOffset = Mathf.RoundToInt(coords.Angle / Periodicity * 360);
        int rotationState = Utils.Mod(rotationOffset, Periodicity);

        return new GridPoint(scaleExponent, rotationState);
    }

    public void MoveToSlot(GridPoint slot, ref GameObject go)
    {
        float newScale = GetScale(slot);
        float newRotation = GetRotation(slot);

        go.transform.localScale = new Vector3(newScale, 1, newScale);
        go.transform.localRotation = Quaternion.Euler(0, newRotation, 0);
    }

    // this should take scale into account
    public static void MoveByTick(ref GameObject go, float scaleChange)
    {
        go.transform.localScale -= new Vector3(scaleChange, 0, scaleChange);
    }
}

public readonly struct Point
{
    public readonly float Scale;
    public readonly float Angle;

    public Point(float scale, float angle)
    {
        Scale = scale;
        Angle = angle;
    }

    public static Point GetCoordinates(ref GameObject go)
    {
        Vector3 localScale = go.transform.localScale;
        Vector3 localEulerRotation = go.transform.localRotation.eulerAngles;

        Assert.AreApproximatelyEqual(localScale.x, localScale.z);
        Assert.AreEqual(localScale.y, 1);
        Assert.AreEqual(localEulerRotation.x, 0);
        Assert.AreEqual(localEulerRotation.z, 0);

        return new Point(
            localScale.x,
            localEulerRotation.y
        );
    }
}

public class Utils
{
    // helper function implementing modulo operation
    // https://stackoverflow.com/questions/1082917
    public static int Mod(int k, int n)
    {
        return ((k %= n) < 0) ? k + n : k;
    }
}
