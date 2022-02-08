using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq; // Any

public class BlockSpawner : MonoBehaviour
{
    public UIHandler uiHandler;
    public GameObject BlockPrefab;
    public GameObject BlockParentPrefab;
    public GameObject emptyObject;
    // public List<List<Slot>> layouts = LayoutCreator.CreateLayouts();
    public List<List<Slot>> layouts = LayoutCreator.Create3BlockLayouts();

    private Board board;

    public void Start()
    {
        uiHandler = FindObjectOfType<UIHandler>();
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
    }

    public GameObject SpawnBlock(Slot spawnSlot)
    {
        int layoutIndex = Random.Range(0, layouts.Count);
        List<Slot> blockLayout = layouts[layoutIndex];

        Slot boardSlot;
        foreach (Slot layoutSlot in blockLayout)
        {
            boardSlot = spawnSlot + layoutSlot;
            if (!board.IsEmpty(boardSlot))
            {
                Debug.LogError("GAME OVER");
                Debug.LogError("Cannot Spawn Block at boardSlot" + boardSlot);
                board.isPlaying = false;
                uiHandler.joystick.SetActive(false);
                uiHandler.playButton.SetActive(true);
                board.DestroyAll();
                board.currentBlock = null;
            }
        }

        if (board.isPlaying)
        {
            GameObject parent = Instantiate(BlockParentPrefab);
            List<GameObject> blocks = new List<GameObject>();

            GameObject block;
            foreach (Slot layoutSlot in blockLayout)
            {
                block = Instantiate(BlockPrefab, parent.transform);
                board.grid.MoveToSlot(layoutSlot, block);
                blocks.Add(block);
            }

            parent.GetComponent<BlockParent>().BlockLayout = blockLayout;
            parent.GetComponent<BlockParent>().Blocks = blocks;

            board.grid.MoveToSlot(spawnSlot, parent);

            return parent;
        }
        else
        {
            return emptyObject;
        }
    }
}

public class LayoutCreator
{
    public static List<List<Slot>> Create3BlockLayouts()
    {
        List<List<Slot>> layouts = new List<List<Slot>>();

        List<Slot> ILayout = new List<Slot>();
        ILayout.Add(new Slot(-1, 0));
        ILayout.Add(new Slot(0, 0));
        ILayout.Add(new Slot(1, 0));

        layouts.Add(ILayout);

        List<Slot> LineLayout = new List<Slot>();
        LineLayout.Add(new Slot(0, -1));
        LineLayout.Add(new Slot(0, 0));
        LineLayout.Add(new Slot(0, 1));

        layouts.Add(LineLayout);

        List<Slot> LLayout = new List<Slot>();
        LLayout.Add(new Slot(0, 1));
        LLayout.Add(new Slot(0, 0));
        LLayout.Add(new Slot(1, 0));

        layouts.Add(LLayout);

        List<Slot> LInverseXLayout = new List<Slot>();
        LInverseXLayout.Add(new Slot(0, 1));
        LInverseXLayout.Add(new Slot(0, 0));
        LInverseXLayout.Add(new Slot(-1, 0));

        layouts.Add(LInverseXLayout);

        List<Slot> LInverseYLayout = new List<Slot>();
        LInverseYLayout.Add(new Slot(0, -1));
        LInverseYLayout.Add(new Slot(0, 0));
        LInverseYLayout.Add(new Slot(1, 0));

        layouts.Add(LInverseYLayout);

        List<Slot> LInverseXYLayout = new List<Slot>();
        LInverseXYLayout.Add(new Slot(0, -1));
        LInverseXYLayout.Add(new Slot(0, 0));
        LInverseXYLayout.Add(new Slot(-1, 0));

        layouts.Add(LInverseXYLayout);

        return layouts;
    }

    public static List<List<Slot>> CreateLayouts()
    {
        List<List<Slot>> layouts = new List<List<Slot>>();

        List<Slot> layoutTInverse = new List<Slot>();
        layoutTInverse.Add(new Slot(0, 0));
        layoutTInverse.Add(new Slot(0, 1));
        layoutTInverse.Add(new Slot(0, -1));
        layoutTInverse.Add(new Slot(1, 0));

        layouts.Add(layoutTInverse);

        List<Slot> layoutT = new List<Slot>();
        layoutT.Add(new Slot(0, 0));
        layoutT.Add(new Slot(1, -1));
        layoutT.Add(new Slot(1, 0));
        layoutT.Add(new Slot(1, 1));

        layouts.Add(layoutT);

        List<Slot> layout4x1 = new List<Slot>();
        layout4x1.Add(new Slot(0, 0));
        layout4x1.Add(new Slot(1, 0));
        layout4x1.Add(new Slot(2, 0));
        layout4x1.Add(new Slot(3, 0));

        layouts.Add(layout4x1);

        List<Slot> layout1x4 = new List<Slot>();
        layout1x4.Add(new Slot(0, 1));
        layout1x4.Add(new Slot(0, 0));
        layout1x4.Add(new Slot(0, -1));
        layout1x4.Add(new Slot(0, -2));

        layouts.Add(layout1x4);

        List<Slot> layoutL = new List<Slot>();
        layoutL.Add(new Slot(0, 0));
        layoutL.Add(new Slot(1, 0));
        layoutL.Add(new Slot(2, 0));
        layoutL.Add(new Slot(0, 1));

        layouts.Add(layoutL);

        List<Slot> layoutLInverse = new List<Slot>();
        layoutLInverse.Add(new Slot(0, 0));
        layoutLInverse.Add(new Slot(1, 0));
        layoutLInverse.Add(new Slot(2, 0));
        layoutLInverse.Add(new Slot(0, -1));

        layouts.Add(layoutLInverse);

        List<Slot> layout1x2 = new List<Slot>();
        layout1x2.Add(new Slot(0, 0));
        layout1x2.Add(new Slot(1, 0));

        layouts.Add(layout1x2);

        List<Slot> layout2x1 = new List<Slot>();
        layout2x1.Add(new Slot(0, 0));
        layout2x1.Add(new Slot(0, 1));

        layouts.Add(layout2x1);

        return layouts;
    }

}
