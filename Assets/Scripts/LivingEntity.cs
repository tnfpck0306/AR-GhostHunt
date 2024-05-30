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
    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ

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

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
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

        health += heal;
    }

    public virtual void Die()
    {
        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        if (onDeath != null)
        {
            onDeath();
        }

        // ��� ���¸� ������ ����
        dead = true;
    }

}
