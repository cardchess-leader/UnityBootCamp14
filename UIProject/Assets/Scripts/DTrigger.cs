using UnityEngine;
using System.Collections.Generic;

// 오브젝트에 연결해 Inspector에서 대사를 등록하는 기능
public class DTrigger : MonoBehaviour
{
    public List<Dialog> scripts;
    public void OnDTriggerEnter()
    {
        if (scripts == null || scripts.Count == 0)
        {
            return;
        }
        // DialogManager의 인스턴스를 통해 대화를 시작합니다.
        DialogManager.Instance.StartDialog(scripts);
    }
}
