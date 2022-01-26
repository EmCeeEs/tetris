using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform blockTransform;
    public Transform parentGameObject;
    public Transform[] additionalBlocks;
    private Transform currentSlot;
    private GameManager gameManager;
    public Collider blockCollider;
    float timeAlive = 0.1f;
    float fallingSpeed;
    float scalingFactor;
    public float distanceToCenter;
    public Vector3 blockLocalScale;

    public bool isDocked = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        blockCollider = GetComponentInChildren<Collider>();
        blockTransform = GetComponent<Transform>();
        //additionalBlocks = parentGameObject.GetComponentsInChildren<Transform>();
        //additionalBlocks[0] = null;
        fallingSpeed = gameManager.fallingSpeed;
    }

    void FixedUpdate()
    {
        #region Scaling
            timeAlive += Time.deltaTime; 
            scalingFactor = 6 / timeAlive ;

            if(timeAlive < 1){
            //Debug.Break();
                blockCollider.enabled = false;
                blockTransform.localScale = new Vector3(scalingFactor, scalingFactor, 1);
                foreach (Transform block in additionalBlocks) {
                    block.localScale =  new Vector3(scalingFactor, scalingFactor, 1);
                }
        }
        else if (timeAlive <= 6 ) {
                blockCollider.enabled = true;
                blockTransform.localScale = new Vector3(scalingFactor, scalingFactor, 1);
            foreach (Transform block in additionalBlocks)
            {
                block.localScale =  new Vector3(scalingFactor, scalingFactor, 1);
            }
        }
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        if(other.transform.tag == "slot")
        {
            currentSlot = other.transform;
        }


        if (other.transform.tag == "base")
        {
            isDocked = true;
            parentGameObject.transform.localScale = Vector3.one;
            //blockTransform.transform.SetParent(other.gameObject.transform);
            blockTransform.gameObject.SetActive(false);
            gameManager.BlockOnBaseSpawner(currentSlot);
        }
    }


}
