using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾� ĳ������ ����ü�μ��� ���� ���
public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; // ü���� ǥ���� UI �����̴�

    protected override void OnEnable()
    {
        // LivingEnitiy�� OnEnable() ����
        base.OnEnable();

        // ü�� �����̴� Ȱ��ȭ
        healthSlider.gameObject.SetActive(true);
        // ü�� �����̴��� �ִ밪�� �⺻ ü�°����� ����
        healthSlider.maxValue = startingHealth;
        // ü�� �����̴��� ���� ���� ü�°����� ����
        healthSlider.value = health;

    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // LivingEntity�� OnDamage() ����
        base.OnDamage(damage, hitPoint, hitDirection);
        // ���ŵ� ü���� ü�� �����̴��� �ݿ�
        healthSlider.value = health;
    }

    // HealPack���� ȸ���� �����̴� ����
    public override void Heal(float heal)
    {
        base.Heal(heal);

        healthSlider.value = health;
    }

    // �����۰� �浹 ��, ������ ���
    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            // �浹�� ������Ʈ�κ��� IItem ������Ʈ ��������
            IItem item = other.GetComponent<IItem>();

            if(item != null)
            {
                item.Use(gameObject);
            }

        }
    }
}
