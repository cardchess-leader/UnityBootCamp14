using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "Scriptable Objects/Quiz")]
public class Quiz : ScriptableObject
{
    public string topic;
    public string question;
    public string [] answers;
    public int answerIndex;
    public int difficulty;
}
