using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlimeKillTracker : MonoBehaviour
{
    public static SlimeKillTracker Instance { get; private set; }

    [Header("Slimes to track (drag them here)")]
    [SerializeField] private List<GameObject> slimes = new List<GameObject>();

    [Header("Objective to complete when all are dead")]
    [SerializeField] private ObjectiveSO killAllSlimesObjective;

    [Header("Quest that this objective belongs to (the slime quest)")]
    [SerializeField] private QuestSO slimeQuest;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI slimeCounterText;

    private int totalSlimes;
    private int slimesAlive;
    private int slimesKilled;

    private void Awake()
    {
        Instance = this;

        if (slimeCounterText == null)
            slimeCounterText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        totalSlimes = slimes.Count;
        slimesAlive = totalSlimes;
        slimesKilled = 0;

        UpdateVisibilityAndText(true);
    }

    private void Update()
    {
        UpdateVisibilityAndText();
    }

    // Call this from each slime when it dies
    public void SlimeDied(GameObject slime)
    {
        if (!slimes.Contains(slime))
            return;

        slimesAlive--;
        slimesKilled++;

        UpdateVisibilityAndText(true);

        if (slimesAlive <= 0)
        {
            if (killAllSlimesObjective != null)
                killAllSlimesObjective.Completed = true;

            if (slimeQuest != null)
                slimeQuest.TryEndQuest();
        }
    }

    private void UpdateVisibilityAndText(bool forceUpdateText = false)
    {
        if (slimeCounterText == null || slimeQuest == null)
            return;

        bool shouldShow = slimeQuest.active && !slimeQuest.QuestCompleted;

        slimeCounterText.gameObject.SetActive(shouldShow);

        if (shouldShow && (forceUpdateText || Application.isPlaying))
        {
            slimeCounterText.text = $"Slimes killed: {slimesKilled} / {totalSlimes}";
        }
    }
}
