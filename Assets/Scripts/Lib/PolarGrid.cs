using UnityEngine; // GameObject

public class PolarGrid
{
    private readonly float scale;
    public readonly int Periodicity;
    private readonly float rotationAngle;

    public PolarGrid(int _periodicity = 12, float _scale = 1.2F)
    {
        scale = _scale;
        Periodicity = _periodicity;
        rotationAngle = 360 / Periodicity;
    }

    public float GetScale(Slot slot)
    {
        return Mathf.Pow(scale, slot.Scale);
    }

    public float GetRotation(Slot slot)
    {
        return Utils.Mod(slot.Rotation, Periodicity) * rotationAngle;
    }

    public Slot LowerSlot(Transform transform)
    {
        float currentScale = transform.localScale.x;

        int scaleExponent = -1; // -1 is base
        while (Mathf.Pow(scale, scaleExponent + 1) < currentScale)
        {
            scaleExponent++;
        }
        int angle = Mathf.RoundToInt(transform.localRotation.eulerAngles.y / rotationAngle);
        int rotationState = Utils.Mod(angle, Periodicity);

        return new Slot(scaleExponent, rotationState);
    }

    public void MoveByTick(Transform transform, float scaleChange)
    {
        transform.localScale -= new Vector3(scaleChange, 0, scaleChange);
    }

    public void MoveToSlot(Slot slot, GameObject go)
    {
        float newScale = GetScale(slot);
        float newRotation = GetRotation(slot);

        go.transform.localScale = new Vector3(newScale, 1, newScale);
        go.transform.localRotation = Quaternion.Euler(0, newRotation, 0);
    }
}

public readonly struct Slot
{
    public readonly int Scale;
    public readonly int Rotation;

    public Slot(int scale, int rotation)
    {
        Scale = scale;
        Rotation = rotation;
    }

    public static Slot operator +(Slot a, Slot b)
        => new Slot(a.Scale + b.Scale, a.Rotation + b.Rotation);

    public static Slot operator -(Slot a, Slot b)
        => new Slot(a.Scale - b.Scale, a.Rotation - b.Rotation);


    public static Slot InvertX(Slot Slot)
        => new Slot(-Slot.Scale, Slot.Rotation);

    public static Slot InvertY(Slot Slot)
        => new Slot(Slot.Scale, -Slot.Rotation);

    public override string ToString() => $"({Scale}, {Rotation})";
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
