using System.Collections; // IEnumerator
using System.Collections.Generic; // IEnumerable
using System.Linq;

using UnityEngine;

namespace Tetris
{
	public class Board : MonoBehaviour
	{
		private GameManager GM;
		public ColorManager ColorManager;

		public Material disolve;

		public GameObject PlayerBase;

		[SerializeField]
		private int rotationState = 0;
		private GameObject[,] slots;

		// Move to settings
		private const int ANIMATION_DURATION = 1;
		private const int N_ROWS = 13;

		void Awake()
		{
			GM = GameManager.Instance;

			slots = new GameObject[N_ROWS, Geometry.PERIODICITY];
		}

		public void CheckForCompleteRows()
		{
			List<int> completedRows = GetCompletedRows();

			int nCompletedRows = completedRows.Count();

			if (nCompletedRows > 0)
			{
				StartCoroutine(
					RemoveRowsAnimated(completedRows, ANIMATION_DURATION)
				);

				GM.CurrentScore += GM.ScoreParameters.GetRowPoints(nCompletedRows);
				GM.Speed += 1;
			}
		}

		private IEnumerator RemoveRowsAnimated(List<int> completedRows, int seconds)
		{
			GM.StartAnimation();

			completedRows.ForEach(rowNumber => SetRowMaterial(rowNumber, disolve));

			for (float time = 0; time <= seconds; time += Time.deltaTime)
			{
				disolve.SetFloat("_time", time);
				yield return null;
			}
			disolve.SetFloat("_time", 0);

			RemoveRows(completedRows);

			GM.StopAnimation();
		}

		private void RemoveRows(List<int> completedRows)
		{
			int offset = 0;
			foreach (int rowNumber in completedRows)
			{
				ShiftRowsOnto(rowNumber - offset);
				offset++;
			}
		}

		private void ShiftRowsOnto(int rowNumber)
		{
			ClearRow(rowNumber);

			for (int i = rowNumber + 1; i < slots.GetLength(0); i++)
			{
				for (int j = 0; j < Geometry.PERIODICITY; j++)
				{
					if (slots[i, j] != null)
					{
						slots[i - 1, j] = slots[i, j];
						Geometry.MoveToPoint(new Slot(i - 1, j), slots[i, j]);
						SetBlockColor(slots[i - 1, j], ColorManager.colors[i - 1]);
						slots[i, j] = null;
					}
				}
			}
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

				slots[slot.X, totalRotation] = block;
				SetBlockColor(block, ColorManager.colors[slot.X]);

				GM.CurrentScore += GM.ScoreParameters.GetBlockPoints();
			}
		}

		public void Clear()
		{
			for (int iRow = 0; iRow < slots.GetLength(0); iRow++)
			{
				ClearRow(iRow);
			}
		}

		public void ClearRow(int iRow)
		{
			for (int j = 0; j < Geometry.PERIODICITY; j++)
			{
				Destroy(slots[iRow, j]);
				slots[iRow, j] = null;
			}
		}

		private List<int> GetCompletedRows()
			=> Enumerable.Range(0, slots.GetLength(0))
				.Where(IsRowComplete)
				.ToList();

		private bool IsRowComplete(int rowNumber)
			=> Enumerable.Range(0, Geometry.PERIODICITY)
				.All(x => slots[rowNumber, x] != null);

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
					SetPlayerBaseColor(Color.red);
					return false;
				}
			}
			SetPlayerBaseColor(Color.white);
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

		private void SetBlockColor(GameObject block, Color32 color)
			=> block.GetComponentInChildren<Renderer>().material.SetColor("_BaseColor", color);

		private void SetBlockMaterial(GameObject block, Material material)
			=> block.GetComponentInChildren<Renderer>().material = material;

		private void SetRowMaterial(int rowNumber, Material material)
			 => Enumerable.Range(0, Geometry.PERIODICITY)
				 .ToList()
				.ForEach(
					(columnIndex) => SetBlockMaterial(slots[rowNumber, columnIndex], material)
				);

		private void SetPlayerBaseColor(Color32 color)
			=> PlayerBase.GetComponentsInChildren<Renderer>()[1].material.SetColor("_BaseColor", color);
	}
}
