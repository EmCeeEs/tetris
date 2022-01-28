using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<GameObject> BlockParents;
    public GameObject BlockParent; // prefab

    GameObject newBlock;
    public Transform centerOfUnivers;
    public Transform spawnPoint;
    public Transform brickHolder;

    public GameObject initalBlock;

    public float fallingSpeed = 5.0f;
    public float scalingFactor = 1.0f;
    private float timer;
    private float spawnIntervall = 3f;
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
        Debug.Log(timer);
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

    public void BlockSpawner()
    {
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        GameObject blockParent = Instantiate(BlockParent, spawnPoint, Quaternion.identity);
        BlockParents.Add(blockParent);
    }

    // Spawn Block on PlayerBase
    public void BlockOnBaseSpawner(Transform currentSlot)
    {
        newBlock = Instantiate(blockLevels[int.Parse(currentSlot.transform.name)-1]);
        newBlock.transform.localScale = Vector3.one * 100;
        newBlock.transform.rotation = currentSlot.parent.transform.rotation;

        //newBlock.transform.parent = brickHolder;
        newBlock.transform.parent = currentSlot;
    }
}
