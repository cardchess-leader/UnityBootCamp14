using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public enum LotteryItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[Serializable]
public class LotteryItem
{
    public string Name;
    public LotteryItemRarity Rarity;
}

public class Lottery : MonoBehaviour
{
    public List<LotteryItem> LotteryItems;
    public UnityEvent<LotteryItem> OnPickLottery;
    public Text text;

    public void RandomlyPickLottery()
    {
        int index = UnityEngine.Random.Range(0, LotteryItems.Count);
        LotteryItem pickedItem = LotteryItems[index];
        OnPickLottery?.Invoke(pickedItem);
    }

    public void ShowPickedLotteryItem(LotteryItem item)
    {
        string textColor;
        switch(item.Rarity)
        {
            case LotteryItemRarity.Common:
                textColor = "white";
                break;
            case LotteryItemRarity.Uncommon:
                textColor = "green";
                break;
            case LotteryItemRarity.Rare:
                textColor = "blue";
                break;
            case LotteryItemRarity.Epic:
                textColor = "purple";
                break;
            case LotteryItemRarity.Legendary:
                textColor = "orange";
                break;
            default:
                textColor = "white";
                break;
        }
        Debug.Log($"<color={textColor}>점심 메뉴: {item.Name}, 등급: {item.Rarity}</color>");
        if (item.Rarity >= LotteryItemRarity.Rare)
        {
            text.text = $"<color={textColor}>점심 메뉴: {item.Name}\n" +
                $"등급: {item.Rarity}</color>";
        } else
        {
            text.text = "점심 메뉴: ";
        }
    }
}

// 패키지
// 유니티의 패키지는 프로젝트에 필요한 기능, 라이브러리, 툴, 애셋들을 모아서 재사용하는 배포 단위를 의미합니다.
// 패키지 사용 방법
// 1. Asset Store에 있는 애셋을 다운받아서 사용한다.
// 2. Unity Registry(유니티 래지스트리): 유니티가 제공하는 공식 패키지 저장소로, 다양한 기능과 툴을 제공합니다. 추가되지 않은 패키지는 Unity Package Manager를 통해 설치할 수 있습니다.

// 자주 사용하는 유니티 공식 패키지
// 1. Cinemachine: 카메라 연출 도구
// 2. TextMesh Pro: 고급 텍스트 렌더링 도구
// 3. Input System: 입력 처리 시스템
// 4. Addressables: 에셋 관리 및 로딩 최적화 도구

// 사용 방법: 
// Import: 유니티 에디터에서 패키지를 가져오는 작업 (유니티 에디터에서는 custom packages(별도의 패키지)를 import하는 기능과, 애셋 스토어에서 다운받은 파일, 유니티 레지스트리 쪽에서 install하고 import하는 기능으로 나뉩니다.)
// Export: 현재 프로젝트에 있는 값들을 패키지로 만드는 작업

// 유니티 패키지의 내부 구조
// 패키지에 대한 메타데이터(.json)
// README.md (패키지에 대한 설명)
// 라이센스
// 버전 변경 기록
// 예시 샘플 (씬이나 코드)
// 런타임 : 스크립트, 에셋
// 에디터 : 에디터 전용 코드
// 테스트 : 테스트 전용 코드

// 패키지 경로
// - 프로젝트 폴더 내부
// - 외부 Git 패키지

// 패키지의 사용 목적
// 1. 재사용 (커스텀 UI, JSON 데이터 매니저, 서버 DB 연동, 공용 스크립트)
// 2. 에디터와 런타임 코드의 분리 보관
// 3. 버전 관리 용도로 쓰기 괜찮다. -> 특정 버전에 대한 고정

// 유니티 빌트 프로파일
// Scene List를 통해서 현재 열려 있는 씬의 목록을 설정할 수 있습니다.

// 플랫폼 설정을 진행할 수 있습니다.

// 플랫폼에 대한 설치 작업은 필요합니다. (유니티 허브에서의 Install)

// Player Settings를 통해 해상도, 아이콘, 로고, 스플래시 이미지, 회사명, 버전 명 등 게임 프로젝트에 필요한 설정들을 진행할 수 있습니다.

// Add Build Profile을 통해 플랫폼 별 빌드의 구성을 저장, 관리할 수 있습니다.

// 추가한 프로파일에서 설정할 수 있는 내용은 다음과 같습니다.
// Override Global Scene List: 현재 씬 목록을 덮어쓰는 기능입니다. 이 기능을 체크할 경우 현재의 빌드 프로파일에서 자체적으로 빌드할 씬 목록만 등록할 수 있습니다.
// 이 기능으로 할만한 작업? >> 데모 버전 빌드 (완성된 씬 중에서 메인만 포함시키고, 다른 씬 제외)

// >> 특정 기능 테스트를 위한 빌드
// >> 서로 다른 시작 씬을 가지는 상황에 대한 빌드도 가능

// Scripting Defines: 스크립트 정의를 추가할 수 있습니다.
// C# 코드에서의 전처리기 정의를 설정합니다.

// C# preprocessor(전처리기)
// 컴파일에 코드의 일부를 조건적으로 포함시키거나 제외시키는 용도로 사용되는 먼저 처리되는 기능들을 의미합니다.

// # + 영단어 조합으로 구성됩니다.
// 대표적으로 사용되는 C# 전처리기 지시어는 다음과 같습니다:
// #define, #if, #else, #elif, #endif, #undef, #warning, #error
// 각 지시어의 역할은 다음과 같습니다:
// #define: 전처리기 심볼을 정의합니다. 심볼은 조건부 컴파일을 위해 쓰는 유니티의 태그 같은 기능
// #define BUG_FIX
// #if: 특정 심볼이 정의되어 있는지 확인하고, 해당 조건이 참일 때만 코드를 포함시킵니다.
// #if BUG_FIX
// #else: #if 조건이 거짓일 때 실행할 코드를 지정합니다.
// #elif: #if 조건이 거짓일 때 다른 조건을 확인합니다.
// #endif: #if 또는 #elif로 시작한 조건부 컴파일 블록을 종료합니다.
// #undef: 정의된 심볼을 제거합니다.
// #warning: 컴파일러에게 경고 메시지를 출력합니다.
// #error: 컴파일러에게 오류 메시지를 출력하고 컴파일을 중단합니다.
// #region, endregion: 스크립트 코드에 대한 접기 영역을 만들어 줍니다.

// 유니티에서는 Player Settings에서 설정을 진행하거나 유니티 6 이상의 버전에서는 Build Profile의 Scripting Defines에 작성합니다.

// 1. Project Settings -> Player - > Other Settings - > Scripting Define Symbols
// 2. Build Profile의 Add Build Profile -> Override Global Scene List 체크 후, Scripting Defines에 작성

// Player Settings에서 설정하는 값들은 다음과 같습니다:
// 1. CompanyName: 회사 이름을 설정합니다.
// 2. ProductName: 프로젝트/게임/앱의 이름을 설정합니다.
// 3. Version: 프로젝트의 버전을 설정합니다.

// Icon: 플랫폼 별 아이콘을 설정하는 기능입니다.
//       모바일 작업에서는 필수적으로 진행해줍니다.
// Cursor: 프로그램 내부에서 사용할 커서의 이미지 -> 기본 이미지를 설정할 수 있습니다.
// Resolution & Presentation 설정
// Run In Background
// 해당 설정을 킬 경우, 유니티 화면에서 벗어나도 실행됩니다.

// Standalone 기준 설정 
// 유니티에서 빌드할 수 있는 PC 환경용 앱
// Windows, macOS, Linux

// 1. 플레이어 로그 파일 생성 여부
// 2. 창 크기 조절 기능 (Resizable Window)
// 3. 게임 창 포커스를 잃어도 렌더링할지에 대한 여부
// 4. alt + enter로 전체 화면 모드로 전환할지 여부 (Allow Fullscreen Switch)
// 5. Force Single Instance: 단일 인스턴스 모드로 실행할지 여부 (한 번에 하나의 인스턴스만 실행 가능)
// 6. Use DGXI flip model swapchain for D3D11: DirectX 11에서 플립 모델 스왑체인을 사용할지 여부 (성능 향상 가능성 있음)

// Splash Image
// 스플래시 이미지 설정
// 이미지 필요, 로고: 화면에 보이는 로고 이미지. 백그라운드: 배경 이미지. 에디터에서는 Preview를 통해 기능 체크 가능
// 로고 기능에서 Show Unity Logo를 제거하면 
// 단, 로고 리스트에 로고가 추가가 되어있을 것

// 로고와 백그라운드로 사용하는 이미지는 Sprite 설정을 해줘야 사용 가능합니다.
// 로고는 2D UI 이미지로 사용되며, 백그라운드는 3D 오브젝트로 사용됩니다.