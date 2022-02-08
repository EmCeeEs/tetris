using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class BlockParent : MonoBehaviour
{
    public List<Slot> BlockLayout { get; set; }
    public List<GameObject> Blocks { get; set; }
    private Board board;
    public Slot lowerSlot;
    public Slot upperSlot;

    public void Start()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
    }

    public void Update()
    {
        lowerSlot = board.grid.LowerSlot(transform);
        upperSlot = lowerSlot + new Slot(1, 0);

        bool isValidMove = true;
        foreach (Slot slot in BlockLayout)
        {
            if (!board.IsEmpty(lowerSlot + slot))
            {
                isValidMove = false;
            }
        }

        if (isValidMove)
        {
            board.grid.MoveByTick(transform);
        }
        else
        {
            board.grid.MoveToSlot(upperSlot, this.gameObject);
            foreach (int i in Enumerable.Range(0, BlockLayout.Count))
            {
                GameObject block = Blocks[i];
                Slot slot = BlockLayout[i];
                board.SetSlot(upperSlot + slot, block);
            }
            board.CheckForCompleteRows();
            Destroy(this.gameObject);
        }
    }
}
