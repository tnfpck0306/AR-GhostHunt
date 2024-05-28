using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    private Rigidbody playerRigidbody;
    public float rotateSpeed = 180f;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ³²Àº Åº¾Ë UI °»½Å
        UpdateUI();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        float turn = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
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
