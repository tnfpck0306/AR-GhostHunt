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
    public event Action onDeath; // 사망시 발동할 이벤트

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

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void Heal(float heal)
    {
        if (dead)
        {
            return;
        }

        // 회복량이 시작 체력을 초과한다면
        if(startingHealth < health + heal)
        {
            heal = startingHealth - health;
        }

        health += heal;
    }

    public virtual void Die()
    {
        // onDeath 이벤트에 등록된 메서드가 있다면 실행
        if (onDeath != null)
        {
            onDeath();
        }

        // 사망 상태를 참으로 변경
        dead = true;
    }

}
