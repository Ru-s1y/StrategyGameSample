using UnityEngine;
using System.Collections;

public class ResourcesBluePrint
{
    public int wood { get; set;}
    public int stone { get; set; }
    public int food { get; set; }

    public ResourcesBluePrint(int wood, int stone, int food)
    {
        this.wood  = wood;
        this.stone = stone;
        this.food  = food;
    }
}
