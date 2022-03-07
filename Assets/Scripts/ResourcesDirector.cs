using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesDirector : MonoBehaviour
{
    public ResourcesBluePrint resources;
    public float defaultValue = 200f;
    private PropertyInfo property;

    public Text wood;
    public Text stone;
    public Text food;
    public Text fps;
    public float timeDis = 0f;

    void Start()
    {
        resources = new ResourcesBluePrint(defaultValue, defaultValue, defaultValue);
        fps.text = "0 fps";
    }

    void Update()
    {
        wood.text  = resources.Wood.ToString();
        stone.text = resources.Stone.ToString();
        food.text  = resources.Food.ToString();

        if (timeDis >= 1) {
            float dTime = 1f / Time.deltaTime;
            fps.text = dTime.ToString() + " fps";
            timeDis = 0f;
        }
        timeDis += Time.deltaTime;
    }

    private float GetPropertyValue(string propertyName)
    {
        property = typeof(ResourcesBluePrint).GetProperty(propertyName);
        return (float)property.GetValue(resources);
    }

    public void Increase(string propertyName, float value)
    {
        float updatedValue = GetPropertyValue(propertyName) + value;
        property.SetValue(resources, updatedValue);
    }

    public void Decrease(string propertyName, float value)
    {
        float propValue = GetPropertyValue(propertyName);
        float updatedValue = 0f;
        if (propValue >= value)
            updatedValue = propValue - value;
        property.SetValue(resources, updatedValue);
    }
}
