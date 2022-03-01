using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float scrollSpeed = 10f;

    public float minY = 2.5f;
    public float maxY = 20f;

    public float lerpSpeed = 5f;
    public float cameraRotateSpeed = 2f;

    private float scroll = 0f;
    private Vector3 position;
    private Vector3 rotateAngle;

    void Update()
    {
        if(Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position    = SetScrollHeight();
        transform.eulerAngles = SetScrollAngleX();
    }

    // カメラの高さ調節
    private Vector3 SetScrollHeight()
    {
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        return position = Vector3.Lerp(transform.position, pos, Time.deltaTime * lerpSpeed);
    }

    // カメラの縦回転調整
    private Vector3 SetScrollAngleX()
    {
        float rotateX  = (position.y + 10f) * cameraRotateSpeed;
        return rotateAngle = new Vector3(rotateX, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
