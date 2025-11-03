using UnityEngine;

public class PlayerIconFollow : MonoBehaviour
{
    public Transform player; // assign your player here

    void LateUpdate()
    {
        // Keep icon in center
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        if (player != null)
        {
            // Rotate icon to match player rotation around Y axis
            float playerYRotation = player.eulerAngles.y;
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -playerYRotation);
        }
    }
}
