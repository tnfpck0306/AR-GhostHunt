using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class SetSkillManager : MonoBehaviour
{
    // 싱글턴 접근용 프로퍼티
    public static SetSkillManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<SetSkillManager>();
            }

            return m_instance;
        }
    }

    public static SetSkillManager m_instance; // 싱글턴이 할당될 변수

    public Text[] skillText = new Text[3]; // 스킬 선택창의 스킬 텍스트
    public SkillData skillData; // 스킬 데이터

    public GameObject playerPref;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 스킬 적용
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

    // 스킬 선택창 활성화
    public void SetActiveSkillUI()
    {
        List<string> skillIndex = new List<string>(skillData.SkillList);
        print(skillIndex.Count);

        for (int index = 0; index < skillText.Length; index++)
        {
            int rand = Random.Range(0, skillIndex.Count);
            print(rand);
            skillText[index].text = skillIndex[rand];
            skillIndex.RemoveAt(rand); // 중복 방지
        }
        gameObject.SetActive(true);

    }
}
