using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Vector3 offset;
    public float followSpeed;
    public float minX = 0f;

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 clampedPosition = new Vector3(Mathf.Clamp(targetPosition.x, minX, float.MaxValue), targetPosition.y, targetPosition.z);
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, followSpeed*Time.fixedDeltaTime);

        transform.position = smoothPosition;
    }
}
