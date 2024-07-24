using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : LivingEntity
{
    public LayerMask WhatISTarget; // ���� ��� ���̾�

    private LivingEntity targetEntity; // ���� ���
    public event Action onCollison;

    public float damage = 20f; // ���ݷ�
    public float speed; // �ӵ�
    public float updateInterval = 0.04f; // �ڷ�ƾ ó�� ���ð�
    public int type; // ������ ����
    public GameObject[] skin; // ��ũ ȿ���� ����Ǵ� ������ ����
    public Renderer meshRenderer;

    private GameObject audioManager; // ����� �Ŵ���
    private AudioSource audioSource; // �ǰ� �Ҹ�

    private bool hasTarget
    {
        get
        {
            // ������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            return false;
        }
    }

    public void Setup(GhostData ghostData)
    {
        // ü�� ����
        startingHealth = ghostData.health;
        health = ghostData.health;
        // ���ݷ� ����
        damage = ghostData.damge;
        // �ӵ� ����
        speed = ghostData.speed;
        // ���� ����
        type = ghostData.type;
    }

    private void Start()
    {
        // ����� �Ŵ������� �Ҹ� �ҷ�����
        audioManager = GameObject.Find("Audio Manager");
        audioSource = audioManager.GetComponent<AudioSource>();

        int killCount = GameManager.instance.kill; // �÷��̾��� ų ��

        StartCoroutine(UpdatePath());
        
        // ������ ������ 2(������)�� ��� ��ũ ȿ��
        if(type == 2)
        {
            StartCoroutine(BlinkEffect());
        }

        // ������ ������ 3(��������)�� ��� ü���� ų �� * 10
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
                // ���� ����� �����Ѵٸ� ����� �ٶ󺸰�
                transform.LookAt(targetEntity.transform);

                // �̵�
                Vector3 direction = targetEntity.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * speed * Time.deltaTime;
            }

            else
            {
                // ���� ����� ������ ����

                // WhatIsTarget(Player) ���̾ ���� �ݶ��̴��� ��������
                Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, WhatISTarget);

                // ��� �ݶ��̴��� ��ȸ�ϸ鼭 ��� �ִ� LivingEntity ã��
                for (int i = 0; i < colliders.Length; i++)
                {
                    // �ݶ��̴��κ��� LivingEntity ������Ʈ ��������
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    // LivingEntity ������Ʈ�� �����ϸ�, �ش� LivingEntity�� ��� �ִٸ�
                    if(livingEntity != null && !livingEntity.dead)
                    {
                        // ���� ����� �ش� LivingEntity�� ����
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
        // �ǰ� �Ҹ� ���
        audioSource.Play();
        StartCoroutine(HitEffect());
        // LivingEntity�� OnDamage()�� �����Ͽ� ����� ����
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    private IEnumerator HitEffect()
    {
        Color basicColor = meshRenderer.material.color; // ������ ������ �ִ� �⺻ ��
        Color hitColor = new Color32(180, 50, 50, 255); // �ǰݽ� ��

        meshRenderer.material.color = hitColor;

        yield return new WaitForSeconds(0.1f);

        meshRenderer.material.color = basicColor;
    }

    private IEnumerator BlinkEffect()
    {
        while (!dead)
        {
            // ��ũ ȿ���� ����Ǵ� ������ Ȱ��ȭ
            for(int i = 0; i < skin.Length; i++)
            {
                skin[i].SetActive(true);
            }
            yield return new WaitForSeconds(1f);

            // ��ũ ȿ���� ����Ǵ� ������ ��Ȱ��ȭ
            for (int i = 0; i < skin.Length; i++)
            {
                skin[i].SetActive(false);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public override void Die()
    {
        // LivingEntity�� Die()�� �����Ͽ� �⺻ ��� ó�� ����
        base.Die();

    }

    private void OnTriggerStay(Collider other)
    {
        // �ڽ��� ������� �ʾ�����,
        if(!dead)
        {
            // ������ LivingEntity Ÿ�� �������� �õ�
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            
            // ������ LivingEntity�� �ڽ��� ���� ����̶�� ����
            if(attackTarget != null && attackTarget == targetEntity)
            {

                // ������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                // ���� ����
                attackTarget.OnDamage(damage, hitPoint, hitNormal);

                onCollison();

            }
        }
    }
}
