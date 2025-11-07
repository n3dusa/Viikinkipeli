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

    [Header("Shop UI")]
    public GameObject shopScreen;
    public TextMeshProUGUI playerMoneyScreenText;
    public TextMeshProUGUI potionCountText;

    [Header("Player HUD")]
    public GameObject inGameHud;

    [Header("Active UI element")]
    public GameObject activeUI;

    // Lets PauseMenu know “Escape has already been handled this frame”
    public static bool EatEscapeThisFrame = false;

    // True if a gameplay UI is currently open
    public bool IsAnyUIOpen => activeUI != null && activeUI.activeSelf;


    void Update()
    {
        // Escape closes current UI
        if (Input.GetKeyDown(KeyCode.Escape) && IsAnyUIOpen)
        {
            EatEscapeThisFrame = true;   //Prevent PauseMenu from seeing the same Escape press
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
            inGameHud.SetActive(false);
            activeUI = selectorUI;
            Time.timeScale = 0f;
        }
        else
        {
            // Closing UI
            if (activeUI == selectorUI)
                activeUI = null;

            inGameHud.SetActive(true);
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


    public void TogglePlayerHUD()
    {
        bool isActive = inGameHud.activeSelf;
        inGameHud.SetActive(!isActive);
    }
}
