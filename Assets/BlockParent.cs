using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParent : MonoBehaviour
{
    public List<GameObject> PlayerBlocks;

    public bool isDocked; // used by block

    public GameObject playerBlockLevel1;

    public Transform spawnPoint;

    public void Start()
    {
        BlockSpawner();
    }

    public void LateUpdate()
    {
        if (isDocked)
        {
            Destroy(this.gameObject,0.5f);
        }
    }

    public void BlockSpawner()
    {
        GameObject block1 = Instantiate(playerBlockLevel1, this.gameObject.transform);
        PlayerBlocks.Add(block1);

        if (Random.Range(0,2) == 1)
        {
            GameObject block2 = Instantiate(playerBlockLevel1, this.gameObject.transform);
            block2.transform.Rotate(Vector3.forward, 30);
            PlayerBlocks.Add(block2);
        }

        if (Random.Range(0, 2) == 1)
        {
            GameObject block3 = Instantiate(playerBlockLevel1, this.gameObject.transform);
            block3.transform.Rotate(Vector3.forward, -30);
            PlayerBlocks.Add(block3);
        }
    }
}
