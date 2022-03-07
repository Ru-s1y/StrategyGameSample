using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    public StatusBluePrint status;
    public float defaultHp = 15f;
    public float defaultAtk = 3f;

    private PropertyInfo property;

    void Start()
    {
        status = new StatusBluePrint(defaultHp, defaultAtk);
    }

    private float GetPropertyValue(string propertyName)
    {
        property = typeof(StatusBluePrint).GetProperty(propertyName);
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
            GetComponent<UnitMove>().Die();
        property.SetValue(status, updatedValue);
    }
}
