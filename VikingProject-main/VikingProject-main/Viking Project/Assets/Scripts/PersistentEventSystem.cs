using UnityEngine;

public class PersistentEventSystem : MonoBehaviour
{
    private void Awake()
    {
        // Jos jo olemassa oleva EventSystem on pelissä, tuhoa tämä duplikaatti
        if (FindObjectsOfType<PersistentEventSystem>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
