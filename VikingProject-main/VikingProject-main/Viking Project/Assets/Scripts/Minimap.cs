using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    [Header("Assign Player Transform here")]
    public Transform player;

    [Header("Camera Height Above Player")]
    public float height = 30f;

    [Header("Rotate With Player")]
    public bool rotateWithPlayer = true;

    void LateUpdate()
    {
        if (player == null) return;

        // Position camera directly above player
        Vector3 newPosition = player.position;
        newPosition.y += height;
        transform.position = newPosition;

        // Keep looking straight down
        if (rotateWithPlayer)
        {
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
