using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Background : MonoBehaviour
{
    public float scrollSpeed = 0.2f;
    private Material _material;

    void Awake()
    {
        // �� ���� ������Ʈ�� Renderer ������Ʈ���� ������ ��Ƽ���� �ν��Ͻ��� �����ɴϴ�.
        _material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // �밢�� �������� ��ũ���մϴ�.
        Vector2 dir = new Vector2(1, 1);
        
        // �� ������Ʈ���� ������ ��Ƽ���� �ν��Ͻ��� �ؽ�ó �������� �����մϴ�.
        _material.mainTextureOffset += dir.normalized * scrollSpeed * Time.deltaTime;
    }
}
