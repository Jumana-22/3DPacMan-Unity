using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // Assign the Player object in the Inspector
    public Vector3 offset = new Vector3(0, 3, -5); // Offset from the player's position
    public float tiltAngle = 20f; // Angle to tilt the camera downward

    void LateUpdate()
    {
        // Update the camera's position to stay behind the player
        Vector3 desiredPosition = player.position + player.rotation * offset;
        transform.position = desiredPosition;

        // Rotate the camera to look at the player's back with a tilt
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = targetRotation * Quaternion.Euler(tiltAngle, 0, 0);
    }
}
