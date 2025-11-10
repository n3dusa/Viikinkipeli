using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI; // Tarvitaan Sliderille

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [Header("UI")]
    public GameObject pauseMenuUI;
    public GameObject settingsPanel;
    public Slider musicSlider;   // 🎵 Liukusäädin musiikille

    [Header("References")]
    public SelectorUIManager uiManager;
    public AudioManager audioManager; // 🎧 Viittaus AudioManageriin

    void Awake()
    {
        // Fallback: auto-find managerit jos ei ole asetettu
        if (uiManager == null)
            uiManager = FindObjectOfType<SelectorUIManager>(true);
        if (audioManager == null)
            audioManager = FindObjectOfType<AudioManager>(true);
    }

    void Start()
    {
        // Jos AudioManager löytyy, haetaan nykyinen äänenvoimakkuus liukusäätimeen
        if (audioManager != null && musicSlider != null)
            musicSlider.value = audioManager.GetMusicVolume();

        // Kytketään liukusäädin funktioon
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    void Update()
    {
        if (SelectorUIManager.EatEscapeThisFrame) return;

        bool esc = Input.GetKeyDown(KeyCode.Escape);
        bool p = Input.GetKeyDown(KeyCode.P);

        // Jos asetukset on auki, ESC palaa takaisin pause-valikkoon
        if (esc && settingsPanel != null && settingsPanel.activeSelf)
        {
            BackFromSettings();
            return;
        }

        if ((esc || p) && uiManager != null && uiManager.IsAnyUIOpen)
            return;

        if (esc || p)
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OpenSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(true);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
    }

    public void BackFromSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
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

    // 🎵 Kutsutaan kun liukusäädintä liikutetaan
    public void OnMusicVolumeChanged(float value)
    {
        if (audioManager != null)
            audioManager.SetMusicVolume(value);
    }
}
