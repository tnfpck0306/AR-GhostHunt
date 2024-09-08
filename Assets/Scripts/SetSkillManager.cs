using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

// 스킬 선택창 UI 관리 및 스킬 적용
public class SetSkillManager : MonoBehaviour
{
    public Text[] skillText = new Text[3]; // 스킬 선택창의 스킬 텍스트
    public Text skillExplaneText; // 스킬 설명 텍스트
    public SkillData skillData; // 스킬 데이터
    public Dropdown languageDrop; // 언어 dropdown

    public GameObject selectSkillUI; // 스킬 선택창
    public GameObject playerPref;

    private LivingEntity player; // 플레이어
    private PlayerHealth playerHealth; // 플레이어 체력
    private PlayerShooter playerShooter; // 플레이어 총

    public GhostSpawner ghostSpawner; // 유령 생성기
    public ItemSpawner itemSpawner; // 아이템 생성기

    public AmmoPack ammoPack; // 탄알 아이템
    public HealPack healPack; // 회복 아이템

    // public Text healthText; // 체력 시각화 텍스트(테스트용)

    public List<string> playerSkillIndex; // 플레이어가 선택할 수 있는 남은 스킬들
    List<string> uiSkillIndex; // UI에 띄울 스킬들

    [SerializeField]private int language;
    public float ghostDebuff = 1;

    public bool isHealthRegen = false; // 체력 회복 스킬 활성화 여부

    private void Awake()
    {
        playerSkillIndex = new List<string>(skillData.SkillList);
        player = playerPref.GetComponent<LivingEntity>();

        playerHealth = playerPref.GetComponent<PlayerHealth>();
        playerShooter = playerPref.GetComponent<PlayerShooter>();

        language = languageDrop.value;

        // 플레이어 능력치 초기화
        playerShooter.gun.gunData.damage = 50;

        // 아이템 수치 초기화
        ammoPack.ammo = 30;
        healPack.heal = 50;

        // 유령 속도 감소 초기화
        ghostDebuff = 1;

    }

    private void Update()
    {
        // healthText.text = player.health.ToString(); // (테스트용 코드)

        language = languageDrop.value;
    }

    // 스킬 적용
    public void SetSkill(string skill)
    {

        switch (skill)
        {
            case "공격력 증가":
            case "Increased attack power":
                playerShooter.gun.gunData.damage += 20; // 플레이어 총 공격력 20 증가
                playerSkillIndex.Remove("공격력 증가");
                break;

            case "탄알 보급":
            case "Supply bullets":
                StartCoroutine(SkillAutoAmmo(playerShooter));
                playerSkillIndex.Remove("탄알 보급");
                break;

            case "체력 증가":
            case "Increased HP":
                playerHealth.healthSlider.maxValue += 50; // 체력 실린더의 값(UI) 50 증가
                player.startingHealth += 50; // 플레이어 최대체력 50 증가
                playerSkillIndex.Remove("체력 증가");
                break;

            case "체력 재생":
            case "HP regeneration":
                isHealthRegen = true;
                playerSkillIndex.Remove("체력 재생");
                break;

            case "아이템 스폰시간 감소":
            case "Reduce item spawn time":
                itemSpawner.timeBetSpawnMin *= 0.85f; // 아이템 생성 최소 시간 15% 감소
                itemSpawner.timeBetSpawnMax *= 0.85f; // 아이템 생성 최대 시간 15& 감소
                playerSkillIndex.Remove("아이템 스폰시간 감소");
                break;

            case "아이템 효율 증가":
            case "Increased item effciency":
                ammoPack.ammo = (int)(ammoPack.ammo * 1.2); // 탄알 아이템 효율 20% 증가
                healPack.heal *= 1.2f; // 회복 아이템 효율 20% 증가
                playerSkillIndex.Remove("아이템 효율 증가");
                break;

            case "유령 속도 감소":
            case "Reduce ghost speed":
                ghostDebuff = 0.6f; // 유령 속도 40% 감소
                playerSkillIndex.Remove("유령 속도 감소");
                break;
        }
    }

    // 스킬 설명 Text
    public void SkillExplaneText(string skill)
    {

        switch (skill)
        {
            case "공격력 증가":
                skillExplaneText.text = "플레이어 공격력 40% 증가";
                break;

            case "탄알 보급":
                skillExplaneText.text = "5초마다 5씩 탄알 자동 보급";
                break;

            case "체력 증가":
                skillExplaneText.text = "플레이어 최대체력 50 % 증가";
                break;

            case "체력 재생":
                skillExplaneText.text = "유령 킬 당 4씩 플레이어 체력 회복";
                break;

            case "아이템 스폰시간 감소":
                skillExplaneText.text = "탄알 및 회복 아이템 스폰시간 15 % 감소";
                break;

            case "아이템 효율 증가":
                skillExplaneText.text = "탄알 및 회복 아이템 효율 20 % 증가";
                break;

            case "유령 속도 감소":
                skillExplaneText.text = "유령 속도 40 % 감소";
                break;

            // 언어가 영어의 경우
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

    // 탄알 보급 스킬
    public IEnumerator SkillAutoAmmo(PlayerShooter playerShooter)
    {
        while (true)
        {
            // 5초에 탄알 5씩 탄창에 추가
            yield return new WaitForSeconds(5f);
            playerShooter.gun.ammoRemain += 5;
        }
    }

    // 체력 회복 스킬
    public void SkillHealthRegen()
    {
        // 체력을 4회복 (유령 킬 당)
        player.Heal(4);
    }

    // 스킬 선택창 활성화
    public void SetActiveSkillUI()
    {
        uiSkillIndex = new List<string>(playerSkillIndex);
        skillExplaneText.text = " ";

        // 스킬 3가지 선택창 UI에 띄우기
        for (int index = 0; index < skillText.Length; index++)
        {
            int rand = Random.Range(0, uiSkillIndex.Count);
            
            // 선택가능한 스킬이 더이상 없다면
            if(uiSkillIndex.Count == 0)
            {
                skillText[index].text = "X";
            }

            else
            {
                skillText[index].text = uiSkillIndex[rand];
                if(language != 0)
                {
                    if (skillText[index].text == "공격력 증가")
                        skillText[index].text = "Increased attack power";
                    else if (skillText[index].text == "탄알 보급")
                        skillText[index].text = "Supply bullets";
                    else if (skillText[index].text == "체력 증가")
                        skillText[index].text = "Increased HP";
                    else if (skillText[index].text == "체력 재생")
                        skillText[index].text = "HP regeneration";
                    else if (skillText[index].text == "아이템 스폰시간 감소")
                        skillText[index].text = "Reduce item spawn time";
                    else if (skillText[index].text == "아이템 효율 증가")
                        skillText[index].text = "Increased item effciency";
                    else if (skillText[index].text == "유령 속도 감소")
                        skillText[index].text = "Reduce ghost speed";
                }

                uiSkillIndex.RemoveAt(rand); // 중복 방지
            }
        }
        selectSkillUI.SetActive(true);

    }
}
