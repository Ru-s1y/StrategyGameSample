using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesDirector : MonoBehaviour
{
    public ResourcesBluePrint resources;
    public int defaultValue = 200;
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
        wood.text  = resources.wood.ToString();
        stone.text = resources.stone.ToString();
        food.text  = resources.food.ToString();

        if (timeDis >= 1) {
            float dTime = 1f / Time.deltaTime;
            fps.text = dTime.ToString() + " fps";
            timeDis = 0f;
        }
        timeDis += Time.deltaTime;
    }

    private int GetPropertyValue(string propertyName)
    {
        property = typeof(ResourcesBluePrint).GetProperty(propertyName);
        return (int)property.GetValue(resources);
    }

    public void Increase(string propertyName, int value)
    {
        int updatedValue = GetPropertyValue(propertyName) + value;
        property.SetValue(resources, updatedValue);
    }

    public void Decrease(string propertyName, int value)
    {
        int updatedValue = GetPropertyValue(propertyName) - value;
        property.SetValue(resources, updatedValue);
    }
}
