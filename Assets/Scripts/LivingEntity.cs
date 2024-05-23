using System;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

// ����ü�� Ȱ���� ���� ������Ʈ���� ����
// ü��, ������, ��� ��� ����
public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100; // ���� ü��
    public float health { get; protected set; } // ���� ü��
    public bool dead { get; protected set; } // ��� ����

    // ����ü�� Ȱ��ȭ�� �� ���¸� ����
    protected virtual void OnEnable()
    {
        // ������� ���� ���·� ����
        dead = false;
        // ü���� ���� ü������ �ʱ�ȭ
        health = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // ��������ŭ ü�� ����
        health -= damage;
        print("ü�°���");

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if(health <= 0 && !dead)
        {
            print("����");
            Die();
        }
    }

    public virtual void Die()
    {
        // ��� ���¸� ������ ����
        dead = true;
    }

}
