using UnityEngine;

public class Utils
{
    private const float SCALE = 0.2f;

    // helper function implementing modulo operation
    // https://stackoverflow.com/questions/1082917
    public static int Mod(int k, int n)
    {
        return ((k %= n) < 0) ? k+n : k;
    }

    public static int Scale2Slot(float scale)
    {
        return Mathf.FloorToInt(scale / SCALE);
    }

    public static float Slot2Scale(int slot)
    {
        return SCALE * slot;
    }
}
