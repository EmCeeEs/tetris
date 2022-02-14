using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using System.Linq;

// use type alias
using Layout = System.Collections.Generic.List<Slot>;

public class BlockSpawner : MonoBehaviour
{
    public UIHandler uiHandler;
    public GameObject BlockPrefab;
    public GameObject BlockParentPrefab;
    public GameObject emptyObject;
    public List<Layout> layouts = LayoutCreator.Create3BlockLayouts();

    private Board board;

    private GameObject currentBlock;

    public void Start()
    {
        uiHandler = FindObjectOfType<UIHandler>();
        board = FindObjectOfType<Board>();
    }

    public void SpawnBlock(Slot spawnSlot)
    {
        int layoutIndex = UnityEngine.Random.Range(0, layouts.Count);
        Layout blockLayout = layouts[layoutIndex];

        bool canSpawn = blockLayout.All(slot => board.IsEmpty(slot + spawnSlot));
        if (!canSpawn)
        {
            Debug.LogError("GAME OVER");
            Debug.LogError($"Cannot Spawn Block at {spawnSlot}");

            board.isPlaying = false;
            uiHandler.joystick.SetActive(false);
            uiHandler.playButton.SetActive(true);
            board.DestroyAll();
            board.currentBlock = null;
        }
        else
        {
            InstantiateBlockFromLayout(spawnSlot, blockLayout);
            board.currentBlock = currentBlock;
        }
    }

    private void InstantiateBlockFromLayout(Slot spawnSlot, Layout layout)
    {

        GameObject parent = Instantiate(BlockParentPrefab);
        List<GameObject> blocks = new List<GameObject>();

        GameObject block;
        foreach (Slot slot in layout)
        {
            block = Instantiate(BlockPrefab, parent.transform);
            board.grid.MoveToSlot(slot, block);
            blocks.Add(block);
        }

        parent.GetComponent<BlockParent>().BlockLayout = layout;
        parent.GetComponent<BlockParent>().Blocks = blocks;

        board.grid.MoveToSlot(spawnSlot, parent);

        currentBlock = parent;
    }
}

public class LayoutCreator
{
    public static List<Layout> Create3BlockLayouts()
        => new List<Layout>(){
            // I layout
            new Layout(){
                new Slot(-1, 0),
                new Slot(0, 0),
                new Slot(1, 0),
            },
            // Line layout
            new Layout(){
                new Slot(0, -1),
                new Slot(0, 0),
                new Slot(0, 1),
            },
            // L layout
            new Layout(){
                new Slot(0, 1),
                new Slot(0, 0),
                new Slot(1, 0),
            },
            // L inverse X
            new Layout(){
                new Slot(0, 1),
                new Slot(0, 0),
                new Slot(-1, 0),
            },
            // L inverse Y
            new Layout(){
                new Slot(0, -1),
                new Slot(0, 0),
                new Slot(1, 0),
            },
            // L inverse X and Y
            new Layout(){
                new Slot(0, -1),
                new Slot(0, 0),
                new Slot(-1, 0),
            },
        };

    public static Layout InvertX(Layout layout)
    {
        return layout.Select(slot => Slot.InvertX(slot)).ToList();
    }

    public static Layout InvertY(Layout layout)
    {
        return layout.Select(slot => Slot.InvertY(slot)).ToList();
    }
}