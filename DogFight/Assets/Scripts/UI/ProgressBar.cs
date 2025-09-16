using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public int minValue = 0;
    public int maxValue = 10;

    public int currentValue = 0;

    [SerializeField]
    float spacing = 10;

    RectTransform background;
    RectTransform foreground;
    RectTransform bgTemplate;
    RectTransform fgTemplate;

    List<RectTransform> bgList = new List<RectTransform>();
    List<RectTransform> fgList = new List<RectTransform>();

    void AssignFields()
    {
        background = transform.Find("Background").GetComponent<RectTransform>();
        foreground = transform.Find("Foreground").GetComponent<RectTransform>();
        bgTemplate = transform.Find("BgTemplate").GetComponent<RectTransform>();
        fgTemplate = transform.Find("FgTemplate").GetComponent<RectTransform>();
    }

    public int Value
    {
        get => currentValue;
        set
        {
            currentValue = value;
            OnValueChanged();
            UpdateGUI();
        }
    }

    public void SetMinMaxValue(int min, int max)
    {
        minValue = min;
        maxValue = max;
    }

    void OnValueChanged()
    {
        if (currentValue < 0)
        {
            currentValue = 0;
        }
        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }
    }

    void InitValue()
    {
        currentValue = minValue;
    }

    void Start()
    {
        AssignFields();
        InitValue();
        CreateList(bgList, background, bgTemplate);
        CreateList(fgList, foreground, fgTemplate);
        UpdateGUI();
    }
    void UpdateGUI()
    {
        UpdateForeground();
    }

    void CreateList(List<RectTransform> list, RectTransform rectParent, RectTransform template)
    {
        template.gameObject.SetActive(false);
        float curX = 0;
        float itemWidth = template.rect.width;
        for (int i = 0; i < maxValue; i++)
        {
            RectTransform item = CreateItem(rectParent, template);
            list.Add(item);
            Vector3 pos = item.anchoredPosition3D;
            pos.x = curX;
            item.anchoredPosition3D = pos;
            curX += (itemWidth + spacing);
        }
    }

    RectTransform CreateItem(RectTransform rectParent, RectTransform template)
    {
        GameObject obj = GameObject.Instantiate(template.gameObject, rectParent);
        obj.gameObject.SetActive(true);
        RectTransform rectTrans = obj.GetComponent<RectTransform>();
        rectTrans.localScale = Vector3.one;
        rectTrans.localEulerAngles = Vector3.zero;
        rectTrans.anchoredPosition3D = Vector3.zero;
        return rectTrans;
    }

    void UpdateForeground()
    {
        for (int i = 0; i < fgList.Count; i++)
        {
            RectTransform rectTrans = fgList[i];
            if (i < currentValue)
            {
                rectTrans.gameObject.SetActive(true);
            }
            else
            {
                rectTrans.gameObject.SetActive(false);
            }
        }
    }
}