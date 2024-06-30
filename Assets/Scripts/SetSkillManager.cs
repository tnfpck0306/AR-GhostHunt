using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class SetSkillManager : MonoBehaviour
{
    // �̱��� ���ٿ� ������Ƽ
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

    public static SetSkillManager m_instance; // �̱����� �Ҵ�� ����

    public Text[] skillText = new Text[3]; // ��ų ����â�� ��ų �ؽ�Ʈ
    public SkillData skillData; // ��ų ������

    public GameObject playerPref;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // ��ų ����
    public void SetSkill(string var)
    {
        LivingEntity player = playerPref.GetComponent<LivingEntity>();
        PlayerHealth playerHealth = playerPref.GetComponent<PlayerHealth>();

        switch (var)
        {
            case "ü������":
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

    // ��ų ����â Ȱ��ȭ
    public void SetActiveSkillUI()
    {
        List<string> skillIndex = new List<string>(skillData.SkillList);
        print(skillIndex.Count);

        for (int index = 0; index < skillText.Length; index++)
        {
            int rand = Random.Range(0, skillIndex.Count);
            print(rand);
            skillText[index].text = skillIndex[rand];
            skillIndex.RemoveAt(rand); // �ߺ� ����
        }
        gameObject.SetActive(true);

    }
}
