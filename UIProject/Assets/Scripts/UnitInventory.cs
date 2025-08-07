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
            var goldPattern = @"(\d+)���";
            // ���Խ� ������ ����Ͽ� ��� �ݾ��� �����մϴ�.
            Match match = Regex.Match(requirementItem, goldPattern);
            if (match.Success)
            {
                string goldAmountStr = match.Groups[1].Value; // ��� �ݾ� ����
                int goldAmount = int.Parse(goldAmountStr);
                if (gold < goldAmount)
                {
                    return false; // ��尡 �����ϸ� false ��ȯ
                }
            } else
            {
                // inventoryItems�� requirementItem�� �����ϴ��� Ȯ���մϴ�.
                if (!inventoryItems.Contains(requirementItem.Trim()))
                {
                    return false; // �䱸 ���׿� �ʿ��� �������� ������ false ��ȯ
                }
            }
        }
        RemoveItems(requirement);
        return true; // ��� �䱸 ������ �����ϸ� true ��ȯ
    }

    public void RemoveItems(string requirement)
    {
        var requirementList = requirement.Split('+');
        foreach (var requirementItem in requirementList)
        {
            var goldPattern = @"(\d+)���";
            // ���Խ� ������ ����Ͽ� ��� �ݾ��� �����մϴ�.
            Match match = Regex.Match(requirementItem, goldPattern);
            if (match.Success)
            {
                string goldAmountStr = match.Groups[1].Value; // ��� �ݾ� ����
                int goldAmount = int.Parse(goldAmountStr);
                gold -= goldAmount; // ��� �ݾ׿��� ���� ��带 ���ϴ�.
            }
            else
            {
                // inventoryItems���� requirementItem�� �����մϴ�.
                string itemToRemove = requirementItem.Trim();
                if (inventoryItems.Contains(itemToRemove))
                {
                    inventoryItems.Remove(itemToRemove); // ������ ����
                }
            }
        }
    }
}
