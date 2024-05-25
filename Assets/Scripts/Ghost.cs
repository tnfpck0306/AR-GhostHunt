using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : LivingEntity
{
    public LayerMask WhatISTarget; // ���� ��� ���̾�

    private LivingEntity targetEntity; // ���� ���

    public float damage = 20f; // ���ݷ�
    public float timeBetAttack = 0.5f; // ���� ����
    public float speed = 4f;
    private float lastAttackTime; // ������ ���� ����
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
    }

    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if(hasTarget)
            {
                // ���� ����� �����Ѵٸ� �̵�
                Vector3 direction = targetEntity.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * speed * Time.deltaTime;
            }

            else
            {
                // ���� ����� ������ ����

                // 20������ �������� ���� ������ ���� �׷��� �� ���� ��ġ�� ��� �ݶ��̴��� ������
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

        // �ٸ� AI�� �������� �ʵ��� �ڽ��� ��� �ݶ��̴��� ��Ȱ��ȭ
        Collider[] ghostColliders = GetComponent<Collider[]>();
        for (int i = 0; i < ghostColliders.Length; i++)
        {
            ghostColliders[i].enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �ڽ��� ������� �ʾ�����,
        // �ֱ� ���� �������� timeBetAttack �̻� �ð��� �����ٸ� ���� ����
        if(!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            // ������ LivingEntity Ÿ�� �������� �õ�
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            
            // ������ LivingEntity�� �ڽ��� ���� ����̶�� ����
            if(attackTarget != null && attackTarget == targetEntity)
            {
                // �ֱ� ���� �ð� ����
                lastAttackTime = Time.time;

                // ������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                // ���� ����
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }
}
