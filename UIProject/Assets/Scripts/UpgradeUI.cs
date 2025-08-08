using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 업그레이드에 대한 기능
public class UpgradeUI : MonoBehaviour
{
    public UnitStat unitStat; // 유닛의 능력치 스크립트
    public Button button1;
    public Text message1;
    public Text message2;
    public Text message3;
    public Text message4;
    public Text iconText;
    public GameObject messagePanel;

    // 버튼 클릭 시 호출할 메소드 설계
    private void OnUpgradeBtnClick()
    {
        // 업그레이드 시도
        if (unitStat.TryUpgrade())
        {
            // 업그레이드 성공 시 UI 업데이트
            UpdateUI();
            StartCoroutine(ShowMessageCoroutine("강화 성공", 5));
        }
        else
        {
            if (unitStat.isMaxUpgrade)
            {
                // 최대 레벨에 도달했을 때 메시지 출력
                StartCoroutine(ShowMessageCoroutine("최대 레벨에 도달했습니다."));
            } else
            {
                // 업그레이드 실패 시 메시지 출력
                StartCoroutine(ShowMessageCoroutine("강화 실패: 재료가 부족합니다."));
            }

        }
    }

    private void UpdateUI()
    {
        iconText.text = $"마법사 + {unitStat.currUpgrade}";
        message1.text = (unitStat.isMaxUpgrade ? "" : "강화 재료\n") + unitStat.upgradeRequirement;
        message2.text = $"인벤토리:\n" + $"{unitStat.inventory.gold}골드\n" + string.Join('\n', unitStat.inventory.inventoryItems);
        message4.text = $"현재 능력치: \n{unitStat.wizardStat}";
    }

    private void Start()
    {
        button1.onClick.AddListener(OnUpgradeBtnClick);
        // AddListener는 버튼이 클릭되었을 때 호출할 메소드를 등록하는 기능입니다.
        // 전달할 수 있는 메소드는 매개변수가 없는 메소드여야 합니다.
        // 다른 형태로 쓰는 경우라면 delegate 또는 lambda 표현식을 사용해야 합니다.
        // 특징) 이 기능을 통해 이벤트에 기능을 전달한다면 유니티 인스펙터에서
        // 연결된 걸 확인할 수 없습니다.

        // 장점: 직접 등록하지 않아서 잘못 등록하는 실수를 방지할 수 있습니다.
        UpdateUI(); // 시작 시 UI 초기화
    }

    IEnumerator ShowMessageCoroutine(string message, float duration = 1f)
    {
        messagePanel.SetActive(true); // 메시지 패널 활성화
        message3.text = message;
        yield return new WaitForSeconds(duration);
        messagePanel.SetActive(false); // 메시지 패널 비활성화
    }
}
