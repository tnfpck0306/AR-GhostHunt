using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour, IItem
{
    public float heal = 50;

    public void Use(GameObject target)
    {
        print("체력회복");

        LivingEntity player = target.GetComponent<LivingEntity>();

        if(player != null)
        {
            player.Heal(heal);
        }

        Destroy(gameObject);
    }
}
