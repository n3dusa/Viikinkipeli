using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : MonoBehaviour, IInteractable
{
    [Header("Chest Settings")]
    public int coinsGiven = 10;
    public Transform lid;            // Aseta tämän arkun kannen objekti Inspectorissa
    public float openAngle = -70f;   // Kuinka paljon kansi avautuu
    public float openSpeed = 3f;     // Avausnopeus

    private bool isLooted = false;
    private bool opening = false;

    void Update()
    {
        // Jos avataan kantta → käännä sitä kohti target-kulmaa
        if (opening && lid != null)
        {
            Quaternion targetRot = Quaternion.Euler(openAngle, 0f, 0f);
            lid.localRotation = Quaternion.Slerp(lid.localRotation, targetRot, Time.deltaTime * openSpeed);
        }
    }

    public void Interact(PlayerController player)
    {
        // Estä että arkkua ei voi lootata kahdesti
        if (isLooted) return;

        // Anna kolikot pelaajalle
        PlayerData.Instance.coins += coinsGiven;
        Debug.Log("Chest looted! +" + coinsGiven + " coins");

        // Merkitse loottaus ja aloita avausanimaatio
        isLooted = true;
        opening = true;
    }

    public string GetInteractText()
    {
        return "Open Chest";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
