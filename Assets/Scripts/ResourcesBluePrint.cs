using UnityEngine;
using System.Collections;

public class ResourcesBluePrint
{
    public float Wood  { get; set;}
    public float Stone { get; set; }
    public float Food  { get; set; }

    public ResourcesBluePrint(float wood, float stone, float food)
    {
        this.Wood  = wood;
        this.Stone = stone;
        this.Food  = food;
    }
}
