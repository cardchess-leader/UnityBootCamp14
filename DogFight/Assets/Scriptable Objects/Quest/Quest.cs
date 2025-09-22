using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public enum QuestType
    {
        Main,
        Side,
        Daily
    }

    public int questId;
    public string questName;
    public string description;
    public int experienceReward;
}
