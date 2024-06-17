using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour, IItem
{
    public float heal = 50; // 회복량

    public void Use(GameObject target)
    {
        LivingEntity player = target.GetComponent<LivingEntity>();

        // 플레이어가 존재 시
        if(player != null)
        {
            // 회복량 만큼 플레이어 회복
            player.Heal(heal);
        }

        // 사용 후 파괴
        Destroy(gameObject);
    }
}
