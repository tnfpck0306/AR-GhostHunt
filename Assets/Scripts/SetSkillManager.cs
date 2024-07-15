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
    private LivingEntity player;
    private PlayerHealth playerHealth;
    private PlayerShooter playerShooter;

    public GhostSpawner ghostSpawner;
    public ItemSpawner itemSpawner;

    public AmmoPack ammoPack;
    public HealPack healPack;

    public List<string> playerSkillIndex; // �÷��̾ ������ �� �ִ� ���� ��ų��
    List<string> uiSkillIndex; // UI�� ��� ��ų��

    public Text healthText; // ü�� �ð�ȭ �ؽ�Ʈ(�׽�Ʈ��)

    public bool isHealthRegen = false; // ü�� ȸ�� ��ų Ȱ��ȭ ����

    private void Awake()
    {
        playerSkillIndex = new List<string>(skillData.SkillList);
        player = playerPref.GetComponent<LivingEntity>();

        playerHealth = playerPref.GetComponent<PlayerHealth>();
        playerShooter = playerPref.GetComponent<PlayerShooter>();

        // �÷��̾� �ɷ�ġ �ʱ�ȭ
        playerShooter.gun.gunData.damage = 50;

        // ������ ��ġ �ʱ�ȭ
        ammoPack.ammo = 30;
        healPack.heal = 50;

    }

    private void Update()
    {
        healthText.text = player.health.ToString(); // (�׽�Ʈ�� �ڵ�)
        
    }

    // ��ų ����
    public void SetSkill(string skill)
    {

        switch (skill)
        {
            case "���ݷ� ����":
                playerShooter.gun.gunData.damage += 20; // �÷��̾� �� ���ݷ� 20 ����
                playerSkillIndex.Remove("���ݷ� ����");
                break;

            case "ź�� ����":
                SkillAutoAmmo(playerShooter);
                playerSkillIndex.Remove("ź�� ����");
                break;

            case "ü�� ����":
                playerHealth.healthSlider.maxValue += 50; // ü�� �Ǹ����� ��(UI) 50 ����
                player.startingHealth += 50; // �÷��̾� �ִ�ü�� 50 ����
                playerSkillIndex.Remove("ü�� ����");
                break;

            case "ü�� ���":
                isHealthRegen = true;
                playerSkillIndex.Remove("ü�� ���");
                break;

            case "������ �����ð� ����":
                itemSpawner.timeBetSpawnMin *= 0.85f; // ������ ���� �ּ� �ð� 15% ����
                itemSpawner.timeBetSpawnMax *= 0.85f; // ������ ���� �ִ� �ð� 15& ����
                playerSkillIndex.Remove("������ �����ð� ����");
                break;

            case "������ ȿ�� ����":
                // ������ ȿ�� 20% ���� �ڵ�
                ammoPack.ammo = (int)(ammoPack.ammo * 1.2);
                healPack.heal *= 1.2f;
                playerSkillIndex.Remove("������ ȿ�� ����");
                break;

            case "���� �ӵ� ����":

                // �ӵ� ���� �ڵ�
                playerSkillIndex.Remove("���� �ӵ� ����");
                break;

            case "���� Ž����":

                // ���� ����� ������ ã�� �ڵ�
                playerSkillIndex.Remove("���� Ž����");
                break;
        }
    }

    // ź�� ���� ��ų
    public IEnumerator SkillAutoAmmo(PlayerShooter playerShooter)
    {
        while (true)
        {
            // 5�ʿ� ź�� 5�� źâ�� �߰�
            yield return new WaitForSeconds(5f);
            playerShooter.gun.ammoRemain += 5;
        }
    }

    // ü�� ȸ�� ��ų
    public void SkillHealthRegen()
    {
        // ü���� 4ȸ�� (���� ų ��)
        player.Heal(4);
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
