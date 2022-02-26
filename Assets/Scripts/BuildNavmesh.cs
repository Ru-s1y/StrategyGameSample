using UnityEngine;
using UnityEngine.AI;

public class BuildNavmesh : MonoBehaviour
{
    void Awake ()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
