using UnityEngine;

public class MenuCameraMotion : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0); // Rotates the camera slowly
    }
}
