// CameraFollow.cs
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // �÷��̾� Transform
    public Vector3 offset = new Vector3(0, 5, -10); // ī�޶�� �÷��̾��� ��� ��ġ

    void LateUpdate()
    {
        if (player == null) return;

        // �÷��̾��� ��ġ + ���������� ī�޶� ��ġ ����
        transform.position = player.position + offset;

        // ī�޶� �׻� �÷��̾ �ٶ󺸰� ��
        transform.LookAt(player.position);
    }
}