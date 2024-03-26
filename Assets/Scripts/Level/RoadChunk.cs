using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChunk : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField]
    private Vector3 size;

    public float GetLength()
    {
        return size.z;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

}
