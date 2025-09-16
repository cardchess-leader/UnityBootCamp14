using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    // Set the player ability stat here below


    [NonSerialized] public bool isInvincible;
    [NonSerialized] public bool isStealthMode;
    [NonSerialized] public bool isSprintMode;
    [NonSerialized] public bool isDead;
}