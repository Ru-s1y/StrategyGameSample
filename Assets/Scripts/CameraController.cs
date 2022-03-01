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

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos = Vector3.Lerp(transform.position, pos, Time.deltaTime * lerpSpeed);

        float rotateX  = (pos.y + 10f) * cameraRotateSpeed;
        Vector3 rotate = new Vector3(rotateX, 0, 0);

        transform.eulerAngles = rotate;
        transform.position    = pos;
    }
}
