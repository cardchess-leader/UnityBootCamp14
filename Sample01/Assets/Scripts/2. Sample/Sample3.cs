using UnityEngine;
// 유니티에서 사용되는 클래스 사용해 스크립트를 작성합니다.

// 1. 유니티의 Transform 클래스 사용
// 트랜스폼은 유니티 에디터에서 게임 오브젝트를 생성할 때 자동으로 부여되는 컴포넌트입니다.

// Transform은 게임 오브젝트의 위치, 회전, 크기를 관리합니다.
// 컴포넌트의 기능을 호출하는 GetComponent<T>()를 사용하지 않고도 바로 사용이 가능합니다.

//클래스가 제공해주는 속성(Property))을 사용하여 위치, 회전, 크기를 설정할 수 있습니다.
// transform.position -> 게임 오브젝트의 위치를 설정합니다.
// transform.rotation -> 게임 오브젝트의 회전을 설정합니다.
// Quaternion형태의 데이터 x, y, z, w를 사용합니다.
// x, y, z는 회전 축을 나타내고, w는 회전의 크기를 나타냅니다.
// transform.forward -> 게임 오브젝트의 앞쪽 방향을 설정합니다.
// transform.forward = Vector3.forward; // 앞쪽 방향을 설정합니다.
// transform.forward, up,right는 각각 게임 오브젝트의 앞쪽, 위쪽, 오른쪽 방향을 나타냅니다.
// transform.eulerAngles -> 게임 오브젝트의 회전을 오일러 각도로 설정합니다.
// eulerAngles는 x, y, z 축을 기준으로 회전하는 각도를 나타냅니다. (Vector3 형태로 설정합니다.)

// 해당 클래스가 제공해주는 주요 문법(메소드)
// transform.LookAt(Transform target) -> 게임 오브젝트가 타겟을 바라보도록 회전합니다.
// transform.Rotate(Vector3 eulerAngles) -> 게임 오브젝트를 오일러 각도로 회전시킵니다.
// transform.Translate(Vector3 translation) -> 게임 오브젝트를 지정한 방향으로 이동시킵니다.

public class Sample3 : MonoBehaviour
{
    public GameObject go;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(GetComponent<Sample4>().value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
