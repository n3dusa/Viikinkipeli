using System.Collections.Generic;
using UnityEngine;

public class QuestPortalActivator : MonoBehaviour
{
    [Header("Quests that can unlock this portal")]
    [SerializeField] private List<QuestSO> quests = new List<QuestSO>();

    [Header("Portal to enable when selected quests complete")]
    [SerializeField] private GameObject portal;

    [Header("Behaviour")]
    [Tooltip("If true, portal will start enabled when the scene loads IF any quest is already completed." +
             " Turn this off for portals that live in the same scene as the quest objectives.")]
    [SerializeField] private bool unlockIfQuestAlreadyCompletedOnStart = true;

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
        if (portal == null) return;

        bool alreadyUnlocked = false;

        if (unlockIfQuestAlreadyCompletedOnStart)
        {
            // For portals in OTHER scenes: open if any quest already finished
            foreach (var quest in quests)
            {
                if (quest != null && quest.QuestCompleted)
                {
                    alreadyUnlocked = true;
                    break;
                }
            }
        }

        portalOpened = alreadyUnlocked;
        portal.SetActive(alreadyUnlocked);   // closed unless we explicitly want auto-unlock on load
    }

    private void HandleQuestCompleted(QuestSO completedQuest)
    {
        if (portalOpened || portal == null) return;

        portalOpened = true;
        portal.SetActive(true);
        Debug.Log($"Portal enabled because quest '{completedQuest.name}' was completed.");

        // Optional: once opened, we don't care about future completions
        foreach (var quest in quests)
        {
            if (quest != null)
                quest.OnQuestCompleted -= HandleQuestCompleted;
        }
    }
}
