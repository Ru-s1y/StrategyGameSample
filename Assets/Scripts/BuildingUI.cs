using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    public bool buildFlg = false;

    public GameObject housePrefab;
    public GameObject storePrefab;

    public GameObject currentObj;

    private Vector3 point;
    public Color32 transparency = new Color32(0, 0, 0, 128);

    void Update()
    {
        point = GetTouchPoint();

        if (currentObj != null && buildFlg)
            currentObj.transform.position = point;

        if (Input.GetKey("t"))
            Debug.Log("時計回り");

        if (Input.GetKey("r"))
            Debug.Log("反時計回り");

        if (Input.GetMouseButtonDown(0) && buildFlg)
            putObject();

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentObj);
            currentObj = null;
            buildFlg   = false;
        }

    }

    private Vector3 GetTouchPoint()
    {
        RaycastHit hit;
        Ray touchPointToRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(touchPointToRay, out hit, 100))
            return hit.point;

        return Vector3.zero;
    }

    public void OnClickStoreHouseButton()
    {
        Debug.Log("Click StoreHouse Button!");
        currentObj = (GameObject) Instantiate(storePrefab, point, Quaternion.identity);
        SetObjectProperty();
    }

    public void OnClickHouseButton()
    {
        Debug.Log("Click House Button!");
        currentObj = (GameObject) Instantiate(housePrefab, point, Quaternion.identity);
        SetObjectProperty();
    }

    public void CheckEnoughResource()
    {
        return;
    }

    public void SetObjectProperty()
    {
        currentObj.transform.localScale *= 0.7f;
        Color color = currentObj.GetComponent<MeshRenderer>().material.color;
        color.a  = 0.4f;
        currentObj.GetComponent<MeshRenderer>().material.color = color;
        buildFlg = true;
    }

    public void putObject()
    {
        Color color = currentObj.GetComponent<MeshRenderer>().material.color;
        color.a  = 1f;
        currentObj.GetComponent<MeshRenderer>().material.color = color;
        buildFlg = false;
    }
}
