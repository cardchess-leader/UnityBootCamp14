using UnityEngine;
using System.Collections.Generic;

// Singleton GameManager class to manage the game state
public class GameManager : MonoBehaviour
{
    // Use singleton pattern
    public static GameManager Instance { get; private set; }
    public float liftUpBelowThisAltitude = 50f;
    [SerializeField]
    List<Level> levelList;
    int exp = 0;
    int level = 0;

    private void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        UIController.Instance.UpdateLevelText(level, exp, levelList[level].expToNextLevel);
    }

    public float GetAltitude(GameObject target)
    {
        return target.transform.position.y - gameObject.transform.position.y;
    }

    public void AddExp(int amount)
    {
        exp += amount;
        if (exp >= levelList[level].expToNextLevel)
        {
            exp -= levelList[level].expToNextLevel;
            level++;
        }
        UIController.Instance.UpdateLevelText(level, exp, levelList[level].expToNextLevel);
    }
}

// What to implement next:
// 1. Implement auto-targeting system for missiles. (Hint: Use Physics.OverlapSphere to find nearby targets) (Snap to closes target by view angle difference between center of view and enemy)
// 2. Add some kind of scoring system 
// 3. Add sound effects for shooting and explosions (Download assets from Unity Asset Store)
// 4. Add background music 
// 5. Implement enemy attack behavior (e.g., enemy planes shooting at the player)
// 6. Add Player health and Game Over conditions
// 7. Implement a simple main menu and pause menu
// 8. Optimize performance (e.g., object pooling for bullets and explosions)
// 9. Add particle effects for engine trails, explosions, and gunfire
// 10. Implement different types of weapons (e.g., machine guns, missiles) with different behaviors
// 11. Add UI elements to display player health, score, and ammo count (Done)
// 12. Implement level progression with increasing difficulty
// 13. Add Rear View Camera (Done)
// 14. Add reloading message when reloading
