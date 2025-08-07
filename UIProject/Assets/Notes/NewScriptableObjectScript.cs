using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Notes", menuName = "Scriptable Objects/Notes")]
public class NewScriptableObjectScript : ScriptableObject
{
    [TextArea(1, 10)]
    public List<string> notes;
}
