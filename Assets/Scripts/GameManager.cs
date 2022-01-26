using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject playerBlock;
    public GameObject playerBlockLevel1;
    public GameObject playerBlockLevel2;
    GameObject newBlock;
    public Transform centerOfUnivers;
    public Transform spawnPoint;
    public Transform brickHolder;

    public float fallingSpeed = 5.0f;
    public float scalingFactor = 1.0f;
    private float timer;
    public float spawnIntervall = 3f;
    public float rotationAmount = 30;

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


    // Start is called before the first frame update
    void Start()
    {
          
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

    public void BlockSpawner()
    {
        GameObject block = Instantiate(playerBlock, spawnPoint);

        float rotationAmount = 30;
        for (int i = 1; i < 3; i++)
        {
            GameObject addBlock = Instantiate(playerBlockLevel1, spawnPoint);
            addBlock.transform.localScale = Vector3.one;
            addBlock.transform.Rotate(Vector3.forward, rotationAmount);
            addBlock.transform.parent = block.transform;
            rotationAmount = rotationAmount + 30;
        }
        block.transform.localScale = Vector3.one * 100;
    }
    public void BlockOnBaseSpawner(Transform currentBlock)
    {
        Debug.Log(currentBlock.transform.name);
        newBlock = Instantiate(blockLevels[int.Parse(currentBlock.transform.name)-1]);
        newBlock.transform.parent = brickHolder;
    }
}
