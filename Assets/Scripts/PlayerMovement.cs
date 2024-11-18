using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Forward movement speed
    public float rotationAngle = 90f; // Angle of rotation (90 degrees)
    private bool isTurning = false; // To prevent continuous rotation during a key press
    private float moveInterval = 0.05f; // Movement interval

    void Update()
    {

        // Forward movement in intervals
        Vector3 nextPosition = transform.position + transform.forward * speed * Time.deltaTime;
        nextPosition = SnapToInterval(nextPosition);
        transform.position = nextPosition;

        // Turn left or right
        if (!isTurning)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(Rotate(Vector3.up, -rotationAngle));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(Rotate(Vector3.up, rotationAngle));
            }
        }
    }

    private System.Collections.IEnumerator Rotate(Vector3 axis, float angle)
    {
        isTurning = true;

        // Rotate smoothly over time
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation * Quaternion.Euler(axis * angle);
        float elapsed = 0f;
        float duration = 0.2f; // Adjust duration for rotation speed

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;
        isTurning = false;
    }

    // Snaps a position to the nearest interval
    private Vector3 SnapToInterval(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x / moveInterval) * moveInterval,
            position.y,
            Mathf.Round(position.z / moveInterval) * moveInterval
        );
    }
}
