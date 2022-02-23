using System.Collections;
using UnityEngine;

using System.Linq;

public class Board : MonoBehaviour
{
	private GameManager GM;
	public ColorManager ColorManager;

	public Material disolve;

	public bool foundRow = false;
	public float disolveTimer = 0;

	public GameObject PlayerBase;

	private int rotationState = 0;
	private GameObject[,] slots;

	void Awake()
	{
		GM = GameManager.Instance;

		slots = new GameObject[12, Geometry.PERIODICITY];
	}

	public bool IsEmpty(Slot slot)
	{
		if (slot.X <= -1)
		{
			return false;
		}

		int totalRotation = Utils.Mod(slot.Y - rotationState, Geometry.PERIODICITY);
		return slots[slot.X, totalRotation] == null;
	}

	public void SetSlot(Slot slot, GameObject block)
	{
		if (slot.X < ColorManager.colors.Length)
		{
			int totalRotation = Utils.Mod(slot.Y - rotationState, Geometry.PERIODICITY);
			block.transform.SetParent(PlayerBase.transform);

			GM.CurrentScore += GM.ScoreSettings.baseBlockScore;
			GM.UIHandler.UpdateScore(GM.CurrentScore);

			slots[slot.X, totalRotation] = block;
			block.GetComponentsInChildren<Renderer>()[0].material.SetColor("_BaseColor", ColorManager.colors[slot.X]);
		}
	}

	public void CheckForCompleteRows()
	{
		int offset = 0;
		foreach (int rowNumber in Enumerable.Range(0, slots.GetLength(0)))
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
					GM.CurrentScore += GM.ScoreSettings.singleRowScore;
					GM.UIHandler.UpdateScore(GM.CurrentScore);
					GM.Speed += 1;
				}
			}
		}
	}


	private bool IsRowComplete(int rowNumber)
	{
		return Enumerable.Range(0, Geometry.PERIODICITY)
			.Select(x => slots[rowNumber, x] != null)
			.All(x => x);
	}


	private IEnumerator RemoveRow(int rowNumber)
	{
		// remove row
		for (int j = 0; j < Geometry.PERIODICITY; j++)
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
		for (int i = rowNumber + 1; i < slots.GetLength(0); i++)
		{
			for (int j = 0; j < Geometry.PERIODICITY; j++)
			{
				if (slots[i, j] != null)
				{
					slots[i - 1, j] = slots[i, j];
					Geometry.MoveToPoint(new Slot(i - 1, j), slots[i, j]);
					slots[i - 1, j].GetComponentsInChildren<Renderer>()[0].material.SetColor("_BaseColor", ColorManager.colors[i - 1]);
					slots[i, j] = null;
				}
			}
		}
		foundRow = false;
	}

	public void Clear()
	{
		for (int i = 0; i < slots.GetLength(0); i++)
		{
			for (int j = 0; j < Geometry.PERIODICITY; j++)
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
			PlayerBase.transform.Rotate(Vector3.up, -Geometry.RotationAngle());
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

		BlockParent blockScript = block.GetComponent<BlockParent>();
		Slot lowerSlot = GridUtils.SnapToNextX(blockScript.Position);
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
			PlayerBase.transform.Rotate(Vector3.up, +Geometry.RotationAngle());
			rotationState += 1;
			GM.SoundHandler.CanRotateNoise();

		}
	}
}
