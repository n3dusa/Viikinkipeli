using UnityEngine;

public class WinterKeyInteractable : MonoBehaviour, IInteractable
{
    private QuestItem questItem;

    private void Awake()
    {
        questItem = GetComponent<QuestItem>();
    }

    public void Interact(PlayerController player)
    {
        if (questItem != null)
        {
            // Complete the objective
            questItem.ObjectiveInteraction();
        }

        // Remove the key from the world
        Destroy(gameObject);
    }

    public string GetInteractText()
    {
        return "Pick up Winter Key";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}