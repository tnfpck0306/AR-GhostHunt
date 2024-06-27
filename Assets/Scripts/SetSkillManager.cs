using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SetSkillManager : MonoBehaviour
{
    public GameObject playerPref;
    public void SetSkill(string var)
    {
        LivingEntity player = playerPref.GetComponent<LivingEntity>();
        PlayerHealth playerHealth = playerPref.GetComponent<PlayerHealth>();

        switch (var)
        {
            case "체력증가":
                playerHealth.healthSlider.maxValue += 50;
                player.startingHealth += 50;
                print(player.startingHealth);
                break;

            case "1":
                print("1");
                break;

            case "2":
                print("1");
                break;

            case "3":
                print("1");
                break;
        }
    }
}
