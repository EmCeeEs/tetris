using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{

    public float scanRadius = 10;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        float distanceToObstacle = 0;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        //if (Physics.SphereCast(this.transform.position, scanRadius / 100, transform.up, out hit, 10, BaseLayerMask))
        //{
        //    //distanceToObstacle = hit.distance;
        //    Debug.Log(hit);
        //}
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, 10))
        {
            Debug.DrawRay(transform.position, transform.up * scanRadius, Color.yellow, Time.deltaTime, false);
            Debug.DrawRay(transform.position, transform.forward * scanRadius, Color.blue, Time.deltaTime, false);

            Debug.Log("Did Hit");
        }
    }
}
