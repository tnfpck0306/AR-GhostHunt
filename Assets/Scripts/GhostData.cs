using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유령 생성시 사용할 셋업 데이터
[CreateAssetMenu(menuName = "Scriptable/GhostData", fileName = "Ghost Data")]
public class GhostData : ScriptableObject
{
    public float health = 100f; // 체력
    public float damge = 20f; // 공격력
    public float speed = 4f; // 이동속도
}
