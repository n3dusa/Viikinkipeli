using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public PlayerController playerController;

    [Header("UI Elements")]
    [SerializeField] private GameObject questUI;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private Toggle questStatus;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;

    public List<ObjectiveSO> objectives = new List<ObjectiveSO>();
    public List<Toggle> objectiveUIs = new List<Toggle>();
    public List<Text> objectiveLabelObjects = new List<Text>();


    private void Awake()
    {
        // Auto-assign missing PlayerController
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("HUDManager: PlayerController is NOT assigned and cannot be found in the scene!");
            }
        }

        // UI validation
        if (healthSlider == null) Debug.LogError("HUDManager: healthSlider is NOT assigned!");
        if (easeHealthSlider == null) Debug.LogError("HUDManager: easeHealthSlider is NOT assigned!");
        if (questTitle == null) Debug.LogError("HUDManager: questTitle is NOT assigned!");
        if (questDescription == null) Debug.LogError("HUDManager: questDescription is NOT assigned!");
        if (questUI == null) Debug.LogError("HUDManager: questUI is NOT assigned!");
    }


    private void Update()
    {
        RefreshHUDInfo();
        UpdateHealthUI();
    }


    // Set current active quest info on played hud
    void SetObjectiveUI()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (i >= objectiveUIs.Count || i >= objectiveLabelObjects.Count)
                break;

            if (objectiveUIs[i] == null || objectiveLabelObjects[i] == null)
                continue;

            objectiveUIs[i].gameObject.SetActive(true);
            objectiveLabelObjects[i].text = objectives[i].description;
            objectiveUIs[i].isOn = objectives[i].Completed;
        }

        for (int i = objectives.Count; i < objectiveUIs.Count; i++)
        {
            if (objectiveUIs[i] != null)
                objectiveUIs[i].gameObject.SetActive(false);
        }
    }


    public void RefreshHUDInfo()
    {
        if (playerController == null) return;

        if (playerController.currentQuest)
        {
            questUI.SetActive(true);
            objectives = playerController.currentQuest.objectives;
            SetObjectiveUI();
            questTitle.text = playerController.currentQuest.name;
            questDescription.text = playerController.currentQuest.questDescription;
        }
        else
        {
            questUI.SetActive(false);
            questTitle.text = "";
            questDescription.text = "";
        }
    }


    // Health bar sliders
    void UpdateHealthUI()
    {
        if (playerController == null || healthSlider == null || easeHealthSlider == null)
            return;

        PlayerData playerData = PlayerData.Instance;
        if (playerData == null)
        {
            Debug.LogError("HUDManager: PlayerData.Instance is NULL!");
            return;
        }

        float healthLerpSpeed = 0.01f;

        healthSlider.maxValue = playerData.activeMaxHealth;
        easeHealthSlider.maxValue = playerData.activeMaxHealth;

        // Snap healthSlider to current health
        if (healthSlider.value > playerController.currentHealth)
        {
            healthSlider.value = playerController.currentHealth;
        }
        else if (healthSlider.value < playerController.currentHealth)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, playerController.currentHealth, healthLerpSpeed * 2f);
        }

        // Smooth animation using easeHealthSlider
        if (Mathf.Abs(easeHealthSlider.value - playerController.currentHealth) > 0.01f)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, playerController.currentHealth, healthLerpSpeed * 1.5f);
        }
    }
}
