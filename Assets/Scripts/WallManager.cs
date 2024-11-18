using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject wallPrefab; // Assign the Wall prefab in the Inspector
    public bool debugMode = true;  // Toggle to enable/disable gizmo drawing

    // Method to create a wall at a specific position with a specific rotation
    public void CreateWall(Vector3 position, Quaternion rotation)
    {
        GameObject wall = Instantiate(wallPrefab, position, rotation);
    }

    // Draw Gizmos in the Editor
    void OnDrawGizmos()
    {
        if (debugMode && wallPrefab != null)
        {
            Gizmos.color = Color.red; // Set color for gizmos (red for walls)

            // Example hardcoded positions for Gizmo testing (optional)
            Vector3[] wallPositions = {
                new Vector3(0, 1.5f, 5),
                new Vector3(5, 1.5f, 0),
                new Vector3(-5, 1.5f, 0)
            };

            foreach (var position in wallPositions)
            {
                Gizmos.DrawCube(position, wallPrefab.transform.localScale); // Use prefab's scale
            }
        }
    }
}
