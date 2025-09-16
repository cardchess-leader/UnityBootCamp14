using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField]
    public int level;
    [SerializeField]
    public int expToNextLevel;
    [SerializeField]
    public List<GameObject> enemyPrefabList;
}
