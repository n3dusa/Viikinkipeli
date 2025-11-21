using System.Collections.Generic;
using UnityEngine;

public class QuestPortalActivator : MonoBehaviour
{
    [Header("Quests that can unlock this portal")]
    [SerializeField] private List<QuestSO> quests = new List<QuestSO>();

    [Header("Portal to enable when ANY quest completes")]
    [SerializeField] private GameObject portal;

    private bool portalOpened = false;

    private void OnEnable()
    {
        // Subscribe to all quests in the list
        foreach (var quest in quests)
        {
            if (quest != null)
                quest.OnQuestCompleted += HandleQuestCompleted;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from all quests
        foreach (var quest in quests)
        {
            if (quest != null)
                quest.OnQuestCompleted -= HandleQuestCompleted;
        }
    }

    private void Start()
    {
        if (portal != null)
            portal.SetActive(false); // start closed
    }

    private void HandleQuestCompleted(QuestSO completedQuest)
    {
        if (portalOpened) return; // already opened once

        portalOpened = true;

        if (portal != null)
        {
            portal.SetActive(true);
            Debug.Log($"Portal enabled because quest '{completedQuest.name}' was completed.");
        }

        // Optional: once opened, we don't care about future completions
        foreach (var quest in quests)
        {
            if (quest != null)
                quest.OnQuestCompleted -= HandleQuestCompleted;
        }
    }
}
