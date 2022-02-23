using System.Collections.Generic;
using UnityEngine;

using System.Linq;

// use type alias
using Layout = System.Collections.Generic.List<Slot>;

public class BlockSpawner : MonoBehaviour
{
	private GameManager GM;

	public GameObject BlockPrefab;
	public GameObject BlockParentPrefab;

	public List<Layout> layouts = LayoutCreator.Create3BlockLayouts();

	private readonly Slot spawnSlot = new Slot(8, 0);

	public void Awake()
	{
		GM = GameManager.Instance;
	}

	public GameObject SpawnBlock()
	{
		GameObject newBlock = null;

		int layoutIndex = UnityEngine.Random.Range(0, layouts.Count);
		Layout blockLayout = layouts[layoutIndex];

		bool canSpawn = blockLayout.All(slot => GM.Board.IsEmpty(slot + spawnSlot));
		if (!canSpawn)
		{
			GM.EndGame();
		}
		else
		{
			if (!GM.Board.foundRow)
			{
				newBlock = InstantiateBlockFromLayout(spawnSlot, blockLayout);
			}
		}
		return newBlock;
	}


	private GameObject InstantiateBlockFromLayout(Slot spawnSlot, Layout layout)
	{

		GameObject parent = Instantiate(BlockParentPrefab);
		List<GameObject> blocks = new List<GameObject>();

		GameObject block;
		foreach (Slot slot in layout)
		{
			block = Instantiate(BlockPrefab, parent.transform);
			Geometry.MoveToPoint(slot, block);
			blocks.Add(block);
		}

		parent.GetComponent<BlockParent>().BlockLayout = layout;
		parent.GetComponent<BlockParent>().Blocks = blocks;
		parent.GetComponent<BlockParent>().Position = spawnSlot.AsPoint();

		Geometry.MoveToPoint(spawnSlot, parent);

		return parent;
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
