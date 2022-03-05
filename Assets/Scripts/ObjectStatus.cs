using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class ObjectStatus : MonoBehaviour
{
    private BuildNavMesh buildNavMesh;
    public ObjectStatusBluePrint status;
    public float defaultHp = 20f;
    public float defaultResource = 200;

    public float rebuildTime = 2f;

    private PropertyInfo property;

    void Start()
    {
        status = new ObjectStatusBluePrint(defaultHp, defaultResource);
        buildNavMesh = GameObject.Find("Earth").GetComponent<BuildNavMesh>();
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
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
        buildNavMesh.RebuildNavMesh();
    }
}
