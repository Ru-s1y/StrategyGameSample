using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerDirector : MonoBehaviour
{
    public GameObject prefab;
    public GameObject UnitMarkerPrefab;
    public GameObject TargetMarkerPrefab;
    public GameObject selectAreaPrefab;
    public GameObject targetMarker;

    public string unitTagName = "Player";
    public GameObject[] units;
    private bool unitsExist { get { return units != null; } }

    private GameObject selectArea;

    private Vector3 mouseDownPoint;
    private Vector3 mousePressPoint;
    private Vector3 mouseUpPoint;
    private Vector3 markerRotate;

    private bool inputing = false;
    void Start()
    {
        markerRotate = new Vector3(180, 0, 0);
    }

    void Update()
    {
        // クリック押した時
        if (Input.GetMouseButtonDown(0) && !inputing)
        {
            if (targetMarker != null)
                Destroy(targetMarker);

            mouseDownPoint = GetTouchPoint();
            if (mouseDownPoint != Vector3.zero)
            {
                mouseDownPoint.y = 1f;
                selectArea = (GameObject)Instantiate(selectAreaPrefab, mouseDownPoint, Quaternion.identity);
                inputing = true;
            }
        }

        // selectareaの描画処理
        if (!Input.GetMouseButtonUp(0) && inputing)
        {
            mousePressPoint = GetTouchPoint();
            float sizeX = selectArea.GetComponent<Renderer>().bounds.size.x;
            float sizeZ = selectArea.GetComponent<Renderer>().bounds.size.z;

            Vector3 temp = selectArea.transform.localScale;
            temp.x = mousePressPoint.x - mouseDownPoint.x;
            temp.z = mousePressPoint.z - mouseDownPoint.z;

            float shiftX = (temp.x > 0) ? mouseDownPoint.x + (sizeX / 2) : mouseDownPoint.x - (sizeX / 2);
            float shiftZ = (temp.z > 0) ? mouseDownPoint.z + (sizeZ / 2) : mouseDownPoint.z - (sizeZ / 2);

            selectArea.transform.localScale = temp;
            selectArea.transform.position   = new Vector3(shiftX, 0, shiftZ);
        }

        // クリック離した時
        if (Input.GetMouseButtonUp(0) && inputing)
        {
            mouseUpPoint = GetTouchPoint();
            if (mouseUpPoint != Vector3.zero)
            {
                inputing = false;

                if (unitsExist)
                    DestroyCurrentMarker(); // 既存マーカーの削除

                SetSelectUnit();
                SetUnitMarker();

                Destroy(selectArea);
            }
        }

        // 右クリック時
        if (Input.GetMouseButtonDown(1) && unitsExist)
        {
            Vector3 point = GetTouchPoint();

            if (targetMarker != null)
                Destroy(targetMarker.gameObject);

            point.y += 0.5f;
            targetMarker = (GameObject)Instantiate(TargetMarkerPrefab, point, Quaternion.Euler(markerRotate));

            foreach (GameObject unit in units)
            {
                UnitMove unitMove = unit.GetComponent<UnitMove>();
                unitMove.targetMarker = targetMarker;
                unitMove.Move(point);
            }
        }
    }

    // マウスクリックの位置を世界座標で返す
    private Vector3 GetTouchPoint()
    {
        RaycastHit hit;
        Ray touchPointToRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(touchPointToRay, out hit, 100)) {
            return hit.point;
        }
        return Vector3.zero;
    }

    // 選択したユニットをセットする
    private void SetSelectUnit()
    {
        Collider[] colliderList = Physics.OverlapBox(
            selectArea.transform.position,
            selectArea.GetComponent<Renderer>().bounds.size,
            selectArea.transform.rotation
        );

        if (colliderList.Length == 0) return;

        int i = 0;
        GameObject[] temp = null;
        foreach (Collider collider in colliderList)
        {
            if (collider.gameObject.CompareTag(unitTagName))
            {
                Array.Resize<GameObject>(ref temp, i + 1);
                temp[i] = collider.gameObject;
                i++;
            }
        }
        units = temp;
    }

    private void DestroyCurrentMarker()
    {
        if (!unitsExist)
            return;

        foreach (GameObject unit in units)
        {
            if (unit.transform.Find("UnitMarker").gameObject)
                Destroy(unit.transform.Find("UnitMarker").gameObject);
        }
    }

    private void SetUnitMarker()
    {
        if(!unitsExist)
            return;

        foreach(GameObject unit in units)
        {
            Vector3 markerPos = unit.transform.position;
            float unitHeight = unit.GetComponent<CapsuleCollider>().height;
            markerPos.y += unitHeight + 0.5f;
            GameObject marker = (GameObject)Instantiate(UnitMarkerPrefab, markerPos, Quaternion.Euler(markerRotate));
            marker.name = "UnitMarker";
            marker.transform.parent = unit.transform;
        }
    }
}
