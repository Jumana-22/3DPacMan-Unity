using UnityEngine;
using System.IO;

public class WallLoader : MonoBehaviour
{
    public WallManager wallManager; // Assign WallManager in the Inspector

    void Start()
    {
        // Load the JSON file
        string path = Path.Combine(Application.dataPath, "Resources/Configs/walls.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            WallConfig wallConfig = JsonUtility.FromJson<WallConfig>(json);

            // Instantiate walls based on the configuration
            foreach (var wall in wallConfig.walls)
            {
                Vector3 position = new Vector3(wall.x, wall.y, wall.z);
                Quaternion rotation = wall.rotateLeft ? Quaternion.Euler(0, -90, 0) : Quaternion.identity;
                wallManager.CreateWall(position, rotation);
            }
        }
        else
        {
            Debug.LogError($"Configuration file not found at: {path}");
        }
    }
}
