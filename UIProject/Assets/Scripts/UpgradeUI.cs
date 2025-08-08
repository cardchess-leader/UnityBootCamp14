using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ���׷��̵忡 ���� ���
public class UpgradeUI : MonoBehaviour
{
    public UnitStat unitStat; // ������ �ɷ�ġ ��ũ��Ʈ
    public Button button1;
    public Text message1;
    public Text message2;
    public Text message3;
    public Text message4;
    public Text iconText;
    public GameObject messagePanel;

    // ��ư Ŭ�� �� ȣ���� �޼ҵ� ����
    private void OnUpgradeBtnClick()
    {
        // ���׷��̵� �õ�
        if (unitStat.TryUpgrade())
        {
            // ���׷��̵� ���� �� UI ������Ʈ
            UpdateUI();
            StartCoroutine(ShowMessageCoroutine("��ȭ ����", 5));
        }
        else
        {
            if (unitStat.isMaxUpgrade)
            {
                // �ִ� ������ �������� �� �޽��� ���
                StartCoroutine(ShowMessageCoroutine("�ִ� ������ �����߽��ϴ�."));
            } else
            {
                // ���׷��̵� ���� �� �޽��� ���
                StartCoroutine(ShowMessageCoroutine("��ȭ ����: ��ᰡ �����մϴ�."));
            }

        }
    }

    private void UpdateUI()
    {
        iconText.text = $"������ + {unitStat.currUpgrade}";
        message1.text = (unitStat.isMaxUpgrade ? "" : "��ȭ ���\n") + unitStat.upgradeRequirement;
        message2.text = $"�κ��丮:\n" + $"{unitStat.inventory.gold}���\n" + string.Join('\n', unitStat.inventory.inventoryItems);
        message4.text = $"���� �ɷ�ġ: \n{unitStat.wizardStat}";
    }

    private void Start()
    {
        button1.onClick.AddListener(OnUpgradeBtnClick);
        // AddListener�� ��ư�� Ŭ���Ǿ��� �� ȣ���� �޼ҵ带 ����ϴ� ����Դϴ�.
        // ������ �� �ִ� �޼ҵ�� �Ű������� ���� �޼ҵ忩�� �մϴ�.
        // �ٸ� ���·� ���� ����� delegate �Ǵ� lambda ǥ������ ����ؾ� �մϴ�.
        // Ư¡) �� ����� ���� �̺�Ʈ�� ����� �����Ѵٸ� ����Ƽ �ν����Ϳ���
        // ����� �� Ȯ���� �� �����ϴ�.

        // ����: ���� ������� �ʾƼ� �߸� ����ϴ� �Ǽ��� ������ �� �ֽ��ϴ�.
        UpdateUI(); // ���� �� UI �ʱ�ȭ
    }

    IEnumerator ShowMessageCoroutine(string message, float duration = 1f)
    {
        messagePanel.SetActive(true); // �޽��� �г� Ȱ��ȭ
        message3.text = message;
        yield return new WaitForSeconds(duration);
        messagePanel.SetActive(false); // �޽��� �г� ��Ȱ��ȭ
    }
}
