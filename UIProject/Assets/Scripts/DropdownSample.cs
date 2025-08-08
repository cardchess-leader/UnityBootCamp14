using System.Collections.Generic;
using UnityEngine;
using TMPro; // ����: TMPro ���ӽ����̽� ���

// ��� �ٿ��� �������
// 1. Template : ��� �ٿ��� �������� �� ����Ʈ �׸�
// 2. Caption / Item Text : ���� ���õ� �׸� / ����Ʈ �׸��� �ؽ�Ʈ
// TMP�� ���� ���, �ѱ� ����� ���� Label�� Item Label���� ��� ���� ��Ʈ�� ������ �ּž� ����� �� �ֽ��ϴ�

// 3. Options: ��� �ٿ ǥ�õ� �׸� ���� ����Ʈ
// �ν����͸� ���� ���� ����� �����մϴ�.
// ����ϸ� �ٷ� ����Ʈ�� ����˴ϴ�.

// 4. OnValueChanged: ����ڰ� �׸��� �������� �� ȣ��Ǵ� �̺�Ʈ
// �ν����Ϳ��� �̺�Ʈ�� ����� �� �ֽ��ϴ�. (�Ǵ� ��ũ��Ʈ���� ��� ����)
// ��� �ٿ� ���� ���� ���� �߻� �� ȣ��˴ϴ�.


public class DropdownSample : MonoBehaviour
{
    public List<TMP_Dropdown> dropdownList;
    public TMP_Text message;

    void Start()
    {
        UpdateUI();
        for(int i = 0; i < dropdownList.Count; i++)
        {
            int index = i;
            var dropdown = dropdownList[i];
            dropdown.value = 0;
            dropdown.onValueChanged.AddListener((_) => UpdateUI());
        }
    }

    void UpdateUI()
    {
        try
        {
            string selectedText(TMP_Dropdown dropdown) => dropdown.options[dropdown.value].text;

            message.text = $"�̸�: {selectedText(dropdownList[0])}\n" +
                $"������: {selectedText(dropdownList[1])}\n" +
                $"���ӱ�: {selectedText(dropdownList[2])}\n" +
                $"����: {selectedText(dropdownList[3])}\n";
        } catch (System.Exception e)
        {
            Debug.LogError("��Ӵٿ� ������Ʈ �� ���� �߻�: " + e.Message);
            message.text = "��Ӵٿ� ������Ʈ �� ������ �߻��߽��ϴ�.";
            return;
        }
    }

    public void ResetDropdowns()
    {
        foreach (var dropdown in dropdownList)
        {
            dropdown.value = 0; // ù ��° �ɼ����� �ʱ�ȭ
        }
        UpdateUI(); // UI ������Ʈ
    }

    public void SetRandom()
    {
        foreach (var dropdown in dropdownList)
        {
            int randomIndex = Random.Range(0, dropdown.options.Count);
            dropdown.value = randomIndex; // ������ �ɼ����� ����
        }
        UpdateUI(); // UI ������Ʈ
        if (GameObject.Find("Score") != null)
        {
            GameObject.Find("Score").GetComponent<TMP_Text>().text = "����: " + Random.Range(0, 100).ToString();
        }
    }
}
