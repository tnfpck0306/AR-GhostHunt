using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : LivingEntity
{
    public LayerMask WhatISTarget; // 추적 대상 레이어

    private LivingEntity targetEntity; // 추적 대상

    public float damage = 20f; // 공격력
    public float timeBetAttack = 0.5f; // 공격 간격
    public float speed = 4f;
    private float lastAttackTime; // 마지막 공격 시점
    public float updateInterval = 0.04f;

    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            return false;
        }
    }

    public void Setup(GhostData ghostData)
    {
        // 체력 설정
        startingHealth = ghostData.health;
        health = ghostData.health;
        // 공격력 설정
        damage = ghostData.damge;
        // 속도 설정
        speed = ghostData.speed;
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if(hasTarget)
            {
                // 추적 대상이 존재한다면 이동
                Vector3 direction = targetEntity.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * speed * Time.deltaTime;
            }

            else
            {
                // 추적 대상이 없으면 중지

                // 20유닛의 반지름을 가진 가상의 구를 그렸을 때 구와 겹치는 모든 콜라이더를 가져옴
                // 단, WhatIsTarget 레이어를 가진 콜라이더만 가져오도록 필터링
                Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, WhatISTarget);

                // 모든 콜라이더를 순회하면서 살아 있는 LivingEntity 찾기
                for (int i = 0; i < colliders.Length; i++)
                {
                    // 콜라이더로부터 LivingEntity 컴포넌트 가져오기
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    // LivingEntity 컴포넌트가 존재하며, 해당 LivingEntity가 살아 있다면
                    if(livingEntity != null && !livingEntity.dead)
                    {
                        // 추적 대상을 해당 LivingEntity로 설정
                        targetEntity = livingEntity;

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(updateInterval);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // LivingEntity의 OnDamage()를 실행하여 대미지 적용
        base.OnDamage(damage, hitPoint, hitNormal);
    }
    
    public override void Die()
    {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();

        // 다른 AI를 방해하지 않도록 자신의 모든 콜라이더를 비활성화
        Collider[] ghostColliders = GetComponent<Collider[]>();
        for (int i = 0; i < ghostColliders.Length; i++)
        {
            ghostColliders[i].enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 자신이 사망하지 않았으며,
        // 최근 공격 시점에서 timeBetAttack 이상 시간이 지났다면 공격 가능
        if(!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            // 상대방의 LivingEntity 타입 가져오기 시도
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            
            // 상대방의 LivingEntity가 자신의 추적 대상이라면 공격
            if(attackTarget != null && attackTarget == targetEntity)
            {
                // 최근 공격 시간 갱신
                lastAttackTime = Time.time;

                // 상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                // 공격 실행
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }
}
