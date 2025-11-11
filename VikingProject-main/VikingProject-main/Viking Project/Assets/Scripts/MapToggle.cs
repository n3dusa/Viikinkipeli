using UnityEngine;

public class MapToggle : MonoBehaviour
{
    [Header("Camera References")]
    public Camera mainCamera;
    public Camera mapCamera;

    [Header("Minimap Parent Object")]
    public GameObject minimapRoot; 

    [Header("SelectorUI")]
    public GameObject selectorUI; 
    [Header("PlayerGUI")]
    public GameObject playerGUI; 
    // The object that holds your minimap camera and UI

    private bool mapOpen = false;

    void Start()
    {
        // Ensure default state
        if (mainCamera)  mainCamera.enabled = true;
        if (mapCamera)   mapCamera.enabled = false;
        if (minimapRoot) minimapRoot.SetActive(true);
        if (selectorUI) selectorUI.SetActive(true);
        if (playerGUI) playerGUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    void ToggleMap()
    {
        mapOpen = !mapOpen;

        if (mainCamera)  mainCamera.enabled = !mapOpen;
        if (mapCamera)   mapCamera.enabled = mapOpen;

        if (minimapRoot) minimapRoot.SetActive(!mapOpen);
        if (selectorUI) selectorUI.SetActive(!mapOpen);
        if (playerGUI) playerGUI.SetActive(!mapOpen);
    }
}
