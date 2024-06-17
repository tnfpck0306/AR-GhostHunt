using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템들이 공통적으로 가져야 하는 인터페이스
public interface IItem
{
    // 아이템으로 사용되는 타입들은 Iitem을 상속하고 Use 메서드를 반드시 구현해야 한다
    void Use(GameObject target);
}