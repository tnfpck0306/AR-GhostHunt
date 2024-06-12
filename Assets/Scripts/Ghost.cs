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
    public float updateInterval = 0.04f;

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
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());

        // ų �� ���� �ӵ� ����
        speed = GameManager.instance.kill * 0.1f + speed;
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

                // 30������ �������� ���� ������ ���� �׷��� �� ���� ��ġ�� ��� �ݶ��̴��� ������
                // ��, WhatIsTarget ���̾ ���� �ݶ��̴��� ���������� ���͸�
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
        // LivingEntity�� OnDamage()�� �����Ͽ� ����� ����
        base.OnDamage(damage, hitPoint, hitNormal);
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
