using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class ObjectStatus : MonoBehaviour
{
    private BuildNavMesh buildNavMesh;
    private ResourcesDirector resourcesDirector;
    public string objName;
    public ObjectStatusBluePrint status;
    public GameObject particlePrefab;
    private GameObject particle;
    public float defaultHp = 20f;
    public float defaultResource = 200;
    public float defaultParticleHeight = 0.3f;

    public float rebuildTime = 2f;

    private PropertyInfo property;

    void Start()
    {
        objName = gameObject.tag;
        status  = new ObjectStatusBluePrint(defaultHp, defaultResource);
        buildNavMesh      = GameObject.Find("Earth").GetComponent<BuildNavMesh>();
        resourcesDirector = GameObject.Find("ResourcesDirector").GetComponent<ResourcesDirector>();
    }

    private float GetPropertyValue(string propertyName)
    {
        property = typeof(ObjectStatusBluePrint).GetProperty(propertyName);
        return (float)property.GetValue(status);
    }

    public void IncreaseStatus(string propertyName, float value)
    {
        float updatedValue = GetPropertyValue(propertyName) + value;
        property.SetValue(status, updatedValue);
    }

    public void DecreaseStatus(string propertyName, float value)
    {
        float propValue = GetPropertyValue(propertyName);
        float updatedValue = 0f;
        if (propValue >= value)
            updatedValue = propValue - value;
        else
            DestroyObject();
        property.SetValue(status, updatedValue);

        // パーティクルの表示
        Vector3 objSize = GetComponent<MeshCollider>().bounds.size;
        Vector3 objPos  = transform.position;
        objPos.y += (objName == "Wood") ? defaultParticleHeight : objSize.y;
        particle = (GameObject)Instantiate(particlePrefab, objPos, transform.rotation);
        particle.transform.parent = transform;
    }

    private void DestroyObject()
    {
        resourcesDirector.Increase(objName, status.resource);
        Destroy(gameObject);
        buildNavMesh.RebuildNavMesh();
    }
}
