using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30; // �߰� ź��

    public void Use(GameObject target)
    {
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        // PlayerShooter ������Ʈ�� ���� && �� ������Ʈ�� ����
        if(playerShooter != null && playerShooter.gun != null)
        {
            // �߰� ź�� ��ŭ źâ �߰�
            playerShooter.gun.ammoRemain += ammo;
        }

        // ����� �ı�
        Destroy(gameObject);
    }
}
