using UnityEngine;
using UnityEngine.UI;

public class EnchantController : MonoBehaviour
{
    public string itemName = "아이템 이름";
    Text title, damage, count, prob;
    int enchantSuccessCnt = 0;
    int maxEnchantCount = 10;

    void UpdateUIText()
    {
        title.text = $"{itemName} : (+{enchantSuccessCnt})";
        damage.text = $"공격력: 50(+{enchantSuccessCnt * 5})";
        count.text = $"강화 수치: {enchantSuccessCnt} / {maxEnchantCount}";
        prob.text = $"성공 확률: {100 - enchantSuccessCnt * 10}%";
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        title = transform.Find("title").GetComponent<Text>();
        damage = transform.Find("damage").GetComponent<Text>();
        count = transform.Find("count").GetComponent<Text>();
        prob = transform.Find("prob").GetComponent<Text>();
        UpdateUIText();
    }

    // Update is called once per frame
    void Update()
    {
        if (enchantSuccessCnt == maxEnchantCount) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (Random.Range(0f, 10f) >= enchantSuccessCnt)
            {
                enchantSuccessCnt++;
            }
            UpdateUIText();
        }
    }
}
