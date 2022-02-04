using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isMoving = false;

    private Board board;

    public float scaleChange = 0.02f;
    private float currentScale;

    private void Awake()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        SetScale(0);
    }

    private void Start()
    {
        isMoving = true;
    }


    private void Update()
    {
        if (isMoving) {
            float nextScale = currentScale - scaleChange;
            int nextSlot = Utils.Scale2Slot(nextScale);

            if (board.IsEmpty(nextSlot))
            {
                SetScale(nextScale);
            }
            else
            {
                int currentSlot = Utils.Scale2Slot(currentScale);
                board.SetSlot(currentSlot, this.gameObject);
                isMoving = false;
            }
        }
    }

    public float GetScale()
    {
        return currentScale;
    }

    public void SetScale(float scale)
    {
        currentScale = scale;
        transform.localScale = new Vector3(currentScale + 1, 1, currentScale + 1);
    }
}
