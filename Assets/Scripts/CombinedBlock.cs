using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class CombinedBlock : MonoBehaviour
{
    public GameObject PlayerBlockPrefab;

    private List<GameObject> blocks;
    
    private Collider blockCollider;

    public Vector3 spawnScale = new Vector3(10, 1, 10);
    public Vector3 scaleChange = new Vector3(2f, 0.0f, 2f);

    private void Awake()
    {
        blocks = new List<GameObject>();
        CreateCombinedBlock();

        // move to start position
        transform.localScale = Vector3.Scale(transform.localScale, spawnScale);
    }

    private void FixedUpdate()
    {
        transform.localScale -= scaleChange;
    }

    private void CreateCombinedBlock()
    {
        GameObject block1 = Instantiate(PlayerBlockPrefab, transform);
        GameObject block2 = Instantiate(PlayerBlockPrefab, transform);
        GameObject block3 = Instantiate(PlayerBlockPrefab, transform);

        block2.transform.Rotate(Vector3.forward, 30);
        block3.transform.Rotate(Vector3.forward, -30);
        
        blocks.Add(block1);
        blocks.Add(block2);
        blocks.Add(block3);

        CombineMeshes();
    }

    // https://docs.unity3d.com/ScriptReference/Mesh.CombineMeshes.html
    private void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        // Possible Bug
        // For some reason GetComponentsInChildren also returns the mesh of this component.
        // Which is currently null as we are about to set it.
        // To fix this skip the first entry.
        meshFilters = meshFilters.Skip(1).ToArray();

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            // this blows up the scaling BIGTIME
            // supposedly due to https://answers.unity.com/questions/1200226
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);

        transform.GetComponent<MeshFilter>().mesh = mesh;
        transform.GetComponent<MeshCollider>().sharedMesh = mesh;

        // renormalize scaling
        transform.localScale *= 100;
        transform.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        if(other.transform.tag == "Slot")
        {
            Debug.Log("SLOT");
        }
        if (other.transform.tag == "Base")
        {
            Debug.Log("BASE");
        }
    }
}