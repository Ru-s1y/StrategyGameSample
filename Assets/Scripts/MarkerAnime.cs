using UnityEngine;

public class MarkerAnime : MonoBehaviour
{
    public float rotateSpeed = 100f;
    void Update()
    {
        transform.Rotate(0, (transform.rotation.y + rotateSpeed) * Time.deltaTime, 0);
    }
}
