using UnityEngine;
using System.Collections.Generic;

public class MyBlock
{
    private readonly List<MySlot> layout;
    private readonly MySlot startSlot;
    private readonly GameObject parent;

    public MyBlock(List<MySlot> blockLayout, MySlot spawnSlot)
    {
        parent = new GameObject();
        layout = blockLayout;
        startSlot = spawnSlot;
    }
}
