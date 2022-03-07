using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStatusBluePrint
{
    public float hp       { get; set; }
    public float resource { get; set; }

    public ObjectStatusBluePrint(float hp, float resource)
    {
        this.hp       = hp;
        this.resource = resource;
    }
}
