using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

// use type alias
using Layout = System.Collections.Generic.List<Slot>;

public class BlockParent : MonoBehaviour
{
    public Layout BlockLayout { get; set; }
    public List<GameObject> Blocks { get; set; }
    private Board board;
    public Slot LowerSlot;
    public Slot UpperSlot;

    private PolarGrid grid;

    public void Start()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        grid = new PolarGrid(board.grid.Periodicity);
    }

    public void FixedUpdate()
    {
        LowerSlot = board.grid.LowerSlot(transform);
        UpperSlot = LowerSlot + new Slot(1, 0);

        bool isValidMove = true;
        foreach (Slot slot in BlockLayout)
        {
            if (!board.IsEmpty(LowerSlot + slot))
            {
                isValidMove = false;
            }
        }

        if (isValidMove)
        {
            board.grid.MoveByTick(transform, board.scaleChange);
        }
        else
        {
            board.grid.MoveToSlot(UpperSlot, gameObject);
            foreach (int i in Enumerable.Range(0, BlockLayout.Count))
            {
                GameObject block = Blocks[i];
                Slot slot = BlockLayout[i];
	
                board.SetSlot(UpperSlot + slot, block);
			}
			board.CheckForCompleteRows();
            Destroy(gameObject);
        }
    }

    public void InvertX()
    {
        Layout invertedLayout = LayoutCreator.InvertX(BlockLayout);

        ApplyLayout(invertedLayout);
    }

    public void ApplyLayout(Layout layout)
    {
        BlockLayout = layout;

        foreach (int i in Enumerable.Range(0, BlockLayout.Count))
        {
            GameObject block = Blocks[i];
            Slot slot = BlockLayout[i];
            grid.MoveToSlot(slot, block);
        }
    }

    public void InvertY()
    {
        Layout invertedLayout = LayoutCreator.InvertY(BlockLayout);

        ApplyLayout(invertedLayout);
    }
}
