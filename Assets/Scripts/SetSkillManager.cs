using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

// ��ų ����â UI ���� �� ��ų ����
public class SetSkillManager : MonoBehaviour
{
    public Text[] skillText = new Text[3]; // ��ų ����â�� ��ų �ؽ�Ʈ
    public Text skillExplaneText; // ��ų ���� �ؽ�Ʈ
    public SkillData skillData; // ��ų ������
    public Dropdown languageDrop; // ��� dropdown

    public GameObject selectSkillUI; // ��ų ����â
    public GameObject playerPref;

    private LivingEntity player; // �÷��̾�
    private PlayerHealth playerHealth; // �÷��̾� ü��
    private PlayerShooter playerShooter; // �÷��̾� ��

    public GhostSpawner ghostSpawner; // ���� ������
    public ItemSpawner itemSpawner; // ������ ������

    public AmmoPack ammoPack; // ź�� ������
    public HealPack healPack; // ȸ�� ������

    // public Text healthText; // ü�� �ð�ȭ �ؽ�Ʈ(�׽�Ʈ��)

    public List<string> playerSkillIndex; // �÷��̾ ������ �� �ִ� ���� ��ų��
    List<string> uiSkillIndex; // UI�� ��� ��ų��

    [SerializeField]private int language;
    public float ghostDebuff = 1;

    public bool isHealthRegen = false; // ü�� ȸ�� ��ų Ȱ��ȭ ����

    private void Awake()
    {
        playerSkillIndex = new List<string>(skillData.SkillList);
        player = playerPref.GetComponent<LivingEntity>();

        playerHealth = playerPref.GetComponent<PlayerHealth>();
        playerShooter = playerPref.GetComponent<PlayerShooter>();

        language = languageDrop.value;

        // �÷��̾� �ɷ�ġ �ʱ�ȭ
        playerShooter.gun.gunData.damage = 50;

        // ������ ��ġ �ʱ�ȭ
        ammoPack.ammo = 30;
        healPack.heal = 50;

        // ���� �ӵ� ���� �ʱ�ȭ
        ghostDebuff = 1;

    }

    private void Update()
    {
        // healthText.text = player.health.ToString(); // (�׽�Ʈ�� �ڵ�)

        language = languageDrop.value;
    }

    // ��ų ����
    public void SetSkill(string skill)
    {

        switch (skill)
        {
            case "���ݷ� ����":
            case "Increased attack power":
                playerShooter.gun.gunData.damage += 20; // �÷��̾� �� ���ݷ� 20 ����
                playerSkillIndex.Remove("���ݷ� ����");
                break;

            case "ź�� ����":
            case "Supply bullets":
                StartCoroutine(SkillAutoAmmo(playerShooter));
                playerSkillIndex.Remove("ź�� ����");
                break;

            case "ü�� ����":
            case "Increased HP":
                playerHealth.healthSlider.maxValue += 50; // ü�� �Ǹ����� ��(UI) 50 ����
                player.startingHealth += 50; // �÷��̾� �ִ�ü�� 50 ����
                playerSkillIndex.Remove("ü�� ����");
                break;

            case "ü�� ���":
            case "HP regeneration":
                isHealthRegen = true;
                playerSkillIndex.Remove("ü�� ���");
                break;

            case "������ �����ð� ����":
            case "Reduce item spawn time":
                itemSpawner.timeBetSpawnMin *= 0.85f; // ������ ���� �ּ� �ð� 15% ����
                itemSpawner.timeBetSpawnMax *= 0.85f; // ������ ���� �ִ� �ð� 15& ����
                playerSkillIndex.Remove("������ �����ð� ����");
                break;

            case "������ ȿ�� ����":
            case "Increased item effciency":
                ammoPack.ammo = (int)(ammoPack.ammo * 1.2); // ź�� ������ ȿ�� 20% ����
                healPack.heal *= 1.2f; // ȸ�� ������ ȿ�� 20% ����
                playerSkillIndex.Remove("������ ȿ�� ����");
                break;

            case "���� �ӵ� ����":
            case "Reduce ghost speed":
                ghostDebuff = 0.6f; // ���� �ӵ� 40% ����
                playerSkillIndex.Remove("���� �ӵ� ����");
                break;
        }
    }

    // ��ų ���� Text
    public void SkillExplaneText(string skill)
    {

        switch (skill)
        {
            case "���ݷ� ����":
                skillExplaneText.text = "�÷��̾� ���ݷ� 40% ����";
                break;

            case "ź�� ����":
                skillExplaneText.text = "5�ʸ��� 5�� ź�� �ڵ� ����";
                break;

            case "ü�� ����":
                skillExplaneText.text = "�÷��̾� �ִ�ü�� 50 % ����";
                break;

            case "ü�� ���":
                skillExplaneText.text = "���� ų �� 4�� �÷��̾� ü�� ȸ��";
                break;

            case "������ �����ð� ����":
                skillExplaneText.text = "ź�� �� ȸ�� ������ �����ð� 15 % ����";
                break;

            case "������ ȿ�� ����":
                skillExplaneText.text = "ź�� �� ȸ�� ������ ȿ�� 20 % ����";
                break;

            case "���� �ӵ� ����":
                skillExplaneText.text = "���� �ӵ� 40 % ����";
                break;

            // �� ������ ���
            case "Increased attack power":
                skillExplaneText.text = "Player attack power increased by 40%";
                break;

            case "Supply bullets":
                skillExplaneText.text = "Automatically supply 5 bullet every 5 seconds";
                break;

            case "Increased HP":
                skillExplaneText.text = "Player max HP increased by 50%";
                break;

            case "HP regeneration":
                skillExplaneText.text = "Player HP is restored by 4 per ghost killed";
                break;

            case "Reduce item spawn time":
                skillExplaneText.text = "15% reduction in bullet and recovery item spawn time";
                break;

            case "Increased item effciency":
                skillExplaneText.text = "Increases bullet and recovery item efficiency by 20%";
                break;

            case "Reduce ghost speed":
                skillExplaneText.text = "40% reduction in ghost speed";
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
        skillExplaneText.text = " ";

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
                if(language != 0)
                {
                    if (skillText[index].text == "���ݷ� ����")
                        skillText[index].text = "Increased attack power";
                    else if (skillText[index].text == "ź�� ����")
                        skillText[index].text = "Supply bullets";
                    else if (skillText[index].text == "ü�� ����")
                        skillText[index].text = "Increased HP";
                    else if (skillText[index].text == "ü�� ���")
                        skillText[index].text = "HP regeneration";
                    else if (skillText[index].text == "������ �����ð� ����")
                        skillText[index].text = "Reduce item spawn time";
                    else if (skillText[index].text == "������ ȿ�� ����")
                        skillText[index].text = "Increased item effciency";
                    else if (skillText[index].text == "���� �ӵ� ����")
                        skillText[index].text = "Reduce ghost speed";
                }

                uiSkillIndex.RemoveAt(rand); // �ߺ� ����
            }
        }
        selectSkillUI.SetActive(true);

    }
}
