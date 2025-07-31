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
    [Tooltip("�÷��̾ ������ ������ ��Ÿ���ϴ�. ������ ������ ���� �ֽ��ϴ�.")]
    public Job jobSelections;

    [Header("- Other Player Info -")]
    [TextArea(3, 5)]
    [Tooltip("�÷��̾ �Է��� ���� ���¸޽��� �Դϴ�.")]
    public string statusMessage;

    [Header("- Player Inventory -")]
    public List<Item> inventory;

    [Header("- Player Events -")]
    [Tooltip("�̺�Ʈ ����Ʈ�� �߰��ϰ�, ������ ����� ���� ���� ������Ʈ�� ����ϼ���.")]
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
