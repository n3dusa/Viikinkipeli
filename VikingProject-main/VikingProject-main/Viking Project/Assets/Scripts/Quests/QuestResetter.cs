using System.Collections.Generic;
using UnityEngine;

public class QuestResetter : MonoBehaviour
{
    private static bool hasResetThisSession = false;

    [Header("Quests to reset at game start")]
    [SerializeField] private List<QuestSO> questsToReset = new List<QuestSO>();

    private void Awake()
    {
        // If we've already reset once in this play session, do nothing
        if (hasResetThisSession)
        {
            Destroy(gameObject);   // no longer needed
            return;
        }

        // First time ever this session → reset all quests
        foreach (var quest in questsToReset)
        {
            if (quest != null)
            {
                quest.ResetQuest();
                // Optional debug:
                // Debug.Log("[QuestResetter] Reset quest: " + quest.name);
            }
        }

        hasResetThisSession = true;

        // We don't need this object anymore
        Destroy(gameObject);
    }
}
