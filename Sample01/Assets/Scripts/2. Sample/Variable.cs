using System;
using UnityEngine;
// 유니티 인스펙터에서 변수에 대한 표현 확인용
//[Flags]
public enum Type
{
    fire, water, grass, poison, ghost, electric, psychic, ice, dragon, dark, fairy
}

// 여러 개를 선택하는 기능 (Flags) 사용 예시
// 값을 2의 거듭제곱으로 설정하여 비트 연산을 통해 조합할 수 있습니다.
// 2의 제곱수는 비트 연산으로 표현하기 쉽습니다.

[Flags]
public enum Type2
{
    poison = 1 << 0, // 1
    ghost = 1 << 1, // 2 (shifted left by 1)
    dragon = 1 << 2, // 4
    fairy = 1 << 3, // 8
    fire = 1 << 4, // 16
    water = 1 << 5, // 32
    //dark = poison | ghost, // 3 (combination of poison and ghost)
    ice = 253
}

public class Variable : MonoBehaviour
{
    // Unity Primitive Types
    // Below are the primitive types most commonly used in Unity.
    // null represents empty or uninitialized values.
    // Nullable: type? allows the type for null values.
    public int Integer;
    public float Float;
    public string Sentence;
    public int maxValue = int.MaxValue;
    public int minValue = int.MinValue;
    public byte minValueByte = byte.MinValue;
    public byte? minValueByte2 = null;

    public Type type;
    public Type2 type2;
    public bool isDead;
}
