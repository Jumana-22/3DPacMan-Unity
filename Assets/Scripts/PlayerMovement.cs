using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Forward movement speed
    public float rotationAngle = 90f; // Angle of rotation (90 degrees)
    private bool isTurning = false; // To prevent continuous rotation during a key press
    private Rigidbody rb; // Reference to the Rigidbody
    private float turnDirection = 0f; // To store rotation input

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on the player!");
            return;
        }

        // Apply the initial forward velocity
        rb.velocity = transform.forward * speed;

        // Freeze rotation along physics axes to prevent unintended behavior
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Capture rotation input
        if (!isTurning)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                turnDirection = -rotationAngle;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                turnDirection = rotationAngle;
            }
        }
    }

    void FixedUpdate()
    {
        // Maintain constant forward velocity
        rb.velocity = transform.forward * speed;

        // Handle rotation
        if (turnDirection != 0 && !isTurning)
        {
            StartCoroutine(Rotate(Vector3.up, turnDirection));
            turnDirection = 0; // Reset turn direction after starting the rotation
        }
    }

    private System.Collections.IEnumerator Rotate(Vector3 axis, float angle)
    {
        isTurning = true;

        // Smoothly rotate using Rigidbody
        Quaternion initialRotation = rb.rotation;
        Quaternion targetRotation = rb.rotation * Quaternion.Euler(axis * angle);
        float elapsed = 0f;
        float duration = 0.2f; // Duration of the rotation

        while (elapsed < duration)
        {
            rb.MoveRotation(Quaternion.Slerp(initialRotation, targetRotation, elapsed / duration));
            elapsed += Time.fixedDeltaTime; // Use fixedDeltaTime for physics updates
            yield return new WaitForFixedUpdate();
        }

        rb.MoveRotation(targetRotation); // Ensure exact final rotation
        isTurning = false;
    }
}
