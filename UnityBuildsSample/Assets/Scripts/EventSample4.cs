using System;
using UnityEngine;

public class DamageEventArgs : EventArgs
{
    // 전달할 값에 대한 설계(프로퍼티로 작업하며, get기능만 주로 활성화 합니다.)
    public int Value { get; } // Value에 대한 접근만 가능
    public string EventName { get; }

    public DamageEventArgs(int value, string eventName)
    {
        Value = value; // 생성자에서 Value를 초기화
        EventName = eventName; // 생성자에서 EventName을 초기화
    }
}

public class EventSample4 : MonoBehaviour
{
    public event EventHandler<DamageEventArgs> OnDamaged; // 데미지를 받았을 때에 대한 이벤트 핸들러

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            TakeDamage(UnityEngine.Random.Range(10, 200), "적의 공격"); // 데미지를 받았다고 이벤트 발생
        }
    }

    public void TakeDamage(int damage, string eventName)
    {
        OnDamaged?.Invoke(this, new DamageEventArgs(damage, eventName));

        Debug.Log($"<color=red>[{eventName}] 플레이어가 {damage} 데미지를 받았습니다. </color>");
    }
}
