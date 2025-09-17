using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Scriptable Objects/PlayerStat")]
public class PlayerStat : ScriptableObject
{
    public float statUpgradeMultiplier = 1.2f;
    public float baseThrust = 40;
    public float boostMultiplier = 10;
    public float maxSpeed = 250;
    public float bulletDamage = 10f;
    public float missileDamage = 100f;
}
