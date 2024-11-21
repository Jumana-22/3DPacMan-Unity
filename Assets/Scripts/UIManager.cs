using TMPro; // Import for TextMeshPro support
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI livesText; // Reference to the UI Text for lives
    public PlayerMovement player; // Reference to the player script

    void Update()
    {
        // Update the lives text based on the player's current lives
        if (player != null && livesText != null)
        {
            livesText.text = "Lives: " + player.lives;
        }
    }
}
