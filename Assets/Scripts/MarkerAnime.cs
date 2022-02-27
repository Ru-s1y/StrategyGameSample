using UnityEngine;

public class MarkerAnime : MonoBehaviour
{
    public Vector3 pos;
    public float rotateSpeed = 100f;
    public float angularVelocity = 1.5f;
    public float roundTrip = 0.1f;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        float posY = Mathf.Sin(Time.time * angularVelocity) * roundTrip + pos.y;
        transform.Rotate(0, (transform.rotation.y + rotateSpeed) * Time.deltaTime, 0);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
