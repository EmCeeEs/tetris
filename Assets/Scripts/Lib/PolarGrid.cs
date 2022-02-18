using System;
using UnityEngine;

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

    private float RotationAngle() => 360 / Periodicity;

    public float GetScale(Slot slot)
    {
        return Mathf.Pow(Scale, slot.X);
    }

    public float GetRotation(Slot slot)
    {
        return Utils.Mod(slot.Y, Periodicity) * RotationAngle();
    }

    public Slot LowerSlot(ref GameObject go)
    {
        Transform transform = go.transform;
        float currentScale = transform.localScale.x;

        int scaleExponent = -1; // -1 is base
        while (Mathf.Pow(Scale, scaleExponent + 1) < currentScale)
        {
            scaleExponent++;
        }
        int angle = Mathf.RoundToInt(transform.localRotation.eulerAngles.y / RotationAngle());
        int rotationState = Utils.Mod(angle, Periodicity);

        return new Slot(scaleExponent, rotationState);
    }


    public void MoveToSlot(Slot slot, ref GameObject go)
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

public readonly struct Slot
{
    public readonly int X;
    public readonly int Y;

    public Slot(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Slot operator +(Slot a, Slot b)
        => new Slot(a.X + b.X, a.Y + b.Y);

    public static Slot operator -(Slot a, Slot b)
        => new Slot(a.X - b.X, a.Y - b.Y);


    public static Slot InvertX(Slot Slot)
        => new Slot(-Slot.X, Slot.Y);

    public static Slot InvertY(Slot Slot)
        => new Slot(Slot.X, -Slot.Y);

    public override string ToString() => $"({X}, {Y})";
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
