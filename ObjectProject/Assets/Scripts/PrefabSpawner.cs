using UnityEngine;
// 1. 프리팹을 직접 할당하여 스폰하는 MonoBehaviour 스크립트
// 2. 생성된 오브젝트에 대한 정보를 내부에서 가진다.
// 3. 생성 후에 파괴에 대한 딜레이 시간을 가진다.

// 해당 스크립트를 가진 오브젝트가 실행되면, 생성을 진행하고 일정 시간 후에 파괴하는 기능을 구현한다.
// 조건) 프리팹이 등록이 되어 있어야 하며, 등록되지 않은 경우 경고 메시지를 출력한다.

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn
    GameObject spawnedObject; // The spawned object reference
    public float delay = 5f; // Delay before destroying the spawned object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (prefab != null)
        {
            spawnedObject = Instantiate(prefab);
            // 생성 코드 Instantiate()
            // 1. Instantiate(prefab); 해당 프리팹의 정보에 맞게 위치와 회전을 설정하여 생성
            // 2. Instantiate(prefab, transform.position, Quaternion.Identity); 현재 오브젝트의 위치와 회전을 기준으로 생성
            // 3. Instantiate(prefab, parent); 부모 오브젝트를 지정하여 생성
            // 4. Instantiate(prefab, transform.position, transform.rotation, parent); 현재 오브젝트의 위치와 회전을 기준으로 부모 오브젝트를 지정하여 생성
            // 오브젝트를 생성하고, 그 오브젝트를 전달한 위치의 자식으로써 등록합니다.
            Debug.Log(spawnedObject.name + "이 생성되었습니다.");
            spawnedObject.name = "생성된 오브젝트"; // 생성된 오브젝트의 이름을 변경합니다.
            //spawnedObject.AddComponent<Rigidbody>(); // 생성된 오브젝트에 Rigidbody 컴포넌트를 추가합니다.
            Destroy(spawnedObject, delay); // 일정 시간 후에 생성된 오브젝트를 파괴합니다.
        }
        else
        {
            Debug.LogWarning("등록된 프리팹이 따로 없습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
