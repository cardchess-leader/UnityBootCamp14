using System.Collections.Generic;
using UnityEngine;
using TMPro; // 변경: TMPro 네임스페이스 사용

// 드롭 다운의 구성요소
// 1. Template : 드롭 다운이 펼쳐졌을 때 리스트 항목
// 2. Caption / Item Text : 현재 선택된 항목 / 리스트 항목의 텍스트
// TMP를 쓰는 경우, 한글 사용을 위해 Label과 Item Label에서 사용 중인 폰트를 수정해 주셔야 사용할 수 있습니다

// 3. Options: 드롭 다운에 표시될 항목에 대한 리스트
// 인스펙터를 통해 직접 등록이 가능합니다.
// 등록하면 바로 리스트에 적용됩니다.

// 4. OnValueChanged: 사용자가 항목을 선택했을 때 호출되는 이벤트
// 인스팩터에서 이벤트를 등록할 수 있습니다. (또는 스크립트에서 등록 가능)
// 드롭 다운 값에 대한 변경 발생 시 호출됩니다.


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

            message.text = $"이름: {selectedText(dropdownList[0])}\n" +
                $"아이템: {selectedText(dropdownList[1])}\n" +
                $"게임기: {selectedText(dropdownList[2])}\n" +
                $"점수: {selectedText(dropdownList[3])}\n";
        } catch (System.Exception e)
        {
            Debug.LogError("드롭다운 업데이트 중 오류 발생: " + e.Message);
            message.text = "드롭다운 업데이트 중 오류가 발생했습니다.";
            return;
        }
    }

    public void ResetDropdowns()
    {
        foreach (var dropdown in dropdownList)
        {
            dropdown.value = 0; // 첫 번째 옵션으로 초기화
        }
        UpdateUI(); // UI 업데이트
    }

    public void SetRandom()
    {
        foreach (var dropdown in dropdownList)
        {
            int randomIndex = Random.Range(0, dropdown.options.Count);
            dropdown.value = randomIndex; // 랜덤한 옵션으로 설정
        }
        UpdateUI(); // UI 업데이트
        if (GameObject.Find("Score") != null)
        {
            GameObject.Find("Score").GetComponent<TMP_Text>().text = "점수: " + Random.Range(0, 100).ToString();
        }
    }
}
