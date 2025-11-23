using UnityEngine;

public class QuestObjectToggle : MonoBehaviour
{
    [Header("Quest that controls this object")]
    [SerializeField] private QuestSO targetQuest;

    [Header("Object to show/hide (leave empty to use this GameObject)")]
    [SerializeField] private GameObject targetObject;

    private void Awake()
    {
        // If not set, just control this GameObject
        if (targetObject == null)
            targetObject = gameObject;
    }

    private void Update()
    {
        if (targetQuest == null || targetObject == null)
            return;

        // Show key only while quest is active and not completed
        bool shouldShow = targetQuest.active && !targetQuest.QuestCompleted;

        if (targetObject.activeSelf != shouldShow)
        {
            targetObject.SetActive(shouldShow);
        }
    }
}
