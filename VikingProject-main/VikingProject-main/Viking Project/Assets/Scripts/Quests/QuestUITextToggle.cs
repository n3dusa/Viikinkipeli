using UnityEngine;
using TMPro;

public class QuestUITextToggle : MonoBehaviour
{
    [Header("Show this text only when THIS quest is the current quest")]
    [SerializeField] private QuestSO targetQuest;

    [Header("UI Element (TMP Text)")]
    [SerializeField] private TextMeshProUGUI textElement;

    private void Awake()
    {
        // Auto-find if not assigned
        if (textElement == null)
            textElement = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (textElement == null || targetQuest == null || PlayerData.Instance == null)
            return;

        var currentQuest = PlayerData.Instance.currentQuest;

        // Only show if:
        // 1) There *is* a current quest
        // 2) That quest is exactly the target quest (the slime quest asset)
        // 3) It is active
        // 4) It is not completed
        bool shouldShow =
            currentQuest == targetQuest &&
            targetQuest.active &&
            !targetQuest.QuestCompleted;

        textElement.gameObject.SetActive(shouldShow);
    }
}
