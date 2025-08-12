using UnityEngine;

[CreateAssetMenu(fileName = "프리셋", menuName = "스텟/프리셋")]
public class Preset : ScriptableObject
{
    public string presetName;
    public PlayerData playerData;
}
