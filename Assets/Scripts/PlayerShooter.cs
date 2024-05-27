using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    private void Update()
    {
        // ³²Àº Åº¾Ë UI °»½Å
        UpdateUI();
    }

    // Åº¾Ë UI °»½Å
    private void UpdateUI()
    {
        if(UIManager.instance != null)
        {
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
    }
}
