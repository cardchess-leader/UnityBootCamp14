using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;

class QuestProgressData
{
    public int killCount = 0;
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> quests;
    private Quest activeQuest;
    private QuestProgressData questProgressData;
    private float timer = 0;

    // Here are the list of fields that are required for all list of quests.


    private Coroutine questClearCoroutine;

    void Start()
    {
        PickQuest();
        ResetQuestProgressData();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    private void ResetQuestProgressData()
    {
        questProgressData = new QuestProgressData();
    }

    private void PickQuest()
    {
        if (quests.Count > 0)
        {
            activeQuest = quests[Random.Range(0, quests.Count)];
            quests.Remove(activeQuest);
        }
    }

    bool CheckQuestClear()
    {
        if (activeQuest == null) return false;
        switch(activeQuest.questId)
        {
            case 1:
                if (questProgressData.killCount >= 10)
                {
                    if (questClearCoroutine == null)
                    {
                        questClearCoroutine = StartCoroutine(QuestClearCoroutine());
                    }
                    return true;
                }
                break;
        }
        return false;
    }

    IEnumerator QuestClearCoroutine()
    {
        yield return new WaitForSeconds(2f);
        switch(activeQuest.questId)
        {
            case 1:

            break;
        }
        PickQuest();
        questClearCoroutine = null;
    }
}
