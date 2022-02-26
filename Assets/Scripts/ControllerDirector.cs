using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerDirector : MonoBehaviour
{
    public GameObject prefab;
    public GameObject selectAreaPrefab;
    public string unitTagName = "Player";
    public GameObject[] units;

    private GameObject selectArea;

    private Vector3 mouseDownPoint;
    private Vector3 mousePressPoint;
    private Vector3 mouseUpPoint;

    private bool inputing = false;

    void Update()
    {
        // クリック押した時
        if (Input.GetMouseButtonDown(0) && !inputing)
        {
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
                // 選択処理
                SetSelectUnit();

                Destroy(selectArea);
            }
        }

        // 右クリック時
        if (Input.GetMouseButtonDown(1) && units != null)
        {
            Vector3 point = GetTouchPoint();
            foreach (GameObject unit in units)
            {
                UnitMove unitMove = unit.GetComponent<UnitMove>();
                unitMove.goal = point;
                unitMove.Move();
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
}
