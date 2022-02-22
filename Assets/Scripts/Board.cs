using System.Collections;
using UnityEngine;

using System.Linq;

public class Board : MonoBehaviour
{
	private GameManager GM;
	public float scaleChange = 0.02F;
	public ColorManager ColorManager;

	public Material disolve;

	public float currentScore;
	private readonly float singleBlockPoint = 10;
	private readonly float tetrisScore = 100;

	public bool foundRow = false;
	public float disolveTimer = 0;

	public GameObject PlayerBase;

	private int rotationState = 0;
	private GameObject[,] slots;

	private const int N_ROWS = 12;
	private const int N_BLOCKS_PER_ROW = 12;
	private float rotationAngle;
	public PolarGrid grid;

	void Awake()
	{
		GM = GameManager.Instance;

		slots = new GameObject[N_ROWS, N_BLOCKS_PER_ROW];
		rotationAngle = 360 / N_BLOCKS_PER_ROW;
		grid = new PolarGrid();
	}

	public bool IsEmpty(Slot slot)
	{
		if (slot.Scale == -1)
		{
			return false;
		}

		int totalRotation = Utils.Mod(slot.Rotation - rotationState, N_BLOCKS_PER_ROW);
		return slots[slot.Scale, totalRotation] == null;
	}

	public void SetSlot(Slot slot, GameObject block)
	{
		if (slot.Scale < ColorManager.colors.Length)
		{
			int totalRotation = Utils.Mod(slot.Rotation - rotationState, N_BLOCKS_PER_ROW);
			block.transform.SetParent(PlayerBase.transform);

			currentScore += singleBlockPoint;
			GM.UIHandler.UpdateScore(currentScore);

			slots[slot.Scale, totalRotation] = block;
			block.GetComponentsInChildren<Renderer>()[0].material.SetColor("_BaseColor", ColorManager.colors[slot.Scale]);
		}
	}

	public void CheckForCompleteRows()
	{
		int offset = 0;
		foreach (int rowNumber in Enumerable.Range(0, N_ROWS))
		{
			int shifted = rowNumber - offset;
			if (IsRowComplete(shifted))
			{
				Debug.Log($"ROW COMPLETED {shifted}");
				if (foundRow == false)
				{
					foundRow = true;
					StartCoroutine(RemoveRow(shifted));
					offset++;
					currentScore += tetrisScore;
					GM.UIHandler.UpdateScore(currentScore);
					scaleChange += 0.001F;
				}
			}
		}
	}


	private bool IsRowComplete(int rowNumber)
	{
		return Enumerable.Range(0, N_BLOCKS_PER_ROW)
			.Select(x => slots[rowNumber, x] != null)
			.All(x => x);
	}


	private IEnumerator RemoveRow(int rowNumber)
	{
		// remove row
		for (int j = 0; j < N_BLOCKS_PER_ROW; j++)
		{
			slots[rowNumber, j].GetComponentsInChildren<Renderer>()[0].material = disolve;
			Destroy(slots[rowNumber, j], 1);
			slots[rowNumber, j] = null;
			foundRow = true;
		}

		yield return new WaitForSeconds(1);
		disolveTimer = 0;
		disolve.SetFloat("_time", disolveTimer);

		// shift other rows
		for (int i = rowNumber + 1; i < N_ROWS; i++)
		{
			for (int j = 0; j < N_BLOCKS_PER_ROW; j++)
			{
				if (slots[i, j] != null)
				{
					slots[i - 1, j] = slots[i, j];
					grid.MoveToSlot(new Slot(i - 1, j), slots[i, j]);
					slots[i - 1, j].GetComponentsInChildren<Renderer>()[0].material.SetColor("_BaseColor", ColorManager.colors[i - 1]);
					slots[i, j] = null;
				}
			}
		}
		foundRow = false;
	}

	public void Clear()
	{
		for (int i = 0; i < N_ROWS; i++)
		{
			for (int j = 0; j < N_BLOCKS_PER_ROW; j++)
			{
				Destroy(slots[i, j]);
			}
		}
	}

	public void RotateRight()
	{
		Slot rotationAsSlot = new Slot(0, 1);

		if (CanRotate(GM.currentBlock, rotationAsSlot))
		{
			PlayerBase.transform.Rotate(Vector3.up, -rotationAngle);
			rotationState -= 1;
			GM.SoundHandler.CanRotateNoise();
		}
	}

	private bool CanRotate(GameObject block, Slot rotationAsSlot)
	{
		// happens if block destroyed but none spawned yet
		if (!block)
		{
			return true;
		}

		Slot lowerSlot = grid.LowerSlot(block.transform);
		Slot upperSlot = lowerSlot + new Slot(1, 0);

		foreach (Slot layoutSlot in block.GetComponent<BlockParent>().BlockLayout)
		{
			if (!IsEmpty(lowerSlot + layoutSlot + rotationAsSlot) || !IsEmpty(upperSlot + layoutSlot + rotationAsSlot))
			{
				GetComponentsInChildren<Renderer>()[1].material.SetColor("_BaseColor", Color.red);
				return false;
			}
		}
		GetComponentsInChildren<Renderer>()[1].material.SetColor("_BaseColor", Color.white);
		return true;
	}

	public void RotateLeft()
	{
		Slot rotationAsSlot = new Slot(0, -1);

		if (CanRotate(GM.currentBlock, rotationAsSlot))
		{
			PlayerBase.transform.Rotate(Vector3.up, +rotationAngle);
			rotationState += 1;
			GM.SoundHandler.CanRotateNoise();

		}
	}
}
