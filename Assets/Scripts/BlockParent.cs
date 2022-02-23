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

	public Point Position;

	public void Awake()
	{
		GM = GameManager.Instance;
	}

	public void FixedUpdate()
	{
		Slot LowerSlot = GridUtils.SnapToNextX(Position);
		Slot UpperSlot = GridUtils.SnapToPreviousX(Position);

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
			Position -= new Point(0.05F, 0);
			Geometry.MoveToPoint(Position, gameObject);
		}
		else
		{
			Geometry.MoveToPoint(UpperSlot, gameObject);
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
			Geometry.MoveToPoint(slot, block);
		}
	}

	public void InvertY()
	{
		Layout invertedLayout = LayoutCreator.InvertY(BlockLayout);

		ApplyLayout(invertedLayout);
	}
}
