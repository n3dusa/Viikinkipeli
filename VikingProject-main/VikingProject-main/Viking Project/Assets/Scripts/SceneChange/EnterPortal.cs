using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTravelTrigger : MonoBehaviour, IInteractable
{
    [Header("Scene Travel Settings")]
    [SerializeField] private string sceneName;         // Name of the scene to load
    [SerializeField] private string interactText;      // Interaction prompt

    [Header("Requirements (Optional)")]
    [SerializeField] private bool requiresQuest = false;

    public void Interact(PlayerController player)
    {
        // If this interaction requires a quest but the player doesn't have one
        if (requiresQuest && !player.currentQuest)
        {
            Debug.Log("You need a quest to use this travel point.");
            return;
        }

        // If no scene is specified, do nothing but log a warning
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("SceneTravelTrigger: No scene name assigned.");
            return;
        }

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string levelName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100f) + "%");
            yield return null;
        }
    }

    // ------------ IInteractable Interface ------------

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
