using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] blocks;
    GameObject currentBlock;
    public Transform centerOfUnivers;
    public Transform spawnPoint;
    [SerializeField]
    public float fallingSpeed = 5.0f;
    public float scalingFactor = 1.0f;
    float timer;
    public float spawnIntervall = 2.0f;


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
        // TODO:
        // Set start position
        // select random block
        // Block falls down to center & scales

    }

    public void BlockSpawner()
    {
            currentBlock = blocks[Random.Range(0, blocks.Length)];
            Instantiate(currentBlock, spawnPoint);     
    }
}
