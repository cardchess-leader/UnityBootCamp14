// 충돌 이벤트 설계
// 오브젝트와 오브젝트 간의 물리적인 충돌 발생 시 호출됩니다.
// 둘 중 하나라도 Rigidbody(강체)를 가지고 있어야 처리됩니다.


// OncollisionEnter: 충돌 발생 시 1번 호출
// OnCollisionStay: 충돌 중일 때 매 프레임마다 호출
// OnCollisionExit: 충돌이 끝났을 때 호출 (충돌 상태에서 벗어났을 때)

// 트리거도 OnTriggerEnter, OnTriggerStay, OnTriggerExit가 있습니다.
// 2D일 경우 OnCollisionEnter2D처럼 마지막에 2D가 붙습니다.

// 일반적인 물리적 충돌 Collision
// Is Trigger가 true인 경우 Trigger로 처리됩니다.

// Is Kinematic: true인 경우 물리 엔진의 영향을 받지 않습니다.
// Constraints: Rigidbody의 움직임을 제한합니다. Freeze Position, Freeze Rotation 등을 설정할 수 있습니다.
// Linear Damping: Rigidbody의 선형 속도 감소를 설정합니다.
// Angular Damping: Rigidbody의 각속도 감소를 설정합니다.
// Damping이 적용되는 공식은 다음과 같습니다: 
// Interpolate: Rigidbody의 움직임을 보간합니다.
// 모드는 None, Interpolate, Extrapolate가 있습니다.
// None: 물리 엔진의 움직임을 그대로 사용합니다.
// Interpolate(보간): 이전 프레임의 위치를 보간하여 부드러운 움직임을 만듭니다. (주어진 범위 내에서 움직임을 부드럽게 만듭니다.)
// --> 두 물리 업데이트에서의 리지드바디의 위치와 속도를 이용해 현재 프레임에서의 위치를 계산하고 적용하는 방식
// Extrapolate(외삽): 다음 프레임의 위치를 예측하여 부드러운 움직임을 만듭니다. (주어진 범위 내에서 움직임을 예측하여 부드러운 움직임을 만듭니다.)
// --> 현재 프레임의 위치와 속도를 이용해 다음 프레임에서의 위치를 예측하고 적용하는 방식

// 자주 쓰는 기능:
// AddForce(Vector3 force, ForceMode mode);
// 오브젝트가 x, y, z축 방향으로 물리적인 힘을 받도록 합니다. 

// MovePosition(Vector3 position); 위치로 이동합니다.
// MoveRotation(Quaternion rotation); 회전값이 되도록 회전합니다.
// ------------------------------------------------------------------------

// 적 생성기 (Generator)
// EnemyManager.cs 를 통해 일정 시간마다 적을 생성합니다.

// 레이어의 용도
// 1. 선택적 렌더링 : 카메라 등에서 특정 레이어만 렌더링할 수 있습니다.
// 2. 충돌 필터링 : Physics 설정에서 특정 레이어 간의 충돌을 제어할 수 있습니다.
// 3. 성능 최적화 : 불필요한 충돌 계산을 줄여 성능을 향상시킬 수 있습니다.
// 4. 레이캐스트 충돌 : LayerMask를 사용하여 레이캐스트가 특정 레이어에만 충돌하도록 설정할 수 있습니다.


// 레이어 충돌 Matrix
// Physics 설정에서 레이어 간의 충돌을 제어할 수 있는 매트릭스입니다.
// 각 레이어의 행과 열이 충돌 여부를 나타냅니다.
// 예를 들어, 레이어 0과 레이어 1이 충돌하지 않도록 설정하면, 레이어 0의 오브젝트와 레이어 1의 오브젝트는 충돌하지 않습니다.

// 정렬 레이어(Sorting Layer)
// 2D 게임에서 오브젝트의 렌더링 순서를 제어합니다.
// 정렬 레이어는 렌더링 순서를 결정하는데 사용되며, 각 오브젝트는 하나의 정렬 레이어에 속합니다.