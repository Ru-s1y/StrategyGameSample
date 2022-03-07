using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBluePrint
{
    public float hp  { get; set; }
    public float atk { get; set; }

    public StatusBluePrint(float hp, float atk)
    {
        this.hp  = hp;
        this.atk = atk;
    }
}
