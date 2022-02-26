using System.Collections.Generic;
using UnityEngine;

using System.Linq;

// use type alias
using Layout = System.Collections.Generic.List<Slot>;

public class BlockState
{
	// logic
	public Layout BlockLayout { get; set; }
	// representation
	public List<GameObject> Blocks { get; set; }

	public Point Position { get; set; }

}

public class BlockParent : MonoBehaviour
{
	private GameManager GM;
	public BlockState state;

	public void Awake() =>
		GM = GameManager.Instance;

	public void FixedUpdate()
	{
		if (CanMove())
		{
			UpdatePosition();
		}
		else
		{

			AttachToBoard();
			Destroy(gameObject);
		}
	}

	public bool CanMove()
	{
		Slot nextSlot = GridUtils.SnapToNextX(state.Position);

		return state.BlockLayout
			.All(slot => GM.Board.IsEmpty(nextSlot + slot));
	}

	public void UpdatePosition()
	{
		state.Position -= new Point(0.04F + 0.01F * GM.Speed, 0);
		Geometry.MoveToPoint(state.Position, gameObject);
	}

	public void AttachToBoard()
	{
		Slot UpperSlot = GridUtils.SnapToPreviousX(state.Position);

		Geometry.MoveToPoint(UpperSlot, gameObject);
		foreach (int i in Enumerable.Range(0, state.BlockLayout.Count))
		{
			GameObject block = state.Blocks[i];
			Slot slot = state.BlockLayout[i];

			GM.Board.SetSlot(UpperSlot + slot, block);
		}
	}

	public void InvertX()
	{
		Layout invertedLayout = LayoutCreator.InvertX(state.BlockLayout);

		ApplyLayout(invertedLayout);
	}

	public void ApplyLayout(Layout layout)
	{
		state.BlockLayout = layout;

		foreach (int i in Enumerable.Range(0, state.BlockLayout.Count))
		{
			GameObject block = state.Blocks[i];
			Slot slot = state.BlockLayout[i];
			Geometry.MoveToPoint(slot, block);
		}
	}

	public void InvertY()
	{
		Layout invertedLayout = LayoutCreator.InvertY(state.BlockLayout);

		ApplyLayout(invertedLayout);
	}
}
