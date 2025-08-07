using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class UnitInventory : MonoBehaviour
{
    public int gold;
    public List<string> inventoryItems;

    public bool HasRequiredItems(string requirement)
    {
        var requirementList = requirement.Split('+');
        // use regex to check how much gold is required
        foreach(var requirementItem in requirementList)
        {
            var goldPattern = @"(\d+)골드";
            // 정규식 패턴을 사용하여 골드 금액을 추출합니다.
            Match match = Regex.Match(requirementItem, goldPattern);
            if (match.Success)
            {
                string goldAmountStr = match.Groups[1].Value; // 골드 금액 추출
                int goldAmount = int.Parse(goldAmountStr);
                if (gold < goldAmount)
                {
                    return false; // 골드가 부족하면 false 반환
                }
            } else
            {
                // inventoryItems가 requirementItem을 포함하는지 확인합니다.
                if (!inventoryItems.Contains(requirementItem.Trim()))
                {
                    return false; // 요구 사항에 필요한 아이템이 없으면 false 반환
                }
            }
        }
        RemoveItems(requirement);
        return true; // 모든 요구 사항을 충족하면 true 반환
    }

    public void RemoveItems(string requirement)
    {
        var requirementList = requirement.Split('+');
        foreach (var requirementItem in requirementList)
        {
            var goldPattern = @"(\d+)골드";
            // 정규식 패턴을 사용하여 골드 금액을 추출합니다.
            Match match = Regex.Match(requirementItem, goldPattern);
            if (match.Success)
            {
                string goldAmountStr = match.Groups[1].Value; // 골드 금액 추출
                int goldAmount = int.Parse(goldAmountStr);
                gold -= goldAmount; // 골드 금액에서 현재 골드를 뺍니다.
            }
            else
            {
                // inventoryItems에서 requirementItem을 제거합니다.
                string itemToRemove = requirementItem.Trim();
                if (inventoryItems.Contains(itemToRemove))
                {
                    inventoryItems.Remove(itemToRemove); // 아이템 제거
                }
            }
        }
    }
}
