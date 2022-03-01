using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBuild : MonoBehaviour
{
    [HideInInspector] public GameObject marker;
    public GameObject markerPrefab;

    public Color32 unitColor;
    public Color32 targetColor;

    private Vector3 markerRotate;

    void Start()
    {
        markerRotate = new Vector3(180, 0, 0);
        unitColor    = new Color32(0, 0, 255, 137);
        targetColor  = new Color32(255, 0, 0, 137);
    }

    // ユニットにマーカーをつける
    public void SetUnitMarker(GameObject unit)
    {
        Vector3 unitPos   = unit.transform.position;
        float unitHeight  = unit.GetComponent<CapsuleCollider>().height;
        unitPos.y += unitHeight;
        SetMarker(unitPos);

        marker.GetComponent<Renderer>().material.color = unitColor;
        marker.name = "UnitMarker";
        marker.transform.parent = unit.transform;
    }

    // 指定位置にマーカーを生成
    public void SetMarker(Vector3 markerPos)
    {
        markerPos.y += 0.5f;
        marker = (GameObject)Instantiate(markerPrefab, markerPos, Quaternion.Euler(markerRotate));
        marker.GetComponent<Renderer>().material.color = targetColor;
        marker.name = "TargetMarker";
    }

    // ユニットマーカーの削除
    public void DestroyUnitMarker(GameObject unit)
    {
        if (unit.transform.Find("UnitMarker").gameObject)
        {
            Destroy(unit.transform.Find("UnitMarker").gameObject);
        }
    }
}
