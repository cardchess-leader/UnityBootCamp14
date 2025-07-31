using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PlayerStat : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public string name;
        [Range(0, 100)] public float value;
    }
    [Flags]
    public enum Job
    {
        Mage = 1 << 0,
        Warrior = 1 << 1,
        Thief = 1 << 2,
        Archer = 1 << 3,
    }
    [Header("- Basic Stats -")]
    [Range(0, 100)] public int hp;
    [Range(0, 100)] public float speed;
    [Tooltip("플레이어가 선택한 직업을 나타냅니다. 여러개 선택할 수도 있습니다.")]
    public Job jobSelections;

    [Header("- Other Player Info -")]
    [TextArea(3, 5)]
    [Tooltip("플레이어가 입력한 현재 상태메시지 입니다.")]
    public string statusMessage;

    [Header("- Player Inventory -")]
    public List<Item> inventory;

    [Header("- Player Events -")]
    [Tooltip("이벤트 리스트를 추가하고, 실행할 기능을 가진 게임 오브젝트를 등록하세요.")]
    public UnityEvent action;

    private void Update()
    {
        action.Invoke();
    }

    public void Heal()
    {
        hp++;
        if (hp == 100)
        {
            transform.localScale *= 1.1f;
        }
    }
}
