using System.Collections.Generic;
using UnityEngine;

using System.Linq;

// use type alias
using Layout = System.Collections.Generic.List<Slot>;

public class BlockParent : MonoBehaviour
{
	private GameManager GM;

	public Layout BlockLayout { get; set; }
	public List<GameObject> Blocks { get; set; }

	public Slot LowerSlot;
	public Slot UpperSlot;

	private PolarGrid grid;

	public void Awake()
	{
		GM = GameManager.Instance;
		grid = new PolarGrid(GM.Board.grid.Periodicity);
	}

	public void FixedUpdate()
	{
		LowerSlot = GM.Board.grid.LowerSlot(transform);
		UpperSlot = LowerSlot + new Slot(1, 0);

		bool isValidMove = true;
		foreach (Slot slot in BlockLayout)
		{
			if (!GM.Board.IsEmpty(LowerSlot + slot))
			{
				isValidMove = false;
			}
		}

		if (isValidMove)
		{
			GM.Board.grid.MoveByTick(transform, GM.Board.scaleChange);
		}
		else
		{
			GM.Board.grid.MoveToSlot(UpperSlot, gameObject);
			foreach (int i in Enumerable.Range(0, BlockLayout.Count))
			{
				GameObject block = Blocks[i];
				Slot slot = BlockLayout[i];

				GM.Board.SetSlot(UpperSlot + slot, block);
			}
			GM.Board.CheckForCompleteRows();
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
