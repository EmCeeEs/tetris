using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Transform blockTransform;
    private Transform centerOfUnivers;
    private GameManager gameManager;
    private Collider meshCollider;
    float timeAlive;
    float fallingSpeed;
    float scalingFactor;
    public float distanceToCenter;
    public Vector3 blockLocalScale;

    public bool isDocked = false;

    void Start()
    {
        centerOfUnivers = FindObjectOfType<Player>().transform;
        gameManager = FindObjectOfType<GameManager>();
        blockTransform = GetComponent<Transform>();
        fallingSpeed = gameManager.fallingSpeed;
    }

    void FixedUpdate()
    {
        #region Scaling
            timeAlive += Time.deltaTime; 
            scalingFactor = 100 * 6 / timeAlive;

            if(timeAlive < 1){
                blockTransform.position = Vector3.one * 100;
            }
            else if (timeAlive <= 6) {
                blockTransform.position = Vector3.zero;
                blockTransform.localScale = new Vector3(scalingFactor, scalingFactor, 100);
            }
        #endregion
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision);
        isDocked = true;
        Debug.Log("collsion");
        blockTransform.transform.SetParent(collision.gameObject.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
    }


}
