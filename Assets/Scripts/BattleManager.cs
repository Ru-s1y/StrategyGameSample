using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public void AttackUnit(GameObject unit, GameObject target)
    {
        UnitStatus unitStatus   = unit.GetComponent<UnitStatus>();
        UnitStatus targetStatus = target.GetComponent<UnitStatus>();

        targetStatus.DecreaseStatus("hp", unitStatus.status.atk);
        Debug.Log(targetStatus.status.hp);
    }

    public void AttackObject(GameObject unit, GameObject obj)
    {
        UnitStatus   unitStatus = unit.GetComponent<UnitStatus>();
        ObjectStatus objStatus  = obj.GetComponent<ObjectStatus>();

        objStatus.DecreaseStatus("hp", unitStatus.status.atk);
        Debug.Log(objStatus.status.hp);
    }
}
