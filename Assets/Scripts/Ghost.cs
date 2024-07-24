using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : LivingEntity
{
    public LayerMask WhatISTarget; // 추적 대상 레이어

    private LivingEntity targetEntity; // 추적 대상
    public event Action onCollison;

    public float damage = 20f; // 공격력
    public float speed; // 속도
    public float updateInterval = 0.04f; // 코루틴 처리 대기시간
    public int type; // 유령의 종류
    public GameObject[] skin; // 블링크 효과가 적용되는 유령의 부위
    public Renderer meshRenderer;

    private GameObject audioManager; // 오디오 매니저
    private AudioSource audioSource; // 피격 소리

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
        // 종류 설정
        type = ghostData.type;
    }

    private void Start()
    {
        // 오디오 매니저에서 소리 불러오기
        audioManager = GameObject.Find("Audio Manager");
        audioSource = audioManager.GetComponent<AudioSource>();

        int killCount = GameManager.instance.kill; // 플레이어의 킬 수

        StartCoroutine(UpdatePath());
        
        // 유령의 종류가 2(뿔유령)인 경우 블링크 효과
        if(type == 2)
        {
            StartCoroutine(BlinkEffect());
        }

        // 유령의 종류가 3(보스유령)인 경우 체력은 킬 수 * 10
        else if(type == 3)
        {
            startingHealth = killCount * 10;
            health = killCount * 10;
        }
    }

    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if(hasTarget)
            {
                // 추적 대상이 존재한다면 대상을 바라보고
                transform.LookAt(targetEntity.transform);

                // 이동
                Vector3 direction = targetEntity.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * speed * Time.deltaTime;
            }

            else
            {
                // 추적 대상이 없으면 중지

                // WhatIsTarget(Player) 레이어를 가진 콜라이더만 가져오기
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
        // 피격 소리 재생
        audioSource.Play();
        StartCoroutine(HitEffect());
        // LivingEntity의 OnDamage()를 실행하여 대미지 적용
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    private IEnumerator HitEffect()
    {
        Color basicColor = meshRenderer.material.color; // 유령이 가지고 있는 기본 색
        Color hitColor = new Color32(180, 50, 50, 255); // 피격시 색

        meshRenderer.material.color = hitColor;

        yield return new WaitForSeconds(0.1f);

        meshRenderer.material.color = basicColor;
    }

    private IEnumerator BlinkEffect()
    {
        while (!dead)
        {
            // 블링크 효과에 적용되는 부위들 활성화
            for(int i = 0; i < skin.Length; i++)
            {
                skin[i].SetActive(true);
            }
            yield return new WaitForSeconds(1f);

            // 블링크 효과에 적용되는 부위들 비활성화
            for (int i = 0; i < skin.Length; i++)
            {
                skin[i].SetActive(false);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public override void Die()
    {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();

    }

    private void OnTriggerStay(Collider other)
    {
        // 자신이 사망하지 않았으면,
        if(!dead)
        {
            // 상대방의 LivingEntity 타입 가져오기 시도
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            
            // 상대방의 LivingEntity가 자신의 추적 대상이라면 공격
            if(attackTarget != null && attackTarget == targetEntity)
            {

                // 상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                // 공격 실행
                attackTarget.OnDamage(damage, hitPoint, hitNormal);

                onCollison();

            }
        }
    }
}
