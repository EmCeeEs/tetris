using System.Collections.Generic;
using System.Linq;

using UnityEngine;


// use type alias (this file only)
using Layout = System.Collections.Generic.List<Slot>;

public class BlockSpawner : MonoBehaviour
{
	private GameManager GM;

	public GameObject BlockPrefab;
	public GameObject BlockParentPrefab;

	public List<Layout> layouts = LayoutCreator.Create3BlockLayouts();
	// public List<Layout> layouts = LayoutCreator.Create4BlockLayouts();
	// public List<Layout> layouts = LayoutCreator.SquareBlockzz();

	private readonly Slot spawnSlot = new Slot(10, 0);

	public void Awake()
	{
		GM = GameManager.Instance;
	}

	public GameObject SpawnBlock()
	{
		int layoutIndex = Random.Range(0, layouts.Count);
		Layout blockLayout = layouts[layoutIndex];

		bool canSpawn = blockLayout.All(slot => GM.Board.IsEmpty(slot + spawnSlot));
		if (!canSpawn)
		{
			GM.EndGame();
			return null;
		};

		return InstantiateBlockFromLayout(spawnSlot, blockLayout);
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
	public static List<Layout> SquareBlockzz()
		=> new List<Layout>(){
			// I layout
			new Layout(){
				new Slot(-1, 0),
				new Slot(0, 0),
				new Slot(1, 0),
			},
		};

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

	public static List<Layout> Create4BlockLayouts()
		=> new List<Layout>(){
            // 2x2 layout
            new Layout(){
				new Slot(0, 0),
				new Slot(0, 1),
				new Slot(1, 1),
				new Slot(1, 0),
			},
            // I layout
            new Layout(){
				new Slot(3, 0),
				new Slot(2, 0),
				new Slot(1, 0),
				new Slot(0, 0),
			},
            // Line layout
            new Layout(){
				new Slot(0, -1),
				new Slot(0, 0),
				new Slot(0, 1),
				new Slot(0, 2),
			},
            // L layout
            new Layout(){
				new Slot(2, 0),
				new Slot(1, 0),
				new Slot(0, 0),
				new Slot(0, 1),
			},
            // J layout
            new Layout(){
				new Slot(2, 0),
				new Slot(1, 0),
				new Slot(0, 0),
				new Slot(0, -1),
			},
			// ___| layout
            new Layout(){
				new Slot(0, -1),
				new Slot(0, 0),
				new Slot(0, 1),
				new Slot(1, 1),
			},
			// |___ layout
            new Layout(){
				new Slot(1, -1),
				new Slot(0, -1),
				new Slot(0, 0),
				new Slot(0, 1),
			},
            // S layout
            new Layout(){
				new Slot(0, -1),
				new Slot(0, 0),
				new Slot(1, 0),
				new Slot(1, 1),
			},
            // Z layout
            new Layout(){
				new Slot(1, -1),
				new Slot(1, 0),
				new Slot(0, 0),
				new Slot(0, 1),
			},
			// T layout
			new Layout(){
				new Slot(0, -1),
				new Slot(0, 0),
				new Slot(1, 0),
				new Slot(0, 1),
			},
			// |- layout
			new Layout(){
				new Slot(1, 0),
				new Slot(0, 0),
				new Slot(0, 1),
				new Slot(-1, 0),
			},
			// -| layout
			new Layout(){
				new Slot(1, 0),
				new Slot(0, 0),
				new Slot(0, -1),
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
