using UnityEngine;
// 키(Key)와 값(Value)을 저장하는 PlayerPrefs를 활용한 테스트 코드
// 키(Key) : 값에 접근하기 위한 데이터로, 키는 고유해야 합니다.
// 값(Value) : 키에 대응하는 데이터로, 문자열, 정수, 부동소수점 숫자 등을 저장할 수 있습니다.
// 키와 값이 한 쌍으로 관리되며, 키가 삭제되면 해당 값도 함께 삭제됩니다.
// 키를 통해 값을 조회하고, 추가하고, 삭제하는 과정을 매우 빠르게 진행할 수 있습니다.

// 유니티 내에서 사용되는 Key: Value 형태의 데이터
// 1. Dictionary<K, V> : C#에서 제공되는 표준 자료 구조
// 2. PlayerPrefs : 유니티에서 제공하는 키-값 저장 시스템(클래스)
// 3. Json : 외부에서 작성된 json파일도 Key: Value 형태로 저장할 수 있습니다.
// 4. ScriptableObject : 자체적으로는 제공이 안되나 Dictionary와 섞어서 사용이 가능합니다.


// PlayerPrefs: 
// - Unity에서 제공하는 간단한 키-값 저장 시스템
// 복잡한 데이터나 큰 용량을 요구하는 데이터 저장에는 부적합합니다.

// 주로 고려되는 사용 사례:
// 1. 게임 설정 저장 (예: 사운드 볼륨, 그래픽 품질 등)
// 2. 플레이어 진행 상황 저장 (예: 레벨, 점수 등)
// 3. 간단한 데이터 저장 (예: 플레이어 이름, 아이템 획득 여부 등)

// 장점: 즉각적이고 간편한 저장 / 로드에 대한 구현에서는 편함,
//       플랫폼 별로의 저장 경로, 포맷 걱정 없이 사용됩니다.
//       ex) Windows -> 레지스트리 경로
//           MacOS -> ~/Library/Preferences/unity.[회사명].[프로젝트명].plist
//           Android -> /data/data/[패키지명]/shared_prefs/[패키지명].xml
//           iOS -> /Library/Preferences/[패키지명].plist
//           WebGL -> 브라우저의 로컬 스토리지에 저장
//       - 플랫폼 별로 저장 경로가 다르지만, 유니티가 자동으로 관리해줍니다.

// 단점 : 플레이어가 편집이 가능한 데이터이기 때문에, 보안이 중요한 게임에서는 사용을 피해야 합니다.

public class PlayerPrefsTester : MonoBehaviour
{
    public int score;
    public int maxScore = 100; // 최대 점수 설정

    private void Start()
    {
        score = PlayerPrefs.GetInt("score", 1); // score 키로 저장된 값을 가져오고, 없으면 명시된 기본값 1을 사용합니다.
        PlayerPrefs.SetInt("maxScore", maxScore); // score 키로 저장된 값을 가져오고, 없으면 명시된 기본값 1을 사용합니다.

        PlayerPrefs.Save(); // 변경된 내용을 즉시 저장합니다. 이 코드가 없어도 유니티는 자동으로 저장하지만, 명시적으로 호출하면 즉시 저장됩니다.
        // PlayerPrefs.Save()를 호출하지 않는 경우 다음과 같은 상황이 발생할 수 있습니다:
        // 1. 게임이 종료되거나 재시작될 때, 변경된 내용이 저장되지 않을 수 있습니다.
        // 2. 다른 스크립트나 기능에서 PlayerPrefs를 읽을 때, 최신 값이 반영되지 않을 수 있습니다.


        Debug.Log($"현재 점수: {score}"); 
        Debug.Log($"Max Score is: {PlayerPrefs.GetInt("maxScore")}"); // maxScore 키로 저장된 값을 가져옵니다.
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll(); // 모든 PlayerPrefs 데이터를 삭제합니다.
        Debug.Log("모든 PlayerPrefs 데이터가 삭제되었습니다.");
    }
}