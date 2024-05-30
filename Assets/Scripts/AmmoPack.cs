using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30; // �߰� ź��

    public void Use(GameObject target)
    {
        print("ź�� �߰�");
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        // PlayerShooter ������Ʈ�� ���� && �� ������Ʈ�� ����
        if(playerShooter != null && playerShooter.gun != null)
        {
            playerShooter.gun.ammoRemain += ammo;
        }

        Destroy(gameObject);
    }
}
