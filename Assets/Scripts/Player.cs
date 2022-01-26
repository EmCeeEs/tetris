using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float vertical;
    PlayerControls inputActions;

    public Transform playerBase;

    public bool moveLeft;
    public bool moveRight;

    private int cooldownTimer = 0;
    private int MAX_COOLDOWN = 3;

    public Transform rayCastOrigin;
    public float scanRadius = 30f;

    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        if (cooldownTimer == 0)
        {
            HandleRotation();
        }
        else
        {
            cooldownTimer -= 1;
        }
        //SlotStatusHandler();
    }

    void HandleRotation()
    {
        moveLeft = inputActions.PlayerMovement.PlayerRotationLeft.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
        moveRight = inputActions.PlayerMovement.PlayerRotationRight.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

        if (moveRight && !moveLeft)
        {
            playerBase.Rotate(new Vector3(0, 0, 1), 30);
            cooldownTimer = MAX_COOLDOWN;
        }
        if (!moveRight && moveLeft)
        {
            playerBase.Rotate(new Vector3(0, 0, 1), -30);
            cooldownTimer = MAX_COOLDOWN;
        }


    }

    //void SlotStatusHandler()
    //{
    //    RaycastHit[] hits;

    //    hits = Physics.SphereCastAll(rayCastOrigin.transform.position, scanRadius , transform.up);

    //    //Debug.Log(hits.Length);

    //    //if (Physics.SphereCast(rayCastOrigin.transform.position, scanRadius+10, transform.up, out hit))
    //    //{
    //    //    //distanceToObstacle = hit.distance;
    //    //    Debug.Log("Hit Sphere in level 2");
    //    //}
    //}

}
