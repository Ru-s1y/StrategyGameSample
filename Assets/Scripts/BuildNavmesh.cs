using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildNavMesh : MonoBehaviour
{
    private NavMeshSurface navSur;
    public float delayTime = 1f;

    void Start()
    {
        navSur = GameObject.Find("Earth").GetComponent<NavMeshSurface>();
    }

    public void RebuildNavMesh()
    {
        StartCoroutine("Bake");
    }

    IEnumerator Bake()
    {
        yield return new WaitForSeconds(delayTime);
        navSur.BuildNavMesh();
        Debug.Log("ビルドしました。");
    }
}
