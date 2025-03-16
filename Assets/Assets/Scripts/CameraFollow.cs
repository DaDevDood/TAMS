using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 10f;
    public Vector3 offset;
    public Vector3 velocity = Vector3.zero;

    public static CameraFollow instance;

    private void Start()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.gameHosted)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        
    }
}
