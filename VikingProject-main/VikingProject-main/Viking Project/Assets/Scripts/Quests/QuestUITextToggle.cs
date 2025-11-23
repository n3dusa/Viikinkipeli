using UnityEngine;
using TMPro;

public class QuestUITextToggle : MonoBehaviour
{
    [Header("Quest to Watch")]
    [SerializeField] private QuestSO quest;

    [Header("UI Element (TMP Text)")]
    [SerializeField] private TextMeshProUGUI textElement;

    private void Start()
    {
        UpdateVisibility();
    }

    private void Update()
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        if (quest == null || textElement == null)
            return;

        // Show text if quest is active and not completed
        bool shouldShow = quest.active && !quest.QuestCompleted;
        textElement.gameObject.SetActive(shouldShow);
    }
}
