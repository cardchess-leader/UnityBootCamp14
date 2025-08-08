using UnityEngine;
using System.Collections.Generic;

// ������Ʈ�� ������ Inspector���� ��縦 ����ϴ� ���
public class DTrigger : MonoBehaviour
{
    public List<Dialog> scripts;
    public void OnDTriggerEnter()
    {
        if (scripts == null || scripts.Count == 0)
        {
            return;
        }
        // DialogManager�� �ν��Ͻ��� ���� ��ȭ�� �����մϴ�.
        DialogManager.Instance.StartDialog(scripts);
    }
}
