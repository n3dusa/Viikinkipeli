using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    [SerializeField] private PlayerStatsSO stats;
    public static PlayerData Instance { get; private set; }

    [Header("Weapon")]
    public WeaponSO weapon;

    [Header("Player Quests")]
    public QuestSO currentQuest;
    public List<ObjectiveSO> objectives;

    [Header("Blessing")]
    public BlessingSO blessing;

    [Header("Health")]
    public float activeMaxHealth;
    public int healthPotions;

    [Header("Coins")]
    public int coins;

    [Header("Speed")]
    public float activeSpeed;

    private void Awake() {
        Debug.Log($"[PlayerData] Awake on {gameObject.name}", this);

        if (Instance != null && Instance != this) {
            Debug.Log($"[PlayerData] Duplicate found, destroying {gameObject.name}", this);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);   // THIS is what should move it to DontDestroyOnLoad

        activeMaxHealth = stats.maxHealth;
        activeSpeed = stats.movementSpeed;
    }
    public void UpdateWeapon( WeaponSO newWeapon ) {
        weapon = newWeapon;
        // Optionally, update the player object with the new weapon
    }

    // Method to update the player's quests
    public void UpdateCurrentQuest( QuestSO updatedCurrentQuest ) {
        currentQuest = updatedCurrentQuest;
        // Optionally, update the player object with the new quests
    }

    // Method to update the player's blessing
    public void UpdateBlessing( BlessingSO newBlessing ) {
        blessing = newBlessing;
        // Optionally, update the player object with the new blessing
    }

    // Method to update the player's health
    public void UpdateHealth( float newHealth ) {
        activeMaxHealth = newHealth;
        // Optionally, update the player object with the new health
    }

    public void UpdateCoins( int newCoins ) { 
        coins += newCoins;
    } 
    public void UpdateSpeed( float newSpeed ) {
        activeSpeed = newSpeed;
        // Optionally, update the player object with the new health
    }

    public void UpdatePotions(int newAmount) {
        healthPotions = newAmount;
    }
}