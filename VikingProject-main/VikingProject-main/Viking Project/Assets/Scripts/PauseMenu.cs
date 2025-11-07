using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [Header("UI")]
    public GameObject pauseMenuUI;
    public GameObject settingsPanel;

    [Header("References")]
    // Assign in Inspector (optional, we also auto-find in Awake)
    public SelectorUIManager uiManager;

    void Awake()
    {
        // Fallback: auto-find the SelectorUIManager if not wired in Inspector
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<SelectorUIManager>(true);
        }
    }

    void Update()
    {
        // If Escape was already used to close a gameplay UI this frame, ignore it here.
        if (SelectorUIManager.EatEscapeThisFrame) return;

        bool esc = Input.GetKeyDown(KeyCode.Escape);
        bool p   = Input.GetKeyDown(KeyCode.P);

        // If Settings is open, Escape should go back to the Pause menu (not resume gameplay)
        if (esc && settingsPanel != null && settingsPanel.activeSelf)
        {
            BackFromSettings();
            return;
        }

        // If any gameplay UI (quest board, shop, etc.) is open, do NOT toggle pause here.
        if ((esc || p) && uiManager != null && uiManager.IsAnyUIOpen)
        {
            // SelectorUIManager handles closing on Escape. We do nothing.
            return;
        }

        // Normal pause toggle
        if (esc || p)
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuUI != null)  pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuUI != null)   pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OpenSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(true);
        if (pauseMenuUI != null)   pauseMenuUI.SetActive(false);
    }

    public void BackFromSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuUI != null)   pauseMenuUI.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();

        if (cam != null && player != null)
        {
            cam.Follow = player.transform;
            cam.LookAt = player.transform;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
