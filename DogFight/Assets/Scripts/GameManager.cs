using UnityEngine;
using System.Collections.Generic;

// Singleton GameManager class to manage the game state
public class GameManager : MonoBehaviour
{
    // Use singleton pattern
    public static GameManager Instance { get; private set; }
    [SerializeField]
    List<Level> levelList;
    int exp = 0;
    int level = 0;

    public Level GetLevel { get => levelList[level]; }

    private void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Keep this instance across scenes
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

    // Press G Key to restart the game (for testing purposes)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    public float GetAltitude(GameObject target)
    {
        return target.transform.position.y - gameObject.transform.position.y;
    }

    public void AddExp(int amount)
    {
        exp += amount;
        if (exp >= levelList[level].expToNextLevel && level < levelList.Count - 1)
        {
            OnLevelUp();
        }
        UIController.Instance.UpdateLevelText(level, exp, levelList[level].expToNextLevel);
    }

    public void OnLevelUp()
    {
        exp -= levelList[level].expToNextLevel;
        level++;
        Upgrade.Instance.OnLevelUp();
    }
}

// What to implement next:
// 1. Implement auto-targeting system for missiles. (Done)
// 2. Add some kind of scoring system (Done)
// 3. Add sound effects for shooting and explosions (Download assets from Unity Asset Store)
// 4. Add background music 
// 5. Implement enemy attack behavior (Done)
// 6. Add Player health and Game Over conditions (Player Health done, game over not done)
// 7. Implement a simple main menu and pause menu
// 8. Optimize performance (e.g., object pooling for bullets and enemies. Maybe explosions too?)
// 9. Add particle effects for engine trails, explosions, and gunfire
// 10. Implement different types of weapons (e.g., machine guns, missiles) with different behaviors
// 11. Add UI elements to display player health, score, and ammo count (Done)
// 12. Implement level progression with increasing difficulty
// 13. Add Rear View Camera (Done)
// 14. Add reloading message when reloading (Done)
// 15. Game UI Improvements 
// 16. Find fonts that fits the low-poly art style
// 17. Add radar system that shows nearby enemies and objectives in minimap (Done)
// 18. Implement missile smoke trails system (Done)
// 19. Give the bullets firing a small noise to simulate inaccuracy (Done)
// 20. Lock-on only applies when launching missiles
// 21. Add weapon selection system
// 22. Special skill: target all enemies on screen and lock-on them all
// 23. Timer system (Done)
// 24. Different customcursor for different weapon types
// 25. Make different weapon details into ScriptableObject
// 26. Make Inventory system for weapons and items
// 27. Make different enemy types with different behaviors (Make a new script for each enemy type, and hold shared behavior in a base class)
// 28. Make inventories notifier of each equipped weapons. (Like in Overwatch)
// 29. Build a storyline and missions
// 30. Add checkpoints and save/load system
// 31. Implement multiplayer functionality (e.g., co-op or PvP modes)
// 32. Implement text animation and sound effects for the storyline and missions
// 33. Implement Hellfire missile skill (Done)
// 34. Incremental slot expansion system for skills (Not done)
// 35. Implement Movement logic for the ally aircrafts.
// 36. Implement shield mode (visual, feature

// Bugfix
// 1. When shooting bullets, sudden move when tilting sideways (Fixed)
// 2. The auto-targetting is sometimes jammed. Why is this happening and how to fix this issue? (Fixed)
// 3. At the start of the game, the radar system is blinking from the center.  (Fixed)
// 4. Later, change the rendering order for the aiming reticle. 
// 5. The paperplane booster effect is not showing
// 6. The aim mouse pointer is too slow to follow the mouse position. (Fixed)

// List of feature upgrades
// 1. Missile launch ability
// 2. Rear view camera
// 3. Radar system

// List of Skills to implement
// 1. Hellfire missile barrage (Done)
// 2. Speed boost (Done)
// 3. EMP blast (Not done)
// 4. Stealth mode (Done)
// 5. Shield (Not done)
// 6. Call airstrike (Not done)
// 7. Repair drone (Not done)
// 8. Nuke launch (Done)
// 


// View slight change with mouse position movement (Done)
// Bullet disappearing too soon 
// Rear view camera not working (Fixed)
// Nuke cannot be seen

// Refactor so that upgrade uses delegate/event hook system