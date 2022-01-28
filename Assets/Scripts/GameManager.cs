using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject playerBlock;
    public GameObject playerBlockLevel1;
    public GameObject playerBlockLevel2;
    GameObject newBlock;
    Block newBlockBock;
    public Transform centerOfUnivers;
    public Transform spawnPoint;
    public Transform brickHolder;

    public GameObject initalBlock;

    public float fallingSpeed = 5.0f;
    public float scalingFactor = 1.0f;
    private float timer;
    public float spawnIntervall = 100f;
    public float rotationAmount = 30;
    public List<Transform> toSpawnLocations;
    public GameObject[] blockLevels;

    public GameObject[] column1;
    public GameObject[] column2;
    public GameObject[] column3;
    public GameObject[] column4;
    public GameObject[] column5;
    public GameObject[] column6;
    public GameObject[] column7;
    public GameObject[] column8;
    public GameObject[] column9;
    public GameObject[] column10;
    public GameObject[] column11;
    public GameObject[] column12;

    private void Awake()
    {
        BlockSpawner();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > spawnIntervall)
        {
            BlockSpawner();
            timer = 0;
        }
    }

    private void LateUpdate()
    {
        foreach (Transform slot in toSpawnLocations)
        {
            BlockOnBaseSpawner(slot);
        }
        toSpawnLocations.Clear();
    }

    // Spawns new PlayerBlock
    public void BlockSpawner()
    {
        initalBlock = Instantiate(playerBlock, spawnPoint);
        initalBlock.GetComponentInChildren<Collider>().enabled = false;

        // Get Script Component of new PlayerBlock
        Block addBlockBlock = initalBlock.GetComponentInChildren<Block>();
        BlockParent blockParent = initalBlock.GetComponent<BlockParent>();
        blockParent.PlayerBlocks.Add(initalBlock);

        float rotationAmount = 30;

        for (int i = 1; i < 2; i++)
        {
            // Instantiate other "arctype" and disable Collider to to spawnPointColiision
            GameObject addBlock = Instantiate(playerBlockLevel1, spawnPoint);

            // Add to BlockParentList
            blockParent.PlayerBlocks.Add(addBlock);

            addBlock.GetComponent<Collider>().enabled = false;
            addBlock.AddComponent<Block>();

            // Rotate new Block and parent to Outside Container PlayerBlock
            addBlock.transform.Rotate(Vector3.forward, rotationAmount);
            addBlock.transform.parent = initalBlock.transform;

        }
        initalBlock.transform.localScale = Vector3.one * 100;
    }

    // Spawn Block on PlayerBase
    public void BlockOnBaseSpawner(Transform currentSlot)
    {
        newBlock = Instantiate(blockLevels[int.Parse(currentSlot.transform.name)-1]);
        newBlock.transform.rotation = currentSlot.parent.transform.rotation;

        newBlock.transform.parent = brickHolder;
    }
}
