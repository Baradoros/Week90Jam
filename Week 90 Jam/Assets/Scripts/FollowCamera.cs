using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    [Range(0,1)]
    public float smoothSpeed = 0.125f;

    public Vector3 offset = new Vector3(0, 10, -10);

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate() {
        // Position to move towards
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

        // Sideways movement is not tracked with rotation. Looks better that way
        transform.LookAt(new Vector3(transform.position.x, target.position.y, target.position.z));
    }
}
