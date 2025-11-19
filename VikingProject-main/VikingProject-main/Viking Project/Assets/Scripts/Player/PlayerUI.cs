using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("HUD References")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI potionCountText;
    [SerializeField] private PlayerController playerController;

    private void Update()
    {
        if (PlayerData.Instance == null)
            return;

        if (playerController == null) return;

        healthText.text = $"HP: {playerController.currentHealth:0}";

        // Potion count display
        if (potionCountText != null)
        {
            potionCountText.text = PlayerData.Instance.healthPotions.ToString();
        }
    }
}
