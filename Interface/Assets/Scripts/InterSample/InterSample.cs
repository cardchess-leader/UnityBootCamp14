using UnityEngine;

// 인터페이스(inteface)

//접근제한자 interface 이름 {...} 단 구현은 하지 않는다.
// 접근 제한자 class 자식클래스명 : 부모클래스명, 인터페이스1, 인터페이스2 {...}
// 특징 : 여러개의 인터페이스 연결이 가능합니다.

// 기존의 클래스 상속 : 기능에 대한 재사용이 목적, 다중 상속이 불가능합니다.
//                      접근 제한자에 대한 사용이 가능합니다.
// 인터페이스 상속 : 기능에 대한 설계를 부탁함. (인터페이스의 모든 기능을 구현해야 합니다.)
//                   인터페이스에 대한 상속 가능 (인터페이스가 인터페이스를 상속받을 수 있습니다.)
//                   다중 상속에 대한 허용이 됩니다. (선언만 포함하기에 충돌이 발생하지 않습니다.)
//                   구현의 주체는 결국 인터페이스를 구현할 "클래스" 자체입니다.
//                   공통적인 동작에 대한 설계를 진행하는 도구입니다.
//                   인터페이스는 접근 제한자를 사용할 수 없습니다. (public, private, protected 등)

// 결론: 인터페이스는 결합도가 낮아서, 유연성이 높은 코드를 짜기 수월합니다.
// 결합도란? 결합도가 낮다는 것은 서로의 의존성이 낮다는 것을 의미합니다. (영어로는 "loose coupling"이라고 합니다.)
// 

public interface IThrowable
{

}

public interface IWeapon {
}

public interface ICountable {
}

public interface  IPotion
{
}

public interface IUsable
{
    
}

public class Item
{
}

public class Sword : Item, IWeapon {
    
}

public class Javelin : Item, IWeapon, IThrowable, ICountable {

}

public class MaxPotion : Item, IPotion, IUsable
{

}

public class FirePotion : Item, IPotion, IThrowable
{
}

public class InterSample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
