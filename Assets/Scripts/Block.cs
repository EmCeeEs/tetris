using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameManager gameManager;
    public Transform blockTransform;
    //public Transform parentGameObject;
    public List<Transform> additionalBlocks;
    //public List<Transform> currentSlot = new List<Transform>();
    public Transform currentSlot;
    public Collider blockColliders;

    float timeAlive = 0.1f;
    float scalingFactor;
    public int blockCounter = 1;
    public bool isDocked = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //blockCollider = GetComponentInChildren<Collider>();
        blockTransform = GetComponent<Transform>();
    }

    private void Awake()
    {
        blockColliders = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        timeAlive += Time.deltaTime;
        BlockScaling(timeAlive);
    }

    public void BlockScaling(float timeAlive)
    {
        scalingFactor = 6 / timeAlive;

        if (timeAlive < 1)
        {
            //foreach (Collider collider in blockColliders)
            //{
            blockColliders.enabled = false;
            //}
            blockTransform.localScale = new Vector3(scalingFactor, scalingFactor, 1);
            //foreach (Transform block in additionalBlocks)
            //{
            //    block.GetComponent<Collider>().enabled = false;
            //    block.localScale = new Vector3(scalingFactor, scalingFactor, 1);
            //}
        }
        else if (timeAlive <= 6)
        {
            //foreach (Collider collider in blockColliders)
            //{
            //    collider.enabled = true;
            //}
            blockColliders.enabled = true;

            blockTransform.localScale = new Vector3(scalingFactor, scalingFactor, 1);
            //foreach (Transform block in additionalBlocks)
            //{
            //    block.GetComponent<Collider>().enabled = true;
            //    block.localScale = new Vector3(scalingFactor, scalingFactor, 1);
            //}
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if PlayerBlock moves through Slots around the Base and Adds to List
        if(other.transform.tag == "slot")
        {
            //Debug.Log(other.transform.name);
            currentSlot = other.transform;
            //Debug.Log(currentSlot);

        }
        // Check if PlayerBlock touches base 
        if (other.transform.tag == "base")
        {
            isDocked = true;
            //parentGameObject.transform.localScale = Vector3.one;
            //for (int i = currentSlot.Count - blockCounter; i < currentSlot.Count; i++)
            //{
            //    Debug.Log(currentSlot[i]);
                //gameManager.BlockOnBaseSpawner(currentSlot);
            gameManager.toSpawnLocations.Add(currentSlot);
            //}
        }
    }

    // Function to add additionalBlocks to archtype
    public void AddBlockToBlock(GameObject newBlock)
    {
        Transform newBlockTransform = newBlock.GetComponent<Transform>();
        additionalBlocks.Add( newBlockTransform);
    }
}
