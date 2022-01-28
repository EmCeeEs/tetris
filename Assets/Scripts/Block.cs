using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameManager gameManager;
    public Transform blockTransform;
    public Transform parentGameObject;
    public List<Transform> additionalBlocks;
    //public List<Transform> currentSlot = new List<Transform>();
    public Transform currentSlot;
    public Collider blockColliders;

    public BlockParent blockParent;

    float timeAlive = 0.1f;
    float scalingFactor;
    public int blockCounter = 1;
    public bool isDocked = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //blockCollider = GetComponentInChildren<Collider>();
        blockTransform = GetComponent<Transform>();
        parentGameObject = GetComponentInParent<Transform>();
        blockParent = GetComponentInParent<BlockParent>();
    }

    private void Awake()
    {
        blockColliders = GetComponent<Collider>();
        blockColliders.enabled = false;
    }



    void FixedUpdate()
    {
        timeAlive += Time.deltaTime;
        BlockScaling(timeAlive);
        DeleteAndDockBlock();
    }

    public void BlockScaling(float timeAlive)
    {
        scalingFactor = 6 / timeAlive;

        if (timeAlive < 1)
        {
            blockColliders.enabled = false;
            blockTransform.localScale = new Vector3(scalingFactor, scalingFactor, 1);
        }
        else if (timeAlive <= 6)
        {
            blockColliders.enabled = true;
            blockTransform.localScale = new Vector3(scalingFactor, scalingFactor, 1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if PlayerBlock moves through Slots around the Base and Adds to List
        if(other.transform.tag == "slot")
        {
            currentSlot = other.transform;
        }
        // Check if PlayerBlock touches base 
        if (other.transform.tag == "base")
        {
            blockColliders.enabled = false;      
            blockParent.isDocked = true;
        }
    }

    // Function to add additionalBlocks to archtype
    public void AddBlockToBlock(GameObject newBlock)
    {
        Transform newBlockTransform = newBlock.GetComponent<Transform>();
        additionalBlocks.Add(newBlockTransform);
    }

    public void DeleteAndDockBlock()
    {
        if (blockParent.isDocked && currentSlot)
        {
            blockColliders.enabled = false;
            gameManager.toSpawnLocations.Add(currentSlot);
            Destroy(blockTransform.gameObject);
        }
    }
}
