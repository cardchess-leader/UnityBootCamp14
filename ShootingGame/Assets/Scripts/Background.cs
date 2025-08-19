using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Background : MonoBehaviour
{
    public float scrollSpeed = 0.2f;
    private Material _material;

    void Awake()
    {
        // 이 게임 오브젝트의 Renderer 컴포넌트에서 고유한 머티리얼 인스턴스를 가져옵니다.
        _material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // 대각선 방향으로 스크롤합니다.
        Vector2 dir = new Vector2(1, 1);
        
        // 이 오브젝트만의 고유한 머티리얼 인스턴스의 텍스처 오프셋을 변경합니다.
        _material.mainTextureOffset += dir.normalized * scrollSpeed * Time.deltaTime;
    }
}
