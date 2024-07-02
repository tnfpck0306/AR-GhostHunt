using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

// ��ų ����â UI ���� �� ��ų ����
public class SetSkillManager : MonoBehaviour
{
    public Text[] skillText = new Text[3]; // ��ų ����â�� ��ų �ؽ�Ʈ
    public SkillData skillData; // ��ų ������

    public GameObject selectSkillUI; // ��ų ����â
    public GameObject playerPref; // �÷��̾�(ü�� ������Ʈ)

    public ItemSpawner itemSpawner;

    List<string> playerSkillIndex; // �÷��̾ ������ �� �ִ� ���� ��ų��
    List<string> uiSkillIndex; // UI�� ��� ��ų��

    private void Awake()
    {
        playerSkillIndex = new List<string>(skillData.SkillList);
    }

    // ��ų ����
    public void SetSkill(string skill)
    {
        LivingEntity player = playerPref.GetComponent<LivingEntity>();
        PlayerHealth playerHealth = playerPref.GetComponent<PlayerHealth>();

        switch (skill)
        {
            case "ü������":
                playerHealth.healthSlider.maxValue += 50; // ü�� �Ǹ����� ��(UI) 50 ����
                player.startingHealth += 50; // �÷��̾� �ִ�ü�� 50 ����
                playerSkillIndex.Remove("ü������");
                break;

            case "1":
                print("1");
                break;

            case "������ �����ð� ����":
                itemSpawner.timeBetSpawnMin *= 0.85f; // ������ ���� �ּ� �ð� 15% ����
                itemSpawner.timeBetSpawnMax *= 0.85f; // ������ ���� �ִ� �ð� 15& ����
                playerSkillIndex.Remove("������ �����ð� ����");
                break;

            case "3":
                print("3");
                break;
        }
    }

    // ��ų ����â Ȱ��ȭ
    public void SetActiveSkillUI()
    {
        uiSkillIndex = new List<string>(playerSkillIndex);

        // ��ų 3���� ����â UI�� ����
        for (int index = 0; index < skillText.Length; index++)
        {
            int rand = Random.Range(0, uiSkillIndex.Count);
            
            // ���ð����� ��ų�� ���̻� ���ٸ�
            if(uiSkillIndex.Count == 0)
            {
                skillText[index].text = "X";
            }

            else
            {
                skillText[index].text = uiSkillIndex[rand];
                uiSkillIndex.RemoveAt(rand); // �ߺ� ����
            }
        }
        selectSkillUI.SetActive(true);

    }
}
