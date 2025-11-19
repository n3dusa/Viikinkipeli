using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlimeKillTracker : MonoBehaviour
{
    public static SlimeKillTracker Instance { get; private set; }

    [Header("Slimes to track (drag them here)")]
    public List<GameObject> slimes = new List<GameObject>();

    [Header("Objective to complete")]
    public ObjectiveSO killAllSlimesObjective;

    [Header("Quest to complete")]
    public QuestSO questToComplete;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI slimeCounterText;

    private int totalSlimes;
    private int slimesAlive;
    private int slimesKilled;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalSlimes = slimes.Count;
        slimesAlive = totalSlimes;
        slimesKilled = 0;

        UpdateUI();
        Debug.Log($"Tracking {totalSlimes} slimes.");
    }

    // Called by each slime when it dies
    public void SlimeDied(GameObject slime)
    {
        if (!slimes.Contains(slime))
            return;

        slimesAlive--;
        slimesKilled++;

        Debug.Log($"Slime died. Remaining: {slimesAlive}");
        UpdateUI();

        if (slimesAlive <= 0)
        {
            Debug.Log("All slimes killed!");

            if (killAllSlimesObjective != null)
                killAllSlimesObjective.Completed = true;

            if (questToComplete != null)
                questToComplete.TryEndQuest();
        }
    }

    private void UpdateUI()
    {
        if (slimeCounterText == null)
            return;

        slimeCounterText.text = $"Slimes killed: {slimesKilled} / {totalSlimes}";
    }
}
