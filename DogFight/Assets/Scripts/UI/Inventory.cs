using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum WeaponMode
    {
        MachineGun,
        Missile,
    }

    // Make this singleton so that it can be accessed from other scripts
    public static Inventory Instance { get; private set; }
    [SerializeField]
    List<InventorySlot> inventorySlotList = new List<InventorySlot>();

    private void Awake()
    {
        // Ensure that there is only one instance of Inventory
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // ÄÄÆ÷³ÍÆ®¸¸ ÆÄ±«, GameObject´Â À¯Áö
            Destroy(this);
            return;
        }
    }

    public WeaponMode weaponMode = WeaponMode.Missile;

    private void Update()
    {
        // Switch weapon mode with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponMode = WeaponMode.Missile;
            inventorySlotList[0].Highlight(true);
            inventorySlotList[1].Highlight(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponMode = WeaponMode.MachineGun;
            inventorySlotList[0].Highlight(false);
            inventorySlotList[1].Highlight(true);
        }
    }
}
