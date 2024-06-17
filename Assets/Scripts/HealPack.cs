using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour, IItem
{
    public float heal = 50; // ȸ����

    public void Use(GameObject target)
    {
        LivingEntity player = target.GetComponent<LivingEntity>();

        // �÷��̾ ���� ��
        if(player != null)
        {
            // ȸ���� ��ŭ �÷��̾� ȸ��
            player.Heal(heal);
        }

        // ��� �� �ı�
        Destroy(gameObject);
    }
}
