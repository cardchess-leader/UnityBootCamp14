using UnityEngine;

public class ItemTester : MonoBehaviour
{
    public SOMaker item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(item.description);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LevelUp()
    {
        item.level++;
        Debug.Log("레벨이 증가했습니다!");
    }
}
