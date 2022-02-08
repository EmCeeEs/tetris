using UnityEngine;
using System.Collections.Generic;

public class MyBlock
{
    private List<MySlot> layout;
    private MySlot startSlot;
    private GameObject parent;

    public MyBlock(List<MySlot> blockLayout, MySlot spawnSlot)
    {
        parent = new GameObject();
        layout = blockLayout;
        startSlot = spawnSlot;
    }
}
