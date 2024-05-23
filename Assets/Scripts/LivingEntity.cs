using System;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

// 생명체로 활동할 게임 오브젝트에게 제공
// 체력, 데미지, 사망 기능 제공
public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100; // 시작 체력
    public float health { get; protected set; } // 현재 체력
    public bool dead { get; protected set; } // 사망 상태

    // 생명체가 활성화될 때 상태를 리셋
    protected virtual void OnEnable()
    {
        // 사망하지 않은 상태로 시작
        dead = false;
        // 체력을 시작 체력으로 초기화
        health = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // 데미지만큼 체력 감소
        health -= damage;
        print("체력감소");

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if(health <= 0 && !dead)
        {
            print("죽음");
            Die();
        }
    }

    public virtual void Die()
    {
        // 사망 상태를 참으로 변경
        dead = true;
    }

}
