using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform blockTransform;
    public Transform blockOrigin;
    public Transform centerOfUnivers;
    public GameManager gameManager;
    float delta;
    float fallingSpeed;
    public float distanceToCenter;
    public Vector3 blockLocalScale;

    public bool isDocked = false;

   

    void Start()
    {
        blockTransform = GetComponent<Transform>();
        blockOrigin = blockTransform.parent.parent.transform;
        centerOfUnivers = FindObjectOfType<Player>().transform;
        gameManager = FindObjectOfType<GameManager>();

        fallingSpeed = gameManager.fallingSpeed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Movement
            delta = Time.deltaTime;
            if (!isDocked)
            {
                BlockMovement(delta);
            }    
        #endregion

        #region Scaling
            distanceToCenter = Vector3.Distance(blockOrigin.transform.position, centerOfUnivers.transform.position);
            blockTransform.localScale = Vector3.one * (1+distanceToCenter/50) * 100;
        #endregion

        #region CollisionDetection

        //if (blockOrigin.transform.position.y < 0.1)
        //{
        //    blockTransform.localScale = Vector3.one * 100;
        //    GameObject.Destroy(this);
        //}
        #endregion
    }

    private void BlockMovement(float delta)
    {
        blockOrigin.position -= Vector3.up * delta * fallingSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision);
        isDocked = true;
        //Debug.Log("collsion");
        blockOrigin.transform.SetParent(collision.gameObject.transform);
    }


}
