// CameraFollow.cs
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // 플레이어 Transform
    public Vector3 offset = new Vector3(0, 5, -10); // 카메라와 플레이어의 상대 위치

    void LateUpdate()
    {
        if (player == null) return;

        // 플레이어의 위치 + 오프셋으로 카메라 위치 설정
        transform.position = player.position + offset;

        // 카메라가 항상 플레이어를 바라보게 함
        transform.LookAt(player.position);
    }
}