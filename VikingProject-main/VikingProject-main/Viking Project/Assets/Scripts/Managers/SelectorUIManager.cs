using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectorUIManager : MonoBehaviour
{
    [Header("Blessing selection UI")]
    public GameObject blessingSelectorUI;

    [Header("Weapon selection UI")]
    public GameObject weaponSelectorUI;
    public Image axeImage;
    public GameObject axeButton;
    public TextMeshProUGUI axeButtonText;

    [Header("Quest board UI")]
    public GameObject questBoardUI;
    public GameObject questInfoUI;
    public TextMeshProUGUI questPageText;
    public Button quest1;
    public TextMeshProUGUI quest1Text;
    public Button quest2;
    public TextMeshProUGUI quest2Text;
    public Button navButtonNext;
    public Button navButtonBack;

    [Header("Quest detail UI")]
    public TextMeshProUGUI questHeader;
    public TextMeshProUGUI questDescription;

    [Header("Shop UI (shop updates these)")]
    public GameObject shopScreen;
    public TextMeshProUGUI playerMoneyScreenText; // updated in Shop.cs
    public TextMeshProUGUI potionCountText;       // updated in Shop.cs

    [Header("Player HUD")]
    public GameObject inGameHud;

    // HUD label that always shows current potion count
    [Tooltip("Bind a TMP text on your HUD that should always display health potion count.")]
    public TextMeshProUGUI hudPotionCountText;

    [Header("Active UI element")]
    public GameObject activeUI;

    // Lets PauseMenu know “Escape has already been handled this frame”
    public static bool EatEscapeThisFrame = false;

    // True if a gameplay UI is currently open
    public bool IsAnyUIOpen => activeUI != null && activeUI.activeSelf;

    void Start()
    {
        // Initialize HUD text at startup
        UpdateHudPotionCount();
    }

    void Update()
    {
        // Always keep HUD potion label synced to PlayerData
        UpdateHudPotionCount();

        // Escape closes current non-pause UI
        if (Input.GetKeyDown(KeyCode.Escape) && IsAnyUIOpen)
        {
            EatEscapeThisFrame = true;   // Prevent PauseMenu from seeing the same Escape press
            SelectorBackButton();
        }
    }

    void LateUpdate()
    {
        // Reset the escape flag after PauseMenu had a chance to check
        EatEscapeThisFrame = false;
    }

    // Universal UI toggle used by all screens
    public void ToggleScreen(GameObject selectorUI)
    {
        bool nextActiveState = !selectorUI.activeSelf;
        selectorUI.SetActive(nextActiveState);

        if (nextActiveState)
        {
            // Opening a UI
            if (inGameHud != null) inGameHud.SetActive(false);
            activeUI = selectorUI;
            Time.timeScale = 0f;
        }
        else
        {
            // Closing UI
            if (activeUI == selectorUI)
                activeUI = null;

            if (inGameHud != null) inGameHud.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    // Escape + back button both close UI properly
    public void SelectorBackButton()
    {
        if (activeUI != null)
        {
            ToggleScreen(activeUI);
        }
    }

    // Legacy helper (kept in case other scripts call it)
    public void TogglePlayerHUD()
    {
        if (inGameHud == null) return;
        bool isActive = inGameHud.activeSelf;
        inGameHud.SetActive(!isActive);
    }

    // Update the HUD potion count from PlayerData
    private void UpdateHudPotionCount()
    {
        if (hudPotionCountText == null) return;

        // Use PlayerData as the single source of truth
        int potions = 0;
        if (PlayerData.Instance != null)
        {
            potions = PlayerData.Instance.healthPotions;
        }

        hudPotionCountText.text = potions.ToString();
        
    }
}
