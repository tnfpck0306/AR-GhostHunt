using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30; // 추가 탄알

    public void Use(GameObject target)
    {
        print("탄알 추가");
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        // PlayerShooter 컴포넌트가 존재 && 총 오브젝트가 존재
        if(playerShooter != null && playerShooter.gun != null)
        {
            playerShooter.gun.ammoRemain += ammo;
        }

        Destroy(gameObject);
    }
}
