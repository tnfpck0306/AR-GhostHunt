using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //gun.Fire();
        }
        else if (Input.GetButtonDown("Reload"))
        {
            //gun.Reload();
        }
    }
}
